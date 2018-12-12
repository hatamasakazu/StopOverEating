using UnityEngine;
using System.Collections;

public class Maze1 : MonoBehaviour
{

	// public GameObject pacdot;
	//public GameObject pacdot2;
	public GameObject[] foods;


	// Use this for initialization
	void Start()
	{
		AddPacdot();
	}



	void AddPacdot()
	{
		ArrayList data = new ArrayList();
		//from top to down
		data.Add("1,3,5,7.3,9,11,13,15,19,21,23,25,26.7,29,31,33");  //1
		data.Add("1,7.3,15,19,26.8,33");  //2
		data.Add("1,3,5,7.3,9,11,13,15,17,19,21,23,25,26.7,29,31,33");  //3

		data.Add("1,7.3,11,23,26.7,33");  //4
		data.Add("1,3,5,7.3,11,13,15,19,21,23,26.7,29,31,33");  //5
		data.Add("7.3,15,19,26.7");  //6
		data.Add("7.3,11,13,21,23,26.7");  //7
		data.Add("7.3,11,23,26.7");  //8

		data.Add("7.3,9,11,23,25,26.7");  //9
		data.Add("7.3,11,23,26.7");  //10
		data.Add("7.3,11,13,15,17,19,21,23,26.7");  //11
		data.Add("7.3,11,23,26.7");  //12
		data.Add("1,3,5,7.3,9,11,13,15,19,21,23,25,26.7,29,31,33");  //13
		data.Add("1,7.3,15,19,26.7,33");  //14
		data.Add("1,3,7.3,9,11,13,15,17,19,21,23,25,26.7,31,33");  //15
		data.Add("3,7.3,11,23,26.7,31");  //16
		data.Add("1,3,5,7.3,11,13,15,19,21,23,26.7,29,31,33");  //17
		data.Add("1,15,19,33");  //18
		data.Add("1,3,5,7.3,9,11,13,15,17,19,21,23,25,26.7,29,31,33");  //19
		//data.Add("27,22,16,13,7,2");    //3
		//data.Add("2,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27");    //2




		GameObject obj;
		float ad=0;
		//pacdot.transform.position = new Vector3(2, 30, 0);  //先頭は座標変更のみ
		for (int i = 0; i < data.Count; i++)
		{
			var xs = data[i].ToString().Split(',');

			if (i <= 2) {
				for (int j = 0; j < xs.Length; j++) {
					int number = Random.Range (0, foods.Length);
					obj = (GameObject)Instantiate (foods[number], new Vector3 (float.Parse (xs [j]) - 17, -ad + 18, 0), Quaternion.identity);
					obj.name = foods[number].name + i + j;
					obj.transform.parent = this.transform;
				}
				ad = ad + 2.5f;
			} else if (i > 2 && i <= 8) {
				ad = ad - 0.5f;
				for (int j = 0; j < xs.Length; j++) {
					int number = Random.Range (0, foods.Length);
					obj = (GameObject)Instantiate (foods[number], new Vector3 (float.Parse (xs [j]) - 17, -ad + 18, 0), Quaternion.identity);
					obj.name = foods[number].name+ i + j;
					obj.transform.parent = this.transform;
				}
				ad = ad + 2.5f;
			} else if (i > 8 && i < 20) {
				ad = ad - 0.6f;
				for (int j = 0; j < xs.Length; j++) {
					int number = Random.Range (0, foods.Length);
					obj = (GameObject)Instantiate (foods[number], new Vector3 (float.Parse (xs [j]) - 17, -ad + 18, 0), Quaternion.identity);
					obj.name = foods[number].name+ i + j;
					obj.transform.parent = this.transform;
				}
				ad = ad + 2.5f;
			}


		}
	}
}