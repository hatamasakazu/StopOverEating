using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load_disp : MonoBehaviour {

	public GameObject Load;

	public int a;
	public int b;
	private int c;
	public int d;
	bool flag = false;
	// Use this for initialization
	void Start () {
		Load.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {


		if (Time.timeSinceLevelLoad > a) {
			flag = true;
		}

		if(flag){
			Load.SetActive (true);
			c = a;
			a = a + b;
			flag = false;
		}

		if (Time.timeSinceLevelLoad > (c + d)) {
			Load.SetActive (false);
		}

	}
}
