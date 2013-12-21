#ifndef ART_UTILS_H
#define ART_UTILS_H

#include <math.h>

#define NOMINMAX // because of min/max-problem with windows-headerfile
#pragma warning(disable : 4995)
#pragma warning(disable : 4996)

// our defines
#define NO_TESTCASE 123;
#define USE_LEFT_CAM 123;
#define INOUTPUT "PATRIZK";

// resolutions
const int colorframeWidth_  = 640;
const int colorframeHeight_ = 480;
const int depthframeWidth_  = 320;
const int depthframeHeight_ = 240;

// structs
typedef struct {
	byte r,g,b,a;
} ColorRGBA;

typedef struct {
	int width,height;
} Resolution;

typedef struct {
	int x,y,z;
} Fingertip;

typedef struct {
	int screen_x,screen_y,length;
	byte b,g,r;
} Pixel;

typedef struct {
	int track_method, seg_method;
} Properties;

#endif /* ART_UTILS_H */