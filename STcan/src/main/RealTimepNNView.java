package main;

import receiveddata.RRI;
import android.content.Context;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.graphics.RectF;
import android.util.Log;
import android.view.View;

public class RealTimepNNView  extends View{
	
	private float borderpoint;
	private float height;
    private float width;
	
	private RectF fixrect;
	private RectF moverect;
	private Paint p1, p2;
	
	private RRI rri;
	
	public RealTimepNNView(Context context){
		super(context);
		
		fixrect = new RectF();
		moverect = new RectF();
		p1 = new Paint();
		p2 = new Paint();
		//startcanvas = new Canvas();
		
		// this.rri = new RRI("0");
		
	}
	public RealTimepNNView(Context context, RRI rri){
		super(context);
		
		fixrect = new RectF();
		moverect = new RectF();
		p1 = new Paint();
		p2 = new Paint();
		//startcanvas = new Canvas();
		
		this.rri = rri;
		
	}

	public double getBorderpoint() {
		return borderpoint;
	}

	public void setBorderpoint(float borderpoint) {
		this.borderpoint = borderpoint;
	}
	
	@Override
	protected void onDraw(Canvas canvas) {
		
		height = canvas.getHeight();
		width = canvas.getWidth();
		Log.d(VIEW_LOG_TAG, String.valueOf(width)+String.valueOf(height));
		
		drawpNNrectangle(canvas);
	}
	
	public void drawpNNrect(){
		Log.d(VIEW_LOG_TAG, "invalidate");
		invalidate();
	}
	
	private void drawpNNrectangle(Canvas canvas){ 					// 位置の指定
		//drawBg(canvas);
		fixrect.set(0f, 0, (float)this.getMeasuredWidth(), 100);
		p1.setColor(Color.YELLOW);
		canvas.drawRect(fixrect, p1);
		if(!rri.getshtpNNList().isEmpty())
			setBorderpoint(rri.getshtpNNList().getLast());
		
		if(!rri.getshtpNNList().isEmpty()){
			moverect.set(0f, 0, borderpoint * (float)this.getMeasuredWidth(), 100);
			p2.setColor(Color.GREEN);
			canvas.drawRect(moverect, p2);
		}
	}

}
