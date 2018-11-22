using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;
using TMPro;
public class Movekey_Randam : MonoBehaviour {

	/*pacmanmoveではキーボードが↑←→↓で動くようになっている*/
	/*このスクリプトでは動く動作がランダムで配置される（キーボードの）*/
	/*pacmanmoveかMovekey_Randamのどちらかをplayerにつけることで動く*/


	public float speed = 0.1f;
	Vector2 dest = Vector2.zero;

	Warp warp;
	public int flag1 = 0;


	//TMP_Text is to use TextMeshPro
	//キーのテキストを表示する
	public TMP_Text key_upGUIText;
	public TMP_Text key_downGUIText;
	public TMP_Text key_rightGUIText;
	public TMP_Text key_leftGUIText;


	//ランダムかnot	ランダムかを決めるflag
	public int flag_randam = 0;
	public int Level_flag;

	//not ランダム

	//Level1
	private string[] arrayKey_Level1 = new string[]{"up","right","down","left"};

	//Level2
	private string[] arrayKey_Level2 = new string[]{"p","x","m","r"};
	//Level3 & Level4
	public string[] arraykey_Level3_4 = new string[]{"a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z","up","right","down","left"};
	public string[] arraykeyArrange = new string[4];
	public string[] arraykeyChange = new string[4];


	//ランダム配置


	// Use this for initialization
	void Start () {

		Level_flag = LevelSet.Level_flag;
		warp = GetComponent<Warp> ();


		dest = transform.position;

		if (Level_flag == 1) {
			Level1Keyboard ();
		} else if (Level_flag == 2) {
			Level2Keyboard ();
		} else if (Level_flag == 3) {
			Level3_4Keyboard ();
		} else if (Level_flag == 4) {
			Level3_4Keyboard ();
		} else {
			Level1Keyboard ();
		}
		/*
		if (flag_randam == 0) {
			//シャッフル
			randomarray ();
		} else {
			notrandomarray ();
		}*/


		//key文字表示
		keytext();





	}

	void Level1Keyboard(){
		for (int i = 0; i < 4; i++) {
			arraykeyArrange [i] = arrayKey_Level1 [i];
			Debug.Log (arraykeyArrange [i]);

		}
	}
	void Level2Keyboard(){
		for (int i = 0; i < 4; i++) {
			arraykeyArrange [i] = arrayKey_Level2 [i];
			Debug.Log (arraykeyArrange [i]);

		}
	}
	void Level3_4Keyboard(){
		System.Random rng = new System.Random ();
		int n = arraykey_Level3_4.Length;

		while (n > 1) {
			n--;
			var k = rng.Next (n + 1);
			var tmp = arraykey_Level3_4 [k];
			arraykey_Level3_4 [k] = arraykey_Level3_4 [n];
			arraykey_Level3_4 [n] = tmp;
		}

		for (int i = 0; i < 4; i++) {
			arraykeyArrange [i] = arraykey_Level3_4 [i];
			Debug.Log (arraykeyArrange [i]);

		}
	}


	/*
	//シャッフル
	void randomarray(){
		
		System.Random rng = new System.Random ();
		int n = arraykey.Length;

		while (n > 1) {
			n--;
			var k = rng.Next (n + 1);
			var tmp = arraykey [k];
			arraykey [k] = arraykey [n];
			arraykey [n] = tmp;
		}

		for (int i = 0; i < 4; i++) {
			arraykeyArrange [i] = arraykey [i];
			Debug.Log (arraykeyArrange [i]);

		}


			
	}

	void notrandomarray(){
		for (int i = 0; i < 4; i++) {
			arraykeyArrange [i] = arrayKey_Level1 [i];
			Debug.Log (arraykeyArrange [i]);

		}
	}*/

	void Update(){
		//Move closer to Destination



		Vector2 p = Vector2.MoveTowards(transform.position, dest, speed);
		GetComponent<Rigidbody2D> ().MovePosition (p);






		//check for input if not moving
		if((Vector2)transform.position == dest){
			if (Input.GetKey(arraykeyArrange[0]) && valid(Vector2.up))
				dest = (Vector2)transform.position + Vector2.up;
			if (Input.GetKey(arraykeyArrange[1]) && valid(Vector2.right))
				dest = (Vector2)transform.position + Vector2.right;
			if (Input.GetKey(arraykeyArrange[2]) && valid(-Vector2.up))
				dest = (Vector2)transform.position - Vector2.up;
			if (Input.GetKey(arraykeyArrange[3]) && valid(-Vector2.right))
				dest = (Vector2)transform.position - Vector2.right;
		}


	

		Vector2 dir = (dest - (Vector2)transform.position);
		GetComponent<Animator>().SetFloat ("DirX", dir.x);
		GetComponent<Animator>().SetFloat ("DirY", dir.y);
	}





	bool valid(Vector2 dir){
		//cast line from 'next to pac-man' to 'pac-man'
		Vector2 pos = transform.position;
		RaycastHit2D hit = Physics2D.Linecast (pos + dir, pos);
		return (hit.collider == GetComponent<Collider2D> ());

	}

	//キーの文字を表示させる
	void keytext(){
		key_upGUIText = GameObject.FindWithTag ("upkey").GetComponent<TextMeshProUGUI> ();
		key_downGUIText = GameObject.FindWithTag ("downkey").GetComponent<TextMeshProUGUI> ();
		key_rightGUIText = GameObject.FindWithTag ("rightkey").GetComponent<TextMeshProUGUI> ();
		key_leftGUIText = GameObject.FindWithTag ("leftkey").GetComponent<TextMeshProUGUI> ();
		Debug.Log (arraykeyArrange [0]);





		key_upGUIText.text = "Up key: " + arraykeyArrange [0];
		key_downGUIText.text = "Down key: " + arraykeyArrange [2];
		key_rightGUIText.text = "Right key: " + arraykeyArrange [1];
		key_leftGUIText.text = "Left key: " + arraykeyArrange [3];

	}

}
