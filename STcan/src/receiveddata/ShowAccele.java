package receiveddata;

import android.app.Activity;
import android.os.Bundle;
import android.util.Log;
import android.widget.LinearLayout;

public class ShowAccele extends Activity {
    /** Called when the activity is first created. */
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setTitle("Show Accelerometer Values");
		
        MyDataView av = new MyDataView(this);
       /* TextView tv=new TextView(this);
		tv.setText("HELLO");*/
		LinearLayout mLayout = (LinearLayout) new LinearLayout(this);
		
		Log.d("SA", "ShowAccel canvas: " + mLayout.getWidth());
		// mLayout.addView(tv);
		mLayout.addView(av);
        setContentView(mLayout);		
    }
}