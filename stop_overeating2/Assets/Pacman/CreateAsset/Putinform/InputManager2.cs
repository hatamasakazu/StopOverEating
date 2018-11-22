using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class InputManager2 : MonoBehaviour {

	InputField inputField;

	bool flag = true;
	public int flag_input2 = 1; 

	public GameObject Input1;

	//public GameObject Button;


	InputManager1 Input1_script;

	public static float Eating_finish_time;

	private int a = 0;

	/// <summary>
	/// Start
	/// 
	/// </summary>
	void Start() { 

		inputField = GetComponent<InputField>();

		Input1_script = Input1.GetComponent<InputManager1>();

		//InitInputField();
	}


	void Update(){
		if (flag) {
			if (Input1_script.flag_input_ave == 0) {
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

		Eating_finish_time = (int)a;

		//Button.SetActive (true);

		Debug.Log(inputValue);

		flag_input2 = 0;
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