using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disp_Game : MonoBehaviour {

	private int st_flag;

	public GameObject game1;
	public GameObject game2;
	public GameObject game3;
	public GameObject game4;
	public GameObject game5;

	// Use this for initialization
	void Start () {
		st_flag = Stage_Flag.stage_number;

		if (st_flag == 1) {
			game1.SetActive (true);
			game2.SetActive (false);
			game3.SetActive (false);
			game4.SetActive (false);
			game5.SetActive (false);
		} else if (st_flag == 2) {
			game1.SetActive (false);
			game2.SetActive (true);
			game3.SetActive (false);
			game4.SetActive (false);
			game5.SetActive (false);
		} else if (st_flag == 3) {
			game1.SetActive (false);
			game2.SetActive (false);
			game3.SetActive (true);
			game4.SetActive (false);
			game5.SetActive (false);
		} else if (st_flag == 4) {
			game1.SetActive (false);
			game2.SetActive (false);
			game3.SetActive (false);
			game4.SetActive (true);
			game5.SetActive (false);
		} else if (st_flag == 5) {
			game1.SetActive (false);
			game2.SetActive (false);
			game3.SetActive (false);
			game4.SetActive (false);
			game5.SetActive (true);
		}

	}
	

}
