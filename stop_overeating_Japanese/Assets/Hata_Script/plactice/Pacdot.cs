using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Pacdot : MonoBehaviour {

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
	public int French_fri_score;
	public int Apple_score;

	//calscore
	public int French_fri_cal_score;
	public int Apple_cal_score;


	//setup HPbar
	public float frenchfrie_hpbar;
	public float Apple_hpbar;


	/*ここを変えることでHPBarが減る時間を管理可能*/
	//1秒ごとに減っていく

	public float time_hpbar = (-0.1f)/(1000);
	//時間間隔
	public float interval = 1f;


	public float span = 2f;

	//playerのtext
	public TMP_Text playerHPtext;
	public TMP_Text playerCaltext;
	public TMP_Text playerScoretext;


	//deleteするかどうかを決める
	Cal_score calscript;
	GameObject cal;

	public int flag_warp = 0;

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

	}



	//TakaDamage is to change HPber
	//ここでsocreを加算している

	void OnTriggerEnter2D(Collider2D co){


		if (calscript.calscore < 2500) {

			if (co.tag == "pacman" || co.tag == "Soldier" || co.tag == "Death") {
				//Debug.Log (co.tag);

				Debug.Log ("calscoreqqq"+calscript.calscore);
				Destroy (gameObject);
				//Debug.Log (gameObject.name);

				/*食べ物を追加する場合ここに追加*/

				if (gameObject.tag == "French_Fries") {

					//flag
					a = 0;
					//score add
					FindObjectOfType<Score_stage> ().AddPoint (French_fri_score);

					//calscore add
					FindObjectOfType<Cal_score> ().AddCalPoint (French_fri_cal_score);

					//Change HP bar
					TakeDamage (a, frenchfrie_hpbar);
					//Debug.Log ("Hit");

					//playerの上についているtextを変える
					changePlayerText (frenchfrie_hpbar * 100, French_fri_cal_score, French_fri_score);



				}

				if (gameObject.tag == "Apple" || gameObject.tag == "apple") {
					a = 1;
					//score add
					FindObjectOfType<Score_stage> ().AddPoint (Apple_score);

					//calscore add
					FindObjectOfType<Cal_score> ().AddCalPoint (Apple_cal_score);

					//Change HP bar
					TakeDamage (a, Apple_hpbar);
					//Debug.Log ("Hit2");

					//playerの上についているtextを変える
					changePlayerText (Apple_hpbar * 100, Apple_cal_score, Apple_score);
				}

				if (gameObject.tag == "Waypoint") {
					flag_warp = 1;
				}

			}
		} else if (calscript.calscore > 2500) {
			if (co.tag == "pacman" || co.tag == "Soldier" || co.tag == "Death") {
				//Debug.Log (co.tag);
				Debug.Log ("calscoreppp"+calscript.calscore);

				//Destroy (gameObject);
				Debug.Log (gameObject.name);

				/*食べ物を追加する場合ここに追加*/

				if (gameObject.tag == "French_Fries") {

					//flag
					a = 0;
					//score add
					FindObjectOfType<Score_stage> ().AddPoint (French_fri_score);

					//calscore add
					FindObjectOfType<Cal_score> ().AddCalPoint (French_fri_cal_score);

					//Change HP bar
					TakeDamage (a, frenchfrie_hpbar);
					//Debug.Log ("Hit");

					//playerの上についているtextを変える
					changePlayerText (frenchfrie_hpbar * 100, French_fri_cal_score, French_fri_score);



				}

				if (gameObject.tag == "Apple" || gameObject.tag == "apple") {
					a = 1;
					//score add
					FindObjectOfType<Score_stage> ().AddPoint (Apple_score);

					//calscore add
					FindObjectOfType<Cal_score> ().AddCalPoint (Apple_cal_score);

					//Change HP bar
					TakeDamage (a, Apple_hpbar);
					//Debug.Log ("Hit2");

					//playerの上についているtextを変える
					changePlayerText (Apple_hpbar * 100, Apple_cal_score, Apple_score);
				}

				if (gameObject.tag == "Waypoint") {
					flag_warp = 1;
				}

			}
		}

	}




	//1秒ごとに減っていく関数
	public IEnumerator loop(){

		while (true) {

			yield return new WaitForSeconds (interval);
			TakeDamage(3,time_hpbar);
		}
	}

	private void onTimer(){
		Debug.Log ("on timer");
	}

	//HPbar change

	void TakeDamage ( int a, float damage )
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
		case 3:
			health.UpdateBar (damage, HP);
			break;
		}


		// <------- This is where you will want to update the Simple Health Bar. Only AFTER the value has been modified.
	}

	void changePlayerText(float HPscore,int Calscore,int Score){

		playerHPtext.text = HPscore.ToString();
		playerCaltext.text = Calscore.ToString ();
		playerScoretext.text = Score.ToString ();

	}
		
}
