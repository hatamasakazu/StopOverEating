
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ren : MonoBehaviour {

	/*このスクリプトではscore、HPbarの管理をしている*/
	/*このスクリプトを変化させることでHPが減る量、またスコアをいじることができる*/
	/*新しいキャラクターを使う場合,OnTriggerEnter2D内にco.tag == "name"*を追加する*/

	GameObject singlebar;

	SimpleHealthBar health;

	playerscore_disp playerText;

	float HP = 0;

	int a = 0;





	/*新しく食べ物を追加した場合ここに追加すること*/
	//score
	public int apple_score;
	public int cake_score;
	public int chococorone_score;
	public int corn_score;
	public int frenchfries_score;
	public int hamburger_score;




	//calscore
	public int apple_cal_score;
	public int cake_cal_score;
	public int chococorone_cal_score;
	public int corn_cal_score;
	public int frenchfries_cal_score;
	public int hamburger_cal_score;



	//setup HPbar
	public float apple_hpbar;
	public float cake_hpbar;
	public float chococorone_hpbar;
	public float corn_hpbar;
	public float frenchfries_hpbar;
	public float hamburger_hpbar;
	/*Hpを変化させるために必要な変数*/
	private float ch_hp = 0.02f;


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
	public string path_parent = "maze";
	public string foodpath;

	//good food か bad food かを見極める

	public int goodorbad_flag;

	//現在のcalscoreを記録する
	//public static string currentcalkey = "Currentcalkey";
	int currentcalscore=0;
	int a_flag=0;

	private int f;


	//サウンドを追加する
	public AudioClip sound;
	AudioManager audiomanager_script;
	GameObject audio_ga;

	void Start(){


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


		if (gameObject.tag == "apple") {
			currentcalscore = apple_cal_score;
			a_flag = 0;
			alldatarecord (apple_score, apple_cal_score, apple_hpbar,goodorbad_flag,a_flag);
			audiomanager_script.PlayClip (sound);
		}
		else if (gameObject.tag == "cake") {
			currentcalscore = cake_cal_score;
			a_flag = 1;
			alldatarecord (cake_score, cake_cal_score, cake_hpbar, goodorbad_flag,a_flag);

		}
		else if (gameObject.tag == "chococorone") {
			currentcalscore = chococorone_cal_score;
			a_flag = 2;
			alldatarecord (chococorone_score, chococorone_cal_score, chococorone_hpbar, goodorbad_flag,a_flag);
			audiomanager_script.PlayClip (sound);
		}
		else if (gameObject.tag == "corn") {
			currentcalscore = corn_cal_score;
			a_flag = 3;
			alldatarecord (corn_score, corn_cal_score, corn_hpbar, goodorbad_flag,a_flag);
			audiomanager_script.PlayClip (sound);
		}
		else if (gameObject.tag == "frenchfries") {
			currentcalscore = frenchfries_cal_score;
			a_flag = 4;
			alldatarecord (frenchfries_score, frenchfries_cal_score, frenchfries_hpbar, goodorbad_flag,a_flag);
			audiomanager_script.PlayClip (sound);
		}
		else if (gameObject.tag == "hamburger") {
			currentcalscore = hamburger_cal_score;
			a_flag = 5;
			alldatarecord (hamburger_score, hamburger_cal_score, hamburger_hpbar, goodorbad_flag,a_flag);
			audiomanager_script.PlayClip (sound);
		}

		if ((calscript.calscore+currentcalscore) <= 2500) {
			//Debug.Log ("大事1"+(calscript.calscore+currentcalscore));
			foods (co, 1, 1);
		} else if ((calscript.calscore+currentcalscore) > 2500) {
			//Debug.Log ("大事2"+(calscript.calscore+currentcalscore));
			foods (co, 2, 2);
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
		//Debug.Log (foodpath);
	}

	void deadoralive(Collider2D co){
		Debug.Log ("消すための操作");
		//一度時間を止めて特別な画面を表示したい

		pause_script.pausing = true;


	}

	void foods(Collider2D co,int flag1, int flag2){

		if (co.tag == "pacman" || co.tag == "Soldier" || co.tag == "Death") {
			//Debug.Log (co.tag);

			//Debug.Log ("calscoreqqq" + calscript.calscore);
			Debug.Log (co.name);
			if (flag2 == 1) {
				Destroy (gameObject);
				f = 0;
			} else if (flag2 == 2) {
				Debug.Log ("消すか悩む");


				deadoralive (co);

			}

			//Debug.Log (gameObject.name);

			/*食べ物を追加する場合ここに追加*/
			if (flag1 == 1) {
				if (f == 0) {
					if (gameObject.tag == "apple") {

						//flag
						a = 0;
						//score add

						FindObjectOfType<Score_stage> ().AddPoint (apple_score);

						//calscore add
						FindObjectOfType<Cal_score> ().AddCalPoint (apple_cal_score);

						//Change HP bar
						TakeDamage (a, apple_hpbar * ch_hp);
						//Debug.Log ("Hit");

						//playerの上についているtextを変える
						changePlayerText (apple_hpbar, apple_cal_score, apple_score);



					}

					if (gameObject.tag == "cake") {
						a = 1;
						//score add

						FindObjectOfType<Score_stage> ().AddPoint (cake_score);

						//calscore add
						FindObjectOfType<Cal_score> ().AddCalPoint (cake_cal_score);

						//Change HP bar
						TakeDamage (a, cake_hpbar * ch_hp);
						//Debug.Log ("Hit2");

						//playerの上についているtextを変える
						changePlayerText (cake_hpbar, cake_cal_score, cake_score);
					}

					if (gameObject.tag == "chococorone") {

						//flag
						a = 2;
						//score add
						FindObjectOfType<Score_stage> ().AddPoint (chococorone_score);

						//calscore add
						FindObjectOfType<Cal_score> ().AddCalPoint (chococorone_cal_score);

						//Change HP bar
						TakeDamage (a, chococorone_hpbar * ch_hp);
						//Debug.Log ("Hit");

						//playerの上についているtextを変える
						changePlayerText (chococorone_hpbar, chococorone_cal_score, chococorone_score);



					}

					if (gameObject.tag == "corn") {
						a = 3;
						//score add

						FindObjectOfType<Score_stage> ().AddPoint (corn_score);

						//calscore add
						FindObjectOfType<Cal_score> ().AddCalPoint (corn_cal_score);

						//Change HP bar
						TakeDamage (a, corn_hpbar * ch_hp);
						//Debug.Log ("Hit2");

						//playerの上についているtextを変える
						changePlayerText (corn_hpbar, corn_cal_score, corn_score);
					}

					if (gameObject.tag == "frenchfries") {

						//flag
						a = 4;
						//score add

						FindObjectOfType<Score_stage> ().AddPoint (frenchfries_score);


						//calscore add
						FindObjectOfType<Cal_score> ().AddCalPoint (frenchfries_cal_score);

						//Change HP bar
						TakeDamage (a, frenchfries_hpbar * ch_hp);
						//Debug.Log ("Hit");

						//playerの上についているtextを変える
						changePlayerText (frenchfries_hpbar, frenchfries_cal_score, frenchfries_score);



					}

					if (gameObject.tag == "hamburger") {
						Debug.Log ("hamburger");

						a = 5;
						//score add
						FindObjectOfType<Score_stage> ().AddPoint (hamburger_score);

						//calscore add
						FindObjectOfType<Cal_score> ().AddCalPoint (hamburger_cal_score);

						//Change HP bar
						Debug.Log ("ham_hp" + hamburger_hpbar * ch_hp);
						TakeDamage (a, hamburger_hpbar * ch_hp);
						//Debug.Log ("Hit2");

						//playerの上についているtextを変える
						changePlayerText (hamburger_hpbar, hamburger_cal_score, hamburger_score);
					}

				}
			}
		}
	}


	//1秒ごとに減っていく関数
	public IEnumerator loop(){

		while (true) {

			yield return new WaitForSeconds (interval);
			TakeDamage(6,time_hpbar);
		}
	}

	private void onTimer(){
		Debug.Log ("on timer");
	}

	//HPbar change

	public void TakeDamage ( int a, float damage )
	{
		//Debug.Log (HP);

		switch (a) {
		case 0:
			//Debug.Log ("TK0");
			//Debug.Log ("damage" + damage);
			//Debug.Log ("HP" + HP);
			health.UpdateBar (damage,HP);
			//Debug.Log (HP);
			break;
		case 1:
			//Debug.Log ("TK1");
			health.UpdateBar (damage,HP);
			break;
		case 2:
			//Debug.Log ("TK1");
			health.UpdateBar (damage,HP);
			break;
		case 3:
			//Debug.Log ("TK1");
			health.UpdateBar (damage,HP);
			break;
		case 4:
			//Debug.Log ("TK1");
			health.UpdateBar (damage,HP);
			break;
		case 5:
			//Debug.Log ("TK1");
			health.UpdateBar (damage,HP);
			break;
		case 6:
			health.UpdateBar (damage, HP);
			break;
		}


		// <------- This is where you will want to update the Simple Health Bar. Only AFTER the value has been modified.
	}

	public void changePlayerText(float HPscore,int Calscore,int Score){

		playerHPtext.text = HPscore.ToString();
		playerCaltext.text = Calscore.ToString ();
		playerScoretext.text = Score.ToString ();

	}


}
