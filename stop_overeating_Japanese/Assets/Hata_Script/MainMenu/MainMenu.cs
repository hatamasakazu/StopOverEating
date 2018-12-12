using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {


	/*menu画面全体を変更していくスクリプト*/

	private string hightscoreKey;
	private string totalscoreKey;

	private string highcalscoreKey;


	public void Start(){
		hightscoreKey = Score_stage.getA();
		totalscoreKey = Score_stage.getB ();
		highcalscoreKey = Cal_score.getCal ();


		//totalスコアのデータ消去
		PlayerPrefs.DeleteKey (totalscoreKey);
		PlayerPrefs.DeleteKey (highcalscoreKey);

	//	TimeKey = Finish_process.get_time ();
	}


	public void PlayGame()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);	
	}
		
	public void QuitGame ()
	{
		Debug.Log ("Quit");

		//Debug.Log (hightscoreKey);

		//get this key count 
		int a = PlayerPrefs.GetInt (hightscoreKey);
		//Debug.Log (a);

		//Delete this key data
		PlayerPrefs.DeleteKey (hightscoreKey);
		Debug.Log (PlayerPrefs.GetInt (hightscoreKey));
		Application.Quit();	
	}


}
