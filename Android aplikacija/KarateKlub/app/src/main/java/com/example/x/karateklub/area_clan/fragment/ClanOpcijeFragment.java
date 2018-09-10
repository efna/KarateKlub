package com.example.x.karateklub.area_clan.fragment;



import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v7.widget.CardView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import com.example.x.karateklub.R;


public class ClanOpcijeFragment extends Fragment {
    public  static ClanOpcijeFragment newInstance()
    {
        return new ClanOpcijeFragment();
    }


    private OnFragmentInteractionListener mListener;

    public ClanOpcijeFragment() {
        // Required empty public constructor
    }



    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        View view = inflater.inflate(R.layout.fragment_clan_opcije, container, false);
       final android.support.v4.app.FragmentManager fragmentManager=this.getFragmentManager();
       final android.support.v4.app.FragmentTransaction fragmentTransaction=fragmentManager.beginTransaction();
        final CardView clanarine;
        CardView zvanje;
        CardView rezultati;

        clanarine=(CardView) view.findViewById(R.id.clanarine);
        zvanje=(CardView) view.findViewById(R.id.zvanje);
        rezultati=(CardView) view.findViewById(R.id.rezultati);

        clanarine.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                getActivity().setTitle("Članarine");
                android.support.v4.app.Fragment clanarineClanTab=new ClanarineClanTabFragment();
                fragmentTransaction.replace(R.id.fragmentContent, clanarineClanTab);
                fragmentTransaction.addToBackStack(null);
                fragmentTransaction.commit();

            }
        });

        zvanje.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                getActivity().setTitle("Stečena zvanja");

                android.support.v4.app.Fragment zvanjaClana=new StecenaZvanjaClanFragment();
                fragmentTransaction.replace(R.id.fragmentContent, zvanjaClana);
                fragmentTransaction.addToBackStack(null);
                fragmentTransaction.commit();

            }
        });
        rezultati.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                getActivity().setTitle("Rezultati takmičenja");

                android.support.v4.app.Fragment rezultatiTakmicenja=new RezultatiTakmicenjaClanFragment();
                fragmentTransaction.replace(R.id.fragmentContent, rezultatiTakmicenja);
                fragmentTransaction.addToBackStack(null);
                fragmentTransaction.commit();
            }
        });
        return view;
    }

    // TODO: Rename method, update argument and hook method into UI event
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
