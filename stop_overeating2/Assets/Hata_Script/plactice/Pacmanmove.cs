using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Pacmanmove : MonoBehaviour {

	public float speed = 0.4f;
	Vector2 dest = Vector2.zero;

	//ジョイスティック
	public float moveForce = 5, boostMultiplier = 2;
	Rigidbody2D myBody;



	// Use this for initialization
	void Start () {
		myBody = this.GetComponent<Rigidbody2D> ();
		dest = transform.position;
	}
	
	void FixedUpdate(){
		//Move closer to Destination

		Vector2 p = Vector2.MoveTowards(transform.position, dest, speed);
		GetComponent<Rigidbody2D> ().MovePosition (p);


		//ジョイスティック
		/*Vector2 moveVec = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"),CrossPlatformInputManager.GetAxis("Vertical"))*moveForce;
		bool isBoosting = CrossPlatformInputManager.GetButton ("Boost");
		Debug.Log(isBoosting ? boostMultiplier : 1);
		myBody.AddForce(moveVec *(isBoosting ? boostMultiplier : 1));
		*/

		//ジョイスティック2
		/*float vertical = CrossPlatformInputManager.GetAxis("Vertical");
		float horizontal = CrossPlatformInputManager.GetAxis ("Horizontal");
		Vector2 addPos = new Vector2 (horizontal, vertical) * speed;
*/
		//ジョイスティック3
		var v1 = CrossPlatformInputManager.GetAxis("Horizontal") * 1.0f ;
		var v2 = CrossPlatformInputManager.GetAxis ("Vertical")*1.0f;
		Vector2 addPos = new Vector2 (v1, v2)*speed;
		Debug.Log ("v1+" + v1);
		Debug.Log ("v2+" + v2);
		//check for input if not moving
		if((Vector2)transform.position == dest){
			if (Input.GetKey(KeyCode.UpArrow) && valid(Vector2.up))
				dest = (Vector2)transform.position + Vector2.up;
			if (Input.GetKey(KeyCode.RightArrow) && valid(Vector2.right))
				dest = (Vector2)transform.position + Vector2.right;
			if (Input.GetKey(KeyCode.DownArrow) && valid(-Vector2.up))
				dest = (Vector2)transform.position - Vector2.up;
			if (Input.GetKey(KeyCode.LeftArrow) && valid(-Vector2.right))
				dest = (Vector2)transform.position - Vector2.right;
		}

		//ジョイスティック
		float v3= Mathf.Abs(v1) - Mathf.Abs(v2);
		if ((Vector2)transform.position == dest) {
			if (v1 > 0 && v2 > 0 && v3 > 0) {
				dest = ((Vector2)transform.position + Vector2.right);
			} else if (v1 > 0 && v2 > 0 && v3 < 0) {
				dest = ((Vector2)transform.position + Vector2.up);
			} else if (v1 < 0 && v2 > 0 && v3 > 0) {
				dest = ((Vector2)transform.position - Vector2.right);
			} else if (v1 < 0 && v2 > 0 && v3 < 0) {
				dest = (Vector2)transform.position + Vector2.up;
			} else if (v1 > 0 && v2 < 0 && v3 > 0) {
				dest = (Vector2)transform.position + Vector2.right;
			} else if (v1 > 0 && v2 < 0 && v3 < 0) {
				dest = (Vector2)transform.position - Vector2.up;
			} else if (v1 < 0 && v2 < 0 && v3 > 0) {
				dest = (Vector2)transform.position - Vector2.right;
			} else if (v1 < 0 && v2 < 0 && v3 < 0) {
				dest = (Vector2)transform.position - Vector2.up;
			}
		}

		//タッチ移動する
		if(Input.touchCount > 0){
			if ((Vector2)transform.position == dest) {
				Touch touch = Input.GetTouch (0);
				Debug.Log ("touch");
				float x = touch.deltaPosition.x;
				float y = touch.deltaPosition.y;
				float z = Mathf.Abs (x) - Mathf.Abs (y);
				if (x > 0 && y > 0 && z > 0) {
					dest = (Vector2)transform.position + Vector2.right;
				} else if (x > 0 && y > 0 && z < 0) {
					dest = (Vector2)transform.position + Vector2.up;
				} else if (x < 0 && y > 0 && z > 0) {
					dest = (Vector2)transform.position - Vector2.right;
				} else if (x < 0 && y > 0 && z < 0) {
					dest = (Vector2)transform.position + Vector2.up;
				} else if (x > 0 && y < 0 && z > 0) {
					dest = (Vector2)transform.position + Vector2.right;
				} else if (x > 0 && y < 0 && z < 0) {
					dest = (Vector2)transform.position - Vector2.up;
				} else if (x < 0 && y < 0 && z > 0) {
					dest = (Vector2)transform.position - Vector2.right;
				} else if (x < 0 && y < 0 && z < 0) {
					dest = (Vector2)transform.position - Vector2.up;
				}
				
			}
		}

		Vector2 dir = (dest - (Vector2)transform.position);
		GetComponent<Animator>().SetFloat ("DirX", dir.x);
		GetComponent<Animator>().SetFloat ("DirY", dir.y);
	}
	/*
	void OnCollisionEnter(Collider2D other){
		if (other.gameObject.tag == "maze") {
			Debug.Log ("衝突");
		}
	}

	void OnCollisionStay2D(Collider2D other){
		if (other.gameObject.tag == "maze") {
			Debug.Log ("衝突2");
		}
	}
*/
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "maze") {
			Debug.Log ("衝突3");
		}
	}

	bool valid(Vector2 dir){
		//cast line from 'next to pac-man' to 'pac-man'
		Vector2 pos = transform.position;
		RaycastHit2D hit = Physics2D.Linecast (pos + dir, pos);
		return (hit.collider == GetComponent<Collider2D> ());

	}
}
