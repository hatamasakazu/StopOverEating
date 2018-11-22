using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish_process : MonoBehaviour {

	/*ゲーム画面に追加することでシーンを変えるこのできる時間,終了時間を定めることが可能なスクリプト*/


	public string nextSceneName;

	//Finish time
	//ここを変えるとステージの時間を変える時間を指定できる
	private float change_time = 120.0f;
	//private float fin_time = 300f;
	//public int save_time = 0;
	//Fin_key
	public static string fin_key;

	//Hpscore取得
	HP_score Fin_HPscript;
	GameObject Fin_HP;
	//Calscore取得
	Cal_score calscript;
	GameObject cal;


	public int finish_cal=2000;

	public static string[] nextScene_randam = new string[]{"GAME2","GAME3","GAME4","GAME5"};
	public static string[] nextSceneArrange = new string[5];
	public static int change_i = 0;
	//クリアなら1　失敗なら0
	public static int clear_or_bad_flag = 0;




	void Start(){
		Fin_HP = GameObject.FindWithTag("HPBar");
		Fin_HPscript = Fin_HP.GetComponent<HP_score> ();

		cal = GameObject.FindWithTag ("ScoreGUI");
		calscript = cal.GetComponent<Cal_score> ();
		//Debug.Log (calscript.calscore);
		change_time = InputManager4.Stage_change_time;

		change_time = 10f;

		Debug.Log ("stagetime" + change_time);



		if (change_i == 0) {
			randomarray_scene ();
		}

	}

	//シャッフル
	void randomarray_scene(){

		System.Random rng = new System.Random ();
		int n = nextScene_randam.Length;

		while (n > 1) {
			n--;
			var k = rng.Next (n + 1);
			var tmp = nextScene_randam [k];
			nextScene_randam [k] = nextScene_randam [n];
			nextScene_randam [n] = tmp;
		}

		for (int i = 0; i < 4; i++) {
			nextSceneArrange [i] = nextScene_randam [i];
			Debug.Log (nextSceneArrange [i]);

		}
		nextSceneArrange[4] = "FinishMenu";




	}


	void changeNext(){
		if (Time.timeSinceLevelLoad > change_time) {
			//Debug.Log (Time.timeSinceLevelLoad+"現在の時刻");
			//PlayerPrefs.SetInt (fin_key, (int)Time.timeSinceLevelLoad);
			clear_or_bad_flag = 0;
			SceneManager.LoadScene("FeedBackGame");
			//SceneManager.LoadScene (nextSceneArrange[change_i], LoadSceneMode.Single);
			//change_i = change_i + 1;
		}
	}

	// Update is called once per frame
	void Update () {
		changeNext ();

		//quittime ();
		HPover_less ();

		//all finish process
		ALLfinish();

		//Debug.Log ("unscaledTime2 " + Time.unscaledTime);
	}

	public static string get_time(){
		return fin_key;
	}

	//これを入れないと勝手に画面がかわるので注意
	private float finish_bar = 3.0f;

	//HP is over or less process
	/*HPが減ったり増えたりした時の処理*/
	/*100を変えたら終了また、0を切っても終了する*/
	/*上記に加えcalscoreが既定の範囲内に入っていいないと終了しない*/
	public void HPover_less(){
		double a = Fin_HPscript.current_HP_disp;
		double b = Fin_HPscript.Max_HP_disp;

		int c = calscript.calscore;


		if (Time.timeSinceLevelLoad > 5) {


			//Debug.Log (c);

			if (c >= finish_cal && c <= (finish_cal+500)) {

				Debug.Log ("侵入");
			//	Debug.Log (c);

				if (a > b) {
					Debug.Log ("c" + c);
					clear_or_bad_flag = 1;
					SceneManager.LoadScene("FeedBackGame");

					//SceneManager.LoadScene (nextSceneArrange[change_i], LoadSceneMode.Single);
					//change_i = change_i + 1;
				}
				if (Time.timeSinceLevelLoad > finish_bar) {
					if (a <= 0) {
						clear_or_bad_flag = 0;
						SceneManager.LoadScene("FeedBackGame");
						//SceneManager.LoadScene (nextSceneArrange[change_i], LoadSceneMode.Single);
						//change_i = change_i + 1;
					}
				}
			}
		}

	}

	//ALL_finish process

	public void ALLfinish(){
		
		float unscal = menu_time.ALLfinish();
		float finishtime = menu_time.finishtime;
		Debug.Log ("finishtime" + finishtime);
		if (unscal >= finishtime) {
			Debug.Log("終わり");
			SceneManager.LoadScene ("FinishMenu");
			//Application.Quit();
		}


	}




}
