using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

/// <summary>
/// Rigidbody���ٶȤ򱣴椷�Ƥ������饹
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

	//music��
	MusicSound musicsound_script;
	public GameObject musicsound;




	/// <summary>
	//menu������ʾ������
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
	/// �F��Pause�Ф���
	/// </summary>
	public bool pausing;

	/// <summary>
	/// �oҕ����GameObject
	/// </summary>
	public GameObject[] ignoreGameObjects;

	/// <summary>
	/// �ݩ`��״�B��������줿˲�g���{�٤뤿�ᡢǰ�ؤΥݩ`��״�r��ӛ�h���Ƥ���
	/// </summary>
	bool prevPausing;

	/// <summary>
	/// Rigidbody�Υݩ`��ǰ���ٶȤ�����
	/// </summary>
	RigidbodyVelocity[] rigidbodyVelocities;

	/// <summary>
	/// �ݩ`���Ф�Rigidbody������
	/// </summary>
	Rigidbody[] pausingRigidbodies;

	/// <summary>
	/// �ݩ`���Ф�MonoBehaviour������
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
		//food������������L��
		BCS = GameObject.FindWithTag ("BCS");



		pausmenu0.SetActive (false);
		pausmenu.SetActive (false);
		pausmenu2.SetActive (false);
		pausmenu3.SetActive (false);
		pausmenu4.SetActive (false);

	}


	/// <summary>
	/// ���I��
	/// </summary>
	void Update() {
		// �ݩ`��״�B���������Ƥ����顢Pause/Resume����ӳ���

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
	/// �ж�
	/// </summary>
	void Pause() {

		//backgroundmusic��ֹͣ
		FindObjectOfType<MusicSound> ().backgroundpause();
		FindObjectOfType<MusicSound> ().musicpause ();
		FindObjectOfType<MusicSound> ().musicpause2 ();
		FindObjectOfType<MusicSound> ().musicpause3 ();

		// Rigidbody��ֹͣ
		// ��Ҫ�ؤ��顢����`���ФǤʤ���IgnoreGameObjects�˺��ޤ�Ƥ��ʤ�Rigidbody����
		Predicate<Rigidbody> rigidbodyPredicate = 
			obj => !obj.IsSleeping() && 
				   Array.FindIndex(ignoreGameObjects, gameObject => gameObject == obj.gameObject) < 0;
		pausingRigidbodies = Array.FindAll(transform.GetComponentsInChildren<Rigidbody>(), rigidbodyPredicate);
		rigidbodyVelocities = new RigidbodyVelocity[pausingRigidbodies.Length];
		for(int i = 0; i < pausingRigidbodies.Length; i++)
		{
			// �ٶȡ����ٶȤⱣ�椷�Ƥ���
			rigidbodyVelocities[i] = new RigidbodyVelocity(pausingRigidbodies[i]);
			pausingRigidbodies[i].Sleep ();
		}

		// MonoBehaviour��ֹͣ
		// ��Ҫ�ؤ��顢�Є����Ĥ��Υ��󥹥��󥹤Ǥʤ���Ρ�IgnoreGameObjects�˺��ޤ�Ƥ��ʤ�MonoBehaviour����
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
	/// ���_
	/// </summary>
	void Resume() {

		musicsound_script.flag = 0;
		musicsound_script.flagbool = true;

		// Rigidbody�����_
		for(int i = 0; i < pausingRigidbodies.Length; i++)
		{
			pausingRigidbodies[i].WakeUp();
			pausingRigidbodies[i].velocity = rigidbodyVelocities[i].velocity;
			pausingRigidbodies[i].angularVelocity = rigidbodyVelocities[i].angularVeloccity;
		}

		// MonoBehaviour�����_
		foreach(var monoBehaviour in pausingMonoBehaviours)
		{
			monoBehaviour.enabled = true;
		}
	}
}