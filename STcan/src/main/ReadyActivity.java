package main;

import jp.aoyama.wil.kana.R;
import receiveddata.Soket;

import android.app.Activity;
import android.content.ComponentName;
import android.content.Intent;
import android.content.ServiceConnection;
import android.os.Bundle;
import android.os.IBinder;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.Toast;

public class ReadyActivity extends Activity {
	public static final String EXTRAS_DEVICE_NAME = "DEVICE_NAME";
	public static final String EXTRAS_DEVICE_ADDRESS = "DEVICE_ADDRESS";
	
	public static String mDeviceAddress;
	
	// Intent request code
	private static final int REQUEST_CONNECT_DEVICE_INSECURE = 1;
	private BluetoothLeService mBluetoothLeService;
	// private BluetoothAdapter mBluetoothAdapter;
	
	private final String ATTENTION = "センサとリンクしてください";
	
	// to control button listener
	private Button check, start, setting, finish, test;
	
	/*private final BroadcastReceiver mGattUpdateReceiver = new BroadcastReceiver(){
		@Override
		public void onReceive(Context context, Intent intent) {
			// TODO Auto-generated method stub
			
		}
	};*/
	private final ServiceConnection mServiceConnection = new ServiceConnection() {

		@Override
		public void onServiceConnected(ComponentName componentName, IBinder service) {
			mBluetoothLeService = ((BluetoothLeService.LocalBinder) service).getService();
			if (!mBluetoothLeService.initialize()) {
				finish();
			}
			mBluetoothLeService.connect(ReadyActivity.mDeviceAddress);
		}

		@Override
		public void onServiceDisconnected(ComponentName componentName) {
			mBluetoothLeService = null;
		}
	};

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_ready);
		
		check 	= (Button) findViewById(R.id.ready_check);
		start 	= (Button) findViewById(R.id.ready_start);
		setting = (Button) findViewById(R.id.ready_settings);
		finish 	= (Button) findViewById(R.id.ready_finish);
		test	= (Button) findViewById(R.id.ready_test);
		
		check.setOnClickListener(new PushButtonListener());
		start.setOnClickListener(new PushButtonListener());
		setting.setOnClickListener(new PushButtonListener());
		finish.setOnClickListener(new PushButtonListener());
		test.setOnClickListener(new PushButtonListener());
		
		Log.d("DRA", "onCreate() started.");
	}
	
	@Override
	protected void onStart() {
		super.onStart();
		
		Log.d("DRA", "onStart() started.");
	}
	public static void main(String[] args) {
		Soket tt = new Soket();
		tt.start();
		Log.d("aa", "受信不可能:" + 2);

	}
	@Override
	protected void onStop() {
		super.onStop();
		
		Log.d("DRA", "onStop() started.");
	}
	
	@Override
	protected void onRestart() {
		super.onRestart();
		
		Log.d("DRA", "onRestart() started.");
	}
	
	/**
     * アクティビティが前面に来るたびにデータを更新
     */
	@Override
	protected void onResume() {
		super.onResume();
		
		Intent gattServiceIntent = new Intent(this, BluetoothLeService.class);
        bindService(gattServiceIntent, mServiceConnection, BIND_AUTO_CREATE);
//        registerReceiver(mGattUpdateReceiver, makeGattUpdateIntentFilter());
        
        Log.d("DRA", "onResume start.");
        
        if (mBluetoothLeService != null) {
            mBluetoothLeService.connect(mDeviceAddress);
            mBluetoothLeService.displayGattServices(true);
            Log.d("RA","connect to device. Address is : " + mDeviceAddress);
        }
		
        Log.d("DRA", "onResume() started.");
	}
	
	@Override
	protected void onPause() {
		super.onPause();
		
		Log.d("DRA", "onPause() started.");
	}
	
	@Override
	protected void onDestroy() {
		super.onDestroy();
		//unregisterReceiver(mGattUpdateReceiver);
        unbindService(mServiceConnection);
		mBluetoothLeService = null;
		
		Log.d("DRA", "onDestroy() started.");
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.device_list, menu);
		return true;
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		// Handle action bar item clicks here. The action bar will
		// automatically handle clicks on the Home/Up button, so long
		// as you specify a parent activity in AndroidManifest.xml.
		Intent serverIntent = null;
		switch (item.getItemId()) {
		case R.id.menu_scan:
			// Launch the DeviceActivity to see devices and do scan
			serverIntent = new Intent(this, DeviceListActivity.class);
			startActivityForResult(serverIntent, REQUEST_CONNECT_DEVICE_INSECURE);
			return true;
			
		case android.R.id.home:
			onBackPressed();
			return true;
		}
		return super.onOptionsItemSelected(item);
	}
	
	@Override
	protected void onActivityResult(int requestcode, int resultcode, Intent data){
		super.onActivityResult(requestcode, resultcode, data);
		Log.d("DCA", "onActivityResult has started.");
		
		if (requestcode == 1 && data != null) 
		{
			if(resultcode == RESULT_OK)
			{        
				mDeviceAddress = data.getStringExtra(EXTRAS_DEVICE_ADDRESS);
				Log.d("DCA", "onActivityResult, mDeviceAddress is " + mDeviceAddress);

			}
			if (resultcode == RESULT_CANCELED) 
			{   

			}
		}
		
	}
	
	Button getButtonSettingActivity() {
		return (Button) findViewById(R.id.button_setting_activity);
	}

	// this listener is to move SettingActivity 
	// ここでボタンが押下された時の処理を書く、どのボタンに関しても
	class PushButtonListener implements View.OnClickListener {
		@Override
		public void onClick(View v) {
			switch(v.getId()){
			case R.id.ready_check:
				if(!mBluetoothLeService.connect(ReadyActivity.mDeviceAddress)){
					showAttention();
					break;
				}else{
					showCheckActivity();
					break;
				}
				
			case R.id.ready_settings:
				if(!mBluetoothLeService.connect(ReadyActivity.mDeviceAddress)){
					showAttention();
					break;
				}else{
					showSettingActivity();
					break;
				}
				
			case R.id.ready_start:
				if(!mBluetoothLeService.connect(ReadyActivity.mDeviceAddress)){
					showAttention();
					break;
				}else{
					showControlActivity();
					break;
				}
			case R.id.ready_test:
				if(!mBluetoothLeService.connect(ReadyActivity.mDeviceAddress)){
					showAttention();
					break;
				}else{
					showTestActivity();
					break;
				}
				
			case R.id.ready_finish:
				finish();
				break;
			}
		}
	}

	private void showSettingActivity() {
		Intent intent = new Intent(this, DeviceSettingActivity.class);
		startActivity(intent);
	}
	
	private void showControlActivity() {
		Intent intent = new Intent(this, DeviceControlActivity.class);
		startActivity(intent);
	}
	
	private void showCheckActivity(){
		Intent intent = new Intent(this, CheckActivity.class);
		startActivity(intent);
	}
	
	private void showTestActivity(){
		Intent intent = new Intent(this, TestActivity.class);
		startActivity(intent);
	}
	
	private void showAttention(){
		Toast.makeText(this, ATTENTION, Toast.LENGTH_SHORT).show();
	}
	
	// 上のメソッドを纏めたい
	private void showActivity(Activity activity){
		Intent i = new Intent(this, activity.getClass());
		startActivity(i);
	}
	
	/*private static IntentFilter makeGattUpdateIntentFilter() {
        final IntentFilter intentFilter = new IntentFilter();
        intentFilter.addAction(BluetoothLeService.ACTION_GATT_CONNECTED);
        intentFilter.addAction(BluetoothLeService.ACTION_GATT_DISCONNECTED);
        intentFilter.addAction(BluetoothLeService.ACTION_GATT_SERVICES_DISCOVERED);
        intentFilter.addAction(BluetoothLeService.ACTION_DATA_AVAILABLE_MEASURE);
        return intentFilter;
    }*/
}
