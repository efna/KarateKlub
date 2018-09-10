package com.example.x.karateklub.area_blagajnik;

import android.content.Intent;
import android.support.design.widget.NavigationView;
import android.support.v4.view.GravityCompat;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.Toolbar;
import android.view.Menu;
import android.view.MenuItem;

import com.example.x.karateklub.R;
import com.example.x.karateklub.area_autentifikacija.LoginActivity;
import com.example.x.karateklub.area_autentifikacija.model.AutentifikacijaResultVM;
import com.example.x.karateklub.area_blagajnik.fragment.BlagajnikOpcijeFragment;

import com.example.x.karateklub.area_blagajnik.fragment.ClanarineBlagajnikFragment;
import com.example.x.karateklub.area_blagajnik.fragment.ClanoviBlagajnikFragment;
import com.example.x.karateklub.helper.MyApiRequest;
import com.example.x.karateklub.helper.MyRunnable;
import com.example.x.karateklub.helper.MySession;


public class MainBlagajnikActivity extends AppCompatActivity implements NavigationView.OnNavigationItemSelectedListener{

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main_blagajnik);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbarBlagajnik);
        setSupportActionBar(toolbar);


        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout_blagajnik);
        ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(
                this, drawer, toolbar, R.string.navigation_drawer_open, R.string.navigation_drawer_close);
        drawer.addDrawerListener(toggle);
        toggle.syncState();

        NavigationView navigationView = (NavigationView) findViewById(R.id.nav_view_blagajnik);
        navigationView.setNavigationItemSelectedListener(this);

//        MyFragmentUtils.openAsReplace(this, R.id.fragmentContent, ClanOpcijeFragment.newInstance());
        android.support.v4.app.Fragment blagajnikOpcijeFragment=new BlagajnikOpcijeFragment();
        android.support.v4.app.FragmentManager fragmentManager=this.getSupportFragmentManager();
        android.support.v4.app.FragmentTransaction fragmentTransaction=fragmentManager.beginTransaction();
        fragmentTransaction.replace(R.id.fragmentContentBlagajnik, blagajnikOpcijeFragment);
        fragmentTransaction.addToBackStack(null);
        fragmentTransaction.commit();

    }

    @Override
    public void onBackPressed() {
        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout_blagajnik);
        if (drawer.isDrawerOpen(GravityCompat.START)) {
            drawer.closeDrawer(GravityCompat.START);
        } else {


            super.onBackPressed();

        }


    }
    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.glavni, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
       // if (id == R.id.action_settings) {
         //   return true;
        //}

        return super.onOptionsItemSelected(item);
    }

    @SuppressWarnings("StatementWithEmptyBody")
    @Override
    public boolean onNavigationItemSelected(MenuItem item) {
        // Handle navigation view item clicks here.
        int id = item.getItemId();
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbarBlagajnik);

        if (id == R.id.dbBlagajnik) {
            toolbar.setTitle(item.getTitle());
            android.support.v4.app.Fragment blagajnikOpcijeFragment=new BlagajnikOpcijeFragment();
             android.support.v4.app.FragmentManager fragmentManager=this.getSupportFragmentManager();
             android.support.v4.app.FragmentTransaction fragmentTransaction=fragmentManager.beginTransaction();
             fragmentTransaction.replace(R.id.fragmentContentBlagajnik, blagajnikOpcijeFragment);
            fragmentTransaction.addToBackStack(null);
             fragmentTransaction.commit();

        }
        else if(id==R.id.clanoviKlubaBlagajnik){
            toolbar.setTitle(item.getTitle());

            android.support.v4.app.Fragment clanoviBlagajnikFr=new ClanoviBlagajnikFragment();
            android.support.v4.app.FragmentManager fragmentManager=this.getSupportFragmentManager();
            android.support.v4.app.FragmentTransaction fragmentTransaction=fragmentManager.beginTransaction();
            fragmentTransaction.replace(R.id.fragmentContentBlagajnik, clanoviBlagajnikFr);
            fragmentTransaction.addToBackStack(null);
            fragmentTransaction.commit();
        }

        else if (id == R.id.clanarineClanaBlagajnik) {
            toolbar.setTitle(item.getTitle());
            android.support.v4.app.Fragment clanarineBlagajnikFragment=new ClanarineBlagajnikFragment();
            android.support.v4.app.FragmentManager fragmentManager=this.getSupportFragmentManager();
            android.support.v4.app.FragmentTransaction fragmentTransaction=fragmentManager.beginTransaction();
            fragmentTransaction.replace(R.id.fragmentContentBlagajnik, clanarineBlagajnikFragment);
            fragmentTransaction.addToBackStack(null);
            fragmentTransaction.commit();

        }
        else if (id == R.id.odjavaBlagajnika) {
            AutentifikacijaResultVM logiraniKorisnik= MySession.getKorisnik();
            final Integer tokenId=logiraniKorisnik.tokenId;
            MyApiRequest.delete(this, "Autentifikacija/Logout/"+tokenId, new MyRunnable<Integer>(){

                @Override
                public void run(Integer o) {
                }
            });
            MySession.setKorisnik( null);



            startActivity(new Intent(this, LoginActivity.class));
        }

        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout_blagajnik);
        drawer.closeDrawer(GravityCompat.START);
        return true;
    }
}
