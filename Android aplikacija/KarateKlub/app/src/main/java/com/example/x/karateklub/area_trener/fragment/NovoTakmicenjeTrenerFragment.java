package com.example.x.karateklub.area_trener.fragment;

import android.app.DatePickerDialog;
import android.graphics.Color;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v7.app.AppCompatActivity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.ImageButton;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import com.example.x.karateklub.R;
import com.example.x.karateklub.area_trener.model.SaveziPregledVM;
import com.example.x.karateklub.area_trener.model.TakmicenjeAddVM;
import com.example.x.karateklub.helper.MyApiRequest;
import com.example.x.karateklub.helper.MyRunnable;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.List;

public class NovoTakmicenjeTrenerFragment extends Fragment {

    private EditText txtNazivTakmicenja;
    private EditText txtDatumOdrzavanja;
    private EditText txtMjestoOdrzavanja;
    private EditText txtOrganizator;
    private TextView viewDatumOdrzavanja;
    private Spinner spinnerSavez;
    private SaveziPregledVM podaci;
    private ImageButton btnBack;

    Calendar kalendar;
    int dan,mjesec,godina;
    private Button btnSpremi;
    public NovoTakmicenjeTrenerFragment() {
        // Required empty public constructor
    }
    public  static NovoTakmicenjeTrenerFragment newInstance()
    {
        return new NovoTakmicenjeTrenerFragment();
    }


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
    final   View view= inflater.inflate(R.layout.layout_novo_takmicenje_trener, container, false);
        getActivity().setTitle("Novo takmičenje");
        txtNazivTakmicenja=view.findViewById(R.id.txtNazivTakmicenja);
        txtDatumOdrzavanja=view.findViewById(R.id.txtDatumOdrzavanja);
        txtMjestoOdrzavanja=view.findViewById(R.id.txtMjestoOdrzavanja);
        txtOrganizator=view.findViewById(R.id.txtOrganizator);
        spinnerSavez = view.findViewById(R.id.spinnerSavez);
        btnSpremi=view.findViewById(R.id.btnSpremiNovoTakmicenje);
        kalendar=Calendar.getInstance();
        dan=kalendar.get(Calendar.DAY_OF_MONTH);
        mjesec=kalendar.get(Calendar.MONTH);
        godina=kalendar.get(Calendar.YEAR);
        btnBack=view.findViewById(R.id.btnBackNovoTakmicenjeTrener);

        txtDatumOdrzavanja.setOnClickListener(new View.OnClickListener() {
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
                            txtDatumOdrzavanja.setText(d.toString()+"/"+m.toString()+"/"+godina);
                        else if(mjesec<10 && dan>10)
                            txtDatumOdrzavanja.setText(dan+"/"+m.toString()+"/"+godina);
                        else if(mjesec>10 && dan<10)
                            txtDatumOdrzavanja.setText(d.toString()+"/"+mjesec+"/"+godina);
                        else
                            txtDatumOdrzavanja.setText(dan+"/"+mjesec+"/"+godina);
                    }
                },godina,mjesec,dan);
                datePickerDialog.show();
            }
        });
        popuniPodatkeTask();
        btnSpremi.setOnClickListener(new View.OnClickListener()
        {
            @Override
            public void onClick(View v)
            {
                if(ValidirajPodatkeNovogTakmicenja())
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
        android.support.v4.app.Fragment takmicenjaTrener= TakmicenjaTrenerFragment.newInstance();
        android.support.v4.app.FragmentManager fragmentManager=getFragmentManager();
        android.support.v4.app.FragmentTransaction fragmentTransaction=fragmentManager.beginTransaction();
        fragmentTransaction.replace(R.id.fragmentContentTrener, takmicenjaTrener);
        fragmentTransaction.addToBackStack(null);
        fragmentTransaction.commit();
    }
    private void popuniPodatkeTask() {

        MyApiRequest.get(getActivity(), "/Savezi/GetAll", new MyRunnable<SaveziPregledVM>() {
            @Override
            public void run(SaveziPregledVM x) {
                podaci = x;
                popuniPodatke();
            }
        });
    }

    private void popuniPodatke() {
        List<String> result = new ArrayList<>();
SaveziPregledVM.Row row=new SaveziPregledVM.Row();
row.id=0;
row.naziv="Odaberite savez";
 podaci.rows.add(0,row);
        for (SaveziPregledVM.Row x : podaci.rows) {
            result.add(x.naziv);
        }

      //  ArrayAdapter<String> dataAdapter = new ArrayAdapter<>(getActivity(), android.R.layout.simple_spinner_item, result);
       // dataAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
       // spinnerSavez.setAdapter(dataAdapter);
        spinnerSavez.setAdapter(new ArrayAdapter(getActivity(), android.R.layout.simple_spinner_item, android.R.id.text1, result) {

            @Override
            public View getView(int position, View convertView, ViewGroup parent) {
                TextView textView = (TextView) super.getView(position, convertView, parent);
                textView.setTextColor(Color.BLUE);
                textView.setTextSize(15);
                return textView; }

            @Override
            public View getDropDownView(int position, View convertView, ViewGroup parent) {
                TextView textView = (TextView) super.getDropDownView(position, convertView, parent); textView.setTextColor(Color.BLUE);
                textView.setTextSize(15);
                return textView; }
        });
    }
    private boolean ValidirajPodatkeNovogTakmicenja(){

        TextView errorText = (TextView)spinnerSavez.getSelectedView();


        if(txtNazivTakmicenja.getText().length()==0)
            txtNazivTakmicenja.setError("Naziv je obavezno polje!");
        else
            txtNazivTakmicenja.setError(null);
        if(txtMjestoOdrzavanja.getText().length()==0 )
            txtMjestoOdrzavanja.setError("Mjesto održavanja je obavezno polje!");
        else
            txtMjestoOdrzavanja.setError(null);
        if(txtDatumOdrzavanja.getText().length()==0 )
            txtDatumOdrzavanja.setError("Datum održavanja je obavezno polje!");
        else
            txtDatumOdrzavanja.setError(null);
        if(txtOrganizator.getText().length()==0 )
            txtOrganizator.setError("Organizator je obavezno polje!");
        else
            txtOrganizator.setError(null);
 if(spinnerSavez.getSelectedItemPosition()==0)
 {
     errorText.setTextColor(Color.RED);
     errorText.setText("Odaberite savez iz liste");

 }

        if(txtNazivTakmicenja.getText().length()==0 || txtDatumOdrzavanja.getText().length()==0 || txtMjestoOdrzavanja.getText().length()==0
                || txtOrganizator.getText().length()==0 || spinnerSavez.getSelectedItemPosition()==0)
            return false;
        else
            return true;
    }

    private void do_btnSpremi_click()
    {
        int position = spinnerSavez.getSelectedItemPosition();
        SaveziPregledVM.Row x = podaci.rows.get(position);
        TakmicenjeAddVM novoTakmicenje = new TakmicenjeAddVM(txtNazivTakmicenja.getText().toString(), txtMjestoOdrzavanja.getText().toString(),
                txtDatumOdrzavanja.getText().toString(),txtOrganizator.getText().toString(),x.id);

        MyApiRequest.post(getActivity(), "Takmicenja/Add", novoTakmicenje, null);
        Toast.makeText(getActivity(), "Takmičenje je uspješno dodano.", Toast.LENGTH_LONG).show();

        android.support.v4.app.Fragment takmicenjaTrenerFragment=new TakmicenjaTrenerFragment();
        android.support.v4.app.FragmentManager fragmentManager=this.getFragmentManager();
        android.support.v4.app.FragmentTransaction fragmentTransaction=fragmentManager.beginTransaction();
        fragmentTransaction.replace(R.id.fragmentContentTrener, takmicenjaTrenerFragment);
        fragmentTransaction.addToBackStack(null);
        fragmentTransaction.commit();

    }
}
