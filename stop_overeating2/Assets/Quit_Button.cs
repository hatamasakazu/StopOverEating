using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit_Button : MonoBehaviour {

	//キーの管理が可能

	private string hightscoreKey;
	private string totalscoreKey;

	private string highcalscoreKey;

	void Start(){
		hightscoreKey = Score_stage.getA();
		totalscoreKey = Score_stage.getB ();
		highcalscoreKey = Cal_score.getCal ();
	}


	public void OnClick(){

		PlayerPrefs.DeleteKey (hightscoreKey);
		Debug.Log ("Quit");
		Application.Quit();	
	}
}
