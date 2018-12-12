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
    /// Start���\�b�h
    /// InputField�R���|�[�l���g�̎擾����я��������\�b�h�̎��s
    /// </summary>
    void Start() {
 
        inputField = GetComponent<InputField>();
 
        InitInputField();

		Button.SetActive (false);
    }
 
 
 
    /// <summary>
    /// Log�o�͗p���\�b�h
    /// ���͒l���擾����Log�ɏo�͂��A������
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
    /// InputField�̏������p���\�b�h
    /// ���͒l�����Z�b�g���āA�t�B�[���h�Ƀt�H�[�J�X����
    /// </summary>
 
 
    void InitInputField() {
 
        // �l�����Z�b�g
        inputField.text = "";
 
        // �t�H�[�J�X
        inputField.ActivateInputField();
    }
 

}