using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PackStage2 : MonoBehaviour {

	/*このスクリプトではscore、HPbarの管理をしている*/
	/*このスクリプトを変化させることでHPが減る量、またスコアをいじることができる*/
	/*新しいキャラクターを使う場合,OnTriggerEnter2D内にco.tag == "name"*を追加する*/

	GameObject singlebar;

	SimpleHealthBar health;

	playerscore_disp playerText;

	float HP = 0;

	int a = 0;





	/*foodのスコアの全てが書かれている*/
	public int score_all;
	public int cal_all;
	public float hp_all;

	/*Hpを変化させるために必要な変数*/
	public float ch_hp = 0.02f;


	/*ここを変えることでHPBarが減る時間を管理可能*/
	//1秒ごとに減っていく

	private float time_hpbar = (-0.02f)/(1000);
	//時間間隔
	public float interval = 1f;


	//playerのtext
	public TMP_Text playerHPtext;
	public TMP_Text playerCaltext;
	public TMP_Text playerScoretext;


	//deleteするかどうかを決める
	Cal_score calscript;
	GameObject cal;
	//pauseメニューを出す
	Pausable pause_script;
	GameObject pause;

	//pausemenu経由で消すかどうかのフラグ

	public static GameObject foodname;
	public string path_parent = "maze3";
	public string foodpath;

	//good food か bad food かを見極める
	//good food is 0 and bad food is 1
	public int goodorbad_flag;

	//現在のcalscoreを記録する
	//public static string currentcalkey = "Currentcalkey";
	int currentcalscore=0;
	//ここでfood番号を決めるしかし使わないかもしれない
	public int a_flag=0;

	private int f;


	//サウンドを追加する
	public AudioClip sound;
	AudioManager audiomanager_script;
	GameObject audio_ga;



	public int flagwarp = 0;

	void Start(){

		PlayerPrefs.SetString ("scriptname", this.name);
		PlayerPrefs.Save ();

		singlebar = GameObject.FindWithTag ("SingleBar");

		health = singlebar.GetComponent<SimpleHealthBar> ();

		playerText = GetComponent<playerscore_disp> ();


		HP = health.GetCurrentFraction;

		StartCoroutine (loop ());

		//playerのtext設定
		playerHPtext = GameObject.FindWithTag ("playerHPbarscore").GetComponent<TextMeshProUGUI> ();
		playerCaltext = GameObject.FindWithTag ("playerCalscore").GetComponent<TextMeshProUGUI> ();
		playerScoretext = GameObject.FindWithTag ("playerScore").GetComponent<TextMeshProUGUI> ();

		//最初にtextを空白にする
		playerHPtext.text = " ";
		playerCaltext.text = " ";
		playerScoretext.text = " ";

		//calscoreを取得する
		cal = GameObject.FindWithTag ("ScoreGUI");
		calscript = cal.GetComponent<Cal_score> ();

		//pauseメニュ-ーを表示する
		pause = GameObject.FindWithTag ("pausemanager");
		pause_script = pause.GetComponent<Pausable> ();

		//soundを追加
		audio_ga = GameObject.FindWithTag("audiomanager");
		audiomanager_script = audio_ga.GetComponent<AudioManager> ();

	}



	//TakaDamage is to change HPber
	//ここでsocreを加算している

	void OnTriggerEnter2D(Collider2D co){
		if (Time.timeSinceLevelLoad > 1) {
			currentcalscore = cal_all;
			alldatarecord (score_all, cal_all, hp_all, goodorbad_flag, a_flag);
			audiomanager_script.PlayClip (sound);


			if ((calscript.calscore + currentcalscore) <= 2500) {
				//Debug.Log ("大事1"+(calscript.calscore+currentcalscore));
				foods (co, 1, 1);
			} else if ((calscript.calscore + currentcalscore) > 2500) {
				//Debug.Log ("大事2"+(calscript.calscore+currentcalscore));
				foods (co, 2, 2);
			}


		}
			

	}

	void alldatarecord(int score, int cal, float bar,int flag,int damage_flag){

		PlayerPrefs.SetInt ("Currntscore", score);
		PlayerPrefs.SetInt ("Currentcalkey", cal);
		PlayerPrefs.SetFloat ("Currentbar", bar);
		string path_children = gameObject.name;
		foodpath = path_parent + "/" + path_children;
		PlayerPrefs.SetString ("foodpath", foodpath);

		//good food or bad food 
		PlayerPrefs.SetInt("goodorbad_flag",flag);
		PlayerPrefs.SetInt ("damage_flag", damage_flag);
		PlayerPrefs.SetFloat ("ch_hp", ch_hp);
		PlayerPrefs.Save ();

	}

	void deadoralive(Collider2D co){
		Debug.Log ("消すための操作");
		//一度時間を止めて特別な画面を表示したい

		pause_script.pausing = true;


	}

	void warpflag(Transform waypoint, int cur, float speed){
		
	}

	void foods(Collider2D co,int flag1, int flag2){

		if (co.tag == "pacman" || co.tag == "Soldier" || co.tag == "Death") {

			Debug.Log (co.name);
			if (flag2 == 1) {
				Destroy (gameObject);
				f = 0;
			} else if (flag2 == 2) {
				Debug.Log ("消すか悩む");


				deadoralive (co);

			}


			/*食べ物を追加する場合ここに追加*/
			if (flag1 == 1) {
				if (f == 0) {

					//scoreを上げる
					FindObjectOfType<Score_stage> ().AddPoint (score_all);

					//calscore add
					FindObjectOfType<Cal_score> ().AddCalPoint (cal_all);

					//Change HP bar
					TakeDamage (a_flag, hp_all * ch_hp);
					//Debug.Log ("Hit");

					//playerの上についているtextを変える
					changePlayerText (hp_all, cal_all, score_all);

				}
			}
		}
	}

	//1秒ごとに減っていく関数
	public IEnumerator loop(){

		while (true) {

			yield return new WaitForSeconds (interval);
			TakeDamage(0,time_hpbar);
		}
	}

	private void onTimer(){
		Debug.Log ("on timer");
	}

	//HPbar change

	public void TakeDamage ( int a_flag, float damage )
	{
		//Debug.Log (HP);

		health.UpdateBar (damage,HP);
		Debug.Log ("a_flag" + a_flag);
		if (a_flag > 0) {
			FindObjectOfType<Food_Count2> ().FoodCount(a_flag);
		}


		// <------- This is where you will want to update the Simple Health Bar. Only AFTER the value has been modified.
	}

	public void changePlayerText(float HPscore,int Calscore,int Score){

		playerHPtext.text = HPscore.ToString();
		playerCaltext.text = Calscore.ToString ();
		playerScoretext.text = Score.ToString ();

	}

}
