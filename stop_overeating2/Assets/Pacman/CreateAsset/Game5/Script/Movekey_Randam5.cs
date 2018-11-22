using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;
using TMPro;
public class Movekey_Randam5 : MonoBehaviour {

	/*pacmanmoveではキーボードが↑←→↓で動くようになっている*/
	/*このスクリプトでは動く動作がランダムで配置される（キーボードの）*/
	/*pacmanmoveかMovekey_Randamのどちらかをplayerにつけることで動く*/

	/*GAME3よりwarp機能を追加した*/
	/*具体的にはスタート位置にwarpスクリプトを追加し移動したい位置にwarpstopスクリプトを追加*/


	public float speed = 0.1f;
	Vector2 dest = Vector2.zero;
	/*warpに必要な変数*/
	//移動したいスタート位置
	Warp warp_scri;
	GameObject warp_obj;

	Warp warp_scri2;
	GameObject warp_obj2;

	Warp warp_scri3;
	GameObject warp_obj3;

	Warp warp_scri4;
	GameObject warp_obj4;

	Warp warp_dummyscri1;
	GameObject warp_dummyobj1;

	Warp warp_dummyscri2;
	GameObject warp_dummyobj2;

	Warp warp_dummyscri3;
	GameObject warp_dummyobj3;

	Warp warp_dummyscri4;
	GameObject warp_dummyobj4;

	Warp warp_dummyscri5;
	GameObject warp_dummyobj5;

	Warp warp_dummyscri6;
	GameObject warp_dummyobj6;

	Warp warp_dummyscri7;
	GameObject warp_dummyobj7;

	Warp warp_dummyscri8;
	GameObject warp_dummyobj8;


	Warp warp_finalscri;
	GameObject warp_finalobj;

	//移動したいゴール位置
	Warpstop warp_stop_scri;
	GameObject warp_stop_obj;

	Warpstop warp_stop_scri2;
	GameObject warp_stop_obj2;

	Warpstop warp_stop_scri3;
	GameObject warp_stop_obj3;

	Warpstop warp_stop_scri4;
	GameObject warp_stop_obj4;

	Warpstop warp_dummystop_scri;
	GameObject warp_dummystop_obj;



	public Transform[] waypoints;
	/*warpここまで*/



	//TMP_Text is to use TextMeshPro
	//キーのテキストを表示する
	public TMP_Text key_upGUIText;
	public TMP_Text key_downGUIText;
	public TMP_Text key_rightGUIText;
	public TMP_Text key_leftGUIText;


	//ランダムかnot	ランダムかを決めるflag
	public int flag_randam = 0;
	public int Level_flag;



	PackStage2 packstage;
	GameObject stagewarp;
	/*
	//ランダムかnot	ランダムかを決めるflag
	public int flag_randam = 0;


	//not ランダム
	private string[] arraynotKey = new string[]{"up","right","down","left"};


	//ランダム配置

	public string[] arraykey = new string[]{"a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z","up","right","down","left"};
	public string[] arraykeyArrange = new string[4];
	public string[] arraykeyChange = new string[4];

	*/
	//not ランダム

	//Level1
	private string[] arrayKey_Level1 = new string[]{"up","right","down","left"};

	//Level2
	private string[] arrayKey_Level2 = new string[]{"p","x","m","r"};
	//Level3 & Level4
	public string[] arraykey_Level3_4 = new string[]{"a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z","up","right","down","left"};
	public string[] arraykeyArrange = new string[4];
	public string[] arraykeyChange = new string[4];





	// Use this for initialization
	void Start () {
		Level_flag = LevelSet.Level_flag;


		/*warpスタート*/
		warp_obj = GameObject.FindWithTag ("warpstart");
		warp_scri = warp_obj.GetComponent<Warp>();

		warp_obj2 = GameObject.FindWithTag ("warpstart2");
		warp_scri2 = warp_obj2.GetComponent<Warp>();

		warp_obj3 = GameObject.FindWithTag ("warpstart3");
		warp_scri3 = warp_obj3.GetComponent<Warp>();

		warp_obj4 = GameObject.FindWithTag ("warpstart4");
		warp_scri4 = warp_obj4.GetComponent<Warp>();

		warp_dummyobj1 = GameObject.Find ("WarpPoint_dummy1");
		warp_dummyscri1 = warp_dummyobj1.GetComponent<Warp>();

		warp_dummyobj2 = GameObject.Find ("WarpPoint_dummy2");
		warp_dummyscri2 = warp_dummyobj2.GetComponent<Warp>();

		warp_dummyobj3 = GameObject.Find ("WarpPoint_dummy3");
		warp_dummyscri3 = warp_dummyobj3.GetComponent<Warp>();

		warp_dummyobj4 = GameObject.Find ("WarpPoint_dummy4");
		warp_dummyscri4 = warp_dummyobj4.GetComponent<Warp>();

		warp_dummyobj5 = GameObject.Find ("WarpPoint_dummy5");
		warp_dummyscri5 = warp_dummyobj5.GetComponent<Warp>();

		warp_dummyobj6 = GameObject.Find ("WarpPoint_dummy6");
		warp_dummyscri6 = warp_dummyobj6.GetComponent<Warp>();

		warp_dummyobj7 = GameObject.Find ("WarpPoint_dummy7");
		warp_dummyscri7 = warp_dummyobj7.GetComponent<Warp>();

		warp_dummyobj8 = GameObject.Find ("WarpPoint_dummy8");
		warp_dummyscri8 = warp_dummyobj8.GetComponent<Warp>();





		warp_finalobj = GameObject.FindWithTag ("warpfinal");
		warp_finalscri = warp_finalobj.GetComponent<Warp>();

		/*warpゴール*/
		warp_stop_obj = GameObject.FindWithTag ("warpstop");
		warp_stop_scri = warp_stop_obj.GetComponent<Warpstop> ();

		warp_stop_obj2 = GameObject.FindWithTag ("warpstop2");
		warp_stop_scri2 = warp_stop_obj2.GetComponent<Warpstop> ();

		warp_stop_obj3 = GameObject.FindWithTag ("warpstop3");
		warp_stop_scri3 = warp_stop_obj3.GetComponent<Warpstop> ();

		warp_stop_obj4 = GameObject.FindWithTag ("warpstop4");
		warp_stop_scri4 = warp_stop_obj4.GetComponent<Warpstop> ();

		warp_dummystop_obj = GameObject.FindWithTag ("warpdummystop");
		warp_dummystop_scri = warp_dummystop_obj.GetComponent<Warpstop> ();


		dest = transform.position;

		/*
		if (flag_randam == 0) {
			//シャッフル
			randomarray ();
		} else {
			notrandomarray ();
		}*/

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
			arraykeyArrange [i] = arraynotKey [i];
			Debug.Log (arraykeyArrange [i]);

		}
	}*/

	void Update(){
		//Move closer to Destination

		/*warp_scriのflagが1の時、warpする*/

		if (warp_scri.flag == 1) {
			Debug.Log ("warp_scri.flag=1");
			gameObject.transform.position = new Vector2 (waypoints [1].transform.position.x, waypoints [1].transform.position.y);

			dest = transform.position;
			/*warpが設定した場所に跳んだときwarp_scri_flagが0となる*/
			if (warp_stop_scri.flag_stop == 1) {
				warp_scri.flag = 0;
				warp_scri2.flag = 0;
				warp_scri3.flag = 0;
				warp_scri4.flag = 0;
				warp_dummyscri1.flag = 0;
				warp_dummyscri2.flag = 0;
				warp_dummyscri3.flag = 0;
				warp_dummyscri4.flag = 0;
				warp_dummyscri5.flag = 0;
				warp_dummyscri6.flag = 0;
				warp_dummyscri7.flag = 0;
				warp_dummyscri8.flag = 0;
				warp_finalscri.flag = 0;
			}


		} else if (warp_scri2.flag == 2) {
			Debug.Log ("warp_stop_scri2.flag_stop=2");
			gameObject.transform.position = new Vector2 (waypoints [2].transform.position.x, waypoints [2].transform.position.y);

			dest = transform.position;
			/*warpが設定した場所に跳んだときwarp_scri2_flagが0となる*/
			if (warp_stop_scri2.flag_stop == 1) {
				warp_scri.flag = 0;
				warp_scri2.flag = 0;
				warp_scri3.flag = 0;
				warp_scri4.flag = 0;
				warp_dummyscri1.flag = 0;
				warp_dummyscri2.flag = 0;
				warp_dummyscri3.flag = 0;
				warp_dummyscri4.flag = 0;
				warp_dummyscri5.flag = 0;
				warp_dummyscri6.flag = 0;
				warp_dummyscri7.flag = 0;
				warp_dummyscri8.flag = 0;
				warp_finalscri.flag = 0;
			}
		}else if (warp_scri3.flag == 3) {
			Debug.Log ("warp_stop_scri3.flag_stop=3");
			gameObject.transform.position = new Vector2 (waypoints [3].transform.position.x, waypoints [3].transform.position.y);

			dest = transform.position;
			/*warpが設定した場所に跳んだときwarp_scri3_flagが0となる*/
			if (warp_stop_scri3.flag_stop == 1) {
				warp_scri.flag = 0;
				warp_scri2.flag = 0;
				warp_scri3.flag = 0;
				warp_scri4.flag = 0;
				warp_dummyscri1.flag = 0;
				warp_dummyscri2.flag = 0;
				warp_dummyscri3.flag = 0;
				warp_dummyscri4.flag = 0;
				warp_dummyscri5.flag = 0;
				warp_dummyscri6.flag = 0;
				warp_dummyscri7.flag = 0;
				warp_dummyscri8.flag = 0;
				warp_finalscri.flag = 0;
			}
		}else if (warp_scri4.flag == 4) {
			Debug.Log ("warp_stop_scri4.flag_stop=4");
			gameObject.transform.position = new Vector2 (waypoints [4].transform.position.x, waypoints [4].transform.position.y);

			dest = transform.position;
			/*warpが設定した場所に跳んだときwarp_scri4_flagが0となる*/
			if (warp_stop_scri4.flag_stop == 1) {
				warp_scri.flag = 0;
				warp_scri2.flag = 0;
				warp_scri3.flag = 0;
				warp_scri4.flag = 0;
				warp_dummyscri1.flag = 0;
				warp_dummyscri2.flag = 0;
				warp_dummyscri3.flag = 0;
				warp_dummyscri4.flag = 0;
				warp_dummyscri5.flag = 0;
				warp_dummyscri6.flag = 0;
				warp_dummyscri7.flag = 0;
				warp_dummyscri8.flag = 0;
				warp_finalscri.flag = 0;
			}
		}
		else if (warp_dummyscri1.flag == 5) {
			Debug.Log ("warp_dummyscri.flag=5");
			gameObject.transform.position = new Vector2 (waypoints [5].transform.position.x, waypoints [5].transform.position.y);

			dest = transform.position;
			/*warpが設定した場所に跳んだときwarp_scri4_flagが0となる*/
			if (warp_dummystop_scri.flag_stop == 1) {
				warp_scri.flag = 0;
				warp_scri2.flag = 0;
				warp_scri3.flag = 0;
				warp_scri4.flag = 0;
				warp_dummyscri1.flag = 0;
				warp_dummyscri2.flag = 0;
				warp_dummyscri3.flag = 0;
				warp_dummyscri4.flag = 0;
				warp_dummyscri5.flag = 0;
				warp_dummyscri6.flag = 0;
				warp_dummyscri7.flag = 0;
				warp_dummyscri8.flag = 0;
				warp_finalscri.flag = 0;
			}
		}
		else if (warp_dummyscri2.flag == 6) {
			Debug.Log ("warp_dummyscri.flag=5");
			gameObject.transform.position = new Vector2 (waypoints [5].transform.position.x, waypoints [5].transform.position.y);

			dest = transform.position;
			/*warpが設定した場所に跳んだときwarp_scri4_flagが0となる*/
			if (warp_dummystop_scri.flag_stop == 1) {
				warp_scri.flag = 0;
				warp_scri2.flag = 0;
				warp_scri3.flag = 0;
				warp_scri4.flag = 0;
				warp_dummyscri1.flag = 0;
				warp_dummyscri2.flag = 0;
				warp_dummyscri3.flag = 0;
				warp_dummyscri4.flag = 0;
				warp_dummyscri5.flag = 0;
				warp_dummyscri6.flag = 0;
				warp_dummyscri7.flag = 0;
				warp_dummyscri8.flag = 0;
				warp_finalscri.flag = 0;
			}
		}
		else if (warp_dummyscri3.flag == 7) {
			Debug.Log ("warp_dummyscri.flag=5");
			gameObject.transform.position = new Vector2 (waypoints [5].transform.position.x, waypoints [5].transform.position.y);

			dest = transform.position;
			/*warpが設定した場所に跳んだときwarp_scri4_flagが0となる*/
			if (warp_dummystop_scri.flag_stop == 1) {
				warp_scri.flag = 0;
				warp_scri2.flag = 0;
				warp_scri3.flag = 0;
				warp_scri4.flag = 0;
				warp_dummyscri1.flag = 0;
				warp_dummyscri2.flag = 0;
				warp_dummyscri3.flag = 0;
				warp_dummyscri4.flag = 0;
				warp_dummyscri5.flag = 0;
				warp_dummyscri6.flag = 0;
				warp_dummyscri7.flag = 0;
				warp_dummyscri8.flag = 0;
				warp_finalscri.flag = 0;
			}
		}
		else if (warp_dummyscri4.flag == 8) {
			Debug.Log ("warp_dummyscri.flag=5");
			gameObject.transform.position = new Vector2 (waypoints [5].transform.position.x, waypoints [5].transform.position.y);

			dest = transform.position;
			/*warpが設定した場所に跳んだときwarp_scri4_flagが0となる*/
			if (warp_dummystop_scri.flag_stop == 1) {
				warp_scri.flag = 0;
				warp_scri2.flag = 0;
				warp_scri3.flag = 0;
				warp_scri4.flag = 0;
				warp_dummyscri1.flag = 0;
				warp_dummyscri2.flag = 0;
				warp_dummyscri3.flag = 0;
				warp_dummyscri4.flag = 0;
				warp_dummyscri5.flag = 0;
				warp_dummyscri6.flag = 0;
				warp_dummyscri7.flag = 0;
				warp_dummyscri8.flag = 0;
				warp_finalscri.flag = 0;
			}
		}
		else if (warp_dummyscri5.flag == 9) {
			Debug.Log ("warp_dummyscri.flag=5");
			gameObject.transform.position = new Vector2 (waypoints [1].transform.position.x, waypoints [1].transform.position.y);

			dest = transform.position;
			/*warpが設定した場所に跳んだときwarp_scri4_flagが0となる*/
			if (warp_dummystop_scri.flag_stop == 1) {
				warp_scri.flag = 0;
				warp_scri2.flag = 0;
				warp_scri3.flag = 0;
				warp_scri4.flag = 0;
				warp_dummyscri1.flag = 0;
				warp_dummyscri2.flag = 0;
				warp_dummyscri3.flag = 0;
				warp_dummyscri4.flag = 0;
				warp_dummyscri5.flag = 0;
				warp_dummyscri6.flag = 0;
				warp_dummyscri7.flag = 0;
				warp_dummyscri8.flag = 0;
				warp_finalscri.flag = 0;
			}
		}
		else if (warp_dummyscri6.flag == 10) {
			Debug.Log ("warp_dummyscri.flag=5");
			gameObject.transform.position = new Vector2 (waypoints [5].transform.position.x, waypoints [5].transform.position.y);

			dest = transform.position;
			/*warpが設定した場所に跳んだときwarp_scri4_flagが0となる*/
			if (warp_dummystop_scri.flag_stop == 1) {
				warp_scri.flag = 0;
				warp_scri2.flag = 0;
				warp_scri3.flag = 0;
				warp_scri4.flag = 0;
				warp_dummyscri1.flag = 0;
				warp_dummyscri2.flag = 0;
				warp_dummyscri3.flag = 0;
				warp_dummyscri4.flag = 0;
				warp_dummyscri5.flag = 0;
				warp_dummyscri6.flag = 0;
				warp_dummyscri7.flag = 0;
				warp_dummyscri8.flag = 0;
				warp_finalscri.flag = 0;
			}
		}
		else if (warp_dummyscri7.flag == 11) {
			Debug.Log ("warp_dummyscri.flag=5");
			gameObject.transform.position = new Vector2 (waypoints [1].transform.position.x, waypoints [1].transform.position.y);

			dest = transform.position;
			/*warpが設定した場所に跳んだときwarp_scri4_flagが0となる*/
			if (warp_dummystop_scri.flag_stop == 1) {
				warp_scri.flag = 0;
				warp_scri2.flag = 0;
				warp_scri3.flag = 0;
				warp_scri4.flag = 0;
				warp_dummyscri1.flag = 0;
				warp_dummyscri2.flag = 0;
				warp_dummyscri3.flag = 0;
				warp_dummyscri4.flag = 0;
				warp_dummyscri5.flag = 0;
				warp_dummyscri6.flag = 0;
				warp_dummyscri7.flag = 0;
				warp_dummyscri8.flag = 0;
				warp_finalscri.flag = 0;
			}
		}
		else if (warp_dummyscri8.flag == 12) {
			Debug.Log ("warp_dummyscri.flag=5");
			gameObject.transform.position = new Vector2 (waypoints [1].transform.position.x, waypoints [1].transform.position.y);

			dest = transform.position;
			/*warpが設定した場所に跳んだときwarp_scri4_flagが0となる*/
			if (warp_dummystop_scri.flag_stop == 1) {
				warp_scri.flag = 0;
				warp_scri2.flag = 0;
				warp_scri3.flag = 0;
				warp_scri4.flag = 0;
				warp_dummyscri1.flag = 0;
				warp_dummyscri2.flag = 0;
				warp_dummyscri3.flag = 0;
				warp_dummyscri4.flag = 0;
				warp_dummyscri5.flag = 0;
				warp_dummyscri6.flag = 0;
				warp_dummyscri7.flag = 0;
				warp_dummyscri8.flag = 0;
				warp_finalscri.flag = 0;
			}
		}
		else if (warp_finalscri.flag == 13) {
			Debug.Log ("warp_finalscri.flag == 6");
			gameObject.transform.position = new Vector2 (waypoints [6].transform.position.x, waypoints [6].transform.position.y);

			dest = transform.position;
			/*warpが設定した場所に跳んだときwarp_scri4_flagが0となる*/
			if (warp_dummystop_scri.flag_stop == 1) {
				warp_scri.flag = 0;
				warp_scri2.flag = 0;
				warp_scri3.flag = 0;
				warp_scri4.flag = 0;
				warp_dummyscri1.flag = 0;
				warp_dummyscri2.flag = 0;
				warp_dummyscri3.flag = 0;
				warp_dummyscri4.flag = 0;
				warp_dummyscri5.flag = 0;
				warp_dummyscri6.flag = 0;
				warp_dummyscri7.flag = 0;
				warp_dummyscri8.flag = 0;
				warp_finalscri.flag = 0;
			}
		}
		else if (warp_scri.flag == 0 && warp_scri2.flag == 0 && warp_scri3.flag == 0 && warp_scri4.flag == 0 && warp_dummyscri1.flag == 0 && warp_finalscri.flag == 0 && warp_dummyscri2.flag == 0 && warp_dummyscri3.flag == 0 && warp_dummyscri4.flag == 0 && warp_dummyscri5.flag == 0 && warp_dummyscri6.flag == 0 && warp_dummyscri7.flag == 0 && warp_dummyscri8.flag == 0) {
			Debug.Log ("warp_scri.flag=0"); 
			//Debug.Log ("flag2" + warp_scri.flag);

			//Debug.Log ("transform" + transform.position);
			//Debug.Log ("dest" + dest);
			Vector2 p = Vector2.MoveTowards (transform.position, dest, speed);
			GetComponent<Rigidbody2D> ().MovePosition (p);





			//check for input if not moving
			if ((Vector2)transform.position == dest) {
				if (Input.GetKey (arraykeyArrange [0]) && valid (Vector2.up))
					dest = (Vector2)transform.position + Vector2.up;
				if (Input.GetKey (arraykeyArrange [1]) && valid (Vector2.right))
					dest = (Vector2)transform.position + Vector2.right;
				if (Input.GetKey (arraykeyArrange [2]) && valid (-Vector2.up))
					dest = (Vector2)transform.position - Vector2.up;
				if (Input.GetKey (arraykeyArrange [3]) && valid (-Vector2.right))
					dest = (Vector2)transform.position - Vector2.right;
			}




			Vector2 dir = (dest - (Vector2)transform.position);
			GetComponent<Animator> ().SetFloat ("DirX", dir.x);
			GetComponent<Animator> ().SetFloat ("DirY", dir.y);
		}
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