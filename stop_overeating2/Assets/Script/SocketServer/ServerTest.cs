using UnityEngine;


/*
 * TestServer.cs
 * SocketServerを継承、開くポートを指定して、送受信したメッセージを具体的に処理する
 */
namespace Script.SocketServer
{
	public class ServerTest : SocketServer {
#pragma warning disable 0649
		// ポート指定（他で使用していないもの、使用されていたら手元の環境によって変更）
		[SerializeField]private int _port;
#pragma warning restore 0649
		
		private void Start(){
			// 接続中のIPアドレスを取得
			var ipAddress = Network.player.ipAddress;
			// 指定したポートを開く
			Listen(ipAddress, _port);

			// システムに接続情報をセット（表示用）
			MyViewer.Instance.SetIpAddressPort (ipAddress + ":" + _port);
		}




		// クライアントからメッセージ受信
		protected override void OnMessage(string msg){
			base.OnMessage(msg);
			
			// ------------------------------------------------
			// あとは送られてきたメッセージによって何かしたいことを書く
			// ------------------------------------------------

			// 今回は受信した整数値を表示用システムにセットする
			int num;
			// 整数値以外は何もしない
			if (int.TryParse (msg, out num)) {
				// ビュアーに値をセットする
				MyViewer.Instance.SetNum (num);
				// クライアントに受領メッセージを返す
				SendMessageToClient ("Accept:"+ num+"\n");
			} else {
				// クライアントにエラーメッセージを返す
				SendMessageToClient ("Error\n");
			}
		}

	}
}