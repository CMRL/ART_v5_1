#ifndef ART_SEGMENTATIONMANAGER_HPP
#define ART_SEGMENTATIONMANAGER_HPP

#include <math.h>
#include <iostream>

#include <boost/filesystem.hpp>

#include "highgui.h"
#include "cxcore.h"
#include "cvaux.h"

#include "Utils_OneCam.h"

using namespace std;
using namespace cv;

class SegmentationManager
{
private:
	bool rendering_;    // show debugging images or not
	Mat backgroundImg_; // a median filtered background image for subtractions
 
public:
    SegmentationManager(bool rendering);
	~SegmentationManager();

	void calibrateBackground(vector<Mat*> someFrames);
	void updateWithBackgroundSubtraction(Mat* currentFrame, ColorRGBA* unityColorArray, float thresholdRGB); // uses created background image if threshold is zero
	void updateWithBackgroundThresholding(Mat* currentFrame, ColorRGBA* unityColorArray, int thres_H_lower, int thres_H_upper, int thres_S, int thres_V); // using HSV instead of RGB values

	Mat handMask_;
};


SegmentationManager::SegmentationManager(bool rendering)
{
	rendering_ = rendering;
	backgroundImg_ = Mat::zeros(colorframeHeight_,colorframeWidth_,CV_8UC3);
	handMask_ = Mat::zeros(colorframeHeight_,colorframeWidth_,CV_8UC1);
}

SegmentationManager::~SegmentationManager()
{
	// nothing to do
}

/************************************************************************/
/* This function takes some frames, converts them from RGB to HSV color */
/* space. It then finds the median colour values for each pixel to		*/
/* compute a more accurate median averaged background image which can	*/
/* then be used for subtraction.										*/
//////////////////////////////////////////////////////////////////////////
/* @param someFrames is a vector of Mat pointers which are the frames	*/
/* to be median averaged to find the background image					*/
//////////////////////////////////////////////////////////////////////////
/************************************************************************/
void SegmentationManager::calibrateBackground(vector<Mat*> someFrames)
{
	vector<Mat*> framesRGB;
	vector<Mat*> framesHSV;

	// convert frames from RGB to HSV
	for(unsigned int i=0; i<someFrames.size(); i++)
	{
		framesRGB.push_back(someFrames[i]);
		//cout << "process RGB/HSV CalibFrame " << i+1 << " of "  << someFrames.size() << endl;

		Mat* tmp = new Mat();
		cvtColor(*framesRGB[i], *tmp, CV_BGR2HSV);
		framesHSV.push_back(tmp);

		if(rendering_)
		{
			//imshow("Frame RGB for MedianBackgroundImage",*framesRGB[i]);
			//imshow("Frame HSV for MedianBackgroundImage",*framesHSV[i]);
			//waitKey();
		}
	}

	// put color values into the array and sort it
	vector<vector<vector<vector<uchar>>>> colorVals;
	cout << "Calibrating BackgroundImage with Median ... ";

	for(int x=0; x<colorframeWidth_; x++)
	{
		vector<vector<vector<uchar>>> tempy;
		colorVals.push_back(tempy);

		for(int y=0; y<colorframeHeight_; y++)
		{
			vector<vector<uchar>> channels;
			tempy.push_back(channels);

			vector<uchar> chan0;
			vector<uchar> chan1;
			vector<uchar> chan2;
			
			// collect data
			for(unsigned int i=0; i<someFrames.size(); i++)
			{
				Vec3b PxCol = framesHSV[i]->at<Vec3b>(y,x);
				chan0.push_back(PxCol.val[0]);
				chan1.push_back(PxCol.val[1]);
				chan2.push_back(PxCol.val[2]);
			}

			channels.push_back(chan0);
			channels.push_back(chan1);
			channels.push_back(chan2);

			// sort data and find median
			for(int j=0; j<3; j++)
			{
				sort(channels[j].begin(),channels[j].end());
				backgroundImg_.at<Vec3b>(y,x)[j] = channels[j][(int)((float)someFrames.size()/2.0f+0.5f)];
			}
		}
	}

	// write background image
	Mat tempy;
	cvtColor(backgroundImg_, tempy, CV_HSV2BGR);
	string output;
	output += INOUTPUT;
	imwrite(output+"_output/BackgroundMedianImage.png", tempy);

	// write frames and delete it
	output += "_output/BackgroundImageCollection";
	boost::filesystem::path dir(output);
	boost::filesystem::create_directory(dir);

	for(unsigned int i=0; i<someFrames.size(); i++)
	{
		stringstream filename;
		filename << output << "/CalibFrame_" << setw(10) << setfill('0') << i+1 << ".png";
		imwrite(filename.str(), *framesRGB[i]);
		delete framesHSV[i];
	}

	cout << "Done" << endl;
}

/************************************************************************/
/* This is a background subtraction function which will subtract the	*/
/* pixels from the given frame based on a threshold value. It will then */
/* copy the pixel data across to the pointer in memory also passed in.  */
//////////////////////////////////////////////////////////////////////////
/* @param currentFrame is the current frame to be subtracted and copied */
/* @param unityColorArray is the pointer to copy the pixel data to		*/
/* @param thresholdRGB is the value to subtract pixels against			*/
//////////////////////////////////////////////////////////////////////////
/************************************************************************/
void SegmentationManager::updateWithBackgroundSubtraction(Mat* currentFrame, ColorRGBA* unityColorArray, float thresholdRGB)
{
	if(thresholdRGB==0)
	{
		// subtract background image instead of using one threshold for the whole image
		// .................
		// .................
		// .................
	}
	else
	{
		int pos; // representing the position in array to copy to
		float compareValue;

		for (int x = 0; x < colorframeWidth_; x++)
		{
			for (int y = 0; y < colorframeHeight_; y++)
			{
				pos = x + (y*colorframeWidth_);

				unityColorArray[pos].b = currentFrame->at<Vec3b>(y,x)[0];
				unityColorArray[pos].g = currentFrame->at<Vec3b>(y,x)[1];
				unityColorArray[pos].r = currentFrame->at<Vec3b>(y,x)[2];

				// background subtraction (RGB)
				compareValue = ((float) unityColorArray[pos].b + (float) unityColorArray[pos].g + (float) unityColorArray[pos].r) / 3.0f;

				if(compareValue < thresholdRGB)
				{
					// should be transparent
					unityColorArray[pos].a = 0;
				}
				else
				{
					// should be opaque
					unityColorArray[pos].a = 255;
				}
			}
		}
	}
}

/************************************************************************/
/* This is a background subtraction function which subtracts the		*/
/* pixels based on an HSV converted frame of the given current frame	*/
/* and using threshold values for each of the H, S, and V components to */
/* subtract pixels against.												*/
/* It also copies the processed image to the point supplied by Unity.	*/
/* There are also operations which take care of eroding and dilating	*/
/* aspects within the image. Small area contours are also identified	*/
/* and removed and the Hand contour is stored.							*/
//////////////////////////////////////////////////////////////////////////
/* @param currentFrame is the Mat to be processed and copied to Unity	*/
/* @param unityColorArray is the pointer to copy the pixel data to		*/
/* @param thres_H_lower is the low end value for the Hue component		*/
/* @param thres_H_upper is the high end value for the Hue component		*/
/* @param thres_S is the threshold value for the Saturation component	*/
/* @param thres_V is the threshold value for the Value component		*/
//////////////////////////////////////////////////////////////////////////
/************************************************************************/
void SegmentationManager::updateWithBackgroundThresholding(Mat* currentFrame, ColorRGBA* unityColorArray, int thres_H_lower, int thres_H_upper, int thres_S, int thres_V)
{
	Mat currentFrameHSV;
	cvtColor(*currentFrame, currentFrameHSV, CV_BGR2HSV);

	Mat colorMat = Mat::zeros(colorframeHeight_,colorframeWidth_, CV_8UC4);
	
	int pos;
	int channelIdx;

	for (int x = 0; x < colorframeWidth_; x++)
	{
		for (int y = 0; y < colorframeHeight_; y++)
		{
			// calculate position in array for unity and in our matrix
			pos = x + (y*colorframeWidth_); 
			
			//channelIdx = currentFrame->step[0]*y + currentFrame->step[1]*x;

			// access data bytes directly if storing pixel data in OpenCV mats
			unityColorArray[pos].b = currentFrame->at<Vec3b>(y,x).val[0];
			unityColorArray[pos].g = currentFrame->at<Vec3b>(y,x).val[1];
			unityColorArray[pos].r = currentFrame->at<Vec3b>(y,x).val[2];
			unityColorArray[pos].a = 255;

			// only weak testing of saturation and value (brightness)
			if( (currentFrameHSV.at<Vec3b>(y,x).val[1] > thres_S) && (currentFrameHSV.at<Vec3b>(y,x).val[2] > thres_V) )
			{
				// find range of background color (hue)
				if( (currentFrameHSV.at<Vec3b>(y,x).val[0] > thres_H_lower) && (currentFrameHSV.at<Vec3b>(y,x).val[0] < thres_H_upper) )
				{
					// should be transparent
					unityColorArray[pos].a = 0;
				}
				//else if( (currentFrameHSV.at<Vec3b>(y,x).val[0] > 65) && (currentFrameHSV.at<Vec3b>(y,x).val[0] < 145) ) {colors[pos].a =  95;}
				//else if( (currentFrameHSV.at<Vec3b>(y,x).val[0] > 55) && (currentFrameHSV.at<Vec3b>(y,x).val[0] < 155) ) {colors[pos].a = 135;}
				//else if( (currentFrameHSV.at<Vec3b>(y,x).val[0] > 45) && (currentFrameHSV.at<Vec3b>(y,x).val[0] < 165) ) {colors[pos].a = 175;}
				//else if( (currentFrameHSV.at<Vec3b>(y,x).val[0] > 35) && (currentFrameHSV.at<Vec3b>(y,x).val[0] < 175) ) {colors[pos].a = 215;}
			}
			else
			{
				// should be opaque
				unityColorArray[pos].a = 255;
			}
		}
	}
	
	// eroding and dilating
	Mat tempy(colorframeHeight_, colorframeWidth_, CV_8UC4, unityColorArray);
	Mat dummy(tempy.rows, tempy.cols, CV_8UC3);
	Mat morpho = Mat::zeros( tempy.rows, tempy.cols, CV_8UC1);
	Mat mix[] = {dummy,morpho};
	int from_to[] = { 0,2 , 1,1 , 2,0 , 3,3 };
	mixChannels( &tempy, 1, mix, 2, from_to, 4 );
	//if(rendering_) imshow("segmentation",morpho);

	int kernelsize = 1; // a 3x3-cross-kernel
	Mat element = getStructuringElement(MORPH_CROSS, Size(2*kernelsize+1, 2*kernelsize+1), Point(kernelsize,kernelsize) );
	morphologyEx(morpho, morpho, MORPH_OPEN , element, Point(-1,-1), 2);
	//if(rendering_) imshow("morphology",morpho);

	int comeback[] = {0,3}; // copy pointers back to array
	mixChannels( &morpho, 1, &tempy, 1, comeback, 1 );

	// remove small areas with contours
	vector<Vec4i> hierarchy;
	vector<vector<Point> > contours_allFound;
	vector<vector<Point> > contours_sortedOut;
	vector<double> areas;
	findContours(morpho, contours_allFound, hierarchy, CV_RETR_TREE, CV_CHAIN_APPROX_NONE);
	
	drawContours(morpho, contours_allFound, -1, Scalar(255,255,255), 1, 8);
	//if(rendering_) imshow("contours",morpho);

	for(unsigned int i=0; i<contours_allFound.size(); i++)
	{
		double area = contourArea(contours_allFound[i]);
		areas.push_back(area);

		// the area has to be large enough
		if(area>30)
		{
			contours_sortedOut.push_back(contours_allFound[i]);
		}
	}

	// find hand segment
	unsigned int largest_number = 0;
	double largest_area = 0;

	for(unsigned int i=0; i<contours_allFound.size(); i++)
	{
		// see also OpenCV Hierarchy documentation
		// hierarchy[i][0] ... NEXT
		// hierarchy[i][1] ... PREVIOUS
		// hierarchy[i][2] ... CHILD
		// hierarchy[i][3] ... PARENT
		if( (largest_area < areas[i]) && (hierarchy[i][3] == -1))
		{
			largest_number = i;
			largest_area = areas[i];
		}
	}

	// save hand segment
	handMask_ = 0;

	if(contours_allFound.size() > 0)
	{
		vector<vector<Point> > hand;
		hand.push_back(contours_allFound[largest_number]);

		for(unsigned int i=0; i<contours_allFound.size(); i++)
		{
			if(hierarchy[i][3] == largest_number) hand.push_back(contours_allFound[i]);
		}

		fillPoly(handMask_, hand, Scalar(255));
	}

	// fill areas again
	Mat filling = Mat::zeros(colorframeHeight_, colorframeWidth_, CV_8UC1);
	fillPoly(filling, contours_sortedOut, Scalar(255));
	//if(rendering_) imshow("filling",filling);
	mixChannels( &filling, 1, &tempy, 1, comeback, 1);
}


#endif /* ART_SEGMENTATIONMANAGER_HPP */