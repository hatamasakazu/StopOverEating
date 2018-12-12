using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Buttonclick : MonoBehaviour {

	public Button target;


	public KeyCode key;

	private void Start()
	{
		target.onClick.AddListener (() => Debug.Log ("ピカチュウ"));
	}

	private void Update()
	{
		
	
		if (Input.GetKeyDown (key)) {
				Debug.Log ("1");
				ExecuteEvents.Execute
			(
					target : target.gameObject,
					eventData: new PointerEventData (EventSystem.current),
					functor: ExecuteEvents.pointerClickHandler
				);
			}

	}
		
		

}
