package receiveddata;

public class RriAverageData implements SerializableReceivedData {

	private static final long serialVersionUID = 1L;
	private int rri;
	private long receivedMillis;
    private double temperature;
    private double accelerationX;
    private double accelerationY;
    private double accelerationZ;

    public RriAverageData(){
    }

    @Override
	public int getEcg(){
    	return getRri();
    }
    
    @Override
	public long getReceivedDate(){
    	return receivedMillis;
    }
    
    @Override
	public void setReceivedDate(long date){
    	receivedMillis = date;
    }
    
    public void setRri(int rri){
        this.rri = rri;
    }

    public void setTemperature(double temperature){
        this.temperature = temperature;
    }

    public void setAccelerationX(double accelerationX){
        this.accelerationX = accelerationX;
    }

    public void setAccelerationY(double accelerationY){
        this.accelerationY = accelerationY;
    }

    public void setAccelerationZ(double accelerationZ){
        this.accelerationZ = accelerationZ;
    }

    public int getRri(){
        return this.rri;
    }

    @Override
	public double getTemperature(){
        return this.temperature;
    }

    @Override
	public double getAccelerationX(){
        return this.accelerationX;
    }

    @Override
	public double getAccelerationY(){
        return this.accelerationY;
    }

    @Override
	public double getAccelerationZ(){
        return this.accelerationZ;
    }
}