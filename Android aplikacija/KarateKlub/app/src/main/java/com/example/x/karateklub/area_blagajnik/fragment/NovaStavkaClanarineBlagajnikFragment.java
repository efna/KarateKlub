package com.example.x.karateklub.area_blagajnik.fragment;

import android.app.DatePickerDialog;

import android.content.DialogInterface;
import android.os.Bundle;
import android.support.v4.app.DialogFragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.DatePicker;
import android.widget.EditText;
import com.example.x.karateklub.R;
import com.example.x.karateklub.area_blagajnik.model.ClanarinePregledVM;
import com.example.x.karateklub.area_blagajnik.model.NeizmireneClanarinePregledVM;
import com.example.x.karateklub.area_blagajnik.model.StavkeClanarineEditVM;
import com.example.x.karateklub.helper.MyApiRequest;

import java.util.Calendar;


public class NovaStavkaClanarineBlagajnikFragment extends DialogFragment {

    public static final String CLANARINA_NOVASTAVKA_KEY = "clanarina_novastavka_key";
   public static final String NEIZMIRENA_CLANARINA_KEY = "neizmirena_clanarina_key";

    ClanarinePregledVM.Row clanarina;
    NeizmireneClanarinePregledVM.Row neizmirenaclanarina;

    private EditText txtBrojPriznanice;
    private EditText txtIznosBrojevima;
    private EditText txtIznosSlovima;
    private EditText txtDatumUplate;
    private EditText txtMjestoUplate;
    Calendar kalendar;
    int dan,mjesec,godina;
    private Button btnSpremi;
    public NovaStavkaClanarineBlagajnikFragment() {
        // Required empty public constructor
    }

    public  static NovaStavkaClanarineBlagajnikFragment newInstance(ClanarinePregledVM.Row x,NeizmireneClanarinePregledVM.Row y)
    {
        NovaStavkaClanarineBlagajnikFragment fragment = new NovaStavkaClanarineBlagajnikFragment();
        Bundle args = new Bundle();
        args.putSerializable(CLANARINA_NOVASTAVKA_KEY, x);
        args.putSerializable(NEIZMIRENA_CLANARINA_KEY, y);

        fragment.setArguments(args);
        return fragment;

    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        if (getArguments() != null) {
            clanarina = (ClanarinePregledVM.Row) getArguments().getSerializable(CLANARINA_NOVASTAVKA_KEY);
           neizmirenaclanarina = (NeizmireneClanarinePregledVM.Row) getArguments().getSerializable(NEIZMIRENA_CLANARINA_KEY);
            setStyle(STYLE_NORMAL, R.style.MojDialog );

        }
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        final View view= inflater.inflate(R.layout.fragment_nova_stavka_clanarine_dialog, container, false);
        getActivity().setTitle("Nova stavka članarine");
        txtBrojPriznanice=view.findViewById(R.id.txtBrojPriznanice);
        txtIznosBrojevima=view.findViewById(R.id.txtIznosBrojevima);
        txtIznosSlovima=view.findViewById(R.id.txtIznosSlovima);
        txtDatumUplate=view.findViewById(R.id.txtDatumUplate);
        txtMjestoUplate=view.findViewById(R.id.txtMjestoUplate);

        btnSpremi=view.findViewById(R.id.btnSpremiNovuStavkuClanarine);
        kalendar=Calendar.getInstance();
        dan=kalendar.get(Calendar.DAY_OF_MONTH);
        mjesec=kalendar.get(Calendar.MONTH);
        godina=kalendar.get(Calendar.YEAR);

        txtDatumUplate.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                DatePickerDialog datePickerDialog=new DatePickerDialog(getActivity(), new DatePickerDialog.OnDateSetListener() {
                    @Override
                    public void onDateSet(DatePicker view, int godina, int mjesec, int dan) {
                        mjesec=mjesec+1;
                        String d="";
                        String m="";
                        if(mjesec < 10){

                            m = "0" + mjesec;
                        }
                        if(dan < 10){

                            d  = "0" + dan ;
                        }

                        if(mjesec<10 && dan<10)
                            txtDatumUplate.setText(d.toString()+"/"+m.toString()+"/"+godina);
                        else if(mjesec<10 && dan>10)
                            txtDatumUplate.setText(dan+"/"+m.toString()+"/"+godina);
                        else if(mjesec>10 && dan<10)
                            txtDatumUplate.setText(d.toString()+"/"+mjesec+"/"+godina);
                        else
                            txtDatumUplate.setText(dan+"/"+mjesec+"/"+godina);
                    }
                },godina,mjesec,dan);
                datePickerDialog.show();
            }
        });

        btnSpremi.setOnClickListener(new View.OnClickListener()
        {
            @Override
         public void onClick(View v)
            {
               if(ValidirajPodatkeNoveStavkeClanarine())
                   do_btnSpremi_click();
            }
        });
        view.findViewById(R.id.button_close_dodavanje_stavke).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                do_btnClose();
            }
        });
    return  view;
    }



    private void do_btnClose() {
        dismiss();
    }
    private boolean ValidirajPodatkeNoveStavkeClanarine(){



        if(txtBrojPriznanice.getText().length()==0)
            txtBrojPriznanice.setError("Broj priznanice je obavezno polje!");
        else
            txtBrojPriznanice.setError(null);
        if(txtIznosBrojevima.getText().length()==0 )
            txtIznosBrojevima.setError("Iznos uplate je obavezno polje!");
        else
            txtIznosBrojevima.setError(null);
        if(txtIznosSlovima.getText().length()==0 )
            txtIznosSlovima.setError("Iznos uplate slovima je obavezno polje!");
        else
            txtIznosSlovima.setError(null);
        if(txtDatumUplate.getText().length()==0 )
            txtDatumUplate.setError("Datum uplate je obavezno polje!");
        else
            txtDatumUplate.setError(null);
        if(txtMjestoUplate.getText().length()==0 )
            txtMjestoUplate.setError("Mjesto uplate je obavezno polje!");
        else
            txtMjestoUplate.setError(null);
        if(txtBrojPriznanice.getText().length()==0 || txtIznosBrojevima.getText().length()==0 || txtIznosSlovima.getText().length()==0
                || txtDatumUplate.getText().length()==0 || txtMjestoUplate.getText().length()==0
                )
            return false;
        else
            return true;
    }

    private void do_btnSpremi_click()
    {
        StavkeClanarineEditVM novaClanarina=new StavkeClanarineEditVM();
        novaClanarina.stavkaId=neizmirenaclanarina.stavkaId;
        novaClanarina.clanarinaId=neizmirenaclanarina.clanarinaId;
        novaClanarina.clanId=neizmirenaclanarina.clanKlubaId;
        novaClanarina.brojPriznanice=txtBrojPriznanice.getText().toString();
        novaClanarina.iznosBrojevima=txtIznosBrojevima.getText().toString();
        novaClanarina.iznosSlovima=txtIznosSlovima.getText().toString();
        novaClanarina.datumUplate=txtDatumUplate.getText().toString();
        novaClanarina.mjestoUplate=txtMjestoUplate.getText().toString();


        MyApiRequest.post(getActivity(), "StavkeClanarine/Add", novaClanarina, null);

        getDialog().dismiss();



    }
}
