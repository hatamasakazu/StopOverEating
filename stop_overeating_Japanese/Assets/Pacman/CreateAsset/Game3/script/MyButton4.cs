using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


/*このスクリプトではScreen1において正解した場合に開かれる*/
/*Screen3に設定すること*/

public class MyButton4 : MonoBehaviour {


	Pausable pause_script;
	GameObject pause;


	//foodのpathを取得し、deleteに使う
	public static string food = "foodpath";
	private string foodpath;

	private int calscore;

	public int foodcurent;



	void Start(){
		
		pause = GameObject.FindWithTag ("pausemanager");
		pause_script = pause.GetComponent<Pausable> ();


	}

	public void OnClickA(){

		/*正解したらpasemenu3に移動し不正解ならpasemenu2に移動*/
		calscore = PlayerPrefs.GetInt ("Currentcalkey",0);
		Debug.Log ("calscore"+calscore);
		Debug.Log ("foodcurent"+foodcurent);
		if (foodcurent == calscore) {
			pause_script.pausmenu0.SetActive (false);
			//pause_script.pausmenu.SetActive (false);
			pause_script.pausmenu2.SetActive (false);
			pause_script.pausmenu3.SetActive (true);
			pause_script.pausmenu4.SetActive (false);
		} else if (foodcurent != calscore) {
			pause_script.pausmenu0.SetActive (false);
			//pause_script.pausmenu.SetActive (false);
			pause_script.pausmenu2.SetActive (true);
			pause_script.pausmenu3.SetActive (false);
			pause_script.pausmenu4.SetActive (false);
		}

	}



}
