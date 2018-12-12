using UnityEngine;
using System.Collections;

public class Maze3 : MonoBehaviour
{

	// public GameObject pacdot;
	//public GameObject pacdot2;
	public GameObject[] foods;
	public GameObject[] foods2;
	public GameObject[] foods3;
	public GameObject[] foods4;
	public GameObject[] cols;

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
 		//from top to down

		data.Add("3,5,7,9,11,13");  //1
		data.Add("2,6,10,14");  //2
		data.Add("1,3,5,7,9,11,13,15");  //3
		data.Add("2,6,10,14");  //4
		data.Add("1,3,5,7,9,11,13,15");  //5
		data.Add("2,6,10,14");  //6
		data.Add("1,3,5,7,9,11,13,15");  //7
		data.Add("4,8,12");  //8


		//top of left
		data2.Add("22,25.8,30");  //9
		data2.Add("19,21,23,25,27,29,31,33");  //10
		data2.Add("20,24,28,32");  //11
		data2.Add("19,21,23,25,27,29,31,33");  //12
		data2.Add("20,24,28,32");  //13
		data2.Add("19,21,23,25,27,29,31,33");  //14
		data2.Add("20,24,28,32");  //15
		data2.Add("21,23,25,26.8,29,31");  //16

		//down of left
		data3.Add("4,8,12");
		data3.Add ("1,3,5,7,9,11,13,15");
		data3.Add ("2,6,10,14");
		data3.Add ("1,3,5,7,9,11,13,15");
		data3.Add ("2,6,10,14");
		data3.Add ("1,3,5,7,9,11,13,15");
		data3.Add ("2,6,10,14");
		data3.Add("3,5,7,9,11,13");

		//down of right
		data4.Add("21,23,25,26.8,29,31");  //9
		data4.Add("20,24,28,32");  //10
		data4.Add("19,21,23,25,27,29,31,33");  //11
		data4.Add("20,24,28,32");  //12
		data4.Add("19,21,23,25,27,29,31,33");  //13
		data4.Add("20,24,28,32");  //14
		data4.Add("19,21,23,25,27,29,31,33");  //15
		data4.Add("22,26,30");  //16

		//colliderを追加する
		ArrayList coldata = new ArrayList();

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


		float ad=0;

		//pacdot.transform.position = new Vector3(2, 30, 0);  //先頭は座標変更のみ
		for (int i = 0; i < data.Count; i++)
		{
			var xs = data[i].ToString().Split(',');
			for (int j = 0; j < xs.Length; j++) {
				int number = Random.Range (0, foods.Length);
					obj = (GameObject)Instantiate (foods [number], new Vector3 (float.Parse (xs [j]) - 17, -ad + 18, 0), Quaternion.identity);
				obj.name = foods [number].name+ i + j;
					obj.transform.parent = this.transform;
			}
			ad = ad + 2.0f;
		
		}
			
		//y座標の距離
		float ad2 = 0;

		for (int i = 0; i < data2.Count; i++) 
		{
			var xs2 = data2 [i].ToString ().Split (',');
			for (int j = 0; j < xs2.Length; j++) {
				int number2 = Random.Range (0, foods2.Length);
				obj2 = (GameObject)Instantiate (foods2 [number2], new Vector3 (float.Parse (xs2 [j]) - 17, -ad2 + 18, 0), Quaternion.identity);
				obj2.name = foods2 [number2].name+ i + j;
				obj2.transform.parent = this.transform;
			}
			ad2 = ad2 + 2.0f;

		}

		//y座標の距離
		float ad3 = 21.5f;

		for (int i = 0; i < data3.Count; i++) 
		{
			var xs3 = data3 [i].ToString ().Split (',');
			for (int j = 0; j < xs3.Length; j++) {
				int number3 = Random.Range (0, foods3.Length);
				obj3 = (GameObject)Instantiate (foods3 [number3], new Vector3 (float.Parse (xs3 [j]) - 17, -ad3 + 18, 0), Quaternion.identity);
				obj3.name = foods3 [number3].name+ i + j;
				obj3.transform.parent = this.transform;
			}
			ad3 = ad3 + 2.0f;

		}

		float ad4 = 21.5f;

		for (int i = 0; i < data4.Count; i++) 
		{
			var xs4 = data4 [i].ToString ().Split (',');
			for (int j = 0; j < xs4.Length; j++) {
				int number4 = Random.Range (0, foods4.Length);
				obj4 = (GameObject)Instantiate (foods4 [number4], new Vector3 (float.Parse (xs4 [j]) - 17, -ad4 + 18, 0), Quaternion.identity);
				obj4.name = foods4 [number4].name+ i + j;
				obj4.transform.parent = this.transform;
			}
			ad4 = ad4 + 2.0f;

		}

		//colliderの座標
		float ad5 = 0;
		for (int i = 0; i < coldata.Count; i++) 
		{
			var xx = coldata [i].ToString ().Split (',');

			for (int j = 0; j < xx.Length; j++) {
				int number = Random.Range (0, cols.Length);
				obj_col = (GameObject)Instantiate (cols [number], new Vector3 (float.Parse (xx [j]) - 17, -ad5 + 17, 0), Quaternion.identity);
				obj_col.name = cols [number].name+ i + j;
				obj_col.transform.parent = this.transform;
				Debug.Log (j);
			}
			ad5 = ad5 + 2.0f;

		}
	}
}