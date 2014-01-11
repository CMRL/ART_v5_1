using UnityEngine;
using System.Collections;

/**
 * The Coord_Handler class is for processing the data relating
 * to the positioning of objects in space - specifically fingertip
 * tracking data. These coordinates are calculated for Unity space
 * using vector mathematics. One of these scripts is attached to
 * each of the planes showing video.
 */
public class Coord_Handler : MonoBehaviour {
	
	public Camera mainCam							; // The main camera in the scene
	public Vector3[] world_pos						; // The array to hold the world positions of fingertips
	public Vector3[] screen_pos						; // The array to hold the screen positions of the fingertips
	private Transform upperLeft, bottomRight		; // The upper left and bottom right corners of this plane (used for calculation)
	private Coordinate[] fingertips					; // The array which holds the values while calculations are done
	private Resolution capture_resolution			; // The resolution of the webcam used to provide the image to this plane

	/**
	 * Used for initialization depending on which plane this script
	 * is attached to.
	 */
	void Start () {
		//mainCam = Camera.main;
		
		if (this.name == "LeftPlane") {
			upperLeft = GameObject.Find("UpperLeftOfLeftGO").transform;
			bottomRight = GameObject.Find("BottomRightOfLeftGO").transform;
		} else if (this.name == "RightPlane") {
			upperLeft = GameObject.Find("UpperLeftOfRightGO").transform;
			bottomRight = GameObject.Find("BottomRightOfRightGO").transform;
		} else {
			print("no planes found..");
		}
		
		fingertips = new Coordinate[5];
		
		world_pos = new Vector3[5];
        screen_pos = new Vector3[5];
	}

	/**
	 * Here is where the coordinates are recalculated for each set of
	 * coordinates that are passed from the plugin. Both Unity world
	 * space coordinates and screen space coordinates are calculated.
	 * Unity provides functions to convert from screen to world and 
	 * vice versa. 
	 */
	void Update () {
		
		Vector3 UL_Pos = mainCam.WorldToScreenPoint(upperLeft.position);
		Vector3 BR_Pos = mainCam.WorldToScreenPoint(bottomRight.position);
		
		Vector3 diff;
		diff.x = UL_Pos.x - BR_Pos.x;
		diff.y = UL_Pos.y - BR_Pos.y;
		
		if (this.name == "LeftPlane")
		{
			fingertips = mainCam.GetComponent<Plugin_Data>().left_fingertips;
			capture_resolution = mainCam.GetComponent<Plugin_Data>().capture_resolution_left;
		}
		else if (this.name == "RightPlane")
		{
			fingertips = mainCam.GetComponent<Plugin_Data>().right_fingertips;
			capture_resolution = mainCam.GetComponent<Plugin_Data>().capture_resolution_right;
		}
		else
		{
			//should never be reached
		}
		
		// Calculations to work out the Unity screen position of each point given from the plugin
		for(int i=0; i<5; i++)
        {
            screen_pos[i].x = BR_Pos.x + (diff.x * fingertips[i].x / capture_resolution.width);;
            screen_pos[i].y = BR_Pos.y + (diff.y * fingertips[i].y / capture_resolution.height);
            screen_pos[i].z = UL_Pos.z - 5; // subtraction only added for testing

            try
            {
                // The place where the sphere will be positioned in Unity
                world_pos[i] = mainCam.ScreenToWorldPoint(screen_pos[i]);
            }
            catch (UnityException e)
            {
                print("cannot make world point from screen point..." + e);
            }
        }
	}


}
