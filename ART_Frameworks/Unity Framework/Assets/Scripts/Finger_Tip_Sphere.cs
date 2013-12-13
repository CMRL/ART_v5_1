using UnityEngine;
using System.Collections;
// Gets finger tips positions from Sort_space ( wihch had these positions from Plugin_Handler)
// Transforms fingerprints to neew location in different colors
public class Finger_Tip_Sphere : MonoBehaviour {
	
	Vector3[] tmp_left;
	Vector3[] tmp_right;
	
	void Update ()
    {
		if (this.name == "Left_Sphere")
		{
			Vector3[] tmp_left = GameObject.Find("LeftPlane").GetComponent<Sort_Space>().sphere_world_pos;
			
			transform.position = tmp_left[0];
        	renderer.material.color = Color.green;
		}
		else if (this.name == "Right_Sphere")
		{
			Vector3[] tmp_right = GameObject.Find("RightPlane").GetComponent<Sort_Space>().sphere_world_pos;
			
			transform.position = tmp_right[0];
        	renderer.material.color = Color.blue;
		}
        
	}
}
