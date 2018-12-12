using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*このスクリプトはwarpする初めのGameObjectに割り当てること*/

public class Warp : MonoBehaviour {

	public int flag=0;

		
	void OnTriggerEnter2D(Collider2D other){
		
		if (other.tag == "Soldier"||other.tag == "Death"||other.tag == "soldierwhite"||other.tag == "soldierblack") {
			Debug.Log ("De1");
			if (gameObject.tag == "warpstart") {
				flag = 1;
				Debug.Log ("warpstart"+flag);
			}
			if (gameObject.tag == "warpstart2") {
				flag = 2;
				Debug.Log ("warpstart2+"+flag);
			}
			if (gameObject.tag == "warpstart3") {
				flag = 3;
				Debug.Log ("warpstart3"+flag);
			}
			if (gameObject.tag == "warpstart4") {
				flag = 4;
				Debug.Log ("warpstart4+"+flag);
			}
			if (gameObject.tag == "warpdummy") {
				flag = 5;
				Debug.Log ("warpdummy+"+flag);
			}
			if (gameObject.tag == "warpdummy1") {
				flag = 5;
				Debug.Log ("warpdummy+"+flag);
			}
			if (gameObject.tag == "warpdummy2") {
				flag = 6;
				Debug.Log ("warpdummy+"+flag);
			}
			if (gameObject.tag == "warpdummy3") {
				flag = 7;
				Debug.Log ("warpdummy+"+flag);
			}
			if (gameObject.tag == "warpdummy4") {
				flag = 8;
				Debug.Log ("warpdummy+"+flag);
			}
			if (gameObject.tag == "warpdummy5") {
				flag = 9;
				Debug.Log ("warpdummy+"+flag);
			}
			if (gameObject.tag == "warpdummy6") {
				flag = 10;
				Debug.Log ("warpdummy+"+flag);
			}
			if (gameObject.tag == "warpdummy7") {
				flag = 11;
				Debug.Log ("warpdummy+"+flag);
			}
			if (gameObject.tag == "warpdummy8") {
				flag = 12;
				Debug.Log ("warpdummy+"+flag);
			}
			if (gameObject.tag == "warpfinal") {
				flag = 13;
				Debug.Log ("warpfinal+"+flag);
			}

		}

	}




}