using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

/// <summary>
/// Rigidbodyの速度を保存しておくクラス
/// </summary>
public class RigidbodyVelocity
{
	public Vector3 velocity;
	public Vector3 angularVeloccity;
	public RigidbodyVelocity(Rigidbody rigidbody)
	{
		velocity = rigidbody.velocity;
		angularVeloccity = rigidbody.angularVelocity;
	}
}

public class Pausable : MonoBehaviour {

	//music用
	MusicSound musicsound_script;
	public GameObject musicsound;




	/// <summary>
	//menu画面を表示させる
	/// </summary>
	public GameObject pausmenu0;
	public GameObject pausmenu;
	public GameObject pausmenu2;
	public GameObject pausmenu3;
	public GameObject pausmenu4;
	private GameObject BCS; 
/*
	MyButton1 button_script0;
	MyButton1 button_script1;

	MyButton2 button_script2;
	MyButton2 button_script3;

	MyButton3 button_script4;
	MyButton3 button_script5;


	private GameObject button0;
	private GameObject button1;
	private GameObject button2;
	private GameObject button3;
	private GameObject button4;
	private GameObject button5;
*/
	GameObject menu;

	TypingSoft typing_script;
	public GameObject typing;

	/// <summary>
	/// 現在Pause中か？
	/// </summary>
	public bool pausing;

	/// <summary>
	/// 無視するGameObject
	/// </summary>
	public GameObject[] ignoreGameObjects;

	/// <summary>
	/// ポーズ状態が変更された瞬間を調べるため、前回のポーズ状況を記録しておく
	/// </summary>
	bool prevPausing;

	/// <summary>
	/// Rigidbodyのポーズ前の速度の配列
	/// </summary>
	RigidbodyVelocity[] rigidbodyVelocities;

	/// <summary>
	/// ポーズ中のRigidbodyの配列
	/// </summary>
	Rigidbody[] pausingRigidbodies;

	/// <summary>
	/// ポーズ中のMonoBehaviourの配列
	/// </summary>
	MonoBehaviour[] pausingMonoBehaviours;


	void Start(){
		/*
		button0 = GameObject.FindWithTag ("goodfoodbutton");
		button_script0 = button0.GetComponent<MyButton1> ();

		button1 = GameObject.FindWithTag ("badfoodbutton");
		button_script1 = button1.GetComponent<MyButton1> ();

		button2 = GameObject.FindWithTag ("continuetypingbutton");
		button_script2 = button2.GetComponent<MyButton2> ();

		button3 = GameObject.FindWithTag ("continuegamebutton");
		button_script3 = button3.GetComponent<MyButton2> ();

		button4 = GameObject.FindWithTag ("deletebutton");
		button_script4 = button4.GetComponent<MyButton3> ();

		button5 = GameObject.FindWithTag ("undeletebutton");
		button_script5 = button5.GetComponent<MyButton3> ();


		typing = GameObject.FindWithTag ("inputfield");
		typing_script = typing.GetComponent<TypingSoft> ();

*/

		//musicsound = GameObject.FindWithTag ("backgroundmusic");
		musicsound_script = musicsound.GetComponent<MusicSound> ();
		//foodスコア画面を隠す
		BCS = GameObject.FindWithTag ("BCS");



		pausmenu0.SetActive (false);
		pausmenu.SetActive (false);
		pausmenu2.SetActive (false);
		pausmenu3.SetActive (false);
		pausmenu4.SetActive (false);

	}


	/// <summary>
	/// 更新処理
	/// </summary>
	void Update() {
		// ポーズ状態が変更されていたら、Pause/Resumeを呼び出す

		if (prevPausing != pausing)
		{
			if (pausing) {
				Pause ();
				BCS.SetActive (false);
				pausmenu0.SetActive (true);
				pausmenu.SetActive (false);
				pausmenu2.SetActive (false);
				pausmenu3.SetActive (false);
				pausmenu4.SetActive (false);
			} else {
				Resume ();
				BCS.SetActive (true);
				pausmenu0.SetActive (false);
				pausmenu.SetActive (false);
				pausmenu2.SetActive (false);
				pausmenu3.SetActive (false);
				pausmenu4.SetActive (false);

			}
			prevPausing = pausing;
		}
	}

	/// <summary>
	/// 中断
	/// </summary>
	void Pause() {

		//backgroundmusicの停止
		FindObjectOfType<MusicSound> ().backgroundpause();
		FindObjectOfType<MusicSound> ().musicpause ();
		FindObjectOfType<MusicSound> ().musicpause2 ();
		FindObjectOfType<MusicSound> ().musicpause3 ();

		// Rigidbodyの停止
		// 子要素から、スリープ中でなく、IgnoreGameObjectsに含まれていないRigidbodyを抽出
		Predicate<Rigidbody> rigidbodyPredicate = 
			obj => !obj.IsSleeping() && 
				   Array.FindIndex(ignoreGameObjects, gameObject => gameObject == obj.gameObject) < 0;
		pausingRigidbodies = Array.FindAll(transform.GetComponentsInChildren<Rigidbody>(), rigidbodyPredicate);
		rigidbodyVelocities = new RigidbodyVelocity[pausingRigidbodies.Length];
		for(int i = 0; i < pausingRigidbodies.Length; i++)
		{
			// 速度、角速度も保存しておく
			rigidbodyVelocities[i] = new RigidbodyVelocity(pausingRigidbodies[i]);
			pausingRigidbodies[i].Sleep ();
		}

		// MonoBehaviourの停止
		// 子要素から、有効かつこのインスタンスでないもの、IgnoreGameObjectsに含まれていないMonoBehaviourを抽出
		Predicate<MonoBehaviour> monoBehaviourPredicate = 
			obj => obj.enabled && 
				   obj != this && 
				   Array.FindIndex(ignoreGameObjects, gameObject => gameObject == obj.gameObject) < 0;
		pausingMonoBehaviours = Array.FindAll(transform.GetComponentsInChildren<MonoBehaviour>(), monoBehaviourPredicate);
		foreach(var monoBehaviour in pausingMonoBehaviours)
		{
			monoBehaviour.enabled = false;
		}

	}

	/// <summary>
	/// 再開
	/// </summary>
	void Resume() {

		musicsound_script.flag = 0;
		musicsound_script.flagbool = true;

		// Rigidbodyの再開
		for(int i = 0; i < pausingRigidbodies.Length; i++)
		{
			pausingRigidbodies[i].WakeUp();
			pausingRigidbodies[i].velocity = rigidbodyVelocities[i].velocity;
			pausingRigidbodies[i].angularVelocity = rigidbodyVelocities[i].angularVeloccity;
		}

		// MonoBehaviourの再開
		foreach(var monoBehaviour in pausingMonoBehaviours)
		{
			monoBehaviour.enabled = true;
		}
	}
}