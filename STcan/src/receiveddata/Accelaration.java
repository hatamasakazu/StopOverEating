package receiveddata;

import java.util.ArrayList;

public class Accelaration {
	
	private ArrayList<AclData> accelaration = new ArrayList<AclData>();		// ここに加速度データ(3軸加速度、時刻)を保存
	
	public ArrayList<AclData> getAccelaration(){							// これで保存されたリストを返す
		return this.accelaration;
	}
	
	public Accelaration(){
		
	}
	
	public class AclData{
		private double accelarationX;
		private double accelarationY;
		private double accelarationZ;
		
		private long time;
		
		public AclData(double x, double y, double z, long ms){
			this.setAccelarationX(x);
			this.setAccelarationY(y);
			this.setAccelarationZ(z);
			this.setTime(ms);
		}
		
		public double getAccelarationX() {
			return accelarationX;
		}
		
		public void setAccelarationX(double x) {
			this.accelarationX = x;
		}
		
		public double getAccelarationY() {
			return accelarationY;
		}
		
		public void setAccelarationY(double y) {
			this.accelarationY = y;
		}
		
		public double getAccelarationZ() {
			return accelarationZ;
		}
		
		public void setAccelarationZ(double z) {
			this.accelarationZ = z;
		}
		
		public long getTime() {
			return time;
		}
		
		public void setTime(long ms) {
			this.time = ms;
		}
		
	}
}
