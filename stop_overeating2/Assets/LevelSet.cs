using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSet : MonoBehaviour {

	public static int Level_flag;

	public static float hp_bar_level = -0.05f; 


	// Use this for initialization
	void Start () {
		Level_flag = PlayerPrefs.GetInt ("Levelflag");
		Debug.Log (Level_flag);
		Level_Set ();
	}


	//Hp-barを減らすことのできる関数
	public void Level_Set(){
		switch (Level_flag) {
		case 0:
			hp_bar_level = -0.05f;
			break;
		case 1:
			hp_bar_level = -0.01f;
			break;
		case 2:
			hp_bar_level = -0.05f;
			break;
		case 3:
			hp_bar_level = -0.1f;
			break;
		case 4:
			hp_bar_level = -0.02f;
			break;
		case 5:
			hp_bar_level = -0.02f;
			break;
		}

	}
	


}
