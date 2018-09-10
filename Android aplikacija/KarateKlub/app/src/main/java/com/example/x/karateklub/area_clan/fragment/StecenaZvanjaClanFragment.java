package com.example.x.karateklub.area_clan.fragment;


import android.content.Context;
import android.content.Intent;
import android.os.Build;
import android.os.Bundle;
import android.support.annotation.RequiresApi;
import android.support.v4.app.Fragment;
import android.support.v7.app.AppCompatActivity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.ImageButton;
import android.widget.ListView;
import android.widget.SearchView;
import android.widget.TextView;
import com.example.x.karateklub.R;
import com.example.x.karateklub.area_autentifikacija.model.AutentifikacijaResultVM;
import com.example.x.karateklub.area_clan.MainClanActivity;
import com.example.x.karateklub.area_clan.model.StecenaZvanjaPregledVM;
import com.example.x.karateklub.helper.F;
import com.example.x.karateklub.helper.MyApiRequest;
import com.example.x.karateklub.helper.MyRunnable;
import com.example.x.karateklub.helper.MySession;


public class StecenaZvanjaClanFragment extends Fragment {
    SearchView searchView;
   private ListView listaStecenihZvanja;
   private BaseAdapter adapter;
   private StecenaZvanjaPregledVM podaci;
    AutentifikacijaResultVM logiraniKorisnik= MySession.getKorisnik();
    final Integer osobaId=logiraniKorisnik.osobaId;
private ImageButton btnBack;

    public  static StecenaZvanjaClanFragment newInstance()
    {
        return new StecenaZvanjaClanFragment();
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {

        super.onCreate(savedInstanceState);
    }

    @RequiresApi(api = Build.VERSION_CODES.LOLLIPOP)
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
       final View view= inflater.inflate(R.layout.layout_stecena_zvanja_clan, container, false);
        btnBack=view.findViewById(R.id.btnBackStecenaZvanja);
        listaStecenihZvanja = view.findViewById(R.id.listaStecenaZvanjaClana);
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

        MyApiRequest.get(getActivity(),"StecenaZvanja/Pregled/"+osobaId, new MyRunnable<StecenaZvanjaPregledVM>() {
            @Override
            public void run(StecenaZvanjaPregledVM x) {
podaci=x;

popuniPodatke(podaci);
            }
        });
    }
    private void popuniPodatkeNakonPretrageTask(String query) {
        String naziv=query;

        MyApiRequest.get(getActivity(),"StecenaZvanja/PretragaByNazivClanId/"+naziv+"/"+osobaId, new MyRunnable<StecenaZvanjaPregledVM>() {
            @Override
            public void run(StecenaZvanjaPregledVM x) {
                podaci=x;

                popuniPodatke(podaci);
            }
        });
    }
    private void popuniPodatke(final StecenaZvanjaPregledVM podaci) {
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
                    view = inflater.inflate(R.layout.row_steceno_zvanje, parent, false);
                }


                StecenaZvanjaPregledVM.Row x = podaci.rows.get(position);

                 F.findView(view, R.id.txtPrvaLinija, TextView.class).setText(x.nazivZvanja + " (" + F.date_ddMMyyy(x.datumSticanja) + ") ");
                F.findView(view, R.id.txtDrugaLinija, TextView.class).setText(x.organizator + " - " + x.mjestoSticanja);
                return view;

            }
        };
      listaStecenihZvanja.setAdapter(adapter);


    }


}
