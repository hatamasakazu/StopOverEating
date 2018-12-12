using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio_start : MonoBehaviour {


	public AudioClip clip;
	private AudioSource audiosource;
	AudioManager audiomanager;
	GameObject audiostart;

	public bool flag_sound = true;
	// Use this for initialization
	void Start(){

		audiostart = GameObject.FindWithTag ("audiomanager");
		audiomanager = audiostart.GetComponent<AudioManager> ();


	}
		
	void Update(){
		if (flag_sound) {
			audiomanager.PlayClip (clip);
			flag_sound = false;
		}
	}

	public void FlagChange(bool a){
		flag_sound = a;
	}
}
