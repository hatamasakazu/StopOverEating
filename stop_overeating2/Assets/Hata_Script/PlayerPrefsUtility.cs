using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public static class PlayerPrefsUtility{

	/// <summary>
	/// リストを保存
	/// </summary>
	public static void SaveList<T> (string key, List<T> value){
		string serizlizesList = Serialize<List<T>> (value);
		PlayerPrefs.SetString (key, serizlizesList);
	}

	///<summary>
	/// リスト読み込み
	/// </summary>

	public static List<T> LoadList<T>(string key){
		if (PlayerPrefs.HasKey (key)) {
			string serizlizesList = PlayerPrefs.GetString (key);
			return Deserialize<List<T>> (serizlizesList);
		}
		return new List<T> ();

	}


	//シリアライズ

	private static string Serialize<T>(T obj){
		BinaryFormatter binaryFormatter = new BinaryFormatter ();
		MemoryStream memoryStream = new MemoryStream ();
		binaryFormatter.Serialize (memoryStream, obj);
		return Convert.ToBase64String (memoryStream.GetBuffer ());

	}

	private static T Deserialize<T>(string str){
		BinaryFormatter binaryFormatter = new BinaryFormatter ();
		MemoryStream memoryStream = new MemoryStream (Convert.FromBase64String (str));
		return (T)binaryFormatter.Deserialize (memoryStream);
	}
}
