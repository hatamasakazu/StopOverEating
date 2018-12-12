using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
 
public class InputManager : MonoBehaviour {
 
    InputField inputField;
	public int flag_input1 = 1; 
	private int a = 0;

	public static float Calibration_time;

	//public GameObject Input1;
	//public GameObject Input2;
	public GameObject Button;

 
    /// <summary>
    /// Startメソッド
    /// InputFieldコンポーネントの取得および初期化メソッドの実行
    /// </summary>
    void Start() {
 
        inputField = GetComponent<InputField>();
 
        InitInputField();

		Button.SetActive (false);
    }
 
 
 
    /// <summary>
    /// Log出力用メソッド
    /// 入力値を取得してLogに出力し、初期化
    /// </summary>
 
 
    public void InputLogger() {
 
        string inputValue = inputField.text;
		Int32.TryParse (inputValue, out a);

		Calibration_time = (int)a;

        Debug.Log(inputValue);
		Debug.Log (Calibration_time);
		flag_input1 = 0;
		//inputField.text = "";
        //InitInputField();
    }
 
 
 
    /// <summary>
    /// InputFieldの初期化用メソッド
    /// 入力値をリセットして、フィールドにフォーカスする
    /// </summary>
 
 
    void InitInputField() {
 
        // 値をリセット
        inputField.text = "";
 
        // フォーカス
        inputField.ActivateInputField();
    }
 

}