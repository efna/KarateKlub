package com.example.x.karateklub.area_trener.fragment;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v7.widget.CardView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import com.example.x.karateklub.R;


public class TrenerOpcijeFragment extends Fragment {
    public  static TrenerOpcijeFragment newInstance()
    {
        return new TrenerOpcijeFragment();
    }


    private OnFragmentInteractionListener mListener;

    public TrenerOpcijeFragment() {
        // Required empty public constructor
    }


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        View view=inflater.inflate(R.layout.fragment_trener_opcije, container, false);
        final android.support.v4.app.FragmentManager fragmentManager=this.getFragmentManager();
        final android.support.v4.app.FragmentTransaction fragmentTransaction=fragmentManager.beginTransaction();
        final CardView clanovi;
        CardView zvanja;
        CardView takmicenja;

        clanovi=(CardView) view.findViewById(R.id.clanoviTrener);
        zvanja=(CardView) view.findViewById(R.id.stecenaZvanjaClanovaTrener);
        takmicenja=(CardView) view.findViewById(R.id.Takmičenja);

        clanovi.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                getActivity().setTitle("Članovi");
                android.support.v4.app.Fragment clanoviTrenerFr=new ClanoviTrenerFragment();
                fragmentTransaction.replace(R.id.fragmentContentTrener, clanoviTrenerFr);
                fragmentTransaction.addToBackStack(null);
                fragmentTransaction.commit();

            }
        });

        zvanja.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                getActivity().setTitle("Zvanja");

                android.support.v4.app.Fragment stecenaZvanjaTrenerFr=new StecenaZvanjaTrenerFragment();
                fragmentTransaction.replace(R.id.fragmentContentTrener, stecenaZvanjaTrenerFr);
                fragmentTransaction.addToBackStack(null);
                fragmentTransaction.commit();

            }
        });
        takmicenja.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                getActivity().setTitle("Takmičenja");
                android.support.v4.app.Fragment takmicenjaTrenerFr=new TakmicenjaTrenerFragment();
                fragmentTransaction.replace(R.id.fragmentContentTrener, takmicenjaTrenerFr);
                fragmentTransaction.addToBackStack(null);
                fragmentTransaction.commit();

            }
        });
        return view;
    }

    public void onButtonPressed(Uri uri) {
        if (mListener != null) {
            mListener.onFragmentInteraction(uri);
        }
    }

    @Override
    public void onAttach(Context context) {
        super.onAttach(context);

    }

    @Override
    public void onDetach() {
        super.onDetach();
        mListener = null;
    }


    public interface OnFragmentInteractionListener {
        // TODO: Update argument type and name
        void onFragmentInteraction(Uri uri);
    }
}
