#ifndef ART_CONFIGHANDLER_HPP
#define ART_CONFIGHANDLER_HPP

#include "Utils_OneCam.h"
#include <boost/property_tree/ptree.hpp>
#include <boost/property_tree/ini_parser.hpp>

using namespace std;
using namespace boost::property_tree;

typedef struct {
	bool useTOF;
	bool captureFromCamera;
	string testcasePath;
	bool flipCam;
} CfgGeneral;

typedef struct {
	bool recalibCamIntrinsics;
	bool recalibCamExtrinsics;
	int nrFrames;
	int boardWidth;
	int boardHeight;
	int tileSize;
	float focalLengthX;
	float focalLengthY;
	float principalX;
	float principalY;
	float rotX;
	float rotY;
	float rotZ;
	float transX;
	float transY;
	float transZ;
	float dist_k1;
	float dist_k2;
	float dist_k3;
	float dist_p1;
	float dist_p2;
} CfgCalibCam;

typedef struct {
    int initialFrames;
	int frameWaitTime;
	int segMethod;
	float thresholdMean_RGB;
	int threshold_H_lower;
	int threshold_H_upper;
	int threshold_S;
	int threshold_V;
} CfgBackgroundSegmentation;

typedef struct {
	int trackMethod;
    int simpleEdgeLimit;
	int simpleCheckRadius;
	int nrHandPixels;
} CfgFingertipTracking;

class ConfigHandler
{
private:
	ptree pt_;

public:
    ConfigHandler();
	~ConfigHandler();
	void reloadConfigs();

	CfgGeneral general_;
	CfgCalibCam calib_cam_;
	CfgBackgroundSegmentation background_segmentation_;
	CfgFingertipTracking fingertip_tracking_;
};
 
ConfigHandler::ConfigHandler()
{
	// load from ConfigFile
	reloadConfigs();
}

ConfigHandler::~ConfigHandler()
{
	// nothing to do
}

void ConfigHandler::reloadConfigs()
{
	string filename;
	filename += INOUTPUT;
	filename += "_input/config.ini";
	ini_parser::read_ini(filename, pt_);
	string section_name;

	section_name = "General.";
	general_.useTOF = pt_.get<bool>(section_name+"useTOF");
	general_.captureFromCamera = pt_.get<bool>(section_name+"captureFromCamera");
	general_.testcasePath = pt_.get<string>(section_name+"testcasePath");
	general_.flipCam  = pt_.get<bool>(section_name+"flipCam");

	section_name = "CalibCamera.";
	calib_cam_.recalibCamIntrinsics = pt_.get<bool>(section_name+"recalibCamIntrinsics");
	calib_cam_.recalibCamExtrinsics = pt_.get<bool>(section_name+"recalibCamExtrinsics");
	calib_cam_.nrFrames = pt_.get<int>(section_name+"nrFrames");
	calib_cam_.boardWidth = pt_.get<int>(section_name+"boardWidth");
	calib_cam_.boardHeight = pt_.get<int>(section_name+"boardHeight");
	calib_cam_.tileSize = pt_.get<int>(section_name+"tileSize");
	calib_cam_.focalLengthX = pt_.get<float>(section_name+"focalLengthX");
	calib_cam_.focalLengthY = pt_.get<float>(section_name+"focalLengthY");
	calib_cam_.principalX = pt_.get<float>(section_name+"principalX");
	calib_cam_.principalY = pt_.get<float>(section_name+"principalY");
	calib_cam_.rotX = pt_.get<float>(section_name+"rotX");
	calib_cam_.rotY = pt_.get<float>(section_name+"rotY");
	calib_cam_.rotZ = pt_.get<float>(section_name+"rotZ");
	calib_cam_.transX = pt_.get<float>(section_name+"transX");
	calib_cam_.transY = pt_.get<float>(section_name+"transY");
	calib_cam_.transZ = pt_.get<float>(section_name+"transZ");
	calib_cam_.dist_k1 = pt_.get<float>(section_name+"dist_k1");
	calib_cam_.dist_k2 = pt_.get<float>(section_name+"dist_k2");
	calib_cam_.dist_k3 = pt_.get<float>(section_name+"dist_k3");
	calib_cam_.dist_p1 = pt_.get<float>(section_name+"dist_p1");
	calib_cam_.dist_p2 = pt_.get<float>(section_name+"dist_p2");

	section_name = "BackgroundSegmentation.";
	background_segmentation_.initialFrames = pt_.get<int>(section_name+"initialFrames");
	background_segmentation_.frameWaitTime = pt_.get<int>(section_name+"frameWaitTime");
	background_segmentation_.segMethod = pt_.get<int>(section_name+"segMethod");
	background_segmentation_.thresholdMean_RGB = pt_.get<float>(section_name+"thresholdMean_RGB");
	background_segmentation_.threshold_H_lower = pt_.get<int>(section_name+"threshold_H_lower");
	background_segmentation_.threshold_H_upper = pt_.get<int>(section_name+"threshold_H_upper");
	background_segmentation_.threshold_S = pt_.get<int>(section_name+"threshold_S");
	background_segmentation_.threshold_V = pt_.get<int>(section_name+"threshold_V");

	section_name = "FingertipTracking.";
	fingertip_tracking_.trackMethod = pt_.get<int>(section_name+"trackMethod");
	fingertip_tracking_.simpleEdgeLimit = pt_.get<int>(section_name+"simpleEdgeLimit");
	fingertip_tracking_.simpleCheckRadius = pt_.get<int>(section_name+"simpleCheckRadius");
	fingertip_tracking_.nrHandPixels = pt_.get<int>(section_name+"nrHandPixels");
}

#endif /* ART_CONFIGHANDLER_HPP */