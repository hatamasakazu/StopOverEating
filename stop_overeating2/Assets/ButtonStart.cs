using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonStart : MonoBehaviour {


	public GameObject StartScreen;


	public void OnClick(){
		StartScreen.SetActive (false);
	}
}
