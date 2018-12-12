using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*このスクリプトを呼び出せば簡単に音楽再生が可能となる*/

public class AudioManager : MonoBehaviour {

	private static AudioManager instance;
	private AudioSource audiosource;

	void Awake(){
		instance = this;
		audiosource = GetComponent<AudioSource> ();
	}

	void Update(){
		
	}

	public static AudioManager GetInstance(){
		return instance;
	}

	public void PlayClip(AudioClip clip){
		audiosource.PlayOneShot (clip);
	}

	public static void Play(AudioClip clip){
		GetInstance ().PlayClip (clip);
	}
}
