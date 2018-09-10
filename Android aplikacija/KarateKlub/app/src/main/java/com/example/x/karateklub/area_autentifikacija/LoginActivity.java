package com.example.x.karateklub.area_autentifikacija;

import android.content.Intent;
import android.os.Build;
import android.os.Bundle;
import android.support.design.widget.Snackbar;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;
import com.example.x.karateklub.R;
import com.example.x.karateklub.area_autentifikacija.model.AutentifikacijaLoginPostVM;
import com.example.x.karateklub.area_autentifikacija.model.AutentifikacijaResultVM;
import com.example.x.karateklub.area_blagajnik.MainBlagajnikActivity;
import com.example.x.karateklub.area_clan.MainClanActivity;
import com.example.x.karateklub.area_trener.MainTrenerActivity;
import com.example.x.karateklub.helper.MyApiRequest;
import com.example.x.karateklub.helper.MyRunnable;
import com.example.x.karateklub.helper.MySession;


public class LoginActivity extends AppCompatActivity
{

    private EditText korisnickoime;
    private EditText password;

    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        korisnickoime = (EditText) findViewById(R.id.korisnickoime);
        password = (EditText) findViewById(R.id.lozinka);
        Button btnLogin  = (Button) findViewById(R.id.btnprijava);



        btnLogin.setOnClickListener(new View.OnClickListener()
        {
            @Override
            public void onClick(View v)
            {
            if(ValidirajPodatkeZaLogin())
            do_btnLogin_click();
        }
        });
    }
private boolean ValidirajPodatkeZaLogin(){
        if(korisnickoime.getText().length()==0)
            korisnickoime.setError("Korisničko ime je obavezno polje!");
        else
            korisnickoime.setError(null);
        if(password.getText().length()==0)
            password.setError("Lozinka je obavezno polje!");
        else
            password.setError(null);
        if(korisnickoime.getText().length()==0 || password.getText().length()==0)
            return false;
        else
            return true;
}

    private void do_btnLogin_click()
    {

        String deviceInfo = android.os.Build.DEVICE+" | " +  android.os.Build.VERSION.RELEASE + " | " + android.os.Build.PRODUCT + " | " + Build.MODEL;


       AutentifikacijaLoginPostVM model = new AutentifikacijaLoginPostVM(korisnickoime.getText().toString(), password.getText().toString(), deviceInfo);

        MyApiRequest.post(this, "Autentifikacija/LoginCheck", model, new MyRunnable<AutentifikacijaResultVM>() {
            @Override
            public void run(AutentifikacijaResultVM x) {
                checkLogin(x);
            }
        });


    }
    private void checkLogin(AutentifikacijaResultVM x) {
        if (x==null)
        {
            View parentLayout = findViewById(android.R.id.content);
            Snackbar.make(parentLayout, "Pogrešan usernam/password", Snackbar.LENGTH_LONG).show();
       }
        else
        {

            MySession.setKorisnik( x);
            if(x.isClanKluba==true  || x.isTrener==true || x.isBlagajnik==true)
            Toast.makeText(LoginActivity.this, "Dobro došli,"+" "+x.ime+" "+x.prezime, Toast.LENGTH_LONG).show();

            if(x.isClanKluba==true)
            startActivity(new Intent(this, MainClanActivity.class));
            else if(x.isBlagajnik==true)
                startActivity(new Intent(this, MainBlagajnikActivity.class));
            else if(x.isTrener==true)
                startActivity(new Intent(this, MainTrenerActivity.class));
            else
            {
                Toast.makeText(LoginActivity.this, "Prijava nije moguća pod ovim korisničkim nalogom.", Toast.LENGTH_LONG).show();


            }

        }
    }

}


