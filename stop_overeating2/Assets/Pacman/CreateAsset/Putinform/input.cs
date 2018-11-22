using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class input : MonoBehaviour {






	string col;


	bool One;

	InputField inputField;



	//Pause画面で使う関数

	private GameObject button1;
	private GameObject button2;
	public GameObject Input;



	//現在のcalscoreを記録する



	// Use this for initialization
	void Start () {


		inputField = GetComponent<InputField> ();
		InitInputField ();


	}

	//タイピングする機能
	private int index;

	public void InputLogger(){
		string inputValue = inputField.text;
		Debug.Log ("UIT.text" + inputValue);
	 


	}
	void InitInputField(){
		inputField.text = "";
		inputField.ActivateInputField ();

	}





}
