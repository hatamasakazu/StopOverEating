using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSound : MonoBehaviour {

	//musicを再生し注意をうながす
	//soundを管理するスクリプト

	//Hpscore取得
	HP_score HPscript;
	GameObject HP;
	//Calscore取得
	Cal_score calscript;
	GameObject cal;


	/*music一覧*/
	private AudioSource backgroundmusic;
	private AudioSource sound01;
	private AudioSource sound02;
	private AudioSource sound03;


	//need変数
	/*この時間を変えることでalarmのタイミングを変えることが可能*/
	public double background_start = 50;
	public double alart = 45;
	public double alart2 = 40;
	public double alart3 = 30;
	public float count = 1f;

	bool bg;
	bool One;
	bool One2;
	bool One3;
	//bool One2;

	public int flag = 1;
	public bool flagbool;


	// Use this for initialization
	void Start () {
		HP = GameObject.FindWithTag("HPBar");
		HPscript = HP.GetComponent<HP_score> ();

		cal = GameObject.FindWithTag ("ScoreGUI");
		calscript = cal.GetComponent<Cal_score> ();
		AudioSource[] audioSources = GetComponents<AudioSource> ();
		backgroundmusic = audioSources [0];
		sound01 = audioSources [1];
		sound02 = audioSources [2];
		sound03 = audioSources [3];


		One = true;
		//One2 = true;
		//One3 = true;


	}


	// Update is called once per frame
	void Update () {
		double a = HPscript.current_HP_disp;
		double b = HPscript.Max_HP_disp;

		int c = calscript.calscore;
		if (flag == 1) {

			if (bg) {
				if (a > alart) {
					backgroundstart ();

					bg = false;
				} else if (a < alart) {
					backgroundpause ();
				}
			} else if (a < alart) {
				bg = true;
			}


			if (Time.timeSinceLevelLoad > 2) {


				//Debug.Log (a);

				/*一度だけ処理を行う*/
				if (One) {
					if (a < alart) {
					
						musicstart ();

						One = false;
					} else if (a >= alart || a <= alart2) {

						musicstop ();
					} 

				} else if (a >= alart || a <= alart2) {
				
					One = true;
				}

				if (One2) {
					if (a <= alart2 && alart3 <= a) {
						musicstart2 ();
						One2 = false;
					} else if (a > alart2 || a < alart3) {
						musicstop2 ();
					}
				} else if (a > alart2 || a < alart3) {
					One2 = true;
				}

				if (One3) {
					if (a < alart3) {
						musicstart3 ();
						One3 = false;
					} else if (a >= alart3) {
						musicstop3 ();
					}
				} else if (a >= alart3) {
					One3 = true;
				}
				
			}
		} else if (flag == 0) {
			if (flagbool) {
				if (a > alart) {
					backgroundstart ();
					flagbool = false;
					flag = 1;
				} else if (a >= alart2 && a <= alart) {
					musicstart ();
					flagbool = false;
					flag = 1;
				} else if (a >= alart3 && a <= alart2) {
					musicstart2 ();
					flagbool = false;
					flag = 1;
				} else if (a < alart3) {
					musicstart3 ();
					flagbool = false;
					flag = 1;

				}
			}

		}
	}

	public void backgroundstart(){
		backgroundmusic.Play ();
	}

	public void musicstart(){
		//musicsource = gameObject.GetComponent<AudioSource> ();

		sound01.Play ();

	}

	public void musicstart2(){
		sound02.Play ();
	}

	public void musicstart3(){
		sound03.Play ();
	}


	public void backgroundstop(){
		backgroundmusic.Stop();
	}

	public void musicstop(){
		
		sound01.Stop();

	}

	public void musicstop2(){
		sound02.Stop ();
	}

	public void musicstop3(){
		sound03.Stop ();
	}

	public void backgroundpause(){
		
		backgroundmusic.Pause();
	}

	public void musicpause(){

		sound01.Pause ();

	}

	public void musicpause2(){

		sound02.Pause();

	}

	public void musicpause3(){
		sound03.Pause ();
	}
}
