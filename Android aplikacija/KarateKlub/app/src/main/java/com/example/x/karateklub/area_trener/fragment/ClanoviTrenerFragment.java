package com.example.x.karateklub.area_trener.fragment;


import android.content.Context;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v7.app.AppCompatActivity;
import android.util.Base64;
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
import com.example.x.karateklub.area_trener.MainTrenerActivity;
import com.example.x.karateklub.area_trener.model.ClanoviKubaPregledClanovaVM;
import com.example.x.karateklub.helper.F;
import com.example.x.karateklub.helper.MyApiRequest;
import com.example.x.karateklub.helper.MyRunnable;


public class ClanoviTrenerFragment extends Fragment {
    SearchView searchView;
    ClanoviKubaPregledClanovaVM podaci;
    private BaseAdapter adapter;
    private ListView listaAktivnihClanova;
    private ImageView clanImage;
    private ImageButton btnBack;


    public ClanoviTrenerFragment() {
        // Required empty public constructor
    }

    public  static ClanoviTrenerFragment newInstance()
    {
        return new ClanoviTrenerFragment();
    }



    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
       final View view= inflater.inflate(R.layout.layout_clanovi_trener, container, false);
        btnBack=view.findViewById(R.id.btnBackClanoviTrener);

        listaAktivnihClanova = view.findViewById(R.id.listaAktivnihClanovaTrener);
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
    public static byte[] convertStirngToByteArray(String s)
    {
        byte[] byteArray = null;
        if(s!=null)
        {
            if(s.length()>0)
            {
                try
                {
                    byteArray = s.getBytes();
                } catch (Exception e)
                {
                    e.printStackTrace();
                }
            }
        }
        return byteArray;
    }

    public static Bitmap bytesToBitmap (byte[] imageBytes)
    {

        Bitmap bitmap = BitmapFactory.decodeByteArray(imageBytes, 0, imageBytes.length);

        return bitmap;
    }

    private void popuniPodatkeTask() {

        MyApiRequest.get(getActivity(),"ClanoviKluba/PregledClanova/", new MyRunnable<ClanoviKubaPregledClanovaVM>() {
            @Override
            public void run(ClanoviKubaPregledClanovaVM x) {
                podaci=x;

                popuniPodatke(podaci);
            }
        });
    }
    private void popuniPodatkeNakonPretrageTask(String query) {
        String imePrezime=query;

        MyApiRequest.get(getActivity(),"ClanoviKluba/PretragaClanovaByImePrezime/"+imePrezime, new MyRunnable<ClanoviKubaPregledClanovaVM>() {
            @Override
            public void run(ClanoviKubaPregledClanovaVM x) {
                podaci=x;

                popuniPodatke(podaci);
            }
        });
    }
    private void popuniPodatke(final ClanoviKubaPregledClanovaVM podaci) {
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
                    view = inflater.inflate(R.layout.row_clanovi_trener, parent, false);
                }

                clanImage = (ImageView) view.findViewById(R.id.img_clan_trener);
                ClanoviKubaPregledClanovaVM.Row x = podaci.rows.get(position);
                F.findView(view, R.id.txtPrvaLinijaClanoviTrener, TextView.class).setText(x.prezime+" ("+x.imeRoditelja+") "+x.ime);
                F.findView(view, R.id.txtDrugaLinijaClanoviTrener, TextView.class).setText(F.date_ddMMyyy(x.datumRodjenja)+" - "+x.mjestoRodjenja+" ("+x.spol+") ");
                F.findView(view, R.id.txtTrecaLinijaClanoviTrener, TextView.class).setText(x.kontaktTelefon);

                if(x.slika=="" ||x.slika==null)
                {
                    clanImage.setImageResource(R.drawable.nema_sliku);
                }
                else {
                    byte[] decodedString = Base64.decode(x.slika, Base64.DEFAULT);
                    Bitmap decodedByte = BitmapFactory.decodeByteArray(decodedString, 0, decodedString.length);
                    clanImage.setImageBitmap(decodedByte);
                }
                return view;

            }
        };
        listaAktivnihClanova.setAdapter(adapter);
        }

}
