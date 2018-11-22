package receiveddata;


/**
 * Created by htmsk on 2018/11/09.
 */


import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.InputStream;
import java.io.InputStreamReader;

import java.io.OutputStream;

import java.io.OutputStreamWriter;

import java.net.Socket;

import android.util.Log;

import java.lang.Thread;

import receiveddata.RRI;

    public class Soket extends Thread {


        private String m_szIp = "192.168.42.206";    //アクセス先IP
        private  String m_szIp2 = "172.20.10.4";
        private int m_nPort = 11888;            //アクセス先ポート



        private ECG rri_script;



            @Override
            public void run () {
                Log.d("hai2","処理");
                try {
                    Log.d("hai","入った");

                    //通信用ソケット作成
                    Socket socket = new Socket(m_szIp2, m_nPort);

                    InputStream in = socket.getInputStream();

                    OutputStream out = socket.getOutputStream();

                    BufferedReader br = new BufferedReader(new InputStreamReader(in, "UTF-8"));
                    BufferedWriter bw = new BufferedWriter(new OutputStreamWriter(out, "UTF-8"));
                    int rri;
                    rri = rri_script.rri_soket;
                    String str = String.valueOf(rri);
                    String sendData = "テストセンタ";
                    out.write(str.getBytes("UTF-8"));
                    //送信データの表示
                    System.out.println("「"+sendData+"」を送信しました。");

                    //送信ストリームを表示
                    out.close();

                    //終了
                    socket.close();
                    //テキストを送る

                    Log.d("hai3","入った");

/*
                    bw.write(str);
                    bw.flush();
                    Log.d("hai4","入った"+str);
                    //データを確定させて通信処理を起こさせる
                    //Log.d("wakaranai", "値は" + rri);

                    Log.d("hai5","入った"+rri);
                    //相手からのデータ待ち
                    String szData = br.readLine();
                    //表示する
                    Log.d("hai6","入った");

                    Log.d("nya", "受信文字列:" + szData);
                    //後処理
                    in.close();
                    Log.d("ha7","入った");
                    out.close();
                    Log.d("hai8","入った");
                    socket.close();
                    Log.d("hai9","入った");
*/
                } catch (Exception e) {
                    e.printStackTrace();
                    Log.d("aa", "受信文字列:" + 1);
                    int rri2 = rri_script.rri_soket;
                    Log.d("aa2","受信RRI"+ rri2);
                }
            }

       /* public static void main() {
            Soket tt = new Soket();
            tt.start();
            Log.d("nya", "受信文字列:" + 2);

        }*/

    }