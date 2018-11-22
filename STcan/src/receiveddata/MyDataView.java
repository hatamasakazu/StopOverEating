package receiveddata;

import java.util.LinkedList;

import android.R.integer;
import android.content.Context;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.graphics.Rect;
import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;
import android.util.Log;
import android.view.View;

public class MyDataView extends View {

    public final static int MAX_VALUES = 200;
    public final static float SCALING = 0.5f; // for G to graphic
    // private int index=0;
    private long timekeeper;

    private final LinkedList<float[]> fifo; // plotするデータを保存、抽出する場所
    // private float[] myVal=new float[MAX_VALUES];
    private float height;
    private float width;

    private float xYLine;
    private float pX;
    private float minX;
    private float maxX;

    public MyDataView(Context context) {
        super(context);
        timekeeper = android.os.SystemClock.uptimeMillis();
        fifo = new LinkedList<float[]>();
        pX = 0f;
        minX = 0f;
        maxX = 0f;
    }

    protected void onDraw(Canvas canvas) {
    	canvas.translate(canvas.getWidth() / 8, 100);
        height = canvas.getHeight() / 4 * 3;
        width = canvas.getWidth() / 4 * 3;
        xYLine =  120; // 描画開始時のY座標
        
   //   Log.d("MA", "MyAccel Canvas: " + width); // 確認用

        drawValues(canvas);
    }

	

	public void getDrawData(int value) {
		if (android.os.SystemClock.uptimeMillis() < timekeeper + 20) 
			return;
        timekeeper = android.os.SystemClock.uptimeMillis();
        // final float[] val = new float[]{event.values[0], event.values[1], event.values[2]}; // defensive copy because keeps the same object

        final float[] val = new float[]{value};
        // if (event.sensor.getType() == Sensor.TYPE_ACCELEROMETER) {
            if (fifo.size() > MAX_VALUES) fifo.poll();
            fifo.add(val);
        	/*if(index>= MAX_VALUES) index=0;
        	myVal[index]=val[0];*/
            minX = Math.min(minX, val[0]);
            maxX = Math.max(maxX, val[0]);
            // index++;
            invalidate();
        // }
    }
	
	/*private void drawBg(Canvas canvas){ // しろい四角を描く、正直いらない
		Rect rect = new Rect();
        rect.set(0, 0, this.getMeasuredWidth() / 4 * 3, this.getMeasuredHeight() / 3); // ここが重なる原因
        Paint p = new Paint();
        p.setStyle(Paint.Style.FILL);
        p.setColor(Color.WHITE);
        canvas.drawRect(rect, p);
	}*/

    private void drawValues(Canvas canvas) {
    	// drawBg(canvas); // グラフを見やすくするための白い四角を書くためのメソッド
        float len = (width) / MAX_VALUES;
        Paint p1 = new Paint();
        Paint p2 = new Paint();

        float xVal = 0; // 描画開始のx座標,　canvasのサイズと場所に合わせるほうがいいと思う

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
            canvas.drawLine(0, 50, canvas.getWidth() / 4 * 3, 50, p2);
            canvas.drawLine(xVal, xYLine - pX * SCALING, xVal + len, xYLine - x * SCALING, p1);
            Log.d("PLOT", "xVal: " + String.valueOf(xVal));
            Log.d("PLOT", "x: " + String.valueOf(x));
            xVal += len;
            pX = x;
        }
    }
}// END
