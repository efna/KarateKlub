package com.example.x.karateklub.area_clan.fragment;


import android.content.Context;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.ListView;
import android.widget.SearchView;
import android.widget.TextView;
import com.example.x.karateklub.R;
import com.example.x.karateklub.area_autentifikacija.model.AutentifikacijaResultVM;
import com.example.x.karateklub.area_clan.model.ClanarineIzmireneClanarineClanaPregledVM;
import com.example.x.karateklub.helper.F;
import com.example.x.karateklub.helper.MyApiRequest;
import com.example.x.karateklub.helper.MyRunnable;
import com.example.x.karateklub.helper.MySession;



public class ClanarineIzmireneClanarineClanFragment extends Fragment {

    ClanarineIzmireneClanarineClanaPregledVM podaci;
    AutentifikacijaResultVM logiraniKorisnik = MySession.getKorisnik();
    final Integer osobaId = logiraniKorisnik.osobaId;
    private BaseAdapter adapter;
    private ListView listaIzmirenihClanarina;
    SearchView searchView;

    public  static ClanarineIzmireneClanarineClanFragment newInstance()
    {
        return new ClanarineIzmireneClanarineClanFragment();
    }


    public ClanarineIzmireneClanarineClanFragment() {
        // Required empty public constructor
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
       final View view= inflater.inflate(R.layout.fragment_clanarine_izmirene_clanarine_clan, container, false);

        listaIzmirenihClanarina = view.findViewById(R.id.listaIzmirenihClanarinaClana);

        popuniPodatkeTask();

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


        return view;
    }



    private void popuniPodatkeTask() {

        MyApiRequest.get(getActivity(), "Clanarine/IzmireneClanarineClanaPregled/" + osobaId, new MyRunnable<ClanarineIzmireneClanarineClanaPregledVM>() {
            @Override
            public void run(ClanarineIzmireneClanarineClanaPregledVM x) {
                podaci = x;

                popuniPodatke(podaci);
            }
        });
    }

    private void popuniPodatkeNakonPretrageTask(String query) {
        String naziv = query;

        MyApiRequest.get(getActivity(), "Clanarine/PretragaIzmirenihClanarinaClanaByclanIdNaziv/" + osobaId + "/" + naziv, new MyRunnable<ClanarineIzmireneClanarineClanaPregledVM>() {
            @Override
            public void run(ClanarineIzmireneClanarineClanaPregledVM x) {
                podaci = x;

                popuniPodatke(podaci);
            }
        });
    }

    private void popuniPodatke(final ClanarineIzmireneClanarineClanaPregledVM podaci) {
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
                    view = inflater.inflate(R.layout.row_izmirena_clanarina_clan, parent, false);
                }


                ClanarineIzmireneClanarineClanaPregledVM.Row x = podaci.rows.get(position);
                F.findView(view, R.id.txtPrvaLinijaIzmirenaClanarinaClan, TextView.class).setText(x.naziv+" ("+F.date_ddMMyyy(x.datumOd)
                        +" - "+F.date_ddMMyyy(x.datumDo)+") ");
                F.findView(view, R.id.txtDrugaLinijaIzmirenaClanarinaClan, TextView.class).setText(x.mjestoUplate+", "+F.date_ddMMyyy(x.datumUplate) );
                F.findView(view, R.id.txtIznosUplateBrojPriznanice, TextView.class).setText(x.brojPriznanice+" - "+x.iznos);


                return view;

            }
        };
        listaIzmirenihClanarina.setAdapter(adapter);


    }
}
