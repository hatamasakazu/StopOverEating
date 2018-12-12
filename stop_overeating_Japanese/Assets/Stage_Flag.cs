using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage_Flag : MonoBehaviour {

	public static int stage_number;

	// Use this for initialization
	void Start () {
		if (SceneManager.GetActiveScene().name == "GAME") {
			stage_number = 1;
		} else if (SceneManager.GetActiveScene().name == "GAME2") {
			stage_number = 2;
		}else if (SceneManager.GetActiveScene().name == "GAME3") {
			stage_number = 3;
		}else if (SceneManager.GetActiveScene().name == "GAME4") {
			stage_number = 4;
		}else if (SceneManager.GetActiveScene().name == "GAME5") {
			stage_number = 5;
		}

	}
	

}
