package com.example.x.karateklub.area_clan.fragment;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v7.app.AppCompatActivity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.SearchView;
import android.widget.TextView;
import com.example.x.karateklub.R;
import com.example.x.karateklub.area_autentifikacija.model.AutentifikacijaResultVM;
import com.example.x.karateklub.area_clan.MainClanActivity;
import com.example.x.karateklub.area_clan.model.RezultatiTakmicenjaClanPregledVM;
import com.example.x.karateklub.helper.F;
import com.example.x.karateklub.helper.MyApiRequest;
import com.example.x.karateklub.helper.MyRunnable;
import com.example.x.karateklub.helper.MySession;



public class RezultatiTakmicenjaClanFragment extends Fragment {

    RezultatiTakmicenjaClanPregledVM podaci;
    AutentifikacijaResultVM logiraniKorisnik = MySession.getKorisnik();
    final Integer osobaId = logiraniKorisnik.osobaId;
    private BaseAdapter adapter;
    private ListView listaOstvarenihRezultata;
    private ImageButton btnBack;


    public static RezultatiTakmicenjaClanFragment newInstance() {
        return new RezultatiTakmicenjaClanFragment();
    }


    public RezultatiTakmicenjaClanFragment() {
        // Required empty public constructor
    }


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        final View view = inflater.inflate(R.layout.layout_rezultati_takmicenja_clan, container, false);
        listaOstvarenihRezultata = view.findViewById(R.id.listaRezultataClana);
        btnBack=view.findViewById(R.id.btnBackRezultatiTakmicenja);

        popuniPodatkeTask();
        SearchView searchView;
        searchView = view.findViewById(R.id.editText);
        searchView.setOnQueryTextListener(new SearchView.OnQueryTextListener() {
            @Override
            public boolean onQueryTextSubmit(String query) {
                do_btnTraziClick(query);
                return false;
            }

            private void do_btnTraziClick(String query) {
                if (query.isEmpty())
                    popuniPodatkeTask();
                else
                    popuniPodatkeNakonPretrageTask(query);


            }

            @Override
            public boolean onQueryTextChange(String query) {
                do_btnTraziClick(query);
                return false;
            }
        });
        searchView.setIconifiedByDefault(false);

        btnBack.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                do_btnBackClick();
            }
        });
        return view;
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
        Intent intent = new Intent(getActivity(), MainClanActivity.class);
        startActivity(intent);
    }
    private void popuniPodatkeTask() {

        MyApiRequest.get(getActivity(), "RezultatiTakmicenja/PregledRezultataClana/" + osobaId, new MyRunnable<RezultatiTakmicenjaClanPregledVM>() {
            @Override
            public void run(RezultatiTakmicenjaClanPregledVM x) {
                podaci = x;

                popuniPodatke(podaci);
            }
        });
    }

    private void popuniPodatkeNakonPretrageTask(String query) {
        String naziv = query;

        MyApiRequest.get(getActivity(), "RezultatiTakmicenja/PretragaRezultataClana/" + osobaId + "/" + naziv, new MyRunnable<RezultatiTakmicenjaClanPregledVM>() {
            @Override
            public void run(RezultatiTakmicenjaClanPregledVM x) {
                podaci = x;

                popuniPodatke(podaci);
            }
        });
    }

    private void popuniPodatke(final RezultatiTakmicenjaClanPregledVM podaci) {
        adapter = new BaseAdapter() {
            @Override
            public int getCount() {
                return podaci.rows.size();

            }

            @Override
            public Object getItem(int position) {
                return null;
            }

            @Override
            public long getItemId(int position) {
                return 0;
            }

            @Override
            public View getView(int position, View view, ViewGroup parent) {

                if (view == null) {
                    LayoutInflater inflater = (LayoutInflater) getActivity().getSystemService(Context.LAYOUT_INFLATER_SERVICE);
                    view = inflater.inflate(R.layout.row_rezultati_takmicenja_clan, parent, false);
                }


                RezultatiTakmicenjaClanPregledVM.Row x = podaci.rows.get(position);
                F.findView(view, R.id.txtPrvaLinijaRezultatiTakmicenja, TextView.class).setText(x.nazivTakmicenja);
                F.findView(view, R.id.txtDrugaLinijaRezultatiTakmicenja, TextView.class).setText(x.mjestoOdrzavanja + ", " + F.date_ddMMyyy(x.datumOdrzavaja));
                F.findView(view, R.id.txtTrecaLinijaRezultatiTakmicenja, TextView.class).setText(x.uzrast + " - " + x.kategorija);
                F.findView(view, R.id.txtCetvrtaLinijaRezultatiTakmicenja, TextView.class).setText(x.disciplina + " - " + x.osvojenoMjesto + " mjesto");
                ImageView imageView = view.findViewById(R.id.img);
                if (x.osvojenoMjesto.contains("rv"))
                    imageView.setImageResource(R.drawable.first_place_medal);
                else if (x.osvojenoMjesto.contains("ug"))
                    imageView.setImageResource(R.drawable.second_place_medal);
                else
                    imageView.setImageResource(R.drawable.third_place_medal);


                return view;

            }
        };
        listaOstvarenihRezultata.setAdapter(adapter);


    }
}
