package main;

import jp.aoyama.wil.kana.R;
import receiveddata.MyDataView;
import receiveddata.SerializableReceivedData;
import whs.WhsCommands;
import whs.WhsHelper;
import android.app.Activity;
import android.content.BroadcastReceiver;
import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.content.ServiceConnection;
import android.os.Bundle;
import android.os.IBinder;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.LinearLayout;

import java.net.Socket;

public class CheckActivity extends Activity {
	
	public static final String EXTRAS_DEVICE_NAME = "DEVICE_NAME";
	public static final String EXTRAS_DEVICE_ADDRESS = "DEVICE_ADDRESS";
	
	// Intent request code
	// private static final int REQUEST_CONNECT_DEVICE_INSECURE = 1;
	// private String mDeviceAddress;
	private BluetoothLeService mBluetoothLeService;
	
	private MyDataView check;
	private SerializableReceivedData data;
	
	
	/* What is called Inner Class */
	private final BroadcastReceiver mGattUpdateReceiver = new BroadcastReceiver() {
		@Override
		public void onReceive(Context context, Intent intent) {
			final String action = intent.getAction();
			data = (SerializableReceivedData) intent.getSerializableExtra(WhsHelper.INTENT_RECEIVED_OBJ);
			if (BluetoothLeService.ACTION_DATA_AVAILABLE_MEASURE.equals(action)) {
				invalidateOptionsMenu();
				check.getDrawData(data.getEcg());
			} else if (BluetoothLeService.ACTION_GATT_DISCONNECTED.equals(action)) {
				invalidateOptionsMenu();
			} else if (BluetoothLeService.ACTION_GATT_SERVICES_DISCOVERED.equals(action)) {
				mBluetoothLeService.displayGattServices(true);
			}else if(BluetoothLeService.ACTION_DATA_AVAILABLE_SETTING.equals(action)){
				String command = intent.getStringExtra(WhsHelper.INTENT_COMMAND);
				String commandString = intent.getStringExtra(WhsHelper.INTENT_VALUE);
				
				
			}
		}
	};
	private final ServiceConnection mServiceConnection = new ServiceConnection() {

		@Override
		public void onServiceConnected(ComponentName componentName, IBinder service) {
			mBluetoothLeService = ((BluetoothLeService.LocalBinder) service).getService();
			if (!mBluetoothLeService.initialize()) {
				finish();
			}
			mBluetoothLeService.connect(ReadyActivity.mDeviceAddress);
			writeReadSettings();
		}

		@Override
		public void onServiceDisconnected(ComponentName componentName) {
			mBluetoothLeService = null;
		}
	};

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		
		// make mode to "PQRST" mode
		mBluetoothLeService.mLeCommandContainer.clear();			// null pointer exception
		mBluetoothLeService.mLeCommandContainer.add(WhsCommands.CommandBehaviorWritePqrst);
		
		LinearLayout clayout = (LinearLayout)findViewById(R.id.check_root);
		Button cfinish = (Button)findViewById(R.id.check_finish);
		
		cfinish.setOnClickListener(new PushButtonListener());
		
		check = new MyDataView(this);
		clayout.addView(check);
		
	}
	
	@Override
	protected void onStart() {
		super.onStart();
		Log.d("DRA", "onStart() started.");
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
        registerReceiver(mGattUpdateReceiver, makeGattUpdateIntentFilter());
        
        Log.d("DCA", "onResume start.");
        
        if (mBluetoothLeService != null) {
            mBluetoothLeService.connect(ReadyActivity.mDeviceAddress);
            mBluetoothLeService.displayGattServices(true);
            Log.d("RA","connect to device. Address is : " + ReadyActivity.mDeviceAddress);
        }
		
        Log.d("DRA", "onResume() started.");
	}
	
	@Override
	protected void onPause() {
		super.onPause();
		writeReadSettingsFinish();
		
		Log.d("DRA", "onPause() started.");
	}
	
	@Override
	protected void onDestroy() {
		super.onDestroy();
		
		mBluetoothLeService.mLeCommandContainer.clear();
		mBluetoothLeService.mLeCommandContainer.add(WhsCommands.CommandBehaviorWriteRri);
		
		writeReadSettingsFinish();
		
		unregisterReceiver(mGattUpdateReceiver);
		Log.d("DRA", "onDestroy() started.");
	}
	
	class PushButtonListener implements View.OnClickListener{
		@Override
		public void onClick(View v) {
			// TODO Auto-generated method stub
			if(v.getId() == R.id.check_view){
				finish();
			}
		}
	}
	
	public void writeReadSettings() {
		mBluetoothLeService.mLeCommandContainer.clear();
		mBluetoothLeService.mLeCommandContainer.add(WhsCommands.CommandSettingModeStart);
		mBluetoothLeService.mLeCommandContainer.add(WhsCommands.CommandBehaviorAllRead);
		mBluetoothLeService.writeCommands();
	}

	public void writeReadSettingsFinish() {
		mBluetoothLeService.mLeCommandContainer.clear();
		mBluetoothLeService.mLeCommandContainer.add(WhsCommands.CommandSettingModeEnd);
		mBluetoothLeService.mLeCommandContainer.add(WhsCommands.CommandMeasureModeStart);
		mBluetoothLeService.writeCommands();
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.check, menu);
		return true;
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		// Handle action bar item clicks here. The action bar will
		// automatically handle clicks on the Home/Up button, so long
		// as you specify a parent activity in AndroidManifest.xml.
		int id = item.getItemId();
		if (id == R.id.action_settings) {
			return true;
		}
		return super.onOptionsItemSelected(item);
	}
	
	/*private void updateConnectionState(final int resourceId) {
		runOnUiThread(new Runnable() {
			@Override
			public void run() {
				mConnectionState.setText(resourceId);
			}
		});
	}*/
	
	private static IntentFilter makeGattUpdateIntentFilter() {
        final IntentFilter intentFilter = new IntentFilter();
        intentFilter.addAction(BluetoothLeService.ACTION_GATT_CONNECTED);
        intentFilter.addAction(BluetoothLeService.ACTION_GATT_DISCONNECTED);
        intentFilter.addAction(BluetoothLeService.ACTION_GATT_SERVICES_DISCOVERED);
        intentFilter.addAction(BluetoothLeService.ACTION_DATA_AVAILABLE_MEASURE);
        intentFilter.addAction(BluetoothLeService.ACTION_DATA_AVAILABLE_SETTING);
        return intentFilter;
    }
}
