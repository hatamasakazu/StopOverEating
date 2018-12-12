using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_record_time : MonoBehaviour {

	public static float input_record_time;

	
	// Update is called once per frame
	void Update () {

		input_record_time = Time.timeSinceLevelLoad;
		//Debug.Log ("aa"+input_record_time);

	}
}
