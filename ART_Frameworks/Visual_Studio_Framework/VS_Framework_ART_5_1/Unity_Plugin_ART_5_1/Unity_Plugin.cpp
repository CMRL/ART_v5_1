/************************************************************************/
/* This program is the main plugin file which is compiled to create the */
/* dll file to be used in conjunction with Unity. There are several .h  */
/* and .hpp classes which are called. Some of the main functionalities  */
/* included is webcam video retrieval and manipulation. Background		*/
/* subtraction and finger tracking operations are available.			*/
//////////////////////////////////////////////////////////////////////////
/* The functions contained in this programs which are designed to be	*/
/* called from unity have two headers as below, and which header is		*/
/* compiled with depends on the DEFINE in Utils.h. This is to reduce	*/
/* redundancy in code and change the define for the compilation.		*/
/************************************************************************/

#if _MSC_VER
#define EXPORT_API __declspec(dllexport) // VisualStudio needs annotating exported functions with this
#else
#define EXPORT_API // XCode does not need annotating exported functions, so define is empty
#endif

// standard library
#include "iostream"
#include "iomanip"
#include <math.h>
#include <windows.h>

// openCV library
#include "highgui.h"
#include "cxcore.h"
#include "cvaux.h"

// our classes
#include "Utils.h"
#include "SegmentationManager.hpp"
#include "ConfigHandler.hpp"

// namespaces
using namespace std;
using namespace cv;

// use CPP but compile in C-style 
extern "C"
{

ConfigHandler* config_		            ; // all individual settings are stored in the config object
VideoCapture webcamStream_              ; // for video capturing from cameras or files
VideoCapture testcaseStream_            ; // loads the depth data for testcases only
SegmentationManager* segMgr_            ; // instance to do the hand/finger segmentation
double framecounter_                    ; // used to count frames when loading video files for testcases

#ifndef NO_TESTCASE
VideoWriter videoWriterColor_;
VideoWriter videoWriterDepth_;
#endif

/************************************************************************/
/* The start_capture (left or right) function performs operations which */
/* start the appropriate webcam, calibrates a median averaged background*/
/* image and returns the resolution of the device captured				*/
//////////////////////////////////////////////////////////////////////////
/* @param webcam is the int representing the device to be started		*/
//////////////////////////////////////////////////////////////////////////
/* @return the Resolution struct representing the res of the started	*/
/* capture																*/
/************************************************************************/
#ifndef USE_LEFT_CAM
Resolution EXPORT_API start_capture_left(int webcam)
#else
Resolution EXPORT_API start_capture_right(int webcam)
#endif
{
	framecounter_ = 0;

	// open console
	AllocConsole(); 
	freopen("CONOUT$", "w", stdout);

	// load settings from configfile
	config_ = new ConfigHandler();

	if (config_->general_.captureFromCamera)
	{
		// initiate video with camera index (webcam) and set frame rate
		cout << "Beginning application with default webcam device" << endl;

		webcamStream_.open(webcam);
		webcamStream_.set(CV_CAP_PROP_FPS,20);
		webcamStream_.set(CV_CAP_PROP_FRAME_WIDTH,colorframeWidth_);
		webcamStream_.set(CV_CAP_PROP_FRAME_HEIGHT,colorframeHeight_);
	}

	// collect some frames and create background image
	segMgr_ = new SegmentationManager(false);

	if(config_->background_segmentation_.initialFrames>0)
	{
		cout << "Collecting frames for BackgroundImage... ";
	
		vector<Mat*> frames;

		for(int i=0; i<config_->background_segmentation_.initialFrames; i++)
		{	
			Mat* frame = new Mat();

			if(config_->general_.useTOF && config_->general_.captureFromCamera)
			{
				//intelStream_->processDataStreaming(false,false);
				//intelStream_->copyCurrentColor(frame);
				waitKey(1);
			}
			else
			{
				webcamStream_.read(*frame);
				if(testcaseStream_.isOpened())
				{
					Mat woswasi; // otherwise we get an offset between color and depth video files
					testcaseStream_.read(woswasi);
				}
			}

			if(config_->general_.flipCam)
			{
				flip(*frame,*frame,-1);
			}

			frames.push_back(frame);
			Sleep(config_->background_segmentation_.frameWaitTime);
		}
		cout << "Done" << endl;
		
		segMgr_->calibrateBackground(frames);
		
		for(unsigned int i=0; i<frames.size(); i++)
		{
			delete frames[i];
		}
	}

	Resolution unityRes= {colorframeWidth_,colorframeHeight_};
	return unityRes;
}

/************************************************************************/
/* Function called from Unity Script to stop the device from capturing	*/
/* video.																*/
/************************************************************************/
#ifndef USE_LEFT_CAM
void EXPORT_API stop_capture_left()
#else
void EXPORT_API stop_capture_right()
#endif
{
	webcamStream_.release();

	//if(config_->general_.useTOF) delete intelStream_;

	//delete calibCam_;
	//delete fingTr_;
	delete segMgr_;
	delete config_;
}

/************************************************************************/
/* This function is called from a Unity script to reset the             */
/* framecounter variable when using videos as testcases					*/
/************************************************************************/
#ifndef USE_LEFT_CAM
void EXPORT_API resetFrameCounter_left()
#else
void EXPORT_API resetFrameCounter_right()
#endif
{
	framecounter_ = 0;
}

/************************************************************************/
/* Function called from Unity Script to reload settings and parameters  */
/* in the config file												    */
/************************************************************************/
#ifndef USE_LEFT_CAM
void EXPORT_API reloadConfigFile_left()
#else
void EXPORT_API reloadConfigFile_right()
#endif
{
	config_->reloadConfigs();
}

/************************************************************************/
/* This is the function which is called from unity, and which is		*/
/* responsible for performing all of the required operations on a frame */
/* such as background subtraction and finger tracking functionalities,	*/
/* and copying the frame across to Unity.								*/
//////////////////////////////////////////////////////////////////////////
/* @param colors is the pointer passed in from Unity to copy pixels to.	*/
/* @param fingertips in the pointer passed in from Unity to copy the	*/
/*		  finger tracking details to.									*/
/************************************************************************/
#ifndef USE_LEFT_CAM
void EXPORT_API frame_copy_to_Unity_left(ColorRGBA* colors, Fingertip* fingertips)
#else
void EXPORT_API frame_copy_to_Unity_right(ColorRGBA* colors, Fingertip* fingertips)
#endif
{
	// Mats for holding the current RGB and Depth frames
	Mat* currentColorFrame;
	Mat* currentDepthFrame;

	Mat tmpForWebcamUse;

	// prevent from crashing when video end is reached
	if(!config_->general_.captureFromCamera)
	{
		double frameNrs;
		frameNrs = webcamStream_.get(CV_CAP_PROP_FRAME_COUNT);
		Sleep(20); // because video plays too fast

		if(framecounter_ > frameNrs-frameNrs/20)
		{
			cout << "FrameNumber " << framecounter_ << " of " << frameNrs << " frames (video stopped a little bit before the end)" << endl;
			return;
		}

		framecounter_++;
	}

	// get the next frame
	if(config_->general_.useTOF && config_->general_.captureFromCamera)
	{
		//intelStream_->processDataStreaming(false,true,segMgr_,config_); // segmentation and config for fingertracking only

		//currentColorFrame = intelStream_->getCurrentColor();
		//currentDepthFrame = intelStream_->getCurrentDepth();

#ifndef NO_TESTCASE
		Mat tmp(colorframeHeight_,colorframeWidth_,CV_8UC3,Scalar(0,0,0));

		for (int y=0; y<colorframeHeight_; y++)
		{
			for (int x=0; x<colorframeWidth_; x++)
			{
				tmp.at<Vec3b>(y,x)[0] = (uchar)(currentDepthFrame->at<unsigned short>(y,x));
				tmp.at<Vec3b>(y,x)[1] = (uchar)(currentDepthFrame->at<unsigned short>(y,x) >> 8 & 0xFF);
			}
		}

		videoWriterColor_ << *currentColorFrame;
		videoWriterDepth_ << tmp;
#endif

		waitKey(1); // FIXME, then you can delete me (instead no images are shown during the main loop)
	}
	else if(testcaseStream_.isOpened())
	{
		//currentColorFrame = intelStream_->getCurrentColor();
		webcamStream_.read(*currentColorFrame);

		Mat tmp;
		testcaseStream_.read(tmp);
		//currentDepthFrame = intelStream_->getCurrentDepth();

		for (int y=0; y<colorframeHeight_; y++)
		{
			for (int x=0; x<colorframeWidth_; x++)
			{
				currentDepthFrame->data[currentDepthFrame->step[0]*y + currentDepthFrame->step[1]*x + 0] = tmp.data[tmp.step[0]*y + tmp.step[1]*x + 0];
				currentDepthFrame->data[currentDepthFrame->step[0]*y + currentDepthFrame->step[1]*x + 1] = tmp.data[tmp.step[0]*y + tmp.step[1]*x + 1];
			}
		}
	}
	else
	{
		currentColorFrame = &tmpForWebcamUse;
		webcamStream_.read(*currentColorFrame);
	}

	// flip frame if an inverse mounted camera is in use
	if(config_->general_.flipCam)
	{
		flip(*currentColorFrame,*currentColorFrame,-1);
	}

	// show images in openCV windows
	//if(currentColorFrame)imshow("woswasi RGB",*currentColorFrame);
	//if(currentDepthFrame)imshow("woswasi DEPTH",*currentDepthFrame);

	// segmentation process
	if(config_->background_segmentation_.segMethod==1)
	{
		// used inside the box
		segMgr_->updateWithBackgroundSubtraction(currentColorFrame,colors,config_->background_segmentation_.thresholdMean_RGB);
	}
	else if(config_->background_segmentation_.segMethod==2)
	{
		// used outside the box
		int thres_S = config_->background_segmentation_.threshold_S;
		int thres_V = config_->background_segmentation_.threshold_V;
		int thres_H_lower = config_->background_segmentation_.threshold_H_lower;
		int thres_H_upper = config_->background_segmentation_.threshold_H_upper;
		segMgr_->updateWithBackgroundThresholding(currentColorFrame,colors,thres_H_lower,thres_H_upper,thres_S,thres_V);
	}
	else
	{
		cout << "Please choose a segmentation method" << endl;
		return;
	}

	// fingertracking process
	//if(config_->fingertip_tracking_.trackMethod==1)
	//{
	//	fingTr_->findHighestPoint(fingertips,colors,config_->fingertip_tracking_.simpleEdgeLimit,config_->fingertip_tracking_.simpleCheckRadius);
	//}
	//else if(config_->fingertip_tracking_.trackMethod==2)
	//{
	//	fingTr_->fitEllipseOfHandSegment(fingertips, config_->fingertip_tracking_.nrHandPixels,&segMgr_->handMask_);
	//}
	//else if(config_->fingertip_tracking_.trackMethod==3)
	//{
	//	intelStream_->fingertipTracking(fingertips);
	//}
	//else if(config_->fingertip_tracking_.trackMethod==4)
	//{
	//	// MACHINE LEARNING ... comming soon
	//}
	//else
	//{
	//	fingTr_->resetFingertips(fingertips);
	//	cout << "Please choose a fingertracking method" << endl;
	//	return;
	//}

	// we are ready
	return;
}


}