using UnityEngine;
using System.Collections;

/**
 * This script is for testing purposes only. It is to be
 * placed on an object which will be positioned at a fingertip
 * location.
 */
public class Finger_Tip_Sphere : MonoBehaviour {
	
	Vector3[] tmp_left;
	Vector3[] tmp_right;
	
	void Update ()
    {
		if (this.name == "LeftFinger")
		{
			tmp_left = GameObject.Find("LeftPlane").GetComponent<Coord_Handler>().world_pos;
			
			transform.position = tmp_left[0];
        	renderer.material.color = Color.blue;
		}
		else if (this.name == "RightFinger")
		{
			tmp_right = GameObject.Find("RightPlane").GetComponent<Coord_Handler>().world_pos;
			
			transform.position = tmp_right[0];
        	renderer.material.color = Color.blue;
		}
        
	}
}
