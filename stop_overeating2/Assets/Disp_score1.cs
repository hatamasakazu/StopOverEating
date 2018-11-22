using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Disp_score1 : MonoBehaviour {

	public Text text1;
	public Text text2;
	public Text text3;
	public Text text4;

	public Text cal_text;
	public Text com_text;

	public int food_cal1 = 260;
	public int food_cal2 = 77;
	public int food_cal3 = 220;
	public int food_cal4 = 87;

	private int total_cal;

	AudioManager audio_script;
	public GameObject audio_ga;

	public AudioClip audioclip;
	public AudioClip audioclip2;
	public AudioClip audioclip3;
	public AudioClip audioclip4;
	public AudioClip audioclip5;
	public AudioClip audioclip6;

	public AudioClip clearclip;
	public AudioClip unclearclip;

	public GameObject smile;
	public GameObject angry;



	bool flag = true;

	// Use this for initialization
	void Start () {
		
		audio_script = audio_ga.GetComponent<AudioManager> ();

		text1.text = " × " + Food_Count.food_count1;
		text2.text = " × " + Food_Count.food_count2;
		text3.text = " × " + Food_Count.food_count3;
		text4.text = " × " + Food_Count.food_count4;

		caliculation ();

		cal_text.text = "= " + total_cal;



		if (total_cal >= 2500 && total_cal < 3000) {
			audio_script.PlayClip (audioclip);
			com_text.text = "Just right";
			smile.SetActive (true);
			angry.SetActive (false);
		} else if (total_cal >= 3000 && total_cal < 4000) {
			audio_script.PlayClip (audioclip2);
			com_text.text = "A little too much";
			smile.SetActive (false);
			angry.SetActive (true);
		} else if (total_cal >= 4000 && total_cal < 5000) {
			audio_script.PlayClip (audioclip3);
			com_text.text = "Too much";
			smile.SetActive (false);
			angry.SetActive (true);
		} else if (total_cal >= 5000 && total_cal < 6000) {
			audio_script.PlayClip (audioclip4);
			com_text.text = "You should less";
			smile.SetActive (false);
			angry.SetActive (true);
		} else if (total_cal >= 6000) {
			audio_script.PlayClip (audioclip5);
			com_text.text = "Overeating!";
			smile.SetActive (false);
			angry.SetActive (true);
		} else if (total_cal < 2500) {
			audio_script.PlayClip (audioclip6);
			com_text.text = "Too little";
			smile.SetActive (false);
			angry.SetActive (true);
		}

	}

	void Update(){
		if(flag){
			if (Finish_process.clear_or_bad_flag == 1) {
				audio_script.PlayClip (clearclip);
				Debug.Log ("audio1");
			} else if (Finish_process.clear_or_bad_flag == 0) {
				audio_script.PlayClip (unclearclip);
				Debug.Log ("audio2");
			}
			flag = false;
		}
	}
	void caliculation(){
		total_cal = food_cal1 * Food_Count.food_count1 + food_cal2 * Food_Count.food_count2 + food_cal3 * Food_Count.food_count3 + food_cal4 * Food_Count.food_count4;

	}
}
