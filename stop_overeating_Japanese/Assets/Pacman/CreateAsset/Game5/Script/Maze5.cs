using UnityEngine;
using System.Collections;

public class Maze5 : MonoBehaviour
{

	// public GameObject pacdot;
	//public GameObject pacdot2;
	public GameObject[] foods;
	public GameObject[] foods2;
	public GameObject[] foods3;
	public GameObject[] foods4;
	public GameObject[] cols;

	public int flag = 0;


	// Use this for initialization
	void Start()
	{
		AddPacdot();
	}



	void AddPacdot()
	{
		ArrayList data = new ArrayList();
		ArrayList data2 = new ArrayList ();
		ArrayList data3 = new ArrayList ();
		ArrayList data4 = new ArrayList ();


		if (flag == 0) {
			//from top to down

			//top of the left

			data.Add ("2.5,4.5,6.5,9.5,11.5,13.5");  //1
			data.Add ("0.5,2.5,4.5,6.5,9.5,11.5,13.5,15.5");  //2
			data.Add ("0.5,2.5,4.5,6.5,9.5,11.5,13.5,15.5");  //3
			data.Add ("0.5,2.5,4.5,9.5,11.5,13.5,15.5");  //4

			data.Add ("0.5,2.5,4.5");  //5
			data.Add ("0.5,2.5,4.5,6.5");  //6
			data.Add ("0.5,2.5,4.5,6.5");  //7
			data.Add ("0.5,2.5,4.5,6.5");  //8


			//top of the right
			data2.Add ("18.5,20.5,22.5,24.5,27,29,31");  //1
			data2.Add ("18.5,20.5,22.5,24.5,27,29,31,33");  //2
			data2.Add ("18.5,20.5,22.5,24.5,27,29,31,33");  //3
			data2.Add ("18.5,20.5,22.5,24.5,27,29,31,33");  //4
			data2.Add ("20.5,22.5,24.5,27,29,31,33");  //5
			data2.Add ("18.5,20.5,22.5,24.5,27,29,31,33");  //6
			data2.Add ("18.5,20.5,22.5,24.5,27,29,31,33");  //7
			data2.Add ("18.5,20.5,22.5,24.5,29,31,33");  //8

			//down of the left

			data3.Add ("0.5,2.5,4.5,6.5,11.5,13.5,15.5");  //1
			data3.Add ("0.5,2.5,4.5,6.5,9.5,11.5,13.5,15.5");  //2
			data3.Add ("0.5,2.5,4.5,6.5,9.5,11.5");  //3
			data3.Add ("2.5,4.5,6.5,9.5,11.5");  //4

			data3.Add ("0.5,2.5,4.5,6.5,9.5,11.5,13.5,15.5");  //5
			data3.Add ("0.5,2.5,4.5,6.5,9.5,11.5,13.5,15.5");  //6
			data3.Add ("0.5,2.5,4.5,6.5,9.5,11.5,13.5,15.5");  //7
			data3.Add ("2.5,4.5,6.5,9.5,11.5,13.5");  //8

			//down of the right
			data4.Add ("18.5,20.5,22.5,24.5,29,31,33");  //1
			data4.Add ("18.5,20.5,22.5,24.5,27,29,31,33");  //2
			data4.Add ("18.5,20.5,22.5,24.5,27,29,31,33");	//3
			data4.Add ("20.5,22.5,24.5,27,29,31,33");  //4

			data4.Add ("18.5,20.5,22.5,27,29,31");  //5
			data4.Add ("18.5,20.5,22.5,24.5,27,29,31,33");  //6
			data4.Add ("18.5,20.5,22.5,24.5,27,29,31,33");  //7
			data4.Add ("18.5,20.5,22.5,24.5,27,29,31,33");  //8


	
	

		

		} else if (flag == 1) {
			//from top to down

			//top of the left

			data.Add ("2.5,6.5,11.5");  //1
			data.Add ("0.5,4.5,9.5,13.5");  //2
			data.Add ("2.5,6.5,11.5,15.5");  //3
			data.Add ("0.5,4.5,9.5,13.5");  //4

			data.Add ("0.5,4.5");  //5
			data.Add ("2.5,6.5");  //6
			data.Add ("0.5,4.5");  //7
			data.Add ("2.5,6.5");  //8


			//top of the right
			data2.Add ("18.5,22.5,27,31");  //1
			data2.Add ("20.5,24.5,29,33");  //2
			data2.Add ("18.5,22.5,27,31");  //3
			data2.Add ("20.5,24.5,29,33");  //4
			data2.Add ("20.5,24.5,29,33");  //5
			data2.Add ("18.5,22.5,27,31");  //6
			data2.Add ("20.5,24.5,29,33");  //7
			data2.Add ("18.5,22.5,29,33");  //8

			//down of the left

			data3.Add ("2.5,6.5,11.5,15.5");  //1
			data3.Add ("0.5,4.5,9.5,13.5");  //2
			data3.Add ("2.5,6.5,11.5,15.5");  //3
			data3.Add ("4.5,9.5");  //4

			data3.Add ("0.5,4.5,11.5,15.5");  //5
			data3.Add ("2.5,6.5,9.5,13.5");  //6
			data3.Add ("0.5,4.5,11.5,15.5");  //7
			data3.Add ("2.5,6.5,9.5,13.5");  //8

			//down of the right
			data4.Add ("18.5,22.5,29,33");  //1
			data4.Add ("20.5,24.5,27,31");  //2
			data4.Add ("18.5,22.5,29,33");	//3
			data4.Add ("20.5,24.5,27,31");  //4

			data4.Add ("18.5,22.5,27,31");  //5
			data4.Add ("20.5,24.5,29,33");  //6
			data4.Add ("18.5,22.5,27,31");  //7
			data4.Add ("20.5,24.5,29,33");  //8






		}

		//colliderを追加する
		ArrayList coldata = new ArrayList ();

		coldata.Add ("2,4,6,8,10,12,14,16,18,20,22,24,26,28,30,32");	//1
		coldata.Add ("2,4,6,8,10,12,14,16,18,20,22,24,26,28,30,32");	//2
		coldata.Add ("2,4,6,8,10,12,14,16,18,20,22,24,26,28,30,32");	//3
		coldata.Add ("2,4,6,8,10,12,14,16,18,20,22,24,26,28,30,32");	//4
		coldata.Add ("2,4,6,8,10,12,14,16,18,20,22,24,26,28,30,32");	//5
		coldata.Add ("2,4,6,8,10,12,14,16,18,20,22,24,26,28,30,32");	//6
		coldata.Add ("2,4,6,8,10,12,14,16,18,20,22,24,26,28,30,32");	//7
		coldata.Add ("2,4,6,8,10,12,14,16,18,20,22,24,26,28,30,32");	//8
		coldata.Add ("2,4,6,8,10,12,14,16,18,20,22,24,26,28,30,32");	//9
		coldata.Add ("2,4,6,8,10,12,14,16,18,20,22,24,26,28,30,32");	//10
		coldata.Add ("2,4,6,8,10,12,14,16,18,20,22,24,26,28,30,32");	//11
		coldata.Add ("2,4,6,8,10,12,14,16,18,20,22,24,26,28,30,32");	//12
		coldata.Add ("2,4,6,8,10,12,14,16,18,20,22,24,26,28,30,32");	//13
		coldata.Add ("2,4,6,8,10,12,14,16,18,20,22,24,26,28,30,32");	//14
		coldata.Add ("2,4,6,8,10,12,14,16,18,20,22,24,26,28,30,32");	//15
		coldata.Add ("2,4,6,8,10,12,14,16,18,20,22,24,26,28,30,32");	//16
		coldata.Add ("2,4,6,8,10,12,14,16,18,20,22,24,26,28,30,32");	//17
		coldata.Add ("2,4,6,8,10,12,14,16,18,20,22,24,26,28,30,32");	//18

		GameObject obj;
		GameObject obj2;
		GameObject obj3;
		GameObject obj4;
		GameObject obj_col;




		//pacdot.transform.position = new Vector3(2, 30, 0);  //先頭は座標変更のみ

		/*data1*/

		float ad=0.5f;
		for (int i = 0; i < data.Count; i++)
		{
			var xs = data[i].ToString().Split(',');

			if (i <= 3) {
				
					for (int j = 0; j < xs.Length; j++) {
						int number = Random.Range (0, foods.Length);
						obj = (GameObject)Instantiate (foods [number], new Vector3 (float.Parse (xs [j]) - 17, -ad + 18, 0), Quaternion.identity);
					obj.name = foods [number].name+i+j;
						obj.transform.parent = this.transform;
					}
					ad = ad + 2.0f;
				if (i == 3) {
					ad = ad + 1.5f;
				}
			} else if (i > 3 && i <= 7) {
				

				//ad = ad + 0.5f;
				for (int j = 0; j < xs.Length; j++) {
					int number = Random.Range (0, foods.Length);
					obj = (GameObject)Instantiate (foods [number], new Vector3 (float.Parse (xs [j]) - 17, -ad + 18, 0), Quaternion.identity);
					obj.name = foods [number].name+i+j;
					obj.transform.parent = this.transform;

				}
				ad = ad + 2.0f;
			}
				

		}


		float ad2=0.5f;
		for (int i = 0; i < data2.Count; i++) {
			var xs2 = data2 [i].ToString ().Split (',');

			if (i <= 3) {

				for (int j = 0; j < xs2.Length; j++) {
					int number2 = Random.Range (0, foods2.Length);
					obj2 = (GameObject)Instantiate (foods2 [number2], new Vector3 (float.Parse (xs2 [j]) - 17, -ad2 + 18, 0), Quaternion.identity);
					obj2.name = foods2 [number2].name + i+j;
					obj2.transform.parent = this.transform;

				}
				ad2 = ad2 + 2.0f;
			if (i == 3) {
				ad2 = ad2 + 1.5f;
			}
			} else if (i > 3 && i <= 8) {
			//ad = ad - 1.5f;
			for (int j = 0; j < xs2.Length; j++) {
				int number2 = Random.Range (0, foods2.Length);
				obj2 = (GameObject)Instantiate (foods2 [number2], new Vector3 (float.Parse (xs2 [j]) - 17, -ad2 + 18, 0), Quaternion.identity);
				obj2.name = foods2 [number2].name + i+j;
				obj2.transform.parent = this.transform;

			}
			ad2 = ad2 + 2.0f;
			}
		}


		//y座標の距離
		float ad3 = 19.5f;

		for (int i = 0; i < data3.Count; i++) {
			var xs3 = data3 [i].ToString ().Split (',');

			if (i <= 3) {
				for (int j = 0; j < xs3.Length; j++) {
					int number3 = Random.Range (0, foods3.Length);
					obj3 = (GameObject)Instantiate (foods3 [number3], new Vector3 (float.Parse (xs3 [j]) - 17, -ad3 + 18, 0), Quaternion.identity);
					obj3.name = foods3 [number3].name + i+j;
					obj3.transform.parent = this.transform;
				}
				ad3 = ad3 + 2.0f;
				if (i == 3) {
					ad3 = ad3 + 2.0f;
				}

			} else if (i > 3 && i <= 8) {
				for (int j = 0; j < xs3.Length; j++) {
					int number3 = Random.Range (0, foods3.Length);
					obj3 = (GameObject)Instantiate (foods3 [number3], new Vector3 (float.Parse (xs3 [j]) - 17, -ad3 + 18, 0), Quaternion.identity);
					obj3.name = foods3 [number3].name + i+j;
					obj3.transform.parent = this.transform;
				}
				ad3 = ad3 + 2.0f;
			} 
		}

		float ad4 = 19.5f;

	for (int i = 0; i < data4.Count; i++) {
		var xs4 = data4 [i].ToString ().Split (',');

		if (i <= 3) {
			for (int j = 0; j < xs4.Length; j++) {
				int number4 = Random.Range (0, foods4.Length);
				obj4 = (GameObject)Instantiate (foods4 [number4], new Vector3 (float.Parse (xs4 [j]) - 17, -ad4 + 18, 0), Quaternion.identity);
				obj4.name = foods4 [number4].name + i+j;
				obj4.transform.parent = this.transform;
			}
			ad4 = ad4 + 2.0f;
			if (i == 3) {
				ad4 = ad4 + 2.0f;
			}

		} else if (i > 2 && i <= 8) {
			for (int j = 0; j < xs4.Length; j++) {
				int number4 = Random.Range (0, foods4.Length);
				obj4 = (GameObject)Instantiate (foods4 [number4], new Vector3 (float.Parse (xs4 [j]) - 17, -ad4 + 18, 0), Quaternion.identity);
				obj4.name = foods4 [number4].name + i+j;
				obj4.transform.parent = this.transform;
			}
			ad4 = ad4 + 2.0f;
		} 
	}

		//colliderの座標
		float ad5 = 0;
		for (int i = 0; i < coldata.Count; i++) 
		{
			var xx = coldata [i].ToString ().Split (',');

			for (int j = 0; j < xx.Length; j++) {
				int number = Random.Range (0, cols.Length);
				obj_col = (GameObject)Instantiate (cols [number], new Vector3 (float.Parse (xx [j]) - 17, -ad5 + 17, 0), Quaternion.identity);
				obj_col.name = cols [number].name+i +j;
				obj_col.transform.parent = this.transform;
				Debug.Log (j);
			}
			ad5 = ad5 + 2.0f;

		}
	}
}