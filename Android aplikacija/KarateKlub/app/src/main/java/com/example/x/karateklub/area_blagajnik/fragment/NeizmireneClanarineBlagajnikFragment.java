package com.example.x.karateklub.area_blagajnik.fragment;

import android.content.Context;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.BaseAdapter;
import android.widget.ListView;
import android.widget.SearchView;
import android.widget.TextView;

import com.example.x.karateklub.R;
import com.example.x.karateklub.area_blagajnik.model.ClanarinePregledVM;
import com.example.x.karateklub.area_blagajnik.model.NeizmireneClanarinePregledVM;
import com.example.x.karateklub.helper.F;
import com.example.x.karateklub.helper.MyApiRequest;
import com.example.x.karateklub.helper.MyRunnable;


public class NeizmireneClanarineBlagajnikFragment extends Fragment {

    ClanarinePregledVM.Row clanarina;
    NeizmireneClanarinePregledVM podaci;
    private ListView listaNeizmirenihClanarina;
    SearchView searchView;
    private BaseAdapter adapter;

    public static final String CLANARINA_NEIZMIRENECLANARINE_KEY = "clanarina_neizmirene_key";

    public NeizmireneClanarineBlagajnikFragment() {
        // Required empty public constructor
    }
    public  static NeizmireneClanarineBlagajnikFragment newInstance(ClanarinePregledVM.Row x)
    {
        NeizmireneClanarineBlagajnikFragment fragment = new NeizmireneClanarineBlagajnikFragment();
        Bundle args = new Bundle();
        args.putSerializable(CLANARINA_NEIZMIRENECLANARINE_KEY, x);
        fragment.setArguments(args);
        return fragment;

    }
    public static NeizmireneClanarineBlagajnikFragment newInstance(String param1, String param2) {
        NeizmireneClanarineBlagajnikFragment fragment = new NeizmireneClanarineBlagajnikFragment();
        Bundle args = new Bundle();

        fragment.setArguments(args);
        return fragment;
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        if (getArguments() != null) {
            clanarina = (ClanarinePregledVM.Row) getArguments().getSerializable(CLANARINA_NEIZMIRENECLANARINE_KEY);

        }
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        final View view= inflater.inflate(R.layout.fragment_neizmirene_clanarine_blagajnik, container, false);
        listaNeizmirenihClanarina = view.findViewById(R.id.listaNeizmirenihClanarinaBlagajnik);

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

        return  view;
    }
    private void popuniPodatkeTask() {
        Integer clanarinaId=clanarina.id;
        MyApiRequest.get(getActivity(), "Clanarine/NeizmireneClanarinePregled/" +clanarinaId , new MyRunnable<NeizmireneClanarinePregledVM>() {
            @Override
            public void run(NeizmireneClanarinePregledVM x) {
                podaci = x;

                popuniPodatke(podaci);
            }
        });
    }

    private void popuniPodatkeNakonPretrageTask(String query) {
        String imePrezime = query;
        Integer clanarinaId=clanarina.id;

        MyApiRequest.get(getActivity(), "Clanarine/NeizmireneClanarinePretraga/" + clanarinaId + "/" + imePrezime, new MyRunnable<NeizmireneClanarinePregledVM>() {
            @Override
            public void run(NeizmireneClanarinePregledVM x) {
                podaci = x;

                popuniPodatke(podaci);
            }
        });
    }

    private void popuniPodatke(final NeizmireneClanarinePregledVM podaci) {
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
                    view = inflater.inflate(R.layout.row_neizmirene_clanarine_blagajnik, parent, false);
                }


                NeizmireneClanarinePregledVM.Row x = podaci.rows.get(position);
                F.findView(view, R.id.txtPrvaLinijaNeizmirenaClanarinaBlagajnik, TextView.class).setText(x.clanKluba);
                F.findView(view, R.id.txtDrugaLinijaNeizmirenaClanarinaBlagajnik, TextView.class).setText("Period: "+F.date_ddMMyyy(x.datumOd)+" do "+F.date_ddMMyyy(x.datumDo) );


                return view;

            }
        };
        listaNeizmirenihClanarina.setAdapter(adapter);
        listaNeizmirenihClanarina.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                NeizmireneClanarinePregledVM.Row x = podaci.rows.get(position);
                NovaStavkaClanarineBlagajnikFragment dlg = NovaStavkaClanarineBlagajnikFragment.newInstance(clanarina,x);

                android.support.v4.app.FragmentManager fragmentManager=getFragmentManager();

                dlg.show(fragmentManager, "nekitag");

            }
        });

    }

}
