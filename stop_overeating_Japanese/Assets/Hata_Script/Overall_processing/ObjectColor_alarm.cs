using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//背景のcolorを変えるScript

public class ObjectColor_alarm : MonoBehaviour {

	GameObject target;
	//public float red = 255f;
	//public float green = 0f;
	//public float blue = 0f;
	//public float alpha = 255f;


	private float time;
	private float speed = 1.0f;


	//public Sprite sprite;


	// Use this for initialization
	void Start () {


		target = GameObject.FindWithTag ("GroundRed");


	}

	void Update(){

		colorto ();
	}

	void colorfrom(){
		target.GetComponent<Image> ().color = new Color(255f,0f,0f,0f);
	}

	void colorto(){
		target.GetComponent<Image> ().color = GetAlphaColor (target.GetComponent<Image> ().color);	
	}

	Color GetAlphaColor(Color color){
		time += Time.deltaTime * 5.0f * speed;
		color.a = Mathf.Sin (time) * 0.5f + 0.5f;

		return color;

	}
}
