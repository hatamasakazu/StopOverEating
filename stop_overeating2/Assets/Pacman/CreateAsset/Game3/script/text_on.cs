using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class text_on : MonoBehaviour {


	public Text a;
	public Text b;
	public Text c;
	public Text d;
	public Text e;
	public Text f;

	public int foodcaltext_a;
	public int foodcaltext_b;
	public int foodcaltext_c;
	public int foodcaltext_d;
	public int foodcaltext_e;
	public int foodcaltext_f;

	// Use this for initialization
	void Start () {
		a.text = "   A       " + foodcaltext_a;
		b.text = "   B       " + foodcaltext_b;
		c.text = "   C       " + foodcaltext_c;
		d.text = "   D       " + foodcaltext_d;
		e.text = "   E       " + foodcaltext_e;
		f.text = "   F       " + foodcaltext_f;
	}
	

}
