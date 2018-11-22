using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

/// <summary>
/// このスクリプトではstart to Calibrarionと Calibration to食事終了までの
/// 処理が書かれている
/// listValueにすべてのRRIデータがリスト化されており
/// このデータを処理することをこのスクリプトで行い
/// 最終的にゲームLevelを決定している
/// </summary>


namespace Script.SocketServer
{
	public class Calibration : MonoBehaviour {


		public int caliburation;
		public int Sec_interval_average;

		float current_time;
		public GameObject myviewer;
		public GameObject change_color;

		public List<int> listCalburation_allaverage = new List<int> {};

		public List<int> loadlist = new List<int>{ };

		public List<int> listCalibulation_Value = new List<int> {};
		public List<int> listCal_to_finishValue = new List<int>{};
		public List<int> listSec_interval = new List<int>{};
		public List<int> listSec_average = new List<int>{};

		bool flag = true;
		bool flag2 = true;
		bool Sec_flag = false;



		public float caliburation_time;
		public float eatfinish_time;
		public float total_time;

		public GameObject main_gd;
		public GameObject calibration_gd;
		public GameObject calibration_gd2;
		public GameObject GoodorBad_gd;
		public Text calibration_text;
		public Text calibration_text2;

		public int size1;
		public int size2;
		public int size3;

		private int size_sec1=0;
		private int size_sec2=0;
		private int size_sec3;

		public int diff;
		public int Level1;
		public int Level2;
		public int Level3;
		public int Level_flag;

		int a = 0;

		public float Sec_time = 30;
		private float b = 0;

		MyViewer myviewer_script;
		change_color changecolor_script;

		void Start(){
			PlayerPrefs.SetInt ("SceneChange", a);
			myviewer_script = myviewer.GetComponent<MyViewer> ();
			changecolor_script = change_color.GetComponent<change_color> ();
			main_gd.SetActive (true);
			calibration_gd.SetActive (false);
			calibration_gd2.SetActive (false);
			GoodorBad_gd.SetActive (false);

			caliburation_time = InputManager.Calibration_time;
			eatfinish_time = InputManager2.Eating_finish_time;
			total_time = caliburation_time + eatfinish_time;

			Debug.Log ("adad"+caliburation_time);
			Debug.Log ("dadd"+eatfinish_time);
			PlayerPrefs.SetFloat ("calibration_time", caliburation_time);
			PlayerPrefs.SetFloat ("eatingfinish_time", eatfinish_time);
			PlayerPrefs.SetFloat ("total_time", total_time);

			Sec_time = InputManager1.Calibration_interval_time;





		}

		void Update(){
			//Debug.Log ("更新中");
			if (Time.timeSinceLevelLoad < caliburation_time) {
				
				//Debug.Log ("くた１"+Time.timeSinceLevelLoad);
			} else if (Time.timeSinceLevelLoad >= caliburation_time && Time.timeSinceLevelLoad < total_time) {
				
				if (flag) {
					main_gd.SetActive (false);
					calibration_gd.SetActive (true);
					calibration_gd2.SetActive (true);
					GoodorBad_gd.SetActive (true);

					//0~calvulationまでのsizeを取得
					size1 = myviewer_script.listValue.Count;
					size_sec1 = size1;

					try {
						caliburation = (int)myviewer_script.listValue.Average ();
						//ここに必要なaverageのRRIを追加する
						listCalburation_allaverage.Add (caliburation);

						//テキスト表示
						calibration_text.text = "Average RRI is " + caliburation;
						//listの保存は特別ば処理が必要
						//PlayerPrefsUtility.SaveList<int> ("AverageRRIKey", listCalburation_allaverage);
						//loadlist = PlayerPrefsUtility.LoadList<int> ("AverageRRIKey");
						Debug.Log ("きてる3" + caliburation);
						//Debug.Log ("きた２" + loadlist [0]);

					} catch {
						Debug.Log ("例外");
					}
					flag = false;
				}
				//Debug.Log (Time.timeSinceLevelLoad);
				if (Time.timeSinceLevelLoad > (Sec_time+caliburation_time)) {
					Sec_flag = true;
					changecolor_script.flag_Sec = true;
					Debug.Log ("みてみて");
				}

				//30秒ごとのRRIデータの平均をとる関数
				if (Sec_flag) {
					size_sec2 = myviewer_script.listValue.Count;

					Debug.Log ("size_sec2 "+size_sec2+"size_sec1 "+size_sec1+"size1 "+size1);
					size_sec3 = size_sec2 - size_sec1;
					Debug.Log ("size_sec1 "+size_sec1+"size_sec3 "+size_sec3);
					listSec_interval = myviewer_script.listValue.GetRange (size_sec1, size_sec3);

					size_sec1 = size_sec2;

					Sec_interval_average = (int)listSec_interval.Average ();

					listSec_average.Add (Sec_interval_average);

					PlayerPrefs.SetInt ("SecIntervalRRIKey",Sec_interval_average );
					Debug.Log("Sec_interval_average" + Sec_interval_average);
					Debug.Log ("Sec_time"+Sec_time);
					b = Sec_time;
					Sec_time = Sec_time + b;
					Debug.Log ("Sec_timea"+Sec_time);
					Sec_flag = false;
					//Debug.Log ("??"+myviewer_script.record);
					calibration_text2.text = "Average of 30 seconds RRI " + Sec_interval_average;
				}


			} else if (Time.timeSinceLevelLoad >= total_time) {
				if (flag2) {
					//calibration~finishまでのRRIの値の平均を保存する
					try{size2 = myviewer_script.listValue.Count;
						size3 = size2 - size1;
						Debug.Log("size3"+size3 + "count"+myviewer_script.listValue.Count);

						//範囲指定することでlistValueからCalibration~食事終了までのRRIデータを保管することが可能
						listCal_to_finishValue = myviewer_script.listValue.GetRange (size1,size3);
					
						caliburation = (int)listCal_to_finishValue.Average ();
						listCalburation_allaverage.Add (caliburation);
						PlayerPrefsUtility.SaveList<int> ("AverageRRIKey", listCalburation_allaverage);
					
						loadlist = PlayerPrefsUtility.LoadList<int> ("AverageRRIKey");
					 
						diff = (int)Mathf.Abs(listCalburation_allaverage[1]-listCalburation_allaverage[0]);

						PlayerPrefs.SetInt("DiffRRIKey",diff);
						Debug.Log("diff"+diff);
						//ここでレベルの決定
						if(diff >= 0 && diff < Level1){
							Level_flag = 1;
							PlayerPrefs.SetInt("Levelflag",Level_flag);
						}else if(diff >= Level1 && diff < Level2){
							Level_flag = 2;
							PlayerPrefs.SetInt("Levelflag",Level_flag);
						}else if(diff >= Level2 && diff < Level3){
							Level_flag = 3;
							PlayerPrefs.SetInt("Levelflag",Level_flag);
						}else {
							Level_flag = 4;
							PlayerPrefs.SetInt("Levelflag",Level_flag);
						}

						PlayerPrefs.Save();



						SceneManager.LoadScene("Alarm");


					//0にはstart～calibrationまでの平均が入っている
					//Debug.Log ("きたc" + loadlist [0]);
					//1にはcalibration～食事終了までの平均が入っている
					//Debug.Log ("きたa" + loadlist [1]);
						Debug.Log ("count" + loadlist.Count);
					}catch{
						Debug.Log ("っこ");
					}
					flag2 = false;
				}
			}
		}


	}
}
