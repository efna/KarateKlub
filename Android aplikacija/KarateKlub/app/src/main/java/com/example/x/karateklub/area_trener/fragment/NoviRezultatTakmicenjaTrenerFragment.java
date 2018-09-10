package com.example.x.karateklub.area_trener.fragment;

import android.app.FragmentManager;
import android.graphics.Color;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v7.app.AppCompatActivity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import com.example.x.karateklub.R;
import com.example.x.karateklub.area_trener.model.DisciplineTakmicenjaPregledVM;
import com.example.x.karateklub.area_trener.model.OsvojenaMjestaNaTakmicenjuPregledVM;
import com.example.x.karateklub.area_trener.model.RezultatiTakmicenjeAddVM;
import com.example.x.karateklub.area_trener.model.StarosneDobiPregledVM;
import com.example.x.karateklub.area_trener.model.TakmicariPregledVM;
import com.example.x.karateklub.area_trener.model.TakmicenjaPregledVM;
import com.example.x.karateklub.helper.MyApiRequest;
import com.example.x.karateklub.helper.MyRunnable;

import java.util.ArrayList;
import java.util.List;


public class NoviRezultatTakmicenjaTrenerFragment extends Fragment {
    private EditText txtTakmicar;
    private EditText txtBrojTakmicaraUKateogoriji;
    private EditText txtBrojPobjeda;
    private EditText txtBrojPoraza;
    private EditText txtKategorija;
    private TextView viewTakmicar;
    private Spinner spinnerStarosnaDob;
    private Spinner spinnerDisciplina;
    private Spinner spinnerOsvojenoMjestoNaTakmicenju;
private Button btnSpremi;
    private ImageButton btnBack;

    private StarosneDobiPregledVM podaciStarosneDobi;
    private OsvojenaMjestaNaTakmicenjuPregledVM podaciOsvojenogMjesta;
    private DisciplineTakmicenjaPregledVM podaciDiscipline;
    private ImageView imgAddTakmicara;
private RezultatiTakmicenjeAddVM noviRezultatTakmicenja=new RezultatiTakmicenjeAddVM();


    public static final String TAKMICENJEZAREZ_KEY = "takmicenje_key";
    TakmicenjaPregledVM.Row takmicenje;

    public  static NoviRezultatTakmicenjaTrenerFragment newInstance(TakmicenjaPregledVM.Row x)
    {
        NoviRezultatTakmicenjaTrenerFragment fragment = new NoviRezultatTakmicenjaTrenerFragment();
        Bundle args = new Bundle();
        args.putSerializable(TAKMICENJEZAREZ_KEY, x);
        fragment.setArguments(args);
        return fragment;

    }

    public NoviRezultatTakmicenjaTrenerFragment() {
        // Required empty public constructor
    }


    public static NoviRezultatTakmicenjaTrenerFragment newInstance(String param1, String param2) {
        NoviRezultatTakmicenjaTrenerFragment fragment = new NoviRezultatTakmicenjaTrenerFragment();
        Bundle args = new Bundle();

        return fragment;
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        if (getArguments() != null) {
            takmicenje = (TakmicenjaPregledVM.Row) getArguments().getSerializable(TAKMICENJEZAREZ_KEY);

        }
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        final View view= inflater.inflate(R.layout.layout_novi_rezultat_takmicenja_trener, container, false);
        getActivity().setTitle("Novi rezultat takmičenja");
        txtTakmicar=view.findViewById(R.id.txtTakmicar);
        txtBrojTakmicaraUKateogoriji=view.findViewById(R.id.txtBrojTakmicaraUKategoriji);
        txtBrojPobjeda=view.findViewById(R.id.txtBrojPobjeda);
        txtBrojPoraza=view.findViewById(R.id.txtBrojPoraza);
        txtKategorija=view.findViewById(R.id.txtKategorija);
        spinnerStarosnaDob = view.findViewById(R.id.spinnerStarosnaDob);
        spinnerDisciplina = view.findViewById(R.id.spinnerDisciplina);
        spinnerOsvojenoMjestoNaTakmicenju = view.findViewById(R.id.spinnerOsvojenoMjesto);
        imgAddTakmicara= view.findViewById(R.id.imgAddTakmicara);
        imgAddTakmicara.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                do_btnDodajTakmicara();
            }
        });
        btnSpremi=view.findViewById(R.id.btnSpremiNoviRezultatTakmicenja);
        btnBack=view.findViewById(R.id.btnBackNoviRezultatTakmicenjaTrener);

        popuniPodatkeTask();
        btnSpremi.setOnClickListener(new View.OnClickListener()
        {
            @Override
            public void onClick(View v)
            {
                if(ValidirajPodatkeNovogRezultataTakmicenja())
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

        android.support.v4.app.Fragment rezultatiTakmicenjaFragment=RezultatiTakmicenjaTrenerFragment.newInstance(takmicenje);
        android.support.v4.app.FragmentManager fragmentManager=getFragmentManager();
        android.support.v4.app.FragmentTransaction fragmentTransaction=fragmentManager.beginTransaction();
        fragmentTransaction.replace(R.id.fragmentContentTrener, rezultatiTakmicenjaFragment);
        fragmentTransaction.addToBackStack(null);
        fragmentTransaction.commit();
    }
    private void do_btnDodajTakmicara() {

        MyRunnable<TakmicariPregledVM.Row> callBack=new MyRunnable<TakmicariPregledVM.Row>(){

            @Override
            public void run(TakmicariPregledVM.Row result) {

                noviRezultatTakmicenja.takmicarId = result.id;
                txtTakmicar.setText(result.takmicar);
            }
        };


        PretragaTakmicaraDialogFragment dlg = PretragaTakmicaraDialogFragment.newInstance(callBack);

        FragmentManager fm = getActivity().getFragmentManager();
        dlg.show(fm, "nekitag");

    }
    private void popuniPodatkeTask() {

        MyApiRequest.get(getActivity(), "/StarosneDobi/GetAll", new MyRunnable<StarosneDobiPregledVM>() {
            @Override
            public void run(StarosneDobiPregledVM x) {
                podaciStarosneDobi = x;
                popuniPodatkeStarosneDobi();
            }
        });
        MyApiRequest.get(getActivity(), "/DisciplineTakmicenja/GetAll", new MyRunnable<DisciplineTakmicenjaPregledVM>() {
            @Override
            public void run(DisciplineTakmicenjaPregledVM x) {
                podaciDiscipline = x;
                popuniPodatkeDiscipline();
            }
        });
        MyApiRequest.get(getActivity(), "/OsvojenaMjestaNaTakmicenju/GetAll", new MyRunnable<OsvojenaMjestaNaTakmicenjuPregledVM>() {
            @Override
            public void run(OsvojenaMjestaNaTakmicenjuPregledVM x) {
                podaciOsvojenogMjesta = x;
                popuniPodatkeOsvojenogMjesta();
            }
        });
    }

    private void popuniPodatkeStarosneDobi() {
        List<String> result = new ArrayList<>();
        StarosneDobiPregledVM.Row row=new StarosneDobiPregledVM.Row();
        row.id=0;
        row.naziv="Odaberite starosnu dob";
        podaciStarosneDobi.rows.add(0,row);
        for (StarosneDobiPregledVM.Row x : podaciStarosneDobi.rows) {
            result.add(x.naziv);
        }

       // ArrayAdapter<String> dataAdapter = new ArrayAdapter<>(getActivity(), android.R.layout.simple_spinner_item, result);
        //dataAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        //spinnerStarosnaDob.setAdapter(dataAdapter);
        spinnerStarosnaDob.setAdapter(new ArrayAdapter(getActivity(), android.R.layout.simple_spinner_item, android.R.id.text1, result) {

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
    private void popuniPodatkeDiscipline() {
        List<String> result = new ArrayList<>();
        DisciplineTakmicenjaPregledVM.Row row=new DisciplineTakmicenjaPregledVM.Row();
        row.id=0;
        row.naziv="Odaberite disciplinu";
        podaciDiscipline.rows.add(0,row);
        for (DisciplineTakmicenjaPregledVM.Row x : podaciDiscipline.rows) {
            result.add(x.naziv);
        }

     //   ArrayAdapter<String> dataAdapter = new ArrayAdapter<>(getActivity(), android.R.layout.simple_spinner_item, result);
        //dataAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
         //spinnerDisciplina.setAdapter(dataAdapter);
        spinnerDisciplina.setAdapter(new ArrayAdapter(getActivity(), android.R.layout.simple_spinner_item, android.R.id.text1, result) {

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

    private void popuniPodatkeOsvojenogMjesta() {
        List<String> result = new ArrayList<>();
        OsvojenaMjestaNaTakmicenjuPregledVM.Row row=new OsvojenaMjestaNaTakmicenjuPregledVM.Row();
        row.id=0;
        row.naziv="Odaberite osvojeno mjesto";
        podaciOsvojenogMjesta.rows.add(0,row);
        for (OsvojenaMjestaNaTakmicenjuPregledVM.Row x : podaciOsvojenogMjesta.rows) {
            result.add(x.naziv);
        }

       // ArrayAdapter<String> dataAdapter = new ArrayAdapter<>(getActivity(), android.R.layout.simple_spinner_item, result);
        //dataAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        //spinnerOsvojenoMjestoNaTakmicenju.setAdapter(dataAdapter);
        spinnerOsvojenoMjestoNaTakmicenju.setAdapter(new ArrayAdapter(getActivity(), android.R.layout.simple_spinner_item, android.R.id.text1, result) {

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
    private boolean ValidirajPodatkeNovogRezultataTakmicenja(){

        TextView errorTextSpinnerStarosnaDob = (TextView)spinnerStarosnaDob.getSelectedView();
        TextView errorTextSpinnerOsvojenoMjesto = (TextView)spinnerOsvojenoMjestoNaTakmicenju.getSelectedView();
        TextView errorTextSpinnerDisciplina = (TextView)spinnerDisciplina.getSelectedView();


        if(txtTakmicar.getText().length()==0)
            txtTakmicar.setError("Takmicar je obavezno polje!");
        else
            txtTakmicar.setError(null);
        if(txtKategorija.getText().length()==0 )
            txtKategorija.setError("Kategorija je obavezno polje!");
        else
            txtKategorija.setError(null);

        if(spinnerStarosnaDob.getSelectedItemPosition()==0)
        {
            errorTextSpinnerStarosnaDob.setTextColor(Color.RED);
            errorTextSpinnerStarosnaDob.setText("Odaberite starosnu dob iz liste");

        }
        if(spinnerOsvojenoMjestoNaTakmicenju.getSelectedItemPosition()==0)
        {
            errorTextSpinnerOsvojenoMjesto.setTextColor(Color.RED);
            errorTextSpinnerOsvojenoMjesto.setText("Odaberite osvojeno mjesto iz liste");

        }
        if(spinnerDisciplina.getSelectedItemPosition()==0)
        {
            errorTextSpinnerDisciplina.setTextColor(Color.RED);
            errorTextSpinnerDisciplina.setText("Odaberite disciplinu iz liste");

        }
        if(txtTakmicar.getText().length()==0 || txtKategorija.getText().length()==0 || spinnerStarosnaDob.getSelectedItemPosition()==0
                || spinnerDisciplina.getSelectedItemPosition()==0
                || spinnerOsvojenoMjestoNaTakmicenju.getSelectedItemPosition()==0
                )
            return false;
        else
            return true;
    }

    private void do_btnSpremi_click()
    {
        int positionStarosnaDob = spinnerStarosnaDob.getSelectedItemPosition();
        StarosneDobiPregledVM.Row starosnaDob = podaciStarosneDobi.rows.get(positionStarosnaDob);
        int positionDisciplina = spinnerDisciplina.getSelectedItemPosition();
        DisciplineTakmicenjaPregledVM.Row disciplina = podaciDiscipline.rows.get(positionDisciplina);
        int positionOsvojenoMjesto = spinnerOsvojenoMjestoNaTakmicenju.getSelectedItemPosition();
        OsvojenaMjestaNaTakmicenjuPregledVM.Row osvojenoMjesto = podaciOsvojenogMjesta.rows.get(positionOsvojenoMjesto);

noviRezultatTakmicenja.takmicenjeId=takmicenje.id;
noviRezultatTakmicenja.starosnaDobId=starosnaDob.id;
noviRezultatTakmicenja.disciplinaTakmicenjaId=disciplina.id;
noviRezultatTakmicenja.osvojenoMjestoNaTakmicenjuId=osvojenoMjesto.id;
noviRezultatTakmicenja.kategorija=txtKategorija.getText().toString();
noviRezultatTakmicenja.brojTakmicaraUKategoriji=txtBrojTakmicaraUKateogoriji.getText().toString();
noviRezultatTakmicenja.brojPobjeda=txtBrojPobjeda.getText().toString();
noviRezultatTakmicenja.brojPoraza=txtBrojPoraza.getText().toString();
        MyApiRequest.post(getActivity(), "RezultatiTakmicenja/Add", noviRezultatTakmicenja, null);
        Toast.makeText(getActivity(), "Rezultat je uspješno dodan.", Toast.LENGTH_LONG).show();

        android.support.v4.app.Fragment rezultatiTakmicenjaFragment=RezultatiTakmicenjaTrenerFragment.newInstance(takmicenje);
        android.support.v4.app.FragmentManager fragmentManager=getFragmentManager();
        android.support.v4.app.FragmentTransaction fragmentTransaction=fragmentManager.beginTransaction();
        fragmentTransaction.replace(R.id.fragmentContentTrener, rezultatiTakmicenjaFragment);
        fragmentTransaction.addToBackStack(null);
        fragmentTransaction.commit();

    }
}
