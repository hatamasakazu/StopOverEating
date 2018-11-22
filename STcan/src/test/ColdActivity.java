package test;

import jp.aoyama.wil.kana.R;
import main.BluetoothLeService;
import main.ReadyActivity;
import main.RealTimepNNView;
import receiveddata.ECG;
import receiveddata.PqrstData;
import receiveddata.RriAverageData;
import receiveddata.RriPeakData;
import receiveddata.SerializableReceivedData;
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
import android.os.Message;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.LinearLayout;
import android.widget.TextView;

public class ColdActivity extends Activity {

	private TextView mConnectionState;
	private TextView mPqrst;
	private TextView mRri;
	// public static String mDeviceAddress;
	private BluetoothLeService mBluetoothLeService;

	// add by myself
	private TextView melapsedtime;
	private TextView mshtpNN50;
	private TextView mlngpNN50;

	private Button finish;

	private ECG ecg;

	// valuable to write file name
	String file_date, file_path;

	private MeasureTime mt;
	private MakePath mp;

	private RealTimepNNView pNNrect;
	// to measure elapsed time

	private final BroadcastReceiver mGattUpdateReceiver = new BroadcastReceiver() {
		@Override
		public void onReceive(Context context, Intent intent) {
			Log.d("CA", "into BroadcastReceiver.");

			final String action = intent.getAction();
			if (BluetoothLeService.ACTION_GATT_CONNECTED.equals(action)) {
				updateConnectionState(R.string.connected);
				invalidateOptionsMenu();
			} else if (BluetoothLeService.ACTION_GATT_DISCONNECTED.equals(action)) {
				updateConnectionState(R.string.disconnected);
				invalidateOptionsMenu();
				clearUI();
			} else if (BluetoothLeService.ACTION_GATT_SERVICES_DISCOVERED.equals(action)) {
				mBluetoothLeService.displayGattServices(true);
			} else if (BluetoothLeService.ACTION_DATA_AVAILABLE_MEASURE.equals(action)) {
				updateConnectionState(R.string.connected);
				displayData((SerializableReceivedData) intent.getSerializableExtra(WhsHelper.INTENT_RECEIVED_OBJ));
			}
		}
	};

	/* What is called Inner Class */
	private final ServiceConnection mServiceConnection = new ServiceConnection() {

		@Override
		public void onServiceConnected(ComponentName componentName, IBinder service) {
			mBluetoothLeService = ((BluetoothLeService.LocalBinder) service).getService();
			if (!mBluetoothLeService.initialize()) {
				finish();
			}
			mBluetoothLeService.connect(ReadyActivity.mDeviceAddress);
			Log.d("CA", "into ServiceConnected of ServiceConnection.");
		}

		@Override
		public void onServiceDisconnected(ComponentName componentName) {
			mBluetoothLeService = null;
			Log.d("CA", "into ServiceDisconnected of ServiceConnection.");
		}
	};

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_cold);

		melapsedtime = (TextView) findViewById(R.id.cold_elapsedtime); 

		((TextView) findViewById(R.id.cold_address)).setText(ReadyActivity.mDeviceAddress);
		mConnectionState = (TextView) findViewById(R.id.cold_state);
		mPqrst = (TextView) findViewById(R.id.cold_pqrst);
		mRri = (TextView) findViewById(R.id.cold_rri);

		// add by me
		mshtpNN50 = (TextView) findViewById(R.id.cold_shtpnn);
		mlngpNN50 = (TextView) findViewById(R.id.cold_lngpnn);


		finish = (Button) findViewById(R.id.cold_finish);
		finish.setOnClickListener(new PushButtonListener());

		getActionBar().setDisplayHomeAsUpEnabled(true);
		mp = new MakePath();
		file_date = mp.date();

		mt = new MeasureTime(melapsedtime);
		mt.handleMessage(new Message());
		
		/*et = mt.handleMessage2(new Message());
		melapsedtime.setText(et);*/

		ecg = new ECG(file_date, FileString.MODE.COLD);
		LinearLayout rLayout = (LinearLayout)findViewById(R.id.cold_root);
		pNNrect = new RealTimepNNView(this, ecg.getRri());
		rLayout.addView(pNNrect);
	}

	@Override
	protected void onStart() {
		super.onStart();

		Intent gattServiceIntent = new Intent(this, BluetoothLeService.class);
		bindService(gattServiceIntent, mServiceConnection, BIND_AUTO_CREATE);
		registerReceiver(mGattUpdateReceiver, makeGattUpdateIntentFilter());

		Log.d("CA", "onStart start.");

		if (mBluetoothLeService != null) {
			mBluetoothLeService.connect(ReadyActivity.mDeviceAddress);
			mBluetoothLeService.displayGattServices(true);
		}
	}

	/**
	 * アクティビティが前面に来るたびにデータを更新
	 */
	@Override
	protected void onResume() {
		super.onResume();

		/*Intent gattServiceIntent = new Intent(this, BluetoothLeService.class);
        bindService(gattServiceIntent, mServiceConnection, BIND_AUTO_CREATE);
        registerReceiver(mGattUpdateReceiver, makeGattUpdateIntentFilter());

        Log.d("DCA", "onResume start. mBluetoothLeService is " + mBluetoothLeService);

        if (mBluetoothLeService != null) {
            mBluetoothLeService.connect(mDeviceAddress);
            mBluetoothLeService.displayGattServices(true);
        }*/
	}

	@Override
	protected void onDestroy() {
		super.onDestroy();
		unregisterReceiver(mGattUpdateReceiver);
		unbindService(mServiceConnection);
		// mBluetoothLeService = null;
		mt = null;

		try{
			ecg.close();
			Log.d("CA", "file closed.");
		}catch(Exception e){
			Log.e("CA", e.getMessage() + " file closing failed...");
		}
	}

	private void clearUI() {
		melapsedtime.setText("");
		mPqrst.setText("");
		mRri.setText("");
		mshtpNN50.setText("");
		mlngpNN50.setText("");
	}

	class PushButtonListener implements View.OnClickListener {
		@Override
		public void onClick(View v) {
			switch(v.getId()){
			case R.id.cold_finish:
				finish();
				break;
			}
		}
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.cold, menu);
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

	private void updateConnectionState(final int resourceId) {
		runOnUiThread(new Runnable() {
			@Override
			public void run() {
				mConnectionState.setText(resourceId);
			}
		});
	}

	private void displayData(final SerializableReceivedData data) {
		if (data != null) {

			// 心拍データの書き出し(まだ適当な書き出し方法)
			ecg.receiveData(data);

			if (data instanceof PqrstData) {
				mPqrst.setText(Integer.toString(data.getEcg()));
				mshtpNN50.setText("empty");
				mlngpNN50.setText("empty");
			} else if (data instanceof RriAverageData ||
					data instanceof RriPeakData) {
				mPqrst.setText("empty");
				mRri.setText(Integer.toString(data.getEcg()));
				if(!ecg.getRri().getshtpNNList().isEmpty())
					mshtpNN50.setText(String.format("%.3f", ecg.getRri().getshtpNNList().getLast()));
				if(!ecg.getRri().getLngpNNList().isEmpty())
					mlngpNN50.setText(String.format("%.3f", ecg.getRri().getLngpNNList().getLast()));

			} 

			// show pNN percent as figure.
			pNNrect.drawpNNrect();

		}
	}

	private static IntentFilter makeGattUpdateIntentFilter() {
		final IntentFilter intentFilter = new IntentFilter();
		intentFilter.addAction(BluetoothLeService.ACTION_GATT_CONNECTED);
		intentFilter.addAction(BluetoothLeService.ACTION_GATT_DISCONNECTED);
		intentFilter.addAction(BluetoothLeService.ACTION_GATT_SERVICES_DISCOVERED);
		intentFilter.addAction(BluetoothLeService.ACTION_DATA_AVAILABLE_MEASURE);
		return intentFilter;
	}

}
