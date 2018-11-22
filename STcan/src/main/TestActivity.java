package main;


import test.ColdActivity;
import test.ComfortableActivity;
import test.HotActivity;
import jp.aoyama.wil.kana.R;
import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;

public class TestActivity extends Activity {
	
	private Button hot, comfortable, cold, back;

	private   EditText ipaddress;
	private   EditText portnumber;
	public static String ip_text = "";
	public  static String po_text = "";
	private  Button pushbutton;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_test);
		
		hot 		= (Button) findViewById(R.id.test_hot);
		comfortable = (Button) findViewById(R.id.test_comfortble);
		cold 		= (Button) findViewById(R.id.test_cold);
		back		= (Button) findViewById(R.id.test_back);
		
		hot.setOnClickListener(new PushButtonListener());
		comfortable.setOnClickListener(new PushButtonListener());
		cold.setOnClickListener(new PushButtonListener());
		back.setOnClickListener(new PushButtonListener());

		ipaddress = (EditText)findViewById(R.id.ipaddress);
		portnumber = (EditText)findViewById(R.id.portnumber);

		pushbutton = (Button)findViewById(R.id.ipportbutton);
		pushbutton.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View view) {
				ip_text = ipaddress.getText().toString();
				po_text = portnumber.getText().toString();
			}
		});

	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.test, menu);
		return true;
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		// Handle action bar item clicks here. The action bar will
		// automatically handle clicks on the Home/Up button, so long
		// as you specify a parent activity in AndroidManifest.xml.
		int id = item.getItemId();
		if (id == R.id.action_settings) {
			return true;
		}
		return super.onOptionsItemSelected(item);
	}
	
	class PushButtonListener implements View.OnClickListener{
		
		@Override
		public void onClick(View v) {
			// TODO Auto-generated method stub
			switch(v.getId()){
			case R.id.test_hot:
				showHotActivity();
				break;
				
			case R.id.test_comfortble:
				showComfortableActivity();
				break;
				
			case R.id.test_cold:
				showColdActivity();
				break;
				
			case R.id.test_back:
				finish();
				break;
			}
		}
	}
	
	public void showHotActivity(){
		Intent intent = new Intent(this, HotActivity.class);
		startActivity(intent);
	}
	
	public void showComfortableActivity(){
		Intent intent = new Intent(this, ComfortableActivity.class);
		startActivity(intent);
	}
	
	public void showColdActivity(){
		Intent intent = new Intent(this, ColdActivity.class);
		startActivity(intent);
	}
}
