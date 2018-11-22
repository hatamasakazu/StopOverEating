package main;

import java.io.BufferedWriter;
import java.io.File;

import jp.aoyama.wil.kana.R;
import receiveddata.ECG;
import receiveddata.PqrstData;
import receiveddata.RriAverageData;
import receiveddata.RriPeakData;
import receiveddata.SerializableReceivedData;
import test.FileString;
import test.MakePath;
import test.MeasureTime;
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
import android.view.View;
import android.widget.Button;
import android.widget.LinearLayout;
import android.widget.TextView;

public class DeviceControlActivity extends Activity {
	/*public static final String EXTRAS_DEVICE_NAME = "DEVICE_NAME";
	public static final String EXTRAS_DEVICE_ADDRESS = "DEVICE_ADDRESS";*/

	private TextView mConnectionState;
	private TextView mPqrst;
	private TextView mRri;
	private TextView mTemperature;
	private TextView mAccelerationX;
	private TextView mAccelerationY;
	private TextView mAccelerationZ;
	// public static String mDeviceAddress;
	private BluetoothLeService mBluetoothLeService;
	
	
	// add by myself
	private TextView melapsedtime;
	private TextView mshtpNN50;
	private TextView mlngpNN50;
	private BufferedWriter out = null;
	
	private MeasureTime mt;
	private String et;
	
	private Button finish;
	
	private ECG ecg;
	private MakePath mp;
	
	private File dca_dir = null;
	
	// private MyDataView mv;
	
	private RealTimepNNView pNNrect;
	
	// valuable to write file name
	String file_date, file_path;
	
	/* What is called Inner Class */
	private final BroadcastReceiver mGattUpdateReceiver = new BroadcastReceiver() {
		@Override
		public void onReceive(Context context, Intent intent) {
			Log.d("BR", "into BroadcastReceiver.");
			
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
			Log.d("DCA", "into ServiceConnected of ServiceConnection.");
		}

		@Override
		public void onServiceDisconnected(ComponentName componentName) {
			mBluetoothLeService = null;
			Log.d("DCA", "into ServiceDisconnected of ServiceConnection.");
		}
	};

	/*一度しか呼ばれない
	* */
	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		
		Log.d("DCA", "onCreate start.");
		setContentView(R.layout.activity_device_control);
		
		Log.d("DCA", "set view.");
		mp = new MakePath();
		
		melapsedtime = (TextView) findViewById(R.id.elapsedtime); 

		((TextView) findViewById(R.id.device_address)).setText(ReadyActivity.mDeviceAddress);
        mConnectionState = (TextView) findViewById(R.id.connection_state);
        mPqrst = (TextView) findViewById(R.id.pqrst);
        mRri = (TextView) findViewById(R.id.rri);
        mTemperature = (TextView) findViewById(R.id.temperature);
        mAccelerationX = (TextView) findViewById(R.id.acceleration_x);
        mAccelerationY = (TextView) findViewById(R.id.acceleration_y);
        mAccelerationZ = (TextView) findViewById(R.id.acceleration_z);
        
        // add by me
        mshtpNN50 = (TextView) findViewById(R.id.shtpnn);
        mlngpNN50 = (TextView) findViewById(R.id.lngpnn);
        
        finish = (Button) findViewById(R.id.control_finish);
        finish.setOnClickListener(new PushButtonListener());

        getActionBar().setDisplayHomeAsUpEnabled(true);
        try{
    		if(out == null){
    			file_date = mp.date();
    			dca_dir = mp.createdirectry(FileString.DCAString.PATH);
    			out = mp.createPathandWriter(FileString.DCAString.PATH, FileString.DCAString.NAME, file_date); // myBeatの出力データを普通に書き出すファイル

    			out.write("time" + "," + "thermal" + "," + "x" + "," + "y" + "," + "z");
    			Log.d("DCA", "file open. "+file_path);
    		}
    		else{
    			Log.e("DCA", "out exists.");
    		}
    	}catch(Exception e){
    		Log.e("DCA", e.getMessage() + " file opening failed...");
    	}
        
        mt = new MeasureTime(melapsedtime);
        
        mt.handleMessage(new Message());
        
        /*et = mt.handleMessage2(new Message());
        melapsedtime.setText(et);*/
        
        // overrideしたハンドルメッセじゃないと無限にやり続けないらしい
        // mt.handleMessage(new Message(), melapsedtime);
        
        ecg = new ECG(file_date, FileString.MODE.DCA);
        LinearLayout rLayout = (LinearLayout)findViewById(R.id.root);
		pNNrect = new RealTimepNNView(this, ecg.getRri());
        rLayout.addView(pNNrect);
      
	}
	
	@Override
	protected void onStart() {
		super.onStart();
		
		Intent gattServiceIntent = new Intent(this, BluetoothLeService.class);
        bindService(gattServiceIntent, mServiceConnection, BIND_AUTO_CREATE);
        registerReceiver(mGattUpdateReceiver, makeGattUpdateIntentFilter());
        
        Log.d("DCA", "onStart start.");
        
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
    protected void onPause() {
        super.onPause();
        // mBluetoothLeService.displayGattServices(false);
    }

    @Override
    protected void onDestroy() {
        super.onDestroy();
        unregisterReceiver(mGattUpdateReceiver);
        unbindService(mServiceConnection);
        mBluetoothLeService = null;
        mt = null;
        
        try{
        	out.close();
        	// ReceiveUtility.ecg.close();
        	ecg.close();
        	Log.d("DCA", "file closed.");
        }catch(Exception e){
        	Log.e("DCA", e.getMessage() + " file closing failed...");
        }
    }
    
	/* 蜷�繝�繝ｼ繧ｿ蜿守ｴ榊�ｴ謇�縺ｮ蛻晄悄蛹� */
	private void clearUI() {
		melapsedtime.setText("");
		mPqrst.setText("");
        mRri.setText("");
        mTemperature.setText("");
        mAccelerationX.setText("");
        mAccelerationY.setText("");
        mAccelerationZ.setText("");
        mshtpNN50.setText("");
        mlngpNN50.setText("");
	}

	@Override
	protected void onActivityResult(int requestcode, int resultcode, Intent data){
		super.onActivityResult(requestcode, resultcode, data);
		Log.d("DCA", "onActivityResult has started.");
		
		/*mDeviceAddress = data.getStringExtra(EXTRAS_DEVICE_ADDRESS);
		Log.d("DCA", "onActivityResult, mDeviceAddress is " + mDeviceAddress);*/

	}

	class PushButtonListener implements View.OnClickListener {
		@Override
		public void onClick(View v) {
			switch(v.getId()){
			case R.id.control_finish:
				finish();
				break;
			/*case R.id.button_setting_activity:
				showSettingActivity();
				break;*/
			}
		}
	}

	/*private void showSettingActivity() {
		Intent intent = new Intent(this, DeviceSettingActivity.class);
		startActivity(intent);
	}*/

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
			
			mTemperature.setText(String.format("%.2f",data.getTemperature()));
			mAccelerationX.setText(String.format("%.3f",data.getAccelerationX()));
			mAccelerationY.setText(String.format("%.3f",data.getAccelerationY()));
			mAccelerationZ.setText(String.format("%.3f",data.getAccelerationZ()));
			
			// mv.getDrawData(data.getEcg());
			
			
			// show pNN percent as figure.
			pNNrect.drawpNNrect();
			
			// ファイルに書き込み（データ出力）
			try{
				/*データ内容
				* 日付・温度・加速度X・加速度Y・加速度Z*/
				out.newLine();
				out.write( String.valueOf(new java.util.Date()) + "," +String.valueOf(data.getTemperature()) + ","
				+ data.getAccelerationX() + "," + data.getAccelerationY() + "," + data.getAccelerationZ() );
				
			}catch(Exception e){
				Log.e("DCA", e.getMessage() + " data writing failed...");
			}
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
