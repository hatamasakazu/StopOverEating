using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HP_score : MonoBehaviour {


	/*score系の加算は Pacdot スクリプトで記述*/


	// show the HP_score

	//TMP_Text is to use TextMeshPro
	TMP_Text HPscore;


	GameObject HPscript;
	private float maxHP;
	private float currentHP;


	SimpleHealthBar SHscript;


	// Use this for initialization
	void Start () {
		HPscore = GameObject.FindWithTag ("HP_score").GetComponent<TextMeshProUGUI> ();

		HPscript = GameObject.FindWithTag("SingleBar");
		SHscript = HPscript.GetComponent<SimpleHealthBar> ();



	}

	//この値を取れば現在のHPが分かる
	public double current_HP_disp = 0;
	public double Max_HP_disp = 0;
	// Update is called once per frame
	void Update () {

		if(SHscript.ii >= 1){
			current_HP_disp = System.Math.Round(((SHscript.targetFill) * 100));
			Max_HP_disp= System.Math.Round(((SHscript._maxValue) * 50));
			HPscore.text = current_HP_disp + " / " + Max_HP_disp;

		//Debug.Log ("練習" + SHscript.targetFill);
		}
		//HPscore.text = currentHP.ToString () + "" +  maxHP.ToString;
	}
}
