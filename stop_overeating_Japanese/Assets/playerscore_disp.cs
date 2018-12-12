using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerscore_disp : MonoBehaviour {


	public TMP_Text playerHPtext;
	public TMP_Text playerCaltext;




	// Use this for initialization
	void Start () {
		playerHPtext = GameObject.FindWithTag ("playerHPbarscore").GetComponent<TextMeshProUGUI> ();
		playerCaltext = GameObject.FindWithTag ("playerCalscore").GetComponent<TextMeshProUGUI> ();
	
		//最初にtextを空白にする
		playerHPtext.text = " ";
		playerCaltext.text = " ";
	}

	public void changePlayerText(int Score,float HPscore,int Calscore){
		
		playerHPtext.text = HPscore.ToString();
		playerCaltext.text = Calscore.ToString ();
	}

}
