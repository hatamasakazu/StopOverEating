using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Script.SocketServer
{
public class IP_Port_disp : MonoBehaviour {
#pragma warning disable 0649
// ポート指定（他で使用していないもの、使用されていたら手元の環境によって変更）
[SerializeField]private int _port;
#pragma warning restore 0649

		private int port_number;
		public Text ip_text;
		public Text po_text;

		// Use this for initialization
		void Start () {
			var ipAddress = Network.player.ipAddress;
			Debug.Log ("ip" + ipAddress);
			port_number = _port;
			Debug.Log ("port" + port_number);
			ip_text.text = "IP Address: " + ipAddress;

			po_text.text = "Port Number: " + port_number.ToString();

		}
	
		// Update is called once per frame
		void Update () {
		
		}
	}
}
