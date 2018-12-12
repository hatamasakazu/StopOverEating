using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
 
public class InputManager1 : MonoBehaviour {
 
    InputField inputField;
	public int flag_input_ave = 1; 
	private int a = 0;
	bool flag= true;

	public static float Calibration_time;

	public GameObject Input;
	//public GameObject Input2;
	public GameObject Button;

	InputManager input_script;

	public static float Calibration_interval_time;
   
	void Start() { 

		inputField = GetComponent<InputField>();

		input_script = Input.GetComponent<InputManager>();

		//InitInputField();
	}


	void Update(){
		if (flag) {
			if (input_script.flag_input1 == 0) {
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

		Calibration_interval_time = (int)a;

		//Button.SetActive (true);

		Debug.Log(inputValue);

		flag_input_ave = 0;
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