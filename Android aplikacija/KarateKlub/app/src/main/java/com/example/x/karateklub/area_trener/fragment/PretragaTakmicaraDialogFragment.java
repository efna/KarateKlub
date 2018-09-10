package com.example.x.karateklub.area_trener.fragment;

import android.app.DialogFragment;
import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.Bundle;
import android.util.Base64;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.BaseAdapter;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.SearchView;
import android.widget.TextView;
import com.example.x.karateklub.R;
import com.example.x.karateklub.area_trener.model.TakmicariPregledVM;
import com.example.x.karateklub.helper.MyApiRequest;
import com.example.x.karateklub.helper.MyRunnable;


public class PretragaTakmicaraDialogFragment extends DialogFragment {

    public static final String TAKMICARI_KEY = "takmicari_key";
    private ListView listaTakmicara;
    private SearchView searchView;
    private MyRunnable<TakmicariPregledVM.Row> callback;
    private ImageView takmicarImage;
private TakmicariPregledVM podaci;

    public static PretragaTakmicaraDialogFragment newInstance(MyRunnable<TakmicariPregledVM.Row> myCallback) {
        PretragaTakmicaraDialogFragment fragment = new PretragaTakmicaraDialogFragment();
        Bundle args = new Bundle();
        args.putSerializable(TAKMICARI_KEY, myCallback);
        fragment.setArguments(args);
        return fragment;
    }

    public PretragaTakmicaraDialogFragment() {
        // Required empty public constructor
    }


    public static PretragaTakmicaraDialogFragment newInstance(String param1, String param2) {
        PretragaTakmicaraDialogFragment fragment = new PretragaTakmicaraDialogFragment();
        Bundle args = new Bundle();

        fragment.setArguments(args);
        return fragment;
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        if (getArguments() != null) {
            callback = (MyRunnable<TakmicariPregledVM.Row>) getArguments().getSerializable(TAKMICARI_KEY);
        }
        setStyle(STYLE_NORMAL, R.style.MojDialog );
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        final View view= inflater.inflate(R.layout.fragment_pretraga_takmicara_dialog, container, false);

        listaTakmicara = view.findViewById(R.id.listaTakmicara);
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

        view.findViewById(R.id.button_close).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                do_btnClose();
            }
        });
        popuniPodatkeTask();

        return view;
    }

    private void do_btnClose() {
        dismiss();
    }
    private void do_btnTraziClick(String query) {
        popuniPodatkeTask(query);
    }
    private void popuniPodatkeTask() {

        MyApiRequest.get(getActivity(),"Takmicari/PregledTakmicara/", new MyRunnable<TakmicariPregledVM>() {
            @Override
            public void run(TakmicariPregledVM x) {
                podaci=x;

                popuniPodatke(podaci);
            }
        });
    }
    private void popuniPodatkeNakonPretrageTask(String query) {
        String imePrezime=query;

        MyApiRequest.get(getActivity(),"Takmicari/PretragaTakmicaraByImePrezime/"+imePrezime, new MyRunnable<TakmicariPregledVM>() {
            @Override
            public void run(TakmicariPregledVM x) {
                podaci=x;

                popuniPodatke(podaci);
            }
        });
    }
    private void popuniPodatkeTask(String query) {
        String imePrezime=query;
        MyApiRequest.get(getActivity(), "Takmicari/PretragaTakmicaraByImePrezime/" + imePrezime, new MyRunnable<TakmicariPregledVM>() {
            @Override
            public void run(TakmicariPregledVM x) {
                popuniPodatke(x);
            }
        });
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

    private void popuniPodatke(final TakmicariPregledVM podaci) {

        listaTakmicara.setAdapter(new BaseAdapter() {
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
            public View getView(int position, View view, ViewGroup viewGroup) {

                if(view==null)
                {
                    LayoutInflater inflater = (LayoutInflater)getActivity().getSystemService(Context.LAYOUT_INFLATER_SERVICE);
                    view = inflater.inflate(R.layout.row_takmicari, viewGroup,false);
                }
                takmicarImage = (ImageView) view.findViewById(R.id.imgTakmicar);

                TextView takmicar = (TextView) view.findViewById(R.id.txtPrvaLinijaTakmicari);
                TextView regBroj = (TextView) view.findViewById(R.id.txtDrugaLinijaTakmicari);

                TakmicariPregledVM.Row x = podaci.rows.get(position);


                takmicar.setText(x.takmicar);
                regBroj.setText("Registarski broj: "+x.regBroj);
                if(x.slika=="" ||x.slika==null)
                {
                    takmicarImage.setImageResource(R.drawable.nema_sliku);
                }
                else {
                    byte[] decodedString = Base64.decode(x.slika, Base64.DEFAULT);
                    Bitmap decodedByte = BitmapFactory.decodeByteArray(decodedString, 0, decodedString.length);
                    takmicarImage.setImageBitmap(decodedByte);
                }

                return view;
            }
        });

        listaTakmicara.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                TakmicariPregledVM.Row x = podaci.rows.get(position);
                getDialog().dismiss();
                callback.run(x);
            }
        });
    }
}
