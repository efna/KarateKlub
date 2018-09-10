package com.example.x.karateklub.area_blagajnik.fragment;

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
import com.example.x.karateklub.area_blagajnik.MainBlagajnikActivity;
import com.example.x.karateklub.area_blagajnik.model.ClanarinePregledVM;
import com.example.x.karateklub.helper.F;
import com.example.x.karateklub.helper.MyApiRequest;
import com.example.x.karateklub.helper.MyRunnable;


public class ClanarineBlagajnikFragment extends Fragment {
    SearchView searchView;
    ClanarinePregledVM podaci;
    private BaseAdapter adapter;
    private ListView listaClanarina;
    private ImageButton btnBack;

    private FloatingActionButton btnDodaj;

    public ClanarineBlagajnikFragment() {
        // Required empty public constructor
    }

    public  static ClanarineBlagajnikFragment newInstance()
    {
        return new ClanarineBlagajnikFragment();
    }




    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        final View view= inflater.inflate(R.layout.layout_clanarine_blagajnik, container, false);
        listaClanarina = view.findViewById(R.id.listaSvihClanarinaBlagajnik);
        btnDodaj = view.findViewById(R.id.fab);
        btnBack=view.findViewById(R.id.btnBackClanarineBlagajnik);

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
        Intent intent = new Intent(getActivity(), MainBlagajnikActivity.class);
        startActivity(intent);
    }
    private void do_btnDodajClick() {
        android.support.v4.app.Fragment novaClanarinaFragment=new NovaClanarinaBlagajnikFragment();
        android.support.v4.app.FragmentManager fragmentManager=this.getFragmentManager();
        android.support.v4.app.FragmentTransaction fragmentTransaction=fragmentManager.beginTransaction();
        fragmentTransaction.replace(R.id.fragmentContentBlagajnik, novaClanarinaFragment);
        fragmentTransaction.addToBackStack(null);
        fragmentTransaction.commit();
    }
    private void popuniPodatkeTask() {

        MyApiRequest.get(getActivity(),"Clanarine/PregledSvihClanarina/", new MyRunnable<ClanarinePregledVM>() {
            @Override
            public void run(ClanarinePregledVM x) {
                podaci=x;

                popuniPodatke(podaci);
            }
        });
    }
    private void popuniPodatkeNakonPretrageTask(String query) {
        String naziv=query;

        MyApiRequest.get(getActivity(),"Clanarine/PretragaSvihClanarinaByNazivClanarine/"+naziv, new MyRunnable<ClanarinePregledVM>() {
            @Override
            public void run(ClanarinePregledVM x) {
                podaci=x;

                popuniPodatke(podaci);
            }
        });
    }
    private void popuniPodatke(final ClanarinePregledVM podaci) {
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
                    view = inflater.inflate(R.layout.row_clanarine_blagajnik, parent, false);
                }


                ClanarinePregledVM.Row x = podaci.rows.get(position);


                F.findView(view, R.id.txtPrvaLinijaClanarineBlagajnik, TextView.class).setText(x.naziv);
                F.findView(view, R.id.txtDrugaLinijaClanarineBlagajnik, TextView.class).setText(F.date_ddMMyyy(x.datumOd)+" - "+F.date_ddMMyyy(x.datumDo));


                return view;

            }
        };
        listaClanarina.setAdapter(adapter);
        listaClanarina.setOnItemClickListener(new AdapterView.OnItemClickListener() {

            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                ClanarinePregledVM.Row x = podaci.rows.get(position);
                android.support.v4.app.Fragment clanarineTabFragment= ClanarineBlagajnikTabFragment.newInstance(x);
                android.support.v4.app.FragmentManager fragmentManager=getFragmentManager();
                android.support.v4.app.FragmentTransaction fragmentTransaction=fragmentManager.beginTransaction();
                fragmentTransaction.replace(R.id.fragmentContentBlagajnik, clanarineTabFragment);
                fragmentTransaction.addToBackStack(null);
                fragmentTransaction.commit();
            }

        });
        listaClanarina.setOnItemLongClickListener(new AdapterView.OnItemLongClickListener() {
            @Override
            public boolean onItemLongClick(AdapterView<?> parent, View view, int position, long id) {

                ClanarinePregledVM.Row x = podaci.rows.get(position);
                do_listViewLongClick(x);
                return true;
            }
        });
    }
    private void do_listViewLongClick(final ClanarinePregledVM.Row x) {

        final AlertDialog.Builder adb = new AlertDialog.Builder(getActivity());
        adb.setTitle("Brisanje članarine");
        adb.setMessage("Da li želite obrisati članarinu?");

        adb.setPositiveButton("DA", new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {

                dialog.dismiss();
                MyApiRequest.delete(getActivity(), "Clanarine/Remove/" + x.id, new MyRunnable<Integer>(){

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
