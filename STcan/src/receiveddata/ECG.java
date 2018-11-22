package receiveddata;

/*
* RRIデータの出力を行う（Excelへの書き出しも行う）
* ECGデータ（心電図データ）
* Excel上にデータ表示を行う（心拍波形モード）
* */

import java.io.BufferedWriter;
import java.io.File;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Date;

import test.FileString;
import test.MakePath;
import android.util.Log;

public class ECG {
	private ArrayList<VoltAndTime> ecgList = new ArrayList<VoltAndTime>();
	private RRI rri;
	private MakePath mp;
	private BufferedWriter out;
	private String path, name = FileString.ECGString.NAME;
	private File ecg_dir = null;
	
	private Integer count = 1, past_count = 29;
	private Float sum = 0.0f;
	private boolean flag1 = true, flag2 = true, flag3 = true;	// flags of first.
	private boolean sht_ok = false, lng_ok = false;
	public static final float sht = 30.0f, lng = 60.0f;
	public static int rri_soket;
	Soket soket;
	public ECG(String date, String mode) {
		mp = new MakePath();
		rri = new RRI(date, mode);
		setECGPath(mode);
		ecg_dir = mp.createdirectry(path);
		out = mp.createPathandWriter(path, name, date);
		Log.d("ECG", "made RRI file.");
	}

	/*public ArrayList<VoltAndTime> getEcgList() {
		return this.ecgList;
	}

	public void setEcg() {

	}*/
	
	public void setECGPath(String mode){
		switch(mode){
		case FileString.MODE.DCA:
			path = FileString.ECGString.DCA_PATH;
			break;
			
		case FileString.MODE.HOT:
			path = FileString.ECGString.HOT_PATH;
			break;
			
		case FileString.MODE.COM:
			path = FileString.ECGString.COM_PATH;
			break;
			
		case FileString.MODE.COLD:
			path = FileString.ECGString.COLD_PATH;
			break;
		}
	}
	public void receiveData(SerializableReceivedData data) {
		int v = 0;
		int i = 0;
		int evalue = 1450;
		int test = 0;
		int mean = 0;
		
		// ECG値,その時点での時刻をリストに保存
		Log.d("ECG", "into receivedData...");
		Date currentTime = new Date(); 								// 実行開始時刻の取得
		
		// ここで設定ごとの値(心拍波形・心拍間隔移動平均・心拍間隔ピークホールド)の判別をさせる
		
		if (data instanceof PqrstData) {
			v = data.getEcg(); 										// ここでbyte型データをECG値に変換してる(らしい)
			ecgList.add(new VoltAndTime(v, currentTime.getTime())); // リストにデータを追加
		} 
		else if(data instanceof RriAverageData
				|| data instanceof RriPeakData) {
			i = data.getEcg();
			
			if(rri.getRriList().size() >= 2 && i >= evalue){
				for(int j = 0;j < rri.getRriList().size();j++){
					test += rri.getRriList().get(j);
				}
				mean = test / rri.getRriList().size();
				i = mean;
			}
			
			rri.getRriList().add(i);								// add RRI value to rriList.
			if (flag1) { 											// if this is the first time to write data to file.
				rri.getRatList().add(rri.setRRIandTime(i, 0.0f));
				rri.getNormalizedList().add((float)i);
				flag1 = false;
			}
			else {
				sum += ((float)i / 1000.0f); 									// Millisecond convert to second, and calculate elapsed time. ちゃんと1000.0と表記しないとdouble型にキャストされない									// sumは経過時間 
				rri.getRatList().add(rri.setRRIandTime(i, sum));
				
				while(sum >= count && rri.getRatList().size() >= 2){			// 経過時間が整数秒を跨ぐ毎にリサンプリング
					rri.normalize(count);										// re-sampling
					
					Log.d("NL", "normalize rriList. Last value is " + String.valueOf(rri.getNormalizedList().getLast()));
					count++;													// set next integer second
					if(count >= sht){											// if 30sec elapsed
						sht_ok = true;
						if(count >= lng){										// if 1min elapsed
							lng_ok = true;
						}
					}
				}
			}
		}
		
		
		if(past_count < count && sht_ok == true){
			rri.calculatepNN(sht, count);
			Log.d("pNN", "calculated short period pNN is " + rri.getshtpNNList().getLast());
			
			if(lng_ok == true){
				rri.calculatepNN(lng, count);
				Log.d("pNN", "calculated long period pNN is " + rri.getLngpNNList().getLast());
				
				lng_ok = false;
			}
			
			sht_ok = false;
			past_count = count;
		}
		
		// data output to file.
		if (v != 0) {
			try {
				Log.d("PQRST", "into add...");
				
				if(flag2){											// if this is the first time to write data to file.
					out.write("PQRST[mV]" + "," + "Date[ms]");
					flag2 = false;
				}

				out.newLine();

				out.write(v + "," + currentTime.getTime());

				Log.d("PQRST", v + "," + currentTime.getTime() + String.valueOf(new Date()));
			} catch (IOException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
				Log.e("PQRST", "into add failed...");
			}
		} else {
			try {
				Log.d("RRI", "into add...");

				/*心拍間隔算出Excelファイルへの書き込み*/
				if(flag3){											// if this is the first time to write data to file.
					out.write("RRI[ms]" + "," + "elapsed time[s]" + "," + "time");
					flag3 = false;
				}

				out.newLine();
				/*心拍間隔時間[ms]，経過時間[s], */
				out.write(i + "," + sum + "," +  String.valueOf(new java.util.Date())  );
				rri_soket = i;
				//soket.run();

				Log.d("RRI", "RRI value: " + i + " elapsed time: " + sum + "time: " +  String.valueOf(new java.util.Date()) );
			} catch (IOException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
				Log.e("RRI", "into add failed...");
			}
		}

	}

	public void close() {
		try {

			if (rri.getRriList().size() != 0) {
				rri.getHRVandTotalpNN();
				out.newLine();
				out.write("NN50" + "," + rri.getNN50() + "," + "total pNN50" + "," + rri.getpNN50());
				/*out.newLine();
				out.write(rri.getNN50() + "," + rri.getpNN50());*/
			}

			out.close();
			rri.close();
			Log.d("ECG", "file closed.");
		} catch (Exception e) {
			Log.e("ECG", e.getMessage() + " file closing failed...");
		}
	}

	public RRI getRri() {
		return rri;
	}

	public void setRri(RRI rri) {
		this.rri = rri;
	}

	public class VoltAndTime { // PQRST用内部クラス
		private int volt;
		private long time;

		public VoltAndTime(int v, long t) {
			volt = v;
			time = t;
		}

		public long getTime() {
			return time;
		}

		public void setTime(long t) {
			time = t;
		}

		public int getV() {
			return volt;
		}

		public void setV(int v) {
			volt = v;
		}
	}

	public class IntAndTime { // RRI用内部クラス
		private int interval;
		private long time;

		public IntAndTime(int i, long t) {
			interval = i;
			time = t;
		}

		public long getTime() {
			return time;
		}

		public void setTime(long t) {
			time = t;
		}

		public int getInterval() {
			return interval;
		}

		public void setInterval(int i) {
			interval = i;
		}
	}
}