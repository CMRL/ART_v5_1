using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.Diagnostics;


// Basically gets stream from each camera and render it as texture on a given plane
// It is called from each plane
// It also copies these frames on left and write plane in unity using plugins
// Struct to represent the resolution of the image passed from OpenCV
public struct Resolution {
	public int width, height;
}

// Struct to represent an x, y coordinate
public struct Coordinate {
	public int x, y, z;
}

public class Plugin_Handler : MonoBehaviour {
	
	public Resolution capture_resolution;    // image resolution
    public Coordinate[] fingertips;          // array of fingertips
	private Texture2D m_Texture;             // drawn Unity texture
    private Color32[] pixel_data;            // array of pixel data
    private GCHandle m_pixelsHandle;         // pixel data handle in memory
    private IntPtr ptr;                      // pointer to that memory for image pixels
	private string plane_name;				 // The name of the plane "this" is attached to
	private int webcam;						 // int representing which webcam to show
	
	// Various functions imported from the Unity_Plugin
	[DllImport ("Unity_Plugin_Left")]
    private static extern Resolution start_capture_left(int webcam);
	[DllImport ("Unity_Plugin_Left")]
	private static extern void stop_capture_left();
    [DllImport ("Unity_Plugin_Left")]
    private static extern void resetFrameCounter_left();
	[DllImport ("Unity_Plugin_Left")]
    private static extern void frame_copy_to_Unity_left(IntPtr colors, Coordinate[] fingertips);
    [DllImport ("Unity_Plugin_Left")]
    private static extern void reloadConfigFile_left();
	
	[DllImport ("Unity_Plugin_Right")]
    private static extern Resolution start_capture_right(int webcam);
	[DllImport ("Unity_Plugin_Right")]
	private static extern void stop_capture_right();
    [DllImport ("Unity_Plugin_Right")]
    private static extern void resetFrameCounter_right();
	[DllImport ("Unity_Plugin_Right")]
    private static extern void frame_copy_to_Unity_right(IntPtr colors, Coordinate[] fingertips);
    [DllImport ("Unity_Plugin_Right")]
    private static extern void reloadConfigFile_right();
	
	// Use this for initialization
	void Start () {
		try {
			
			plane_name = this.name;
			
			// Start camera to get capture
			if (plane_name == "LeftPlane") {webcam = 0; capture_resolution = start_capture_left(webcam);}
			else if (plane_name == "RightPlane") {webcam = 1; capture_resolution = start_capture_right(webcam);}
			else {/*This should never be reached*/}
			
			// initialize the Texture2d for the stream
			m_Texture = new Texture2D (capture_resolution.width, capture_resolution.height, TextureFormat.ARGB32, false);
			
			// create the array for the pixels data
			pixel_data = m_Texture.GetPixels32(0);
			
			// Allocates memory in the GPU for the pixel data to write to
			m_pixelsHandle = GCHandle.Alloc(pixel_data, GCHandleType.Pinned);
			
			// Give the address in memory to the IntPtr
			ptr = m_pixelsHandle.AddrOfPinnedObject();
			
			 // initialize fingertips
            fingertips = new Coordinate[5];

            for(int i=0; i<5; i++)
            {
                fingertips[i].x = 0;
                fingertips[i].y = 0;
                fingertips[i].z = 0;
            }
			
			if (renderer) {
				//set 2d texture
				renderer.material.mainTexture = m_Texture;
			} else {
				UnityEngine.Debug.Log("BigErrors.COM");
			}
			
		} catch (Exception e) {
			UnityEngine.Debug.Log("Error in start function: " + e.ToString());
		}
	}
	
	void OnDisable() {

		try
        {
			// if its left or right capture
			if (webcam == 0) {stop_capture_left(); resetFrameCounter_left();}
			else if (webcam == 1) {stop_capture_right(); resetFrameCounter_right();}
			else {/*Should never be reached*/}
		}
        catch(Exception e)
        {
            UnityEngine.Debug.Log("stop-capture-error " + e.ToString());
		}
	}
	
	public void set_cameras(int camera) {
		// 0 means left camera, 1 means right camera, -1 means no cam
		webcam = camera;
	}
	
	// Update is called once per frame
	void Update () {
		
		// reloads config file if key was pressed
        if(Input.GetKeyDown("space"))
        {
			if (webcam == 0)
			{
				reloadConfigFile_left();
			}
			else if (webcam == 1)
			{
				reloadConfigFile_right();
			}
        }
		UnityEngine.Debug.Log("Got this far...");
		// this is where we get the frame details from OpenCV
		try
        {
			if (webcam == 0)
			{
				frame_copy_to_Unity_left(ptr, fingertips);
			}
			else if (webcam == 1)
			{
				frame_copy_to_Unity_right(ptr, fingertips);
			}
		}
        catch (Exception e)
        {
			UnityEngine.Debug.Log(e);
		}
		
		// may need a sleep function here for a short time to prevent
		// too many frames from coming through and overwhelming Unity
		UnityEngine.Debug.Log(".");
		
		// Actually upload the changes to the Unity texture (GPU)
		m_Texture.SetPixels32(pixel_data);
		m_Texture.Apply(false);
		
	}
	
	
	
}
