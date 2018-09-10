package com.example.x.karateklub.helper;

import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Intent;
import android.os.AsyncTask;
import android.support.design.widget.Snackbar;
import android.view.View;
import android.widget.Toast;

import com.example.x.karateklub.area_autentifikacija.LoginActivity;
import com.example.x.karateklub.area_clan.model.StecenaZvanjaPregledVM;
import com.google.common.reflect.TypeToken;
import com.google.gson.Gson;

import java.lang.reflect.Type;
import java.util.ArrayList;
import java.util.List;

public class MyApiRequest {
    private static <T> void request(final Activity activity, final String urlAction, final MyUrlConnection.HttpMethod httpMethod, final Object postObject, final MyRunnable<T> myCallback) {
        new AsyncTask<Void, Void, MyApiResult>() {
            private ProgressDialog progressDialog;

            @Override
            protected void onPreExecute() {
                progressDialog = ProgressDialog.show(activity, "Učitavanje", "Sačekajte...");
            }

            @Override
            protected MyApiResult doInBackground(Void... voids)
            {
                String jsonPostObject = postObject==null?null:MyGson.build().toJson(postObject);
                return MyUrlConnection.request(MyConfig.baseUrl +"/"+ urlAction, httpMethod, jsonPostObject, "application/json");
            }

            @Override
            protected void onPostExecute(MyApiResult result) {

                progressDialog.dismiss();

                if (result.isException)
                {
                    if (result.resultCode == 401)
                    {
                        Toast.makeText(activity, "Niste logirani", Toast.LENGTH_LONG).show();
                        activity.startActivity(new Intent(activity, LoginActivity.class));
                    }
                    else {
                        View parentLayout = activity.findViewById(android.R.id.content);
                        Snackbar snackbar;
                        if (result.resultCode == 0) {
                            snackbar = Snackbar.make(parentLayout, "Greška u komunikaciji sa serverom.", Snackbar.LENGTH_LONG);
                        }
                        else if(result.resultCode==204){
                            String naziv=activity.getClass().getSimpleName();
                            boolean istiNaziv = "LoginActivity".equals(naziv);
                            if(istiNaziv==true)
                               snackbar = Snackbar.make(parentLayout, "Pogrešan username ili password.", Snackbar.LENGTH_LONG);

                                else
                                snackbar = Snackbar.make(parentLayout, "Nema dostupnih podataka.", Snackbar.LENGTH_LONG);

                        }
                        else if(result.resultCode==404){

                            snackbar = Snackbar.make(parentLayout, "Nema dostupnih podataka.", Snackbar.LENGTH_LONG);

                        }
                        else {
                            snackbar = Snackbar.make(parentLayout, "Greška " + result.resultCode + ": " + result.errorMessage, Snackbar.LENGTH_LONG);
                        }

                        snackbar.setAction("Ponovi", new View.OnClickListener() {
                            @Override
                            public void onClick(View v) {
                                MyApiRequest.get(activity, urlAction, myCallback);
                            }
                        });
                        snackbar.show();
                    }
                }
                else
                {
                    if (myCallback !=null) {
                        Type genericType = myCallback.getGenericType();

                        T x = null;
                        try {
                        x = MyGson.build().fromJson(result.value, genericType);

                        } catch (Exception e) {
                            View parentLayout = activity.findViewById(android.R.id.content);
                            Snackbar.make(parentLayout, "Greška u aplikaciji. ", Snackbar.LENGTH_LONG).show();
                        }
                        myCallback.run(x);
                    }
                }
            }
        }.execute();
    }


    public static <T> void get(final Activity activity, final String urlAction, final MyRunnable<T> myCallback)
    {
        request(activity, urlAction, MyUrlConnection.HttpMethod.GET, null, myCallback);
    }


    public static <T> void delete(Activity activity, String urlAction, MyRunnable<T> myCallback)
    {
        request(activity, urlAction, MyUrlConnection.HttpMethod.DELETE, null, myCallback);
    }

    public static <T> void post(Activity activity, final String urlAction, Object postObject,  final MyRunnable<T> myCallback)
    {
        request(activity, urlAction, MyUrlConnection.HttpMethod.POST, postObject, myCallback);
    }

    public static <T> void put(Activity activity, final String urlAction, Object postObject,  final MyRunnable<T> myCallback)
    {
        request(activity, urlAction, MyUrlConnection.HttpMethod.PUT, postObject, myCallback);
    }
}
