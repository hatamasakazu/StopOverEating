using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Finalmenu : MonoBehaviour {

	/*menu画面全体を変更していくスクリプト*/

	private string hightscoreKey;
	private string totalscoreKey;

	private string highcalscoreKey;

	private int totalscore;
	private int hightscore;
	private int hightcalscore;

	public GameObject yourscore;
	public GameObject Lankscore;
	public GameObject comment;
	TMP_Text yourscore_disp;
	TMP_Text Lank_disp;
	TMP_Text comment_disp;


	public int LankS;
	public int LankA;
	public int LankB;
	public int LankC;
	public int LankD;
	public int LankE;
	public int LankF;

	public GameObject mainmenu;
	public GameObject mainmenu2;


	public void Start(){

		mainmenu2.SetActive (false);

		hightscoreKey = Score_stage.getA();
		totalscoreKey = Score_stage.getB ();
		highcalscoreKey = Cal_score.getCal ();
		totalscore = PlayerPrefs.GetInt ("totalScore", 0);
		hightscore = PlayerPrefs.GetInt ("hightScore", 0);
		hightcalscore = PlayerPrefs.GetInt ("calhighScore", 0);


		yourscore_disp = yourscore.GetComponent<TextMeshProUGUI> ();
		yourscore_disp.text = "Your score is ' " + totalscore + " '";
		Lank_disp = Lankscore.GetComponent<TextMeshProUGUI> ();
		comment_disp = comment.GetComponent<TextMeshProUGUI> ();


		if (totalscore >= LankS) {
			Lank_disp.text = "Rank: S";
			comment_disp.text = "Excellent!! I rest my case";
		} else if (totalscore < LankS && totalscore >= LankA) {
			Lank_disp.text = "Rank: A";
			comment_disp.text = "Great! Keep up the good work";
		} else if (totalscore < LankA && totalscore >= LankB) {
			Lank_disp.text = "Rank: B";
			comment_disp.text = "Nice! Try a bit more";

		} else if (totalscore < LankB && totalscore >= LankC) {
			Lank_disp.text = "Rank: C";
			comment_disp.text = "Good! But not good enough";

		} else if (totalscore < LankC && totalscore >= LankD) {
			Lank_disp.text = "Rank: D";
			comment_disp.text = "Keep trying";
			

		} else if (totalscore < LankD && totalscore >= LankE) {
			Lank_disp.text = "Rank: E";
			comment_disp.text = "There's long way to go";

		} else if (totalscore < LankE && totalscore >= LankF) {
			Lank_disp.text = "Rank: F";
			comment_disp.text = "Go back to aquare one and start again";

		} else {
			Lank_disp.text = "Rank: Z";
			comment_disp.text = "Get out!";
		}


		//totalスコアのデータ消去
		//PlayerPrefs.DeleteKey (totalscoreKey);


		//	TimeKey = Finish_process.get_time ();
	}




	public void QuitGame ()
	{
		Debug.Log ("Quit");

		//Debug.Log (hightscoreKey);

		//get this key count 
		int a = PlayerPrefs.GetInt (hightscoreKey);
		//Debug.Log (a);

		//Delete this key data
		//PlayerPrefs.DeleteKey (hightscoreKey);
		Debug.Log (PlayerPrefs.GetInt (hightscoreKey));
		mainmenu.SetActive (false);
		mainmenu2.SetActive (true);

	}

	public void Gotomenu(){
		Debug.Log ("gotomenu");
		SceneManager.LoadScene ("Menu");

	}

	public void Continueeating(){
		Debug.Log ("gotomenu2");
		SceneManager.LoadScene ("ContinueEating");
	}

	public void Finisheating(){
		Debug.Log ("gotomenu3");
		Application.Quit ();
	}

}
