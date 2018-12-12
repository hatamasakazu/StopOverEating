using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class TypingSoft : MonoBehaviour {






	string col;


	bool One;

	InputField inputField;



	//Pause画面で使う関数
	Pausable pause_script;
	GameObject pause;
	private GameObject button1;
	private GameObject button2;
	public GameObject Input;



	//現在のcalscoreを記録する
	public static string currentcalkey = "Currentcalkey";


	// Use this for initialization
	void Start () {
		


		pause = GameObject.FindWithTag ("pausemanager");
		pause_script = pause.GetComponent<Pausable> ();




		inputField = GetComponent<InputField> ();
		InitInputField ();


	}
		
	//タイピングする機能
	private int index;

	public void InputLogger(){
		string inputValue = inputField.text;
		Debug.Log ("UIT.text" + inputValue);
		int a = PlayerPrefs.GetInt(currentcalkey); 
		Debug.Log ("currentcalkey " + a);
		if (inputValue == a.ToString ()) {
			Correct ();
		} else {
			Mistake ();
		}


	}
	void InitInputField(){
		inputField.text = "";
		inputField.ActivateInputField ();

	}

	void Correct(){
		Debug.Log ("正解");
	
		inputField.text = "";
		/*正解されたときpausememu3を表示させる*/

		pause_script.pausmenu0.SetActive(false);
		pause_script.pausmenu.SetActive (false);
		pause_script.pausmenu2.SetActive (false);
		pause_script.pausmenu3.SetActive (true);




	}
	void Mistake(){
		Debug.Log ("失敗");
	
		inputField.text = "";
		/*不正解のときpausememu2を表示させる*/
		pause_script.pausmenu0.SetActive(false);
		pause_script.pausmenu.SetActive (false);
		pause_script.pausmenu2.SetActive (true);
		pause_script.pausmenu3.SetActive (false);
	}
	void CorrectAnswrRate(){
		Debug.Log ("正解率計算");
	}
		

}
