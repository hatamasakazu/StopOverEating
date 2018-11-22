using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyButton1_5 : MonoBehaviour {

	Pausable pause_script;
	GameObject pause;
	private int flag;

	AudioManager audio_script;
	GameObject audio_ga;

	public AudioClip audioclip;
	public AudioClip audioclip2;


	void Start(){

		pause = GameObject.FindWithTag ("pausemanager");
		pause_script = pause.GetComponent<Pausable> ();

		audio_ga = GameObject.FindWithTag ("audiomanager");
		audio_script = audio_ga.GetComponent<AudioManager> ();



	}

	public void OnClick(){
		flag = PlayerPrefs.GetInt("goodorbad_flag",0);
		Debug.Log (flag);

		/*good food の処理*/
		if (flag == 0) {
			/*クリックされたら、pasemenu1に戻る様に設定*/
	

			pause_script.pausmenu0.SetActive (false);
			//pause_script.pausmenu.SetActive (false);
			pause_script.pausmenu2.SetActive (false);
			pause_script.pausmenu3.SetActive (true);
			pause_script.pausmenu4.SetActive (false);

			audio_script.PlayClip (audioclip);

		} else if(flag == 1) {
			
			pause_script.pausmenu0.SetActive (false);
			//pause_script.pausmenu.SetActive (false);
			pause_script.pausmenu2.SetActive (false);
			pause_script.pausmenu3.SetActive (false);
			pause_script.pausmenu4.SetActive (false);
			pause_script.pausing = false;

			audio_script.PlayClip (audioclip2);
		}

	}

	public void OnClick2(){

		/*クリックされたら、Game再開するように設定*/
		flag = PlayerPrefs.GetInt("goodorbad_flag",0);
		Debug.Log (flag);

		/*bad food の処理*/
		if (flag == 0) {
			pause_script.pausmenu0.SetActive (false);
			//pause_script.pausmenu.SetActive (false);
			pause_script.pausmenu2.SetActive (false);
			pause_script.pausmenu3.SetActive (false);
			pause_script.pausmenu4.SetActive (false);
			pause_script.pausing = false;

			audio_script.PlayClip (audioclip2);
		} else if(flag == 1){
			pause_script.pausmenu0.SetActive (false);
			//pause_script.pausmenu.SetActive (true);
			pause_script.pausmenu2.SetActive (false);
			pause_script.pausmenu3.SetActive (false);
			pause_script.pausmenu4.SetActive (true);

			audio_script.PlayClip (audioclip);
		}

	}
}
