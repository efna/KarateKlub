package com.example.x.karateklub.area_blagajnik.fragment;

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
import com.example.x.karateklub.area_blagajnik.model.ClanarinePregledVM;
import com.example.x.karateklub.area_blagajnik.model.IzmireneClanarinePregledVM;
import com.example.x.karateklub.helper.F;
import com.example.x.karateklub.helper.MyApiRequest;
import com.example.x.karateklub.helper.MyRunnable;


public class IzmireneClanarineBlagajnikFragment extends Fragment {
    IzmireneClanarinePregledVM podaci;
    ClanarinePregledVM.Row clanarina;
    private BaseAdapter adapter;
    private ListView listaIzmirenihClanarina;
    SearchView searchView;
    public static final String CLANARINA_IZMIRENECLANARINE_KEY = "clanarina_izmirene_key";

    public IzmireneClanarineBlagajnikFragment() {
        // Required empty public constructor
    }


    public  static IzmireneClanarineBlagajnikFragment newInstance(ClanarinePregledVM.Row x)
    {
        IzmireneClanarineBlagajnikFragment fragment = new IzmireneClanarineBlagajnikFragment();
        Bundle args = new Bundle();
        args.putSerializable(CLANARINA_IZMIRENECLANARINE_KEY, x);
        fragment.setArguments(args);
        return fragment;

    }
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        if (getArguments() != null) {
            clanarina = (ClanarinePregledVM.Row) getArguments().getSerializable(CLANARINA_IZMIRENECLANARINE_KEY);

        }
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        final View view= inflater.inflate(R.layout.fragment_izmirene_clanarine_blagajnik, container, false);
        listaIzmirenihClanarina = view.findViewById(R.id.listaIzmirenihClanarinaBlagajnik);

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
Integer clanarinaId=clanarina.id;
        MyApiRequest.get(getActivity(), "Clanarine/IzmireneClanarinePregled/" +clanarinaId , new MyRunnable<IzmireneClanarinePregledVM>() {
            @Override
            public void run(IzmireneClanarinePregledVM x) {
                podaci = x;

                popuniPodatke(podaci);
            }
        });
    }

    private void popuniPodatkeNakonPretrageTask(String query) {
        String imePrezime = query;
        Integer clanarinaId=clanarina.id;

        MyApiRequest.get(getActivity(), "Clanarine/IzmireneClanarinePretraga/" + clanarinaId + "/" + imePrezime, new MyRunnable<IzmireneClanarinePregledVM>() {
            @Override
            public void run(IzmireneClanarinePregledVM x) {
                podaci = x;

                popuniPodatke(podaci);
            }
        });
    }

    private void popuniPodatke(final IzmireneClanarinePregledVM podaci) {
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
                    view = inflater.inflate(R.layout.row_izmirene_clanarine_blagajnik, parent, false);
                }


                IzmireneClanarinePregledVM.Row x = podaci.rows.get(position);
                F.findView(view, R.id.txtPrvaLinijIzmireneClanarineBlagajnik, TextView.class).setText(x.clanKluba);
                F.findView(view, R.id.txtDrugaLinijaIzmireneClanarineBlagajnik, TextView.class).setText("Period: "+F.date_ddMMyyy(x.datumOd)+" do "+F.date_ddMMyyy(x.datumDo) );
                F.findView(view, R.id.txtTrecaLinijaIzmireneClanarineBlagajnik, TextView.class).setText(x.mjestoUplate+", "+F.date_ddMMyyy(x.datumUplate) );
                F.findView(view, R.id.txtCetvrtaLinijaIzmireneClanarineBlagajnik, TextView.class).setText(x.brojPriznanice+" - "+x.iznosBrojevima+" KM");


                return view;

            }
        };
        listaIzmirenihClanarina.setAdapter(adapter);


    }

}
