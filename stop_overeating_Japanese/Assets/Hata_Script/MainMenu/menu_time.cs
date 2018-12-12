using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class menu_time : MonoBehaviour {


	///<summary>
	/// メニュー画面全体の時間を管理するスクリプト
	/// ゲーム全体の終了時間を決めることが可能 finishtimeを変えることで決めれる
	/// </summary>



	//private string hightscoreKey;
	public static string TimeKey;
	public static string FInKey;

	public static int save_time2 = 0;
	public static float fin_time = 20f;

	public static float unscaledTime;

	public void Start(){
		//hightscoreKey = Score_stage.getA();
		TimeKey = Finish_process.get_time ();
		PlayerPrefs.DeleteKey (TimeKey);

	}





	void Update () {

		ALLfinish ();


	}

	//全体の終了関数
	//All finish process

	/// <summary>
	/// この時間を変えることで終了時間を変えることが可能
	/// </summary>
	public static float finishtime = 300f;

	public static float input_record_time;
	public static float calibration_time;
	public static float eatfinish_time;
	public static float alarm_recordtime;
	public static bool flag = true;
	public static float cal_finish_time;



	public static float ALLfinish(){
		if (flag) {
			calibration_time = PlayerPrefs.GetFloat ("calibration_time");
			eatfinish_time = PlayerPrefs.GetFloat ("eatingfinish_time");
			alarm_recordtime = PlayerPrefs.GetFloat ("alarm_record_time");
			input_record_time = Input_record_time.input_record_time;
			finishtime = InputManager3.Game_finish_time;


			Debug.Log ("cal_time" + calibration_time + "eatfinish_time" + eatfinish_time + "alarm_recordtime" + alarm_recordtime + "total_time" + (eatfinish_time+calibration_time));
			Debug.Log ("input_record_time" + input_record_time+"finishtime" + finishtime);
			/*ここでcalibrationtimeとeatingfinishtimeとalarmtimeのtotaltimeを出している*/
			cal_finish_time = finishtime + eatfinish_time + calibration_time + alarm_recordtime + input_record_time;
			Debug.Log ("cal_finish_time"+ cal_finish_time);

			finishtime = cal_finish_time;
			Debug.Log ("finishtime"+finishtime);
				
			flag = false;
		}

		unscaledTime = Time.unscaledTime;
		//Debug.Log ("unscaledTime " + unscaledTime);
		if (unscaledTime >= cal_finish_time) {
			Debug.Log("終わり");
			SceneManager.LoadScene ("FinishMenu");
		}

		return unscaledTime;


	}


}
