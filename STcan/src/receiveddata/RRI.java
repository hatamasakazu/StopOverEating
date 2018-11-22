package receiveddata;

/*norm,sht,lngデータの算出（サンプリングを行う）
Excelデータへの書き込みも行う*/
import java.io.BufferedWriter;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Date;
import java.util.LinkedList;

import test.FileString;
import test.MakePath;
import android.util.Log;

public class RRI {
	// private ArrayList<Integer> rriList = new ArrayList<Integer>();
	private LinkedList<Integer> rriList = new LinkedList<Integer>(); 		// List of RRI.
	// private ArrayList<VoltAndTime> ecg = new ArrayList<VoltAndTime>();
	private LinkedList<RRIandTime> ratList = new LinkedList<RRIandTime>(); 	// List of RRI and Time.
	private LinkedList<Float> normalizedList = new LinkedList<Float>(); 		// List of normalized RRI value.
	private LinkedList<Float> shtpNNList = new LinkedList<Float>(); 		// List of calculated pNN
	private LinkedList<Float> lngpNNList = new LinkedList<Float>();			// sht: every 30sec lng: every 10min
	private LinkedList<Float> ttlpNNList = new LinkedList<Float>();			// ttl: pNN of total measuring period.
	// private LinkedList<Float> latestpNNList = new LinkedList<Float>();		// List of latest 30sec pNN
	
	private int NN50 = 0;
	private float shtpNN50 = 0.0f;
	private float lngpNN50 = 0.0f;
	private float ttlpNN50 = 0.0f;
	// private float latestpNN = 0.0f;
	
	private ArrayList<Integer> hrvList = new ArrayList<Integer>();
	
	// ops to write down data to file
	private BufferedWriter shtout = null, lngout = null, normout = null, latestout = null;
	private String pNN_path, norm_path, mode_path, test_path = FileString.RRIString.TEST_PATH;
	private boolean flag = true;
	private MakePath mp;
	
	private File pNN_dir = null, norm_dir = null, test_dir = null, mode_dir = null;
	public RRI(String date, String mode) {
		mp = new MakePath();
		setPathandName(mode);;
		
		Log.d("RRI", "path is : " + pNN_path + " and name is : " + norm_path);
		
		if(shtout == null && lngout == null && normout == null && latestout == null){
			try {
				test_dir = mp.createdirectry(test_path);
				mode_dir = mp.createdirectry(mode_path);
				pNN_dir = mp.createdirectry(pNN_path);
				norm_dir = mp.createdirectry(norm_path);
				
				Log.d("RRI", "test_dir is :" + test_dir);
				Log.d("RRI", "mode_dir is :" + mode_dir);
				Log.d("RRI", "pNN_dir is :" + pNN_dir);
				Log.d("RRI", "norm_dir is :" + norm_dir);
				
				shtout = mp.createPathandWriter(pNN_path, FileString.RRIString.sht_name, date);
				lngout = mp.createPathandWriter(pNN_path, FileString.RRIString.lng_name, date);
				normout = mp.createPathandWriter(norm_path, FileString.RRIString.norm_name, date);
				latestout = mp.createPathandWriter(pNN_path, FileString.RRIString.latest_name, date);
				
				Log.d("RRI", "Succeeded  make output writer.");
				Log.d("RRI", "True or False, does shtout exist? : " + shtout);
				
				if(flag){
					/*各Excelデータの内容表示を行う*/
					shtout.write("cur_time" + "," + "pNN");
					lngout.write("cur_time" + "," + "pNN");
					normout.write("index" + "," + "norm");
					latestout.write("cur_time" + "pNN");
					flag = false;
				}
				Log.d("RRI", "made pNN and normalize files.");
			} catch (FileNotFoundException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			} catch (IOException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		}
		else{
			Log.d("HA", "outers has already existed");
		}
		
	}

	public String getpNN_path() {
		return pNN_path;
	}

	public void setpNN_path(String pNN_path) {
		this.pNN_path = pNN_path;
	}

	public String getNorm_path() {
		return norm_path;
	}

	public void setNorm_path(String norm_path) {
		this.norm_path = norm_path;
	}

	public String getMode_path() {
		return mode_path;
	}

	public void setMode_path(String mode_path) {
		this.mode_path = mode_path;
	}

	public LinkedList<Integer> getRriList() {
		return rriList;
	}

	public void setRriList(LinkedList<Integer> rriList) {
		this.rriList = rriList;
	}

	public ArrayList<Integer> getHrv() {
		return hrvList;
	}

	public void setHrv(ArrayList<Integer> hrv) {
		this.hrvList = hrv;
	}

	public double getpNN50() {
		return ttlpNN50;
	}

	public void setpNN50(float pNN50) {
		this.ttlpNN50 = pNN50;
	}

	public int getNN50() {
		return NN50;
	}

	public void setNN50(int nn50) {
		NN50 = nn50;
	}

	public LinkedList<Float> getshtpNNList() {
		return shtpNNList;
	}

	public void setshtpNNList(LinkedList<Float> pNNList) {
		this.shtpNNList = pNNList;
	}

	public LinkedList<Float> getLngpNNList() {
		return lngpNNList;
	}

	public void setLngpNNList(LinkedList<Float> lngpNNList) {
		this.lngpNNList = lngpNNList;
	}

	public LinkedList<Float> getTtlpNNList() {
		return ttlpNNList;
	}

	public void setTtlpNNList(LinkedList<Float> ttlpNNList) {
		this.ttlpNNList = ttlpNNList;
	}

	public LinkedList<Float> getNormalizedList() {
		return normalizedList;
	}

	public void setNormalizedList(LinkedList<Float> normalizedList) {
		this.normalizedList = normalizedList;
	}

	public LinkedList<RRIandTime> getRatList() {
		return ratList;
	}

	public void setRatList(LinkedList<RRIandTime> ratList) {
		this.ratList = ratList;
	}
	
	public RRI.RRIandTime setRRIandTime(Integer rri, Float time){
		return new RRIandTime(rri, time);
	}
	
	public void setPathandName(String mode){
		switch(mode){
		case FileString.MODE.HOT:
			setMode_path(FileString.RRIString.HOT_PATH);
			setpNN_path(FileString.RRIString.HOT_pNNPATH);
			setNorm_path(FileString.RRIString.HOT_NORMPATH);
			break;
		case FileString.MODE.COM:
			setMode_path(FileString.RRIString.COM_PATH);
			setpNN_path(FileString.RRIString.COM_pNNPATH);
			setNorm_path(FileString.RRIString.COM_NORMPATH);
			break;
		case FileString.MODE.COLD:
			setMode_path(FileString.RRIString.COLD_PATH);
			setpNN_path(FileString.RRIString.COLD_pNNPATH);
			setNorm_path(FileString.RRIString.COLD_NORMPATH);
			break;
		case FileString.MODE.DCA:
			setpNN_path(FileString.RRIString.DCA_pNNPATH);
			setNorm_path(FileString.RRIString.DCA_NORMPATH);
			break;
		}
	}

	public static int rri;
	public void getHRVandTotalpNN(){
		NN50 = 0;
		for (int i = 0; i < rriList.size() - 1; i++) {
			int abs = Math.abs(rriList.get(i)
					- rriList.get(i + 1));
			hrvList.add(abs);
			rri = abs;
			//Log.d("hai","rri"+rri);
			if(abs >= 50)
				NN50++;
		}
		
		// setpNN50(NN50 / hrvList.size());
		ttlpNN50 = ((float)NN50 / (float)hrvList.size());
		
		Log.d("RRI","NN50 is " + NN50 + ", list size is " + hrvList.size() + " calculated pNN50: " + String.format("%.3f", ttlpNN50));
	}

	public  static int OnRRIreturn(){
		return rri;
	}

	/*正規化を行う*/
	public void normalize(int index){
		float temp;
		int A1 = ratList.get(ratList.size() - 1).getRRI(),
			A2 = ratList.get(ratList.size() - 2).getRRI();
		
		Log.d("NL", "A1: " + A1 +", A2: " + A2);
		
		Float t1 = ratList.get(ratList.size() - 1).getProcessTime(),
			   t2 = ratList.get(ratList.size() - 2).getProcessTime();
		
		Log.d("NL", "t1: " + t1 + ", t2: " + t2 + ", index: " + index);

		//tempは時刻Tにおけるサンプリング値（正規化を行う）
		temp = (float)((A1 - A2) * ((float)index - t2)) / (t1 - t2) + (float)A2;
		
		normalizedList.add(temp);
		
		try {
			/*excelのnorm
			* indexは時間，tempは正規化後の間隔時間[ms]*/
			normout.newLine();
			normout.write(index + "," + temp);
			
			Log.d("NL", "file output index:" + index + ", value: " + temp);
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

	/*pNNの計算を行う*/
	public void calculatepNN(float period, int count){
		NN50 = 0;
		int a = 1;
		for(int i = count - 1; a < period; i--){
			double temp = Math.abs(normalizedList.get(i) - normalizedList.get(i-1)); 	//隣接区間の差分を取得する
			Log.d("NL", "normalized List size:" + normalizedList.size());
			
			// Log.d("pNN", "temp: " + temp);
			Log.d("pNN", "index:" + i);
			
			if(temp > 50.0){ //隣接区間の差分が50以上だったらNN50をカウントする
				NN50++;
				// Log.d("pNN", "NN50: " + NN50);
			}
			a++;
		}

		/*ECGshtはECG.javaで指定した数　=　すなわち30である
		* window_sizeが30の時
		* period = ECG.sht = 30 のとき　NN50/30 を行いshortpNN50を算出
		*/
		if (period == ECG.sht){
			shtpNN50 = ((float)NN50 / period);
			// Log.d("pNN", "pNN before add to List: " + shtpNN50);
			shtpNNList.add(shtpNN50);
			
			// write down to file
			try {
				shtout.newLine();
				shtout.write(new Date() + "," + shtpNN50);
			} catch (IOException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		}
		else if(period == ECG.lng){
			lngpNN50 = ((float)NN50 / period);
			lngpNNList.add(lngpNN50);
			
			// write down to file
			try {
				lngout.newLine();
				lngout.write(new Date() +"," + lngpNN50);
			} catch (IOException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		}
		
	}
	
	public void close(){
		try {
			shtout.close();
			lngout.close();
			normout.close();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
	}

	public class RRIandTime{
		private Integer RRI;
		private Float elapsedTime;
		
		public RRIandTime(Integer rri, Float time) {
			setRRI(rri);
			setProcessTime(time);
		}
		
		public Integer getRRI() {
			return RRI;
		}
		public void setRRI(Integer rri) {
			RRI = rri;
		}
		public Float getProcessTime() {
			return elapsedTime;
		}
		public void setProcessTime(Float processTime) {
			this.elapsedTime = processTime;
		}
	}
}
