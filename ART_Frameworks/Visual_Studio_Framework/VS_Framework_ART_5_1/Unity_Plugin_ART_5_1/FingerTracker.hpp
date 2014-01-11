#ifndef ART_FINGERTRACKER_HPP
#define ART_FINGERTRACKER_HPP

#include <functional>
#include <numeric> 
#include <math.h>

#include "highgui.h"
#include "cxcore.h"
#include "cvaux.h"

#include "Utils.h"

using namespace std;
using namespace cv;

class FingerTracker
{
private:
	ConfigHandler* config_;
	bool ellipseShot(Point* result, Mat* vizualization, Point2d start_pt, Point2d end_pt, Point2f normVec, vector<Point>* contour, Mat* mask);

public:

	FingerTracker(ConfigHandler* config_ptr);
	~FingerTracker();

	void findHighestPoint(Fingertip* fingertips, ColorRGBA* unityArray, int edgeLimit, int checkRadius);
	void fitEllipseOfHandSegment(Fingertip* fingertips, int nrHandPixels, Mat* mask);
	void resetFingertips(Fingertip* fingertips);
};

FingerTracker::FingerTracker(ConfigHandler* config_ptr)
{
	config_ = config_ptr;
}

FingerTracker::~FingerTracker()
{
	// nothing to do
}

void FingerTracker::resetFingertips(Fingertip* fingertips)
{
	// reset each fingertip position
	for(int i=0; i<5; i++)
	{
		fingertips[i].x = 100*i;
		fingertips[i].y = -100;
		fingertips[i].z = 0;
	}
}

bool FingerTracker::ellipseShot(Point* result, Mat* vizualization, Point2d start_pt, Point2d end_pt, Point2f normVec, vector<Point>* contour, Mat* mask)
{
	// find intersections between cutting line and fingers
	Mat cutLine = Mat::zeros(colorframeHeight_,colorframeWidth_,CV_8UC1);
	line(cutLine, start_pt, end_pt, 255, 1);

	// get indices of line segment (loop for testing only)
	vector<Point> nonZeroLocations;

	for(int y=0; y<colorframeHeight_-1; y++)
	{
		for(int x=0; x<colorframeWidth_-1; x++)
		{
			if(cutLine.data[x+(y*colorframeWidth_)]!=0) nonZeroLocations.push_back(Point(x,y));
		}
	}

	// stop it if we don't have masked hand pixels
	if(nonZeroLocations.size()==0) return false;

	// get intersections between hand and cutline
	vector<Point2f> intersections;

	for(unsigned int i=0; i<nonZeroLocations.size(); i++)
	{
		if(pointPolygonTest(*contour,nonZeroLocations[i],false)==1)
		{
			intersections.push_back(nonZeroLocations[i]);
			circle(*vizualization, nonZeroLocations[i], 3, Scalar(255,255,0), -1);
		}
	}

	// try different normal vector lengths till we reach the end of the fingertip
	vector<double> dists;
	vector<Point2f> shots;

	for(unsigned int i=0; i<nonZeroLocations.size(); i++)
	{
		int ctr = 0;
		double dist = 0;
		Point2f current = Point2f(0,0);

		while(true)
		{
			current = (Point2f)nonZeroLocations[i] - (-1.0f*normVec)*(float)ctr;

			// do it as long as we find again a non-masked pixel
			if(mask->at<uchar>((int)current.y,(int)current.x)==0) break;

			dist = norm((Point2f)nonZeroLocations[i]-current);

			ctr++;
		}

		if(dist>0)
		{
			dists.push_back(dist);
			shots.push_back(current);
		}
	}

	if(shots.size()>0)
	{
		// now get the longest normal vector to set it as our fingertip
		vector<double>::iterator iter = max_element(dists.begin(),dists.end());
		int element = distance(dists.begin(),iter);

		*result = (Point)shots[element];
		circle(*vizualization, *result, 5, Scalar(150,20,255), -1);

		return true;
	}

	// otherwise go back along the normalized normal vector and try it again with a new start/end point
	Point2d step = 10*normVec;
	return ellipseShot(result, vizualization, start_pt-step, end_pt-step, normVec, contour, mask);
}

void FingerTracker::fitEllipseOfHandSegment(Fingertip* fingertips, int nrHandPixels, Mat* mask)
{
	resetFingertips(fingertips);

	// calculate the number of hand pixels to cut the mask which includes the arm too
	vector<double> linspace;

	for(int i=0; i<colorframeHeight_-1; i++)
	{
		// collecting values for MATLAB-style "linspace"
		linspace.push_back(1 + (i+1)*0.6/(colorframeHeight_+1));
	}

	// get the hand segment from the whole mask
	double sum = 0;
	unsigned int pos = 0;

	Mat handArea = Mat::zeros(colorframeHeight_,colorframeWidth_,CV_8UC1);

	for(int y=0; y<colorframeHeight_-1; y++)
	{
		for(int x=0; x<colorframeWidth_-1; x++)
		{
			if( (sum > nrHandPixels) ) break;

			pos = x + (y*colorframeWidth_);

			if(mask->data[pos]!=0)
			{
				handArea.data[pos] = 255;
				sum += linspace[colorframeHeight_-y];
			}
		}
	}

	Mat handVisual = Mat::zeros(colorframeHeight_,colorframeWidth_,CV_8UC3);
	cvtColor(handArea,handVisual,CV_GRAY2BGR);

	// we need the contour to fit an ellipse
	vector<vector<Point> > contours;
	findContours(handArea, contours, CV_RETR_EXTERNAL, CV_CHAIN_APPROX_NONE);

	double area = 0;
	int number = 0;

	// looking for the largest contour area
	for(unsigned int i=0; i<contours.size(); i++)
	{
		double my = contourArea(contours[i]);

		if(my > area)
		{
			area = my;
			number = i;
		}
	}

	// the ellipse algorith needs a minimum amount of contour points
	if(contours.size() > 0 && contours[number].size() > 5)
	{
		RotatedRect ellipseInsc = fitEllipse(contours[number]);

		Point2f vertices[4];
		ellipseInsc.points(vertices);

		// draw the rectangle
		for(int i=0; i<4; i++)
		{
			Point3d myColor;

			if(i%2==0) myColor = Point3d(255,0,0);
			else myColor = Point3d(0,255,0);

			line(handVisual, vertices[i], vertices[(i+1)%4], Scalar(myColor.x,myColor.y,myColor.z));
		}

		// find the highest point
		int posHighest = 0;
		int valHighest = 99999;

		for(int i=0; i<4; i++)
		{
			int current = (int)vertices[i].y;

			if(current < valHighest)
			{
				valHighest = current;
				posHighest = i;
			}
		}

		circle(handVisual, vertices[posHighest], 5, Scalar(0,0,255), -1);

		// find the nearest next point with the shortest distance
		int posNearest = 0;
		int valNearest = 99999;

		for(int i=0; i<4; i++)
		{
			if(i==posHighest) continue;

			int dist = (int)norm(vertices[posHighest]-vertices[i]);

			if(dist < valNearest)
			{
				valNearest = dist;
				posNearest = i;
			}
		}

		circle(handVisual, vertices[posNearest], 5, Scalar(0,255,255), -1);

		// calculate and normalize the normal vector
		Point2f normalVector = Point2f(0.0f,0.0f);
		float dx = vertices[posNearest].x - vertices[posHighest].x;
		float dy = vertices[posNearest].y - vertices[posHighest].y;

		int angle = (int)ellipseInsc.angle % 180;

		if( (angle >= 90) && (angle < 180) )
		{
			normalVector.x = -dy;
			normalVector.y = dx;
		}
		else if( (angle >= 0) && (angle < 90) )
		{
			normalVector.x = dy;
			normalVector.y = -dx;
		}

		normalVector = normalVector * (1/norm(normalVector));

		// get result recursively
		Point result;

		if(ellipseShot(&result, &handVisual, vertices[posHighest], vertices[posNearest], normalVector, &contours[number], mask))
		{
			// write result into array
			
			fingertips[0].x = abs(result.x-colorframeWidth_);
			fingertips[0].y = abs(result.y-colorframeHeight_);
			fingertips[0].z = 0;
			/*fingertips[0].x = result.x;
			fingertips[0].y = result.y;
			fingertips[0].z = 0;*/
		}
	}

	imshow("visualization of fitted ellipse", handVisual);
}

void FingerTracker::findHighestPoint(Fingertip* fingertips, ColorRGBA* unityArray, int edgeLimit, int checkRadius)
{
	// no tracking of index/middle/ring/pinky
	resetFingertips(fingertips);

	// track only the thumb as a finger
	int pos;

	// find the highest point with nested loops
	for(int y=colorframeHeight_-1; y >= 0; y--)
	{
		for(int x=colorframeWidth_-1; x >= 0; x--)
		{
			pos = x+(y*colorframeWidth_);

			// transparent pixel
			if (unityArray[pos].a == 255)
			{
				// check around the point
				for(int i=1; i<=checkRadius; i++)
				{
					// where the actual checking begins
					if( !(x<edgeLimit || x>colorframeWidth_-edgeLimit) && (unityArray[pos+i].a==255) && (unityArray[pos-i].a==255) )
					{
						if( !(y<edgeLimit) && (unityArray[pos-(colorframeWidth_*i)].a==255) )
						{
							// if the area has been checked and conditions are satisfied
							if(i==checkRadius)
							{
								// and the copyright goes to somebody else
								/*fingertips[0].x = abs(x-colorframeWidth_);
								fingertips[0].y = abs(y-colorframeHeight_);*/
								
								fingertips[0].x = abs(x);
								fingertips[0].y = abs(y);
								fingertips[0].z = 0;
							}
						}
						else break;
					}
					else break;
				}
			}
		}
	}
}

#endif /* ART_FINGERTRACKER_HPP */