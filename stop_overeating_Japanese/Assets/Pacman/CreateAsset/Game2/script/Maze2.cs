using UnityEngine;
using System.Collections;

public class Maze2 : MonoBehaviour
{

	// public GameObject pacdot;
	//public GameObject pacdot2;
	public GameObject[] foods;

	public GameObject[] cols;


	// Use this for initialization
	void Start()
	{
		AddPacdot();
	}



	void AddPacdot()
	{
		ArrayList data = new ArrayList();
		//from top to down
		data.Add("1,3,5,7,9,11,13,15,17,19,21,23,25,27,29,31,33");  //1
		data.Add("1,3,5,7,9,11,13,15,17,19,21,23,25,27,29,31,33");  //2
		data.Add("1,3,5,7,9,11,13,15,17,19,21,23,25,27,29,31,33");  //3
		data.Add("1,3,5,7,9,11,13,15,17,19,21,23,25,27,29,31,33");  //4
		data.Add("1,3,5,7,9,11,13,15,17,19,21,23,25,27,29,31,33");  //5
		data.Add("1,3,5,7,9,11,13,15,17,19,21,23,25,27,29,31,33");  //6
		data.Add("1,3,5,7,9,11,13,15,17,19,21,23,25,27,29,31,33");  //7
		data.Add("1,3,5,7,9,11,13,15,17,19,21,23,25,27,29,31,33");  //8

		data.Add("1,3,5,7,9,11,13,15,17,19,21,23,25,27,29,31,33");  //9
		data.Add("1,3,5,7,9,11,13,21,23,25,27,29,31,33");  //10
		data.Add("1,3,5,7,9,11,13,15,17,19,21,23,25,27,29,31,33");  //11
		data.Add("1,3,5,7,9,11,13,15,17,19,21,23,25,27,29,31,33");  //12
		data.Add("1,3,5,7,9,11,13,15,17,19,21,23,25,27,29,31,33");  //13
		data.Add("1,3,5,7,9,11,13,15,17,19,21,23,25,27,29,31,33");  //14
		data.Add("1,3,5,7,9,11,13,15,17,19,21,23,25,27,29,31,33");  //15
		data.Add("1,3,5,7,9,11,13,15,17,19,21,23,25,27,29,31,33");  //16
		data.Add("1,3,5,7,9,11,13,15,17,19,21,23,25,27,29,31,33");  //17
		data.Add("1,3,5,7,9,11,13,15,17,19,21,23,25,27,29,31,33");;  //18
		data.Add("1,3,5,7,9,11,13,15,17,19,21,23,25,27,29,31,33");  //19
		//data.Add("27,22,16,13,7,2");    //3
		//data.Add("2,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27");    //2


		//cerclecolliderを作成する
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
		GameObject obj_col;

		float ad=0;
		float ad2 = 0;
			
		//pacdot.transform.position = new Vector3(2, 30, 0);  //先頭は座標変更のみ
		for (int i = 0; i < data.Count; i++)
		{
			var xs = data[i].ToString().Split(',');


			for (int j = 0; j < xs.Length; j++) {
				int number = Random.Range (0, foods.Length);
				obj = (GameObject)Instantiate (foods [number], new Vector3 (float.Parse (xs [j]) - 17, -ad + 18, 0), Quaternion.identity);
				obj.name = foods [number].name + i + j;
				obj.transform.parent = this.transform;
			}
			ad = ad + 2.0f;


		}
		Debug.Log (coldata.Count);

		for (int i = 0; i < coldata.Count; i++) 
		{
			var xx = coldata [i].ToString ().Split (',');

			for (int j = 0; j < xx.Length; j++) {
				int number = Random.Range (0, cols.Length);
				obj_col = (GameObject)Instantiate (cols [number], new Vector3 (float.Parse (xx [j]) - 17, -ad2 + 17, 0), Quaternion.identity);
				obj_col.name = cols [number].name + i + j;
				obj_col.transform.parent = this.transform;
				Debug.Log (j);
			}
			ad2 = ad2 + 2.0f;

		}
	}
}