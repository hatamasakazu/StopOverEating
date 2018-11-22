using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*このスクリプトではScreen1において正解した場合に開かれる*/
/*Screen3に設定すること*/

public class MyButton3 : MonoBehaviour {


	Pausable pause_script;
	GameObject pause;


	//foodのpathを取得し、deleteに使う
	public static string food = "foodpath";
	private string foodpath;
	private int score;
	private int calscore;
	private float bar; 
	private int damage_flag;
	private float ch_hp;
	private int goodbad_flag;
	private string scriptname;


	void Start(){
		
		pause = GameObject.FindWithTag ("pausemanager");
		pause_script = pause.GetComponent<Pausable> ();

	}

	public void OnClick3(){

		/*クリックされたら、foodはデリートし、scoreはいじらないように設定*/

		foodpath = PlayerPrefs.GetString (food);
		//Debug.Log (foodpath);
		GameObject thisfood = GameObject.Find (foodpath);
		//Debug.Log (thisfood);
		Destroy (thisfood);

		pause_script.pausmenu0.SetActive (false);
		pause_script.pausmenu.SetActive (false);
		pause_script.pausmenu2.SetActive (false);
		pause_script.pausmenu3.SetActive (false);
		pause_script.pausing = false;

	}

	public void OnClick4(){


		/*クリックされたら、foodをデリートし、gameを再開するように設定*/

		foodpath = PlayerPrefs.GetString (food);

		GameObject thisfood = GameObject.Find (foodpath);

		Destroy (thisfood);
		score = PlayerPrefs.GetInt ("Currntscore",0);
		calscore = PlayerPrefs.GetInt ("Currentcalkey",0);
		bar = PlayerPrefs.GetFloat ("Currentbar",0);
		damage_flag = PlayerPrefs.GetInt ("damage_flag", 0);
		ch_hp = PlayerPrefs.GetFloat ("ch_hp", 0);
		goodbad_flag = PlayerPrefs.GetInt("goodorbad_flag",0);

		//拡張機能としてdamage_flagがfoodの種類を表していて値を変更できる
		if (goodbad_flag == 0) {
			switch(damage_flag){
			case(0):
				Score_all (score, calscore, bar, ch_hp, damage_flag);
				break;
			case(1):
				Score_all (score, calscore, bar, ch_hp, damage_flag);
				break;
			case(2):
				Score_all (score, calscore, bar, ch_hp, damage_flag);
				break;
			case(3):
				Score_all (score, calscore, bar, ch_hp, damage_flag);
				break;
			case(4):
				Score_all (score, calscore, bar, ch_hp, damage_flag);
				break;
			case(5):
				Score_all (score, calscore, bar, ch_hp, damage_flag);
				break;
			case(6):
				Score_all (score, calscore, bar, ch_hp, damage_flag);
				break;
			}
		} else if (goodbad_flag == 1) {
			switch(damage_flag){
			case(0):
				Score_all (score, (-1)*calscore, bar, ch_hp, damage_flag);
				break;
			case(1):
				Score_all (score, (-1)*calscore, bar, ch_hp, damage_flag);
				break;
			case(2):
				Score_all (score, (-1)*calscore, bar, ch_hp, damage_flag);
				break;
			case(3):
				Score_all (score, (-1)*calscore, bar, ch_hp, damage_flag);
				break;
			case(4):
				Score_all (score, (-1)*calscore, bar, ch_hp, damage_flag);
				break;
			case(5):
				Score_all (score, (-1)*calscore, bar, ch_hp, damage_flag);
				break;
			case(6):
				Score_all (score, calscore, bar, ch_hp, damage_flag);
				break;
			}
		}
		//playerの上についているtextを変える
		//FindObjectOfType<PacdotStage1>().changePlayerText(bar,calscore,score);


		pause_script.pausmenu0.SetActive (false);
		pause_script.pausmenu.SetActive (false);
		pause_script.pausmenu2.SetActive (false);
		pause_script.pausmenu3.SetActive (false);
		pause_script.pausing = false;

	}

	void Score_all(int score, int calscore, float bar, float ch_hp, int damage_flag){
		FindObjectOfType<Score_stage> ().AddPoint (score);

		//calscore add
		FindObjectOfType<Cal_score> ().AddCalPoint (calscore);

		//Change HP bar
		FindObjectOfType<PacdotStage1> ().TakeDamage(damage_flag, bar * ch_hp);
	}

	public void OnClick5(){

		/*クリックされたら、foodはデリートせず、gameを再開するように設定*/
		pause_script.pausmenu0.SetActive (false);
		pause_script.pausmenu.SetActive (false);
		pause_script.pausmenu2.SetActive (false);
		pause_script.pausmenu3.SetActive (false);
		pause_script.pausing = false;

	}
}
