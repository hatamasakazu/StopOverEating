package main;

import android.app.Activity;
import android.util.Log;

import receiveddata.Soket;

/**
 * Created by htmsk on 2018/11/10.
 */

public class MyActivity extends Activity {

    public void  OnStart(){
        Soket sockt = new Soket();
        sockt.start();
        Log.d("socket","start");

    }


}