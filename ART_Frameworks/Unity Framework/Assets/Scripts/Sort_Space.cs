using UnityEngine;
using System.Collections;
// Called with each left and right plane
//Gets frame from camera and finger tip position using Plugin_Handler ( which further deals with plugins for left and right cameras)
public class Sort_Space : MonoBehaviour {
	
	public Camera camera;
	public Vector3[] sphere_world_pos;
	public Vector3[] sphere_screen_pos;
	private Transform upperLeft, bottomRight;
	
	private Coordinate finger_tip_coordinate;
	private Resolution capture_resolution;

	// Use this for initialization
	void Start () {
		camera = Camera.main;
		
		if (this.name == "LeftPlane") {
			upperLeft = GameObject.Find("UpperLeftOfLeftGO").transform;
			bottomRight = GameObject.Find("BottomRightOfLeftGO").transform;
		} else if (this.name == "RightPlane") {
			upperLeft = GameObject.Find("UpperLeftOfRightGO").transform;
			bottomRight = GameObject.Find("BottomRightOfRightGO").transform;
		} else {
			print("no planes found..");
		}
		
		sphere_world_pos = new Vector3[5];
        sphere_screen_pos = new Vector3[5];
		
	}

	// Update is called once per frame
	void Update () {
		
		Vector3 UL_Pos = camera.WorldToScreenPoint(upperLeft.position);
		Vector3 BR_Pos = camera.WorldToScreenPoint(bottomRight.position);
		
		Vector3 diff;
		diff.x = UL_Pos.x - BR_Pos.x;
		diff.y = UL_Pos.y - BR_Pos.y;
		// Get fingerTips and Image to apply as texture on planes
		Coordinate[] fingertips = GetComponent<Plugin_Handler>().fingertips;
		capture_resolution = GetComponent<Plugin_Handler>().capture_resolution;
		
		for(int i=0; i<5; i++)
        {
            sphere_screen_pos[i].x = BR_Pos.x + (diff.x * fingertips[i].x / capture_resolution.width);;
            sphere_screen_pos[i].y = BR_Pos.y + (diff.y * fingertips[i].y / capture_resolution.height);
            sphere_screen_pos[i].z = UL_Pos.z - 5; // subtraction only added for testing

            try
            {
                // the place where the sphere will be positioned
                sphere_world_pos[i] = camera.ScreenToWorldPoint(sphere_screen_pos[i]);
            }
            catch (UnityException e)
            {
                print("cannot make world point from screen point " + e);
            }
        }
	}


}
