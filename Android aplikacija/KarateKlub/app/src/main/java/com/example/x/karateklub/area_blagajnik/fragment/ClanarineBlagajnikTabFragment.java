package com.example.x.karateklub.area_blagajnik.fragment;


import android.os.Bundle;
import android.support.design.widget.TabLayout;
import android.support.v4.app.Fragment;
import android.support.v4.view.ViewPager;
import android.support.v7.app.AppCompatActivity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageButton;
import com.example.x.karateklub.R;
import com.example.x.karateklub.area_blagajnik.model.ClanarinePregledVM;
import com.example.x.karateklub.area_clan.ViewPagerAdapter;

public class ClanarineBlagajnikTabFragment extends Fragment {

  public static final String CLANARINA_KEY = "clanarina_key";
    ClanarinePregledVM.Row clanarina;
    private ImageButton btnBack;

    public ClanarineBlagajnikTabFragment() {
        // Required empty public constructor
    }

    public  static ClanarineBlagajnikTabFragment newInstance(ClanarinePregledVM.Row x)
    {
        ClanarineBlagajnikTabFragment fragment = new ClanarineBlagajnikTabFragment();
        Bundle args = new Bundle();
        args.putSerializable(CLANARINA_KEY, x);
        fragment.setArguments(args);
        return fragment;

    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        if (getArguments() != null) {
            clanarina = (ClanarinePregledVM.Row) getArguments().getSerializable(CLANARINA_KEY);

        }
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
      final View view= inflater.inflate(R.layout.layout_clanarine_blagajnik_tab, container, false);
        TabLayout tabLayout;
        final ViewPager viewPager;
        final ViewPagerAdapter adapter;
        btnBack=view.findViewById(R.id.btnBackClanarineStavkeBlagajnik);

        tabLayout = view.findViewById(R.id.tablayout_id_ClanarineBlagajnik);
        viewPager = view.findViewById(R.id.viewpager_id_clanarineBlagajnik);
        android.support.v4.app.FragmentManager fragmentManager=this.getChildFragmentManager();


        adapter = new ViewPagerAdapter(fragmentManager);
        adapter.AddFragment(NeizmireneClanarineBlagajnikFragment.newInstance(clanarina), "Neizmirene");
        adapter.AddFragment(IzmireneClanarineBlagajnikFragment.newInstance(clanarina), "Izmirene");

        viewPager.setAdapter(adapter);

        tabLayout.setupWithViewPager(viewPager);
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

        android.support.v4.app.Fragment clanarineBlagajnik= ClanarineBlagajnikFragment.newInstance();
        android.support.v4.app.FragmentManager fragmentManager=getFragmentManager();
        android.support.v4.app.FragmentTransaction fragmentTransaction=fragmentManager.beginTransaction();
        fragmentTransaction.replace(R.id.fragmentContentBlagajnik, clanarineBlagajnik);
        fragmentTransaction.addToBackStack(null);
        fragmentTransaction.commit();
    }

}
