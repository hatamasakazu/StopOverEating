using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class InputManager4 : MonoBehaviour {

	InputField inputField;

	bool flag = true;

	public GameObject Input3;
	public GameObject Button;



	InputManager3 Input3_script;

	public static float Stage_change_time;


	private int a = 0;

	/// <summary>
	/// Start
	/// 
	/// </summary>
	void Start() { 

		inputField = GetComponent<InputField>();


		Input3_script = Input3.GetComponent<InputManager3> ();

		//InitInputField();
	}


	void Update(){
		if (flag) {
			if (Input3_script.flag_input3 == 0) {
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

		Stage_change_time = (int)a;

		Button.SetActive (true);

		Debug.Log(Stage_change_time);

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