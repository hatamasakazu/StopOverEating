using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectShaker : MonoBehaviour {


	//Hpscore取得
	HP_score HPscript;
	GameObject HP;
	//Calscore取得
	Cal_score calscript;
	GameObject cal;

	public GameObject camera;
	public double alart = 45;
	public float count = 1f;

	void Start () {

		HP = GameObject.FindWithTag("HPBar");
		HPscript = HP.GetComponent<HP_score> ();

		cal = GameObject.FindWithTag ("ScoreGUI");
		calscript = cal.GetComponent<Cal_score> ();



	}

	//カメラを揺らす
	public void Shack(GameObject shakeObj){
		iTween.ShakePosition (shakeObj, iTween.Hash ("x", 0.3f, "y", 0.3f, "time", 1.5f));

	}

	//カメラを回転させる
	public void Rotate(GameObject target){
		iTween.RotateBy (target,iTween.Hash("x", 0.1f, "y", 0.1f, "time", 0.5f));

	}

	//テキストしか使えない
	public void Color(GameObject target){
		iTween.ColorTo (target, iTween.Hash ("r",255,"g",0,"b",0,"time",0.5f));
	}

	void Update(){



		double a = HPscript.current_HP_disp;
		double b = HPscript.Max_HP_disp;

		int c = calscript.calscore;



		if (Time.timeSinceLevelLoad > 5) {


			//Debug.Log (a);

			if (a < alart) {

			//	Debug.Log ("colorxhange");
				Shack (camera);

			}
			else if(a >= alart){
			//	Debug.Log ("大丈夫");
			}

		}
	
		//Rotate(camera);
		//Color(camera);


	}
}
