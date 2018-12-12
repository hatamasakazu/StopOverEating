using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*このスクリプトはwarp地点のGameObjectに割り当てること*/

public class Warpstop : MonoBehaviour {

	public int flag_stop = 0;

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Soldier"||other.tag == "Death"||other.tag == "soldierwhite"||other.tag == "soldierblack") {
			flag_stop = 1;
			

		}
	}
}
