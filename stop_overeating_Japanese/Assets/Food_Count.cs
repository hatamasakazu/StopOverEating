using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food_Count : MonoBehaviour {

	//fooodがいくつ得られたかをカウントするスクリプト
	public static int food_count1=0;
	public static int food_count2=0;
	public static int food_count3=0;
	public static int food_count4=0;
	public static int food_count5=0;
	public static int food_count6=0;


	public void FoodCount(int a_flag){
		//ここで書くことで各foodが何個得られたかを記録する
		switch(a_flag){
		case 1:
			food_count1 = food_count1 + 1;
			Debug.Log ("food1_" + food_count1);
			break;
		case 2:
			food_count2 = food_count2 + 1;
			Debug.Log ("food2_" + food_count2);
			break;
		case 3:
			food_count3 = food_count3 + 1;
			Debug.Log ("food3_" + food_count3);
			break;
		case 4:
			food_count4 = food_count4 + 1;
			Debug.Log ("food4_" + food_count4);
			break;
		case 5:
			food_count5 = food_count5 + 1;
			Debug.Log ("food5_" + food_count5);
			break;
		case 6:
			food_count6 = food_count6 + 1;
			Debug.Log ("food6_" + food_count6);
			break;
		}
	}
}
