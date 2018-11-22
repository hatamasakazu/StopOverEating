using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class colorchange : MonoBehaviour {

	public float interval = 0.1f;
	public Image renderComponent;


	// Use this for initialization
	void Start () {
		StartCoroutine ("Blink");
	}
	/*
	// Update is called once per frame
	IEnumerator Blink(){
		while (true) {
			var renderComponent = GetComponent<Renderer> ();
			renderComponent = !renderComponent.enabled;
			yield return new WaitForSeconds (interval);
		}
	}*/
}
