using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyButton2_5 : MonoBehaviour {

	Pausable pause_script;
	private GameObject pause;

	private GameObject camera2;

	public GameObject button1;
	public GameObject button2;

	void Start(){

		pause = GameObject.FindWithTag ("pausemanager");
		pause_script = pause.GetComponent<Pausable> ();

		button1 = GameObject.FindWithTag ("continuetypingbutton");
		button2 = GameObject.FindWithTag ("continuegamebutton");
	}

	void Update(){

		camera2 = GameObject.FindWithTag ("MainCamera");
		FindObjectOfType<ObjectShaker> ().Shack (camera2);


	}

	public void OnClick(){


		/*クリックされたら、pasemenu1に戻る様に設定*/

		pause_script.pausmenu0.SetActive(false);
		//pause_script.pausmenu.SetActive (true);
		pause_script.pausmenu2.SetActive (false);
		pause_script.pausmenu3.SetActive (false);
		pause_script.pausmenu4.SetActive (true);

	}

	public void OnClick2(){
		
		/*クリックされたら、Game再開するように設定*/

		pause_script.pausmenu0.SetActive(false);
		//pause_script.pausmenu.SetActive (false);
		pause_script.pausmenu2.SetActive (false);
		pause_script.pausmenu3.SetActive (false);
		pause_script.pausmenu4.SetActive (false);
		pause_script.pausing = false;


	}
}
