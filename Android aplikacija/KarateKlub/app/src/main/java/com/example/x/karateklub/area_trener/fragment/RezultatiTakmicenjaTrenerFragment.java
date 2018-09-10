package com.example.x.karateklub.area_trener.fragment;

import android.content.Context;
import android.content.DialogInterface;
import android.os.Bundle;
import android.support.design.widget.FloatingActionButton;
import android.support.v4.app.Fragment;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.BaseAdapter;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.SearchView;
import android.widget.TextView;
import com.example.x.karateklub.R;
import com.example.x.karateklub.area_trener.model.RezultatiTakmicenjaTrenerPregledVM;
import com.example.x.karateklub.area_trener.model.TakmicenjaPregledVM;
import com.example.x.karateklub.helper.F;
import com.example.x.karateklub.helper.MyApiRequest;
import com.example.x.karateklub.helper.MyRunnable;


public class RezultatiTakmicenjaTrenerFragment extends Fragment {
    public static final String TAKMICENJE_KEY = "takmicenje_key";
    TakmicenjaPregledVM.Row takmicenje;
    RezultatiTakmicenjaTrenerPregledVM podaci;
    private BaseAdapter adapter;
    private ListView listaRezultataTakmicenja;
    private FloatingActionButton btnDodaj;
    private ImageButton btnBack;


    public  static RezultatiTakmicenjaTrenerFragment newInstance(TakmicenjaPregledVM.Row x)
    {
        RezultatiTakmicenjaTrenerFragment fragment = new RezultatiTakmicenjaTrenerFragment();
        Bundle args = new Bundle();
        args.putSerializable(TAKMICENJE_KEY, x);
        fragment.setArguments(args);
        return fragment;

    }


    public RezultatiTakmicenjaTrenerFragment() {
        // Required empty public constructor
    }


    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        if (getArguments() != null) {
            takmicenje = (TakmicenjaPregledVM.Row) getArguments().getSerializable(TAKMICENJE_KEY);

        }
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        View view= inflater.inflate(R.layout.layout_rezultati_takmicenja_trener, container, false);
        getActivity().setTitle("Rezultati takmičenja");

        listaRezultataTakmicenja = view.findViewById(R.id.listaRezultataTakmicenjaTrener);
        btnDodaj = view.findViewById(R.id.fab);
        btnBack=view.findViewById(R.id.btnBackRezultatiTakmicenjaTrener);

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

        btnDodaj.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                do_btnDodajClick();
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
    private void do_btnDodajClick() {
        TakmicenjaPregledVM.Row x =takmicenje;
        android.support.v4.app.Fragment noviRezulatTakmicenja=NoviRezultatTakmicenjaTrenerFragment.newInstance(x);
        android.support.v4.app.FragmentManager fragmentManager=this.getFragmentManager();
        android.support.v4.app.FragmentTransaction fragmentTransaction=fragmentManager.beginTransaction();
        fragmentTransaction.replace(R.id.fragmentContentTrener, noviRezulatTakmicenja);
        fragmentTransaction.addToBackStack(null);
        fragmentTransaction.commit();
    }

    private void popuniPodatkeTask() {
       int takmicenjeId=takmicenje.id;
        MyApiRequest.get(getActivity(), "RezultatiTakmicenja/PregledRezultataTakmicenjaByTrener/" + takmicenjeId, new MyRunnable<RezultatiTakmicenjaTrenerPregledVM>() {
            @Override
            public void run(RezultatiTakmicenjaTrenerPregledVM x) {
                podaci = x;

                popuniPodatke(podaci);
            }
        });
    }

    private void popuniPodatkeNakonPretrageTask(String query) {
        String naziv = query;
int takmicenjeId=takmicenje.id;
        MyApiRequest.get(getActivity(), "RezultatiTakmicenja/PretragaRezultataTakmicenjaByTrener/" + takmicenjeId + "/" + naziv, new MyRunnable<RezultatiTakmicenjaTrenerPregledVM>() {
            @Override
            public void run(RezultatiTakmicenjaTrenerPregledVM x) {
                podaci = x;

                popuniPodatke(podaci);
            }
        });
    }

    private void popuniPodatke(final RezultatiTakmicenjaTrenerPregledVM podaci) {
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
                    view = inflater.inflate(R.layout.row_rezultati_takmicenja_trener, parent, false);
                }


                RezultatiTakmicenjaTrenerPregledVM.Row x = podaci.rows.get(position);
                F.findView(view, R.id.txtPrvaLinijaRezultatiTakmicenjaTrener, TextView.class).setText(x.takmicar);
                F.findView(view, R.id.txtDrugaLinijaRezultatiTakmicenjaTrener, TextView.class).setText(x.starosnaDob + ", " + x.kategorija);
                F.findView(view, R.id.txtTrecaLinijaRezultatiTakmicenjaTrener, TextView.class).setText(x.disciplina + " - " + x.osvojenoMjesto + " mjesto");
                F.findView(view, R.id.txtCetvrtaLinijaRezultatiTakmicenjaTrener, TextView.class).setText("Br. takmičara: "+ x.brojTakmicaraUKategoriji);
                F.findView(view, R.id.txtPetaLinijaRezultatiTakmicenjaTrener, TextView.class).setText("Br.pobjeda: " +x.brojPobjeda);
                F.findView(view, R.id.txtSestaLinijaRezultatiTakmicenjaTrener, TextView.class).setText("Br.poraza: " +x.brojPoraza);


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
        listaRezultataTakmicenja.setAdapter(adapter);

        listaRezultataTakmicenja.setOnItemLongClickListener(new AdapterView.OnItemLongClickListener() {
            @Override
            public boolean onItemLongClick(AdapterView<?> parent, View view, int position, long id) {

                RezultatiTakmicenjaTrenerPregledVM.Row x = podaci.rows.get(position);
                do_listViewLongClick(x);
                return true;
            }
        });
    }
    private void do_listViewLongClick(final RezultatiTakmicenjaTrenerPregledVM.Row x) {

        final AlertDialog.Builder adb = new AlertDialog.Builder(getActivity());
        adb.setTitle("Brisanje rezultata");
        adb.setMessage("Da li želite obrisati rezultat takmičenja?");

        adb.setPositiveButton("DA", new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {

                dialog.dismiss();
                MyApiRequest.delete(getActivity(), "RezultatiTakmicenja/Remove/" + x.id, new MyRunnable<Integer>(){

                    @Override
                    public void run(Integer o) {
                        podaci.rows.remove(x);
                        adapter.notifyDataSetChanged();
                    }
                });


            }
        });

        adb.setNegativeButton("Ne", new DialogInterface.OnClickListener() {
            public void onClick(DialogInterface dialog, int which) {

                dialog.dismiss();
            }
        });
        adb.setIcon(android.R.drawable.ic_dialog_alert);
        adb.show();
    }


}
