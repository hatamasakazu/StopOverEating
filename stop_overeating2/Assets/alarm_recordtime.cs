using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alarm_recordtime : MonoBehaviour {

	public static float alarm_record_time;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		alarm_record_time = Time.timeSinceLevelLoad;
		PlayerPrefs.SetFloat ("alarm_record_time",alarm_record_time);
		//Debug.Log ("alarm_time" + PlayerPrefs.GetFloat ("alarm_record_time"));

	}
}
