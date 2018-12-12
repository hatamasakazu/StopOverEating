using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

/// <summary>
/// Rigidbodyの堀業を隠贋しておくクラス
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

	//music喘
	MusicSound musicsound_script;
	public GameObject musicsound;




	/// <summary>
	//menu鮫中を燕幣させる
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
	/// �F壓Pause嶄か��
	/// </summary>
	public bool pausing;

	/// <summary>
	/// �o��するGameObject
	/// </summary>
	public GameObject[] ignoreGameObjects;

	/// <summary>
	/// ポ�`ズ彜�Bが�筝�された鵬�gを�{べるため、念指のポ�`ズ彜�rを���hしておく
	/// </summary>
	bool prevPausing;

	/// <summary>
	/// Rigidbodyのポ�`ズ念の堀業の塘双
	/// </summary>
	RigidbodyVelocity[] rigidbodyVelocities;

	/// <summary>
	/// ポ�`ズ嶄のRigidbodyの塘双
	/// </summary>
	Rigidbody[] pausingRigidbodies;

	/// <summary>
	/// ポ�`ズ嶄のMonoBehaviourの塘双
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
		//foodスコア鮫中を�Lす
		BCS = GameObject.FindWithTag ("BCS");



		pausmenu0.SetActive (false);
		pausmenu.SetActive (false);
		pausmenu2.SetActive (false);
		pausmenu3.SetActive (false);
		pausmenu4.SetActive (false);

	}


	/// <summary>
	/// 厚仟�I尖
	/// </summary>
	void Update() {
		// ポ�`ズ彜�Bが�筝�されていたら、Pause/Resumeを柵び竃す

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
	/// 嶄僅
	/// </summary>
	void Pause() {

		//backgroundmusicの唯峭
		FindObjectOfType<MusicSound> ().backgroundpause();
		FindObjectOfType<MusicSound> ().musicpause ();
		FindObjectOfType<MusicSound> ().musicpause2 ();
		FindObjectOfType<MusicSound> ().musicpause3 ();

		// Rigidbodyの唯峭
		// 徨勣殆から、スリ�`プ嶄でなく、IgnoreGameObjectsに根まれていないRigidbodyを渇竃
		Predicate<Rigidbody> rigidbodyPredicate = 
			obj => !obj.IsSleeping() && 
				   Array.FindIndex(ignoreGameObjects, gameObject => gameObject == obj.gameObject) < 0;
		pausingRigidbodies = Array.FindAll(transform.GetComponentsInChildren<Rigidbody>(), rigidbodyPredicate);
		rigidbodyVelocities = new RigidbodyVelocity[pausingRigidbodies.Length];
		for(int i = 0; i < pausingRigidbodies.Length; i++)
		{
			// 堀業、叔堀業も隠贋しておく
			rigidbodyVelocities[i] = new RigidbodyVelocity(pausingRigidbodies[i]);
			pausingRigidbodies[i].Sleep ();
		}

		// MonoBehaviourの唯峭
		// 徨勣殆から、嗤�燭�つこのインスタンスでないもの、IgnoreGameObjectsに根まれていないMonoBehaviourを渇竃
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
	/// 壅�_
	/// </summary>
	void Resume() {

		musicsound_script.flag = 0;
		musicsound_script.flagbool = true;

		// Rigidbodyの壅�_
		for(int i = 0; i < pausingRigidbodies.Length; i++)
		{
			pausingRigidbodies[i].WakeUp();
			pausingRigidbodies[i].velocity = rigidbodyVelocities[i].velocity;
			pausingRigidbodies[i].angularVelocity = rigidbodyVelocities[i].angularVeloccity;
		}

		// MonoBehaviourの壅�_
		foreach(var monoBehaviour in pausingMonoBehaviours)
		{
			monoBehaviour.enabled = true;
		}
	}
}