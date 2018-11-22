package test;

import java.text.SimpleDateFormat;
import java.util.TimeZone;

import android.os.Handler;
import android.os.Message;
import android.util.Log;
import android.widget.TextView;

public class MeasureTime extends Handler {
	
	private long start;
	private SimpleDateFormat sdf;
	private TextView tv;
	
	public MeasureTime(TextView melapsedtime) {
		tv = melapsedtime;
		setStart(System.currentTimeMillis());
		sdf = new SimpleDateFormat("HH:mm:ss");
		sdf.setTimeZone(TimeZone.getTimeZone("GMT"));
	}
	
	public long getStart() {
		return start;
	}

	public void setStart(long start) {
		this.start = start;
	}
	
	@Override
	public void handleMessage(Message msg) {
		removeMessages(0);
		String timelabel = sdf.format(System.currentTimeMillis() - start);
		Log.d("MT", "elapsed time is: " + timelabel);
		
		Log.d("MT", "want to set text: " + timelabel);
		tv.setText(timelabel);
		Log.d("MT", "view set text: " + tv.toString());
		
		sendMessageDelayed(obtainMessage(0), 100);
	}

	public String handleMessage2(Message msg){
		removeMessages(0);
		String timelabel = sdf.format(System.currentTimeMillis() - start);
		Log.d("MT", "elapsed time is: " + timelabel);
		sendMessageDelayed(obtainMessage(0), 100);
		
		return timelabel;
	}
	
	public void handleMessage(Message msg, TextView melapsedtime){
		removeMessages(0);
		String timelabel = sdf.format(System.currentTimeMillis() - start);
		Log.d("MT", "elapsed time is: " + timelabel);
		
		Log.d("MT", "want to set text: " + timelabel);
		melapsedtime.setText(timelabel);
		Log.d("MT", "view set text: " + melapsedtime.toString());
		
		sendMessageDelayed(obtainMessage(0), 100);
		
	}

}
