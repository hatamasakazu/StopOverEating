using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class InputManager3 : MonoBehaviour {

	InputField inputField;

	bool flag = true;
	public int flag_input3 = 1; 
	public GameObject Input2;
	//public GameObject Button;



	InputManager2 Input2_script;

	public static float Game_finish_time;


	private int a = 0;

	/// <summary>
	/// Start
	/// 
	/// </summary>
	void Start() { 

		inputField = GetComponent<InputField>();


		Input2_script = Input2.GetComponent<InputManager2> ();

		//InitInputField();
	}


	void Update(){
		if (flag) {
			if (Input2_script.flag_input2 == 0) {
				InitInputField ();
				flag = false;
			}
		}
	}

	/// <summary>
	/// Log
	/// 
	/// </summary>


	public void InputLogger() {

		string inputValue = inputField.text;

		Int32.TryParse (inputValue, out a);

		Game_finish_time = (int)a;

		//Button.SetActive (true);

		Debug.Log(Game_finish_time);
		flag_input3 = 0;

		//InitInputField();
	}



	/// <summary>
	/// InputField
	/// 初期化
	/// </summary>


	void InitInputField() {

		// 抣傪儕僙僢僩
		inputField.text = "";

		// 僼僅乕僇僗
		inputField.ActivateInputField();
	}


}