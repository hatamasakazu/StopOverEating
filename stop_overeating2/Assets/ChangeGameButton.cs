using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeGameButton : MonoBehaviour {

	public void OnClick(){
		
		Food_Count.food_count1 = 0;
		Food_Count.food_count2 = 0;
		Food_Count.food_count3 = 0;
		Food_Count.food_count4 = 0;
		Food_Count.food_count5 = 0;
		Food_Count.food_count6 = 0;

		Food_Count2.food_count1 = 0;
		Food_Count2.food_count2 = 0;
		Food_Count2.food_count3 = 0;
		Food_Count2.food_count4 = 0;
		Food_Count2.food_count5 = 0;
		Food_Count2.food_count6 = 0;

		Food_Count3.food_count1 = 0;
		Food_Count3.food_count2 = 0;
		Food_Count3.food_count3 = 0;
		Food_Count3.food_count4 = 0;
		Food_Count3.food_count5 = 0;
		Food_Count3.food_count6 = 0;

		Food_Count4.food_count1 = 0;
		Food_Count4.food_count2 = 0;
		Food_Count4.food_count3 = 0;
		Food_Count4.food_count4 = 0;
		Food_Count4.food_count5 = 0;
		Food_Count4.food_count6 = 0;

		Food_Count5.food_count1 = 0;
		Food_Count5.food_count2 = 0;
		Food_Count5.food_count3 = 0;
		Food_Count5.food_count4 = 0;
		Food_Count5.food_count5 = 0;
		Food_Count5.food_count6 = 0;


		SceneManager.LoadScene (Finish_process.nextSceneArrange[Finish_process.change_i], LoadSceneMode.Single);
		Finish_process.change_i = Finish_process.change_i + 1;
	}
}
