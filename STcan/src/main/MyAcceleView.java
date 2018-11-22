package main;


import java.util.LinkedList;

import android.content.Context;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.graphics.Rect;
import android.graphics.Region;
import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;
import android.util.Log;
import android.view.View;

public class MyAcceleView extends View implements SensorEventListener {

    public final static int MAX_VALUES = 200;
    public final static int SCALING = 3; // for G to graphic
    private int index = 0;
    private long timekeeper;

    private final LinkedList<float[]> fifo;
    // private float[] myVal=new float[MAX_VALUES];
    private float height;
    private float width;

    private float xYLine;
    private float pX;
    private float minX;
    private float maxX;
    
    private SensorManager sma;

    public MyAcceleView(Context context) {
        super(context);
        timekeeper = android.os.SystemClock.uptimeMillis();
        fifo = new LinkedList<float[]>();
        sma = (SensorManager) context.getSystemService(Context.SENSOR_SERVICE);
        sma.registerListener(this, sma.getDefaultSensor(Sensor.TYPE_ACCELEROMETER), SensorManager.SENSOR_DELAY_FASTEST);
        pX = 0f;
        minX = 0f;
        maxX = 0f;
    }

    protected void onDraw(Canvas canvas) {
    	canvas.translate(canvas.getWidth() / 8, 100);
        height = canvas.getHeight() / 4 * 3;
        width = canvas.getWidth() / 4 * 3;
        // width = canvas.getWidth(); // 画面幅的にこれに落ち着く
        xYLine =  120;
        
   //   Log.d("MA", "MyAccel Canvas: " + width); // 確認用

        drawValues(canvas);
    }

	@Override
	public void onAccuracyChanged(Sensor sensor, int accuracy) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void onSensorChanged(SensorEvent event) {
		if (android.os.SystemClock.uptimeMillis() < timekeeper + 20) 
			return;
        timekeeper = android.os.SystemClock.uptimeMillis();
        final float[] val = new float[]{event.values[0], event.values[1], event.values[2]}; // defensive copy because keeps the same object

        if (event.sensor.getType() == Sensor.TYPE_ACCELEROMETER) {
            if (fifo.size() > MAX_VALUES) fifo.poll();
            fifo.add(val);
        	/*if(index>= MAX_VALUES) index=0;
        	myVal[index]=val[0];*/
            minX = Math.min(minX, val[0]);
            maxX = Math.max(maxX, val[0]);
            index++;
            invalidate();
        }
    }
	
	private void drawBg(Canvas canvas){
		Rect rect = new Rect();
        rect.set(0, 0, this.getMeasuredWidth() / 4 * 3, this.getMeasuredHeight() / 3);
        Paint p = new Paint();
        p.setStyle(Paint.Style.FILL);
        p.setColor(Color.WHITE);
        canvas.drawRect(rect, p);
        canvas.clipRect(rect, Region.Op.INTERSECT);
	}

    private void drawValues(Canvas canvas) {
    	drawBg(canvas);
        float len = (width) / MAX_VALUES;
        Paint p1 = new Paint();
        Paint p2 = new Paint();

        float xVal = 00;

        boolean first = true;

        //int i;
/*        for (i=0;i<=index;i++) {
            float x = myVal[i];
*/        for (float[] f : fifo) {
            float x = f[0];
            if (first) {
                pX = x;
                first = false;
            }
            p1.setColor(Color.RED);
            p2.setColor(Color.BLUE);
            canvas.drawLine(0, 50, canvas.getWidth() / 4 * 3, 50, p1);
            canvas.drawLine(xVal, xYLine - pX * SCALING, xVal + len, xYLine - x * SCALING, p2);
            Log.d("PLOT", "xVal: " + String.valueOf(xVal));
            Log.d("PLOT", "x: " + String.valueOf(x));
            xVal += len;
            pX = x;
        }
    }
}// END
