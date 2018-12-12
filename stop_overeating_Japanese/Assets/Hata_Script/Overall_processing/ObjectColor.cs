using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//背景のcolorを変えるScript

public class ObjectColor : MonoBehaviour {

	GameObject target;
	//public float red = 255f;
	//public float green = 0f;
	//public float blue = 0f;
	//public float alpha = 255f;

	public double alart = 45;
	public float count = 1f;

	private float time;
	private float speed = 1.0f;
	//Hpscore取得
	HP_score HPscript;
	GameObject HP;
	//Calscore取得
	Cal_score calscript;
	GameObject cal;

	//public Sprite sprite;


	// Use this for initialization
	void Start () {

		HP = GameObject.FindWithTag("HPBar");
		HPscript = HP.GetComponent<HP_score> ();

		cal = GameObject.FindWithTag ("ScoreGUI");
		calscript = cal.GetComponent<Cal_score> ();

		target = GameObject.FindWithTag ("GroundRed");


	}

	void Update(){


		double a = HPscript.current_HP_disp;
		double b = HPscript.Max_HP_disp;

		int c = calscript.calscore;



		if (Time.timeSinceLevelLoad > 5) {


			//Debug.Log (a);

			if (a < alart) {

				//Debug.Log ("colorxhange");

				colorto ();

			}
			else if(a >= alart){
				//Debug.Log ("大丈夫");
				colorfrom ();
			}

		}
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
