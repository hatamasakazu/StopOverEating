using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Script.SocketServer{
	public class change_color : MonoBehaviour {


		public GameObject myview;
		public GameObject obcolor;
		MyViewer myviewer_script;
		public GameObject calibration_gd;
		Calibration calibration_script;

		public GameObject smile;
		public GameObject angry;

		public bool flag_Sec = false;

		AudioManager audio_script;
		GameObject audio_ga;

		public AudioClip audioclip;
		public AudioClip audioclip2;


		public Text GoodorBad_text;
		// Use this for initialization
		void Start () {
			myviewer_script = myview.GetComponent<MyViewer> ();
			calibration_script = calibration_gd.GetComponent<Calibration> ();
			smile.SetActive (false);
			angry.SetActive (false);

			audio_ga = GameObject.FindWithTag ("audiomanager");
			audio_script = audio_ga.GetComponent<AudioManager> ();

		}
	
		int num; 
		// Update is called once per frame
		private void Update () {
			try{
				num = myviewer_script.record;
				/*
				if(Time.timeSinceLevelLoad >= calibration_script.caliburation_time){
					if (num <= calibration_script.caliburation && num > 0) {
						//Debug.Log ("高すぎ"+num);
						obcolor.GetComponent<Image>().color = Color.red;
						//GoodorBad_text.text = "Please Relax!!";
						GoodorBad_text.text = "Chew your food well!";
						smile.SetActive (false);
						angry.SetActive (true);

					} else if (num > calibration_script.caliburation) {
						//Debug.Log ("丁度いい" + num);
						obcolor.GetComponent<Image>().color = Color.blue;
						//GoodorBad_text.text = "Relax state";
						GoodorBad_text.text = "Keep it up!";
						smile.SetActive (true);
						angry.SetActive (false);
					}
				}else if(Time.timeSinceLevelLoad < calibration_script.caliburation_time){
					obcolor.GetComponent<Image>().color = Color.black;
					
				}*/


				if(flag_Sec){
					Debug.Log("来とる");
					if(Time.timeSinceLevelLoad >= calibration_script.caliburation_time){
						if (num > calibration_script.Sec_interval_average ) {
							audio_script.PlayClip (audioclip);
							//Debug.Log ("高すぎ"+num);
							obcolor.GetComponent<Image>().color = Color.red;
							//GoodorBad_text.text = "Please Relax!!";
							GoodorBad_text.text = "Chew your food well!";
							smile.SetActive (false);
							angry.SetActive (true);
							Debug.Log(Time.timeSinceLevelLoad);

						} else if (num <= calibration_script.Sec_interval_average && num > 0) {
							audio_script.PlayClip (audioclip2);
							//Debug.Log ("丁度いい" + num);
							obcolor.GetComponent<Image>().color = Color.blue;
							//GoodorBad_text.text = "Relax state";
							GoodorBad_text.text = "Keep it up!";
							smile.SetActive (true);
							angry.SetActive (false);
							Debug.Log(Time.timeSinceLevelLoad);
						}

					}else if(Time.timeSinceLevelLoad < calibration_script.caliburation_time){
						obcolor.GetComponent<Image>().color = Color.black;

					}
					flag_Sec = false;
				}
			}catch{
				Debug.Log ("例外");
			}
		}
	
	}
}