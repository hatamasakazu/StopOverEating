package receiveddata;

import java.util.ArrayList;

public class Temperature {
	private ArrayList<TempData> temperature = new ArrayList<TempData>();
	
	public Temperature(){
		
	}
	
	public ArrayList<TempData> getTemperature(){
		return this.temperature;
	}
	
	public class TempData{
		private int temp;
		private long time;
		
		public TempData(int t, long ms){
			this.setTemp(t);
			this.setTime(ms);
		}

		public int getTemp() {
			return temp;
		}

		public void setTemp(int t) {
			this.temp = t;
		}

		public long getTime() {
			return time;
		}

		public void setTime(long ms) {
			this.time = ms;
		}
		
	}

}
