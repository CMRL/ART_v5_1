using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.Diagnostics;

// Struct to represent the resolution of the image passed from OpenCV
public struct Resolution 
{
	public int width, height;
}

// Struct to represent a 2D/3D coordinate
public struct Coordinate 
{
	public int x, y, z;
}

// Struct to state properties of the application
public struct Properties 
{
	public int track_method, seg_method;
}

// Struct decscribing the layout of the planes (which cams on which planes)
// 1 = normal, 0 = Show nothing, -1 = opposite camera
public struct Plane_Orientaion 
{
	public int left_plane, right_plane;
}

/**
 * The Plugin_Data class is designed to be the main interaction between
 * Unity and the plugin(s). The main operation is to copy pixel data 
 * from the webcams, across to Unity textures. The plugin handles 
 * opertaions such as background subtraction and finger tracking.
 * The data from fingertracking is also passed through to an array
 * of Coordinates.
 */
public class Plugin_Data : MonoBehaviour {
	
	public GameObject left_plane;					// Gameobject for left plane
	public GameObject right_plane;					// Gameobject for right plane
	
	public Properties prop_left;					// Properties for the left cam
	public Properties prop_right;					// Properties for the right cam
	public Plane_Orientaion plane_layout;
	
	public Resolution capture_resolution_left;    	// image resolution of left cam
	public Resolution capture_resolution_right;    	// image resolution of right cam
    public Coordinate[] left_fingertips;          	// array of fingertips to be used in Unity
	public Coordinate[] right_fingertips;          	// array of fingertips to be used in Unity
	public Coordinate[] tmpleft_fingertips;         // array of fingertips for processing
	public Coordinate[] tmpright_fingertips;        // array of fingertips for processing
	
    private IntPtr ptrLeft;                  		// pointer to the memory for image pixels (left image)
	private IntPtr ptrRight;                 		// pointer to the memory for image pixels (right image)
	
	public Texture2D m_Texture_Left;             	// Unity texture for left plane
    private Color32[] pixel_data_Left;            	// array of pixel data for left plane
    private GCHandle m_pixelsHandle_Left;         	// pixel data handle in memory for left plane
	
	public Texture2D m_Texture_Right;             	// Unity texture for right plane
    private Color32[] pixel_data_Right;            	// array of pixel data for right plane
    private GCHandle m_pixelsHandle_Right;         	// pixel data handle in memory for right plane
	
	private bool usingTwoCams;						// Boolean to determine whether one or two cams will be used
	
	// Various functions imported from the Unity_Plugin(s)
	// UnityPluginLeft and UnityPluginRight are for the Two Camera approach
	// whereas .............. is for the one camera split image approach.
	[DllImport ("Unity_Plugin_Left")]
    private static extern Resolution start_capture_left(int webcam);
	[DllImport ("Unity_Plugin_Left")]
	private static extern void stop_capture_left();
    [DllImport ("Unity_Plugin_Left")]
    private static extern void resetFrameCounter_left();
	[DllImport ("Unity_Plugin_Left")]
    private static extern void frame_copy_to_Unity_left(IntPtr colors, Coordinate[] fingertips, Properties prop_left);
    [DllImport ("Unity_Plugin_Left")]
    private static extern void reloadConfigFile_left();
	
	[DllImport ("Unity_Plugin_Right")]
    private static extern Resolution start_capture_right(int webcam);
	[DllImport ("Unity_Plugin_Right")]
	private static extern void stop_capture_right();
    [DllImport ("Unity_Plugin_Right")]
    private static extern void resetFrameCounter_right();
	[DllImport ("Unity_Plugin_Right")]
    private static extern void frame_copy_to_Unity_right(IntPtr colors, Coordinate[] fingertips, Properties prop_right);
    [DllImport ("Unity_Plugin_Right")]
    private static extern void reloadConfigFile_right();
	
	[DllImport ("Unity_Plugin_OneCam")]
    private static extern Resolution start_capture(int webcam);
	[DllImport ("Unity_Plugin_OneCam")]
	private static extern void stop_capture();
    [DllImport ("Unity_Plugin_OneCam")]
    private static extern void resetFrameCounter();
	[DllImport ("Unity_Plugin_OneCam")]
	private static extern void copy_frame_to_Unity(IntPtr colorsLeft, IntPtr colorsRight, Coordinate[] left_fingertips, Coordinate[] right_fingertips, Properties prop_left, Properties prop_right);
    [DllImport ("Unity_Plugin_OneCam")]
    private static extern void reloadConfigFile();
	
	/**
	 * Start function used to initialize variables. Sets up the textures
	 * to be used, identifies the memory to be passed to the plugin, and
	 * sets specific properties of the transaction.
	 */
	void Start () {
		try {			
			usingTwoCams = false;
			
			if (usingTwoCams)
			{
				capture_resolution_left = start_capture_left(0);
				capture_resolution_right = start_capture_right(1);
			}
			else 
			{
				capture_resolution_left = start_capture(0);
				capture_resolution_right.width = capture_resolution_left.width;
				capture_resolution_right.height = capture_resolution_left.height;
			}
			
			// initialize fingertips
            left_fingertips = new Coordinate[5];
			right_fingertips = new Coordinate[5];
			
			tmpleft_fingertips = new Coordinate[5];
			tmpright_fingertips = new Coordinate[5];

            for(int i=0; i<5; i++)
            {
                left_fingertips[i].x = 0;
                left_fingertips[i].y = 0;
                left_fingertips[i].z = 0;
				
				right_fingertips[i].x = 0;
				right_fingertips[i].x = 0;
				right_fingertips[i].x = 0;
				
				tmpleft_fingertips[i].x = 0;
                tmpleft_fingertips[i].y = 0;
                tmpleft_fingertips[i].z = 0;
				
				tmpright_fingertips[i].x = 0;
				tmpright_fingertips[i].x = 0;
				tmpright_fingertips[i].x = 0;
            }
			
			// Make both textures
			m_Texture_Left = new Texture2D (640, 480, TextureFormat.ARGB32, false);
			m_Texture_Right = new Texture2D (640, 480, TextureFormat.ARGB32, false);
			
			// create the arrays for the pixels data
			pixel_data_Left = m_Texture_Left.GetPixels32(0);
			pixel_data_Right = m_Texture_Right.GetPixels32(0);
			
			// Allocates memory in the GPU for the pixel data to write to
			m_pixelsHandle_Left = GCHandle.Alloc(pixel_data_Left, GCHandleType.Pinned);
			m_pixelsHandle_Right = GCHandle.Alloc(pixel_data_Right, GCHandleType.Pinned);
			
			// Give the address in memory to the IntPtr
			ptrLeft = m_pixelsHandle_Left.AddrOfPinnedObject();
			ptrRight = m_pixelsHandle_Right.AddrOfPinnedObject();
			
			left_plane = GameObject.Find("LeftPlane");
			right_plane = GameObject.Find("RightPlane");
			
			// default properties
			prop_left = new Properties();
			prop_left.seg_method = 2;
			prop_left.track_method = 1;
			
			prop_right = new Properties();
			prop_right.seg_method = 2;
			prop_right.track_method = 1;
			
			// default plane layout
			plane_layout = new Plane_Orientaion();
			plane_layout.left_plane = 1;
			plane_layout.right_plane = 1;
			
			
			if (left_plane.renderer)
			{
				left_plane.renderer.material.mainTexture = m_Texture_Left;
			}
			else 
			{
				UnityEngine.Debug.Log ("Left Plane renderer not ready..");
			}
			if (right_plane.renderer)
			{
				right_plane.renderer.material.mainTexture = m_Texture_Right;
			}
			else
			{
				UnityEngine.Debug.Log ("Right Plane renderer not ready..");
			}
			
			
		} catch (Exception e) {
			UnityEngine.Debug.Log("Error in start function (data transfer): " + e.ToString());
		}
	}
	
	/**
	 * Called when this script is disabled (on application shutdown), and
	 * calls the plugin functions to stop both cameras from capturing, or
	 * for the one camera approach, calls to stop the one camera. The frame
	 * counter is also reset incase of using video streams.
	 */
	void OnDisable() {

		try
        {
			if (usingTwoCams)
			{
				stop_capture_left(); resetFrameCounter_left();
				stop_capture_right(); resetFrameCounter_right();
			}
			else
			{
				stop_capture();
				resetFrameCounter();
			}
		}
        catch(Exception e)
        {
            UnityEngine.Debug.Log("stop-capture-error " + e.ToString());
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown(KeyCode.M))
		{
			Application.LoadLevel(1);
		}
		// temporarily changing tracking method on the fly
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			prop_left.track_method = 1; // find highest point
			prop_right.track_method = 1; // find highest point
		}
		else if (Input.GetKeyDown(KeyCode.RightShift))
		{
			prop_left.track_method = 2; // ellipse
			prop_right.track_method = 2; // ellipse
		}
		
		// Get data from the plugin (RGB frame data, tracking data) either
		// with one webcam or with two.
		if (usingTwoCams)
		{
			frame_copy_to_Unity_left(ptrLeft, tmpleft_fingertips, prop_left);
			frame_copy_to_Unity_right(ptrRight, tmpright_fingertips, prop_right);
		}
		else
		{
			copy_frame_to_Unity(ptrLeft, ptrRight, tmpleft_fingertips, tmpright_fingertips, prop_left, prop_right);
			//frame_copy_to_Unity(ptrLeft, ptrRight, tmpleft_fingertips, tmpright_fingertips, prop_left, prop_right);
		}
		
		// Orientation of the planes (which side etc)
		if (plane_layout.left_plane == 1)
		{
			left_plane.renderer.material.mainTexture = m_Texture_Left;
			left_fingertips = tmpleft_fingertips;
		}
		else if (plane_layout.left_plane == -1)
		{
			left_plane.renderer.material.mainTexture = m_Texture_Right;
			left_fingertips = tmpright_fingertips;
		}
		else
		{
			left_plane.renderer.enabled = false;
		}
		
		if (plane_layout.right_plane == 1)
		{
			right_plane.renderer.material.mainTexture = m_Texture_Right;
			right_fingertips = tmpright_fingertips;
		}
		else if (plane_layout.right_plane == -1)
		{
			right_plane.renderer.material.mainTexture = m_Texture_Left;
			right_fingertips = tmpleft_fingertips;
		}
		else
		{
			right_plane.renderer.enabled = false;
		}
		
		// Actually upload the changes to the Unity textures (GPU)
		m_Texture_Left.SetPixels32(pixel_data_Left);
		m_Texture_Right.SetPixels32(pixel_data_Right);
		m_Texture_Left.Apply(false);
		m_Texture_Right.Apply(false);		
		
		
	}
	
	
	
}

