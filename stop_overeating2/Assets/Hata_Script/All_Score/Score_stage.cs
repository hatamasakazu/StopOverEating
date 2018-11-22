using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class Score_stage : MonoBehaviour
{

	// スコアを表示するGUIText

	//TMP_Text is to use TextMeshPro
	public TMP_Text scoreGUIText;
	public TMP_Text highScoreGUIText;
	public TMP_Text totalScoreGUIText;

	// スコア
	private int score;
	private int LastScore;
	private int highScore;

	private int totalScore;

	public static string highScoreKey = "highScore";
	public static string totalScoreKey = "totalScore";

	void Start()
	{
		//TextMeshPro tmp = gameObject.AddComponent<TextMeshPro> ();
		scoreGUIText = GameObject.FindWithTag("Score").GetComponent<TextMeshProUGUI>();
		highScoreGUIText = GameObject.FindWithTag("HighScore").GetComponent<TextMeshProUGUI>();
		totalScoreGUIText = GameObject.FindWithTag ("TotalScore").GetComponent<TextMeshProUGUI> ();
		//highScoreGUIText= GetComponent<TextMeshPro>();
		Initialize();

	}

	void Update()
	{

		if (highScore < totalScore) {
			highScore = totalScore;

			PlayerPrefs.SetInt (highScoreKey, highScore);
		}

		Save ();
		// スコア・ハイスコアを表示する
		totalScoreGUIText.text = "TotalScore: " + totalScore.ToString();
		scoreGUIText.text = "Score: " + score.ToString();
		highScoreGUIText.text = "HighScore: " + highScore.ToString ();

	}

	// ゲーム開始前の状態に戻す
	private void Initialize()
	{
		// スコアを0に戻す
		//score = 0;
		totalScore = PlayerPrefs.GetInt (totalScoreKey, 0);
//F		Debug.Log ("totalScore" + totalScore);


		highScore = PlayerPrefs.GetInt (highScoreKey, 0);


	}

	// ポイントの追加
	public void AddPoint(int point)
	{
		score = score + point;

		totalScore = totalScore + point;

	}

	public void Save()
	{
		//PlayerPrefs.SetInt (highScoreKey, highScore);
		PlayerPrefs.SetInt (totalScoreKey, totalScore);
		PlayerPrefs.Save ();

//		Debug.Log ("2 "+totalScore);

		Initialize ();
	}

	public static string getA(){
		return highScoreKey;
	}

	/*MainMenuで使う*/
	public static string getB(){
		return totalScoreKey;
	}

}