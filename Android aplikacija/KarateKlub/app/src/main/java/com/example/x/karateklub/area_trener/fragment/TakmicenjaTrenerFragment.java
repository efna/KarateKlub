package com.example.x.karateklub.area_trener.fragment;

import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
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
import android.widget.ListView;
import android.widget.SearchView;
import android.widget.TextView;
import com.example.x.karateklub.R;
import com.example.x.karateklub.area_trener.MainTrenerActivity;
import com.example.x.karateklub.area_trener.model.TakmicenjaPregledVM;
import com.example.x.karateklub.helper.F;
import com.example.x.karateklub.helper.MyApiRequest;
import com.example.x.karateklub.helper.MyRunnable;


public class TakmicenjaTrenerFragment extends Fragment {
    SearchView searchView;
    TakmicenjaPregledVM podaci;
    private BaseAdapter adapter;
    private ListView listaTakmicenja;
    private FloatingActionButton btnDodaj;
    private ImageButton btnBack;

    public  static TakmicenjaTrenerFragment newInstance()
    {
        return new TakmicenjaTrenerFragment();
    }

    public TakmicenjaTrenerFragment() {
        // Required empty public constructor
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        final View view= inflater.inflate(R.layout.layout_takmicenja_trener, container, false);
        listaTakmicenja = view.findViewById(R.id.listaTakmicenjaTrener);
        btnDodaj = view.findViewById(R.id.fab);
        btnBack=view.findViewById(R.id.btnBackTakmicenjaTrener);

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
        Intent intent = new Intent(getActivity(), MainTrenerActivity.class);
        startActivity(intent);
    }
    private void do_btnDodajClick() {
        android.support.v4.app.Fragment novoTakmicenjeTrenerFragment=new NovoTakmicenjeTrenerFragment();
        android.support.v4.app.FragmentManager fragmentManager=this.getFragmentManager();
        android.support.v4.app.FragmentTransaction fragmentTransaction=fragmentManager.beginTransaction();
        fragmentTransaction.replace(R.id.fragmentContentTrener, novoTakmicenjeTrenerFragment);
        fragmentTransaction.addToBackStack(null);
        fragmentTransaction.commit();
    }
    private void popuniPodatkeTask() {

        MyApiRequest.get(getActivity(),"Takmicenja/PregledSvihTakmicenja/", new MyRunnable<TakmicenjaPregledVM>() {
            @Override
            public void run(TakmicenjaPregledVM x) {
                podaci=x;

                popuniPodatke(podaci);
            }
        });
    }
    private void popuniPodatkeNakonPretrageTask(String query) {
        String naziv=query;

        MyApiRequest.get(getActivity(),"Takmicenja/PretragaSvihTakmicenjaByNaziv/"+naziv, new MyRunnable<TakmicenjaPregledVM>() {
            @Override
            public void run(TakmicenjaPregledVM x) {
                podaci=x;

                popuniPodatke(podaci);
            }
        });
    }
    private void popuniPodatke(final TakmicenjaPregledVM podaci) {
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
                    view = inflater.inflate(R.layout.row_takmicenja_trener, parent, false);
                }

                TakmicenjaPregledVM.Row x = podaci.rows.get(position);


                F.findView(view, R.id.txtPrvaLinijaTakmicenjaTrener, TextView.class).setText(x.nazivTakmicenja);
                F.findView(view, R.id.txtDrugaLinijaTakmicenjaTrener, TextView.class).setText(F.date_ddMMyyy(x.datumOdrzavanjaTakmicenja)+" - "+x.mjestoOdrzavanjaTakmicenja);
                F.findView(view, R.id.txtTrecaLinijaTakmicenjaTrener, TextView.class).setText("Organizator: "+x.organizatorTakmicenja);
                F.findView(view, R.id.txtCetvrtaLinijaTakmicenjaTrener, TextView.class).setText("Savez: "+x.nazivSaveza);



                return view;

            }
        };
        listaTakmicenja.setAdapter(adapter);
        listaTakmicenja.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                TakmicenjaPregledVM.Row x = podaci.rows.get(position);
                android.support.v4.app.Fragment rezultatiTakmicenjaFragment=RezultatiTakmicenjaTrenerFragment.newInstance(x);
                android.support.v4.app.FragmentManager fragmentManager=getFragmentManager();
                android.support.v4.app.FragmentTransaction fragmentTransaction=fragmentManager.beginTransaction();
                fragmentTransaction.replace(R.id.fragmentContentTrener, rezultatiTakmicenjaFragment);
                fragmentTransaction.addToBackStack(null);
                fragmentTransaction.commit();
            }
        });
        listaTakmicenja.setOnItemLongClickListener(new AdapterView.OnItemLongClickListener() {
            @Override
            public boolean onItemLongClick(AdapterView<?> parent, View view, int position, long id) {

                TakmicenjaPregledVM.Row x = podaci.rows.get(position);
                do_listViewLongClick(x);
                return true;
            }
        });
    }
    private void do_listViewLongClick(final TakmicenjaPregledVM.Row x) {

        final AlertDialog.Builder adb = new AlertDialog.Builder(getActivity());
        adb.setTitle("Brisanje takmičenja");
        adb.setMessage("Da li želite obrisati takmičenje?");

        adb.setPositiveButton("DA", new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {

                dialog.dismiss();
                MyApiRequest.delete(getActivity(), "Takmicenja/Remove/" + x.id, new MyRunnable<Integer>(){

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
