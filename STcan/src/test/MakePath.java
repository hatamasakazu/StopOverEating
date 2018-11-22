package test;

import java.io.BufferedWriter;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.OutputStreamWriter;
import java.io.UnsupportedEncodingException;
import java.util.Calendar;

import android.os.Environment;
import android.util.Log;

public class MakePath {
	
	public MakePath(){
		
	}
	
	public File createdirectry(String path){
		File sthome = Environment.getExternalStorageDirectory();
		File file_dir = null;
		if (sthome.exists() && sthome.canWrite()) {
			Log.d("MP", "sthome.exists() && sthome.canWrite().");
			file_dir = new File(sthome.getAbsolutePath() + path);
			file_dir.mkdir();
		}else{
			Log.d("MP", "!sthome.exists() && !sthome.canWrite().");
		}
		return file_dir;
	}
	
	public BufferedWriter createPathandWriter(String dirpath, String filename, String date) {
		BufferedWriter out = null;
		String pathname = Environment.getExternalStorageDirectory()
				+ dirpath + date + filename;
		Log.d("RRI", pathname);
		try {
			Log.d("MP", "buffered writer before.");
			// out = new BufferedWriter(new FileWriter(new File(pathname)));
			out = new BufferedWriter(new OutputStreamWriter(new FileOutputStream(new File(pathname)), "SHIFT-JIS"));
			Log.d("MP", "buffered writer after.");
		} catch (UnsupportedEncodingException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (FileNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} 
		return out;
	}
	
	public String date(){
		String file_date;
		Calendar calendar = Calendar.getInstance();
		int year 	= calendar.get(Calendar.YEAR);
		int month 	= calendar.get(Calendar.MONTH) + 1; // -1月の値が表示されるから
		int day 	= calendar.get(Calendar.DAY_OF_MONTH);
		int hour 	= calendar.get(Calendar.HOUR_OF_DAY);
		int minute 	= calendar.get(Calendar.MINUTE);
		int second 	= calendar.get(Calendar.SECOND);
		
		file_date = year + "-" + month + "-" + day + "-" + hour + "-" + minute + "-" + second;
		
		return file_date;
	}
	
	/*public void close(BufferedWriter out){
		try {
			out.close();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}*/

}
