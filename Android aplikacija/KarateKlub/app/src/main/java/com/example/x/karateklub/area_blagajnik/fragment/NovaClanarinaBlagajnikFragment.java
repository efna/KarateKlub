package com.example.x.karateklub.area_blagajnik.fragment;

import android.app.DatePickerDialog;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v7.app.AppCompatActivity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.ImageButton;
import android.widget.Toast;

import com.example.x.karateklub.R;
import com.example.x.karateklub.area_autentifikacija.LoginActivity;
import com.example.x.karateklub.area_blagajnik.model.ClanarineAddVM;
import com.example.x.karateklub.helper.MyApiRequest;


import java.util.Calendar;

public class NovaClanarinaBlagajnikFragment extends Fragment {
    private EditText txtNazivClanarine;
    private EditText txtDatumOd;
    private EditText txtDatumDo;
    Calendar kalendar;
    int dan,mjesec,godina;
private Button btnSpremi;
    private ImageButton btnBack;


    public  static NovaClanarinaBlagajnikFragment newInstance()
    {
        return new NovaClanarinaBlagajnikFragment();
    }

    public NovaClanarinaBlagajnikFragment() {
        // Required empty public constructor
    }



    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        if (getArguments() != null) {
        }
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        final View view=inflater.inflate(R.layout.layout_nova_clanarina_blagajnik, container, false);
        getActivity().setTitle("Nova članarina");
txtNazivClanarine=view.findViewById(R.id.txtNazivClanarine);
        txtDatumOd=view.findViewById(R.id.txtDatumOd);
        txtDatumDo=view.findViewById(R.id.txtDatumDo);
        btnSpremi=view.findViewById(R.id.btnSpremiNovuClanarinu);
        btnBack=view.findViewById(R.id.btnBackNovaClanarinaBlagajnik);

        kalendar=Calendar.getInstance();
dan=kalendar.get(Calendar.DAY_OF_MONTH);
mjesec=kalendar.get(Calendar.MONTH);
godina=kalendar.get(Calendar.YEAR);

txtDatumOd.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                DatePickerDialog datePickerDialog=new DatePickerDialog(getActivity(), new DatePickerDialog.OnDateSetListener() {
                    @Override
                    public void onDateSet(DatePicker view, int godina, int mjesec, int dan) {
                        mjesec=mjesec+1;
                       String d="";
                       String m="";
                        if(mjesec < 10){

                            m = "0" + mjesec;
                        }
                        if(dan < 10){

                            d  = "0" + dan ;
                        }

                        if(mjesec<10 && dan<10)
                            txtDatumOd.setText(d.toString()+"/"+m.toString()+"/"+godina);
                        else if(mjesec<10 && dan>10)
                            txtDatumOd.setText(dan+"/"+m.toString()+"/"+godina);
                        else if(mjesec>10 && dan<10)
                            txtDatumOd.setText(d.toString()+"/"+mjesec+"/"+godina);
                        else
                            txtDatumOd.setText(dan+"/"+mjesec+"/"+godina);
                    }
                },godina,mjesec,dan);
                datePickerDialog.show();
            }
        });
        txtDatumDo.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                DatePickerDialog datePickerDialog=new DatePickerDialog(getActivity(), new DatePickerDialog.OnDateSetListener() {
                    @Override
                    public void onDateSet(DatePicker view, int godina, int mjesec, int dan) {
                        mjesec=mjesec+1;
                        String d="";
                        String m="";
                        if(mjesec < 10){

                            m = "0" + mjesec;
                        }
                        if(dan < 10){

                            d  = "0" + dan ;
                        }
                        if(mjesec<10 && dan<10)
                        txtDatumDo.setText(d.toString()+"/"+m.toString()+"/"+godina);
                        else if(mjesec<10 && dan>10)
                            txtDatumDo.setText(dan+"/"+m.toString()+"/"+godina);
                        else if(mjesec>10 && dan<10)
                            txtDatumDo.setText(d.toString()+"/"+mjesec+"/"+godina);
else
                            txtDatumDo.setText(dan+"/"+mjesec+"/"+godina);


                    }
                },godina,mjesec,dan);
                datePickerDialog.show();
            }
        });
        btnSpremi.setOnClickListener(new View.OnClickListener()
        {
            @Override
            public void onClick(View v)
            {
                if(ValidirajPodatkeNoveClanarine())
                    do_btnSpremi_click();
            }
        });
        btnBack.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                do_btnBackClick();
            }
        });

        return  view;
    }
    @Override
    public void onResume() {
        super.onResume();
        ((AppCompatActivity)getActivity()).getSupportActionBar().hide();
    }
    @Override
    public void onStop() {
        super.onStop();
        ((AppCompatActivity)getActivity()).getSupportActionBar().show();
    }
    private void do_btnBackClick(){
        android.support.v4.app.Fragment clanarineBlagajnikFragment=new ClanarineBlagajnikFragment();
        android.support.v4.app.FragmentManager fragmentManager=this.getFragmentManager();
        android.support.v4.app.FragmentTransaction fragmentTransaction=fragmentManager.beginTransaction();
        fragmentTransaction.replace(R.id.fragmentContentBlagajnik, clanarineBlagajnikFragment);
        fragmentTransaction.addToBackStack(null);
        fragmentTransaction.commit();
    }
    private boolean ValidirajPodatkeNoveClanarine(){



        if(txtNazivClanarine.getText().length()==0)
            txtNazivClanarine.setError("Naziv je obavezno polje!");
        else
            txtNazivClanarine.setError(null);
        if(txtDatumOd.getText().length()==0 )
            txtDatumOd.setError("Datum početka je obavezno polje!");
        else
            txtDatumOd.setError(null);
        if(txtDatumDo.getText().length()==0 )
            txtDatumDo.setError("Datum završetka je obavezno polje!");
        else
            txtDatumDo.setError(null);
        if(txtNazivClanarine.getText().length()==0 || txtDatumOd.getText().length()==0 || txtDatumDo.getText().length()==0)
            return false;
        else
            return true;
    }

    private void do_btnSpremi_click()
    {

        ClanarineAddVM novaClanarina = new ClanarineAddVM(txtNazivClanarine.getText().toString(), txtDatumOd.getText().toString(),
                txtDatumDo.getText().toString());

        MyApiRequest.post(getActivity(), "Clanarine/Add", novaClanarina, null);
        Toast.makeText(getActivity(), "Članarina je uspješno dodana.", Toast.LENGTH_LONG).show();

        android.support.v4.app.Fragment clanarineBlagajnikFragment=new ClanarineBlagajnikFragment();
        android.support.v4.app.FragmentManager fragmentManager=this.getFragmentManager();
        android.support.v4.app.FragmentTransaction fragmentTransaction=fragmentManager.beginTransaction();
        fragmentTransaction.replace(R.id.fragmentContentBlagajnik, clanarineBlagajnikFragment);
        fragmentTransaction.addToBackStack(null);
        fragmentTransaction.commit();

    }
}
