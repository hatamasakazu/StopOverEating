using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cal_score : MonoBehaviour {

	/*cal scoreを表示するためのスクリプト*/
	/*Pacdot がこのスクリプトを参照*/

	// スコアを表示するGUIText

	//TMP_Text is to use TextMeshPro
	public TMP_Text Cal_scoreGUIText;

	public TMP_Text Cal_highScoreGUIText;

	public GameObject pacman;

	public int Max_calscore = 2000;

	//記録
	public int[] calrecord = new int[1000];

	public int inc=0;

	// スコア
	public int calscore;
	public int currnetscore;
	public int calhighscore;

	public static string Calhighscorekey = "calhighScore";



	void Start()
	{
		//TextMeshPro tmp = gameObject.AddComponent<TextMeshPro> ();
		Cal_scoreGUIText = GameObject.FindWithTag("Cal_score").GetComponent<TextMeshProUGUI>();
		Cal_highScoreGUIText = GameObject.FindWithTag("Cal_highscore").GetComponent<TextMeshProUGUI>();

		Initialize();
	}

	void Update()
	{

		if (calhighscore < calscore) {
			calhighscore = calscore;

			PlayerPrefs.SetInt (Calhighscorekey, calhighscore);
		}
		int dip = Max_calscore + 500;
		// スコア・ハイスコアを表示する
		Cal_scoreGUIText.text =  calscore  + " / " + Max_calscore.ToString() + " - " + dip;
		Cal_highScoreGUIText.text = calhighscore  + " / " + Max_calscore.ToString() + " - " + dip;

	}

	// ゲーム開始前の状態に戻す
	private void Initialize()
	{
		// スコアを0に戻す
		calscore = 0;

		calhighscore = PlayerPrefs.GetInt (Calhighscorekey, 0);


	}

	// ポイントの追加
	public void AddCalPoint(int point)
	{
		calscore = calscore + point;
		calrecord [inc] = calscore;
		inc = inc + 1;
	}

	public void Save()
	{
		PlayerPrefs.SetInt (Calhighscorekey, calhighscore);
		PlayerPrefs.Save ();

		Initialize ();
	}

	public static string getCal(){
		return Calhighscorekey;
	}


}
