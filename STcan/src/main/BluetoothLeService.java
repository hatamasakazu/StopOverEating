package main;

import java.util.List;
import java.util.UUID;

import receiveddata.SerializableReceivedData;
import receiver.MeasureReceiver;
import receiver.ReceiveUtility;
import whs.WhsCommandContainer;
import whs.WhsCommands;
import whs.WhsGattAttributes;
import whs.WhsHelper;
import android.app.Service;
import android.bluetooth.BluetoothAdapter;
import android.bluetooth.BluetoothDevice;
import android.bluetooth.BluetoothGatt;
import android.bluetooth.BluetoothGattCallback;
import android.bluetooth.BluetoothGattCharacteristic;
import android.bluetooth.BluetoothGattDescriptor;
import android.bluetooth.BluetoothGattService;
import android.bluetooth.BluetoothManager;
import android.bluetooth.BluetoothProfile;
import android.content.Context;
import android.content.Intent;
import android.os.Binder;
import android.os.Handler;
import android.os.IBinder;
import android.util.Log;

public class BluetoothLeService extends Service {
    private final static String TAG = BluetoothLeService.class.getSimpleName();

    private BluetoothManager mBluetoothManager;
    private BluetoothAdapter mBluetoothAdapter;
    private String mBluetoothDeviceAddress;
    private BluetoothGatt mBluetoothGatt;
    private Handler mHandler = new Handler();
    
    public final static String ACTION_GATT_CONNECTED =
            "jp.co.whs2mybeatandroidsample.ACTION_GATT_CONNECTED";
    public final static String ACTION_GATT_DISCONNECTED =
            "jp.co.whs2mybeatandroidsample.ACTION_GATT_DISCONNECTED";
    public final static String ACTION_GATT_SERVICES_DISCOVERED =
            "jp.co.whs2mybeatandroidsample.ACTION_GATT_SERVICES_DISCOVERED";
    public final static String ACTION_DATA_AVAILABLE_MEASURE =
            "jp.co.whs2mybeatandroidsample.ACTION_DATA_AVAILABLE_MEASURE";
    public final static String ACTION_DATA_AVAILABLE_SETTING =
            "jp.co.whs2mybeatandroidsample.ACTION_DATA_AVAILABLE_SETTING";
	
    public final UUID WHS_SERVICE_UUID = 
			UUID.fromString(WhsGattAttributes.WHS_SERVICE_UUID_STRING);
	public final UUID WHS_CHARACTERISTIC_UUID = 
			UUID.fromString(WhsGattAttributes.WHS_CHARACTERISTIC_UUID_STRING);
    
	public WhsCommandContainer mLeCommandContainer = new WhsCommandContainer();
	
    private final BluetoothGattCallback mGattCallback = new BluetoothGattCallback() {
        @Override
        public void onConnectionStateChange(BluetoothGatt gatt, int status, int newState) {
            String intentAction;
            if (newState == BluetoothProfile.STATE_CONNECTED) {
                intentAction = ACTION_GATT_CONNECTED;
                broadcastUpdate(intentAction);
                mBluetoothGatt.discoverServices();

            } else if (newState == BluetoothProfile.STATE_DISCONNECTED) {
                intentAction = ACTION_GATT_DISCONNECTED;
                broadcastUpdate(intentAction);
            }
        }

        @Override
        public void onServicesDiscovered(BluetoothGatt gatt, int status) {
            if (status == BluetoothGatt.GATT_SUCCESS) {
                broadcastUpdate(ACTION_GATT_SERVICES_DISCOVERED);
            }
        }

        @Override
        public void onCharacteristicRead(BluetoothGatt gatt,
                                         BluetoothGattCharacteristic characteristic,
                                         int status) {
            if (status == BluetoothGatt.GATT_SUCCESS) {
                broadcastUpdate(characteristic);
            }
        }

        @Override
        public void onCharacteristicChanged(BluetoothGatt gatt,
                                            BluetoothGattCharacteristic characteristic) {
            broadcastUpdate( characteristic);
        }
    };

    private void broadcastUpdate(final String action) {
        final Intent intent = new Intent(action);
        sendBroadcast(intent);
    }
    
    /**
     * 繝｢繝ｼ繝峨↓繧医ｊBroadCast縺吶ｋ繝�繝ｼ繧ｿ繧貞､画峩
     */
	private void broadcastUpdate(final BluetoothGattCharacteristic characteristic) {
		if (!WHS_CHARACTERISTIC_UUID.equals(characteristic.getUuid())) return;
		final byte[] data = characteristic.getValue();
		if (!ReceiveUtility.checkMeasureMode(data)) {
			//險ｭ螳壹Δ繝ｼ繝�
			receiveSetting(ACTION_DATA_AVAILABLE_SETTING, data);
			return;
		}
		//險域ｸｬ繝｢繝ｼ繝�
		receiveMeasure(ACTION_DATA_AVAILABLE_MEASURE, data);
	}
	
	/**
	 * 險ｭ螳壹Δ繝ｼ繝画凾縺ｮ 蛟､縺ｮ螟牙喧繧帝�夂衍
	 * @param action縲�ACTION_DATA_AVAILABLE_SETTING
	 * @param data WHS-2繧医ｊ蜿嶺ｿ｡縺励◆繝�繝ｼ繧ｿ
	 */
	private void receiveSetting(final String action, final byte[] data) {
		final Intent intent = new Intent(action);
		
    	final String commandString = new String(data);
		if (data != null && data.length > 0) {
			intent.putExtra(WhsHelper.INTENT_VALUE, commandString);
			String commandText = (commandString.length() > 3)? commandString.substring(1, 3):"";
			intent.putExtra(WhsHelper.INTENT_COMMAND, commandText);
			
			if (commandText != null && commandText.equals("AA")) {
				if (commandString.substring(4, 6).equals(WhsHelper.SETTING_RESULT_OK))
					mLeCommandContainer.delete(WhsCommands.CommandSettingModeStart);
			} else if (commandText != null && commandText.equals("ZZ")) {
				if (commandString.substring(4, 6).equals(WhsHelper.SETTING_RESULT_OK))
					mLeCommandContainer.delete(WhsCommands.CommandSettingModeEnd);
			} else if (commandText != null && commandText.equals("SS")) {
				if (commandString.substring(4, 6).equals(WhsHelper.SETTING_RESULT_OK))
					mLeCommandContainer.delete(WhsCommands.CommandMeasureModeStart);
			}
		}
		sendBroadcast(intent);
    }
    
	/**
	 * 貂ｬ螳壹Δ繝ｼ繝画凾縺ｮ 蛟､縺ｮ螟牙喧繧帝�夂衍
	 * @param action縲�ACTION_DATA_AVAILABLE_MEASURE
	 * @param data WHS-2繧医ｊ蜿嶺ｿ｡縺励◆繝�繝ｼ繧ｿ
	 */
    private void receiveMeasure(final String action, final byte[] data) {
		final Intent intent = new Intent(action);
		
		MeasureReceiver receiver = ReceiveUtility.getMeasureObject(data);
        if (receiver != null) {
            SerializableReceivedData receivedData = receiver.getReceivedData();
            receivedData.setReceivedDate(System.currentTimeMillis());
            intent.putExtra(WhsHelper.INTENT_RECEIVED_OBJ, receivedData);
        }
        sendBroadcast(intent);
    }
    
    public class LocalBinder extends Binder {
        public BluetoothLeService getService() {
            return BluetoothLeService.this;
        }
    }

    @Override
    public IBinder onBind(Intent intent) {
        return mBinder;
    }

    @Override
    public boolean onUnbind(Intent intent) {
        close();
        return super.onUnbind(intent);
    }

    private final IBinder mBinder = new LocalBinder();

    public boolean initialize() {
        if (mBluetoothManager == null) {
            mBluetoothManager = (BluetoothManager) getSystemService(Context.BLUETOOTH_SERVICE);
            if (mBluetoothManager == null) {
                return false;
            }
        }

        mBluetoothAdapter = mBluetoothManager.getAdapter();
        if (mBluetoothAdapter == null) {
            Log.e(TAG, "Unable to obtain a BluetoothAdapter.");
            return false;
        }
        return true;
    }

    public boolean connect(final String address) {
        if (mBluetoothAdapter == null || address == null) {
            return false;
        }

        if (mBluetoothDeviceAddress != null && address.equals(mBluetoothDeviceAddress)
                && mBluetoothGatt != null) {
            if (mBluetoothGatt.connect()) {
                return true;
            } else {
                return false;
            }
        }

        final BluetoothDevice device = mBluetoothAdapter.getRemoteDevice(address);
        if (device == null) {
            return false;
        }
        mBluetoothGatt = device.connectGatt(this, false, mGattCallback);
        mBluetoothDeviceAddress = address;
        return true;
    }

    public void disconnect() {
        if (mBluetoothAdapter == null || mBluetoothGatt == null) {
            return;
        }
        mBluetoothGatt.disconnect();
    }

    public void close() {
        if (mBluetoothGatt == null) {
            return;
        }
        mBluetoothGatt.close();
        mBluetoothGatt = null;
    }

    public void readCharacteristic(BluetoothGattCharacteristic characteristic) {
        if (mBluetoothAdapter == null || mBluetoothGatt == null) {
            return;
        }
        mBluetoothGatt.readCharacteristic(characteristic);
    }

    public void setCharacteristicNotification(BluetoothGattCharacteristic characteristic,
                                              boolean enabled) {
        if (mBluetoothAdapter == null || mBluetoothGatt == null) return;
        
        mBluetoothGatt.setCharacteristicNotification(characteristic, enabled);

        if (WHS_CHARACTERISTIC_UUID.equals(characteristic.getUuid())) {
            BluetoothGattDescriptor descriptor = characteristic.getDescriptor(UUID.fromString(WhsGattAttributes.CLIENT_CHARACTERISTIC_CONFIG));
            
			if (descriptor == null) return;
            descriptor.setValue(BluetoothGattDescriptor.ENABLE_NOTIFICATION_VALUE);
            mBluetoothGatt.writeDescriptor(descriptor);
        }
    }

	public void writeDataToCharacteristic(final BluetoothGattCharacteristic characteristic, final byte[] dataToWrite) {
		if (mBluetoothAdapter == null || mBluetoothGatt == null) return;
		
		if (WHS_CHARACTERISTIC_UUID.equals(characteristic.getUuid())) {
			characteristic.setValue(dataToWrite);
			mBluetoothGatt.writeCharacteristic(characteristic);
		}
	}
    public List<BluetoothGattService> getSupportedGattServices() {
        if (mBluetoothGatt == null) return null;
        return mBluetoothGatt.getServices();
    }
    

	public void writeCommands() {
		mHandler.removeCallbacks(mRunnableWrite);
		mHandler.postDelayed(mRunnableWrite, 0);
	}
	
	/**
	 * 送信
	 * @param bytes :送信データ
	 */
	public void sendData(byte[] bytes){
		BluetoothGattService myService = mBluetoothGatt.getService(UUID.fromString(WhsGattAttributes.WHS_SERVICE_UUID_STRING));
		BluetoothGattCharacteristic characteristic = myService.getCharacteristic(UUID.fromString(WhsGattAttributes.WHS_CHARACTERISTIC_UUID_STRING));
		characteristic.setValue(bytes);
		mBluetoothGatt.writeCharacteristic(characteristic);
	}
	
	
    /**
     * WHS-2縺ｸ縺ｮ繧ｳ繝槭Φ繝峨�ｮ逋ｺ陦後ｒ陦後≧Runnable繧ｯ繝ｩ繧ｹ
     * 繧ｳ繝槭Φ繝峨�ｮ逋ｺ陦後�ｯ縲∵ｭ｣蟶ｸ縺ｫ邨ゅｏ繧九∪縺ｧ郢ｰ繧願ｿ斐☆
     */
	private Runnable mRunnableWrite = new Runnable() {
		@Override
		public void run() {
			if (mLeCommandContainer.count() == 0 ) {
				mHandler.removeCallbacks(mRunnableWrite);
				return;
			}
			
			writeWhsPeripheral(getSupportedGattServices());
			mHandler.postDelayed(this, 1000);
		}
	};
	
	public void displayGattServices(boolean value) {
		List<BluetoothGattService> gattServices = this.getSupportedGattServices();
		if (gattServices == null) return;
		
        for (BluetoothGattService gattService : gattServices) {
            String serviceUuid = gattService.getUuid().toString();
            if (serviceUuid.equals(WhsGattAttributes.WHS_SERVICE_UUID_STRING)){
                List<BluetoothGattCharacteristic> gattCharacteristics = gattService.getCharacteristics();
                for (BluetoothGattCharacteristic gattCharacteristic : gattCharacteristics) {
                    String uuid = gattCharacteristic.getUuid().toString();
                    if (uuid.equals(WhsGattAttributes.WHS_CHARACTERISTIC_UUID_STRING)){
                        int p = gattCharacteristic.getProperties();
						if ((p & BluetoothGattCharacteristic.PROPERTY_NOTIFY) > 0){
							 setCharacteristicNotification(gattCharacteristic, value);
							 return;
						}
                    }
                }
            }
        }
	}
	
	/**
	 * WHS縺ｮWrite逕ｨ縺ｮCharacteristic縺ｫ繝�繝ｼ繧ｿ繧呈嶌縺崎ｾｼ繧�
	 * Characteristic縺ｮUUID縺悟酔縺倥�¨otify逕ｨ縺ｮ繧ゅ�ｮ縺後≠繧九�ｮ縺ｧ豕ｨ諢�
	 * @param gattServices
	 */
	private void writeWhsPeripheral(List<BluetoothGattService> gattServices) {
		if (gattServices == null) return;

		for (BluetoothGattService gattService : gattServices) {
			String serviceUuid = gattService.getUuid().toString();
			if (serviceUuid.equals(WhsGattAttributes.WHS_SERVICE_UUID_STRING)) {
				List<BluetoothGattCharacteristic> gattCharacteristics = gattService.getCharacteristics();
				for (BluetoothGattCharacteristic gattCharacteristic : gattCharacteristics) {
					String uuid = gattCharacteristic.getUuid().toString();
					if (uuid.equals(WhsGattAttributes.WHS_CHARACTERISTIC_UUID_STRING)) {
						int p = gattCharacteristic.getProperties();
						//譖ｸ縺崎ｾｼ縺ｿ逕ｨ縺ｮCharateristic繧呈欠螳�
						if ( (p & BluetoothGattCharacteristic.PROPERTY_WRITE_NO_RESPONSE) > 0){
							for (int command : mLeCommandContainer.getmCommands()) {
								writeDataToCharacteristic(gattCharacteristic,WhsCommands.getWriteCommandBytes(command));
								return;
							}
						}
					}
				}
			}
		}
	}
}
