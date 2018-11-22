using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectShaker_alarm : MonoBehaviour {




	public GameObject camera2;
	public double alart = 45;
	public float count = 1f;



	void Update(){
		if (Time.timeSinceLevelLoad > 2) {
			Debug.Log ("きれ");
			Shack (camera2);
			}
	}

	//カメラを揺らす
	public void Shack(GameObject shakeObj){
		Debug.Log ("きてる");
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


}
