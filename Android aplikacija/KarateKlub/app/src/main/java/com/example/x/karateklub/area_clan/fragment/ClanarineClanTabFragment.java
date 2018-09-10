package com.example.x.karateklub.area_clan.fragment;


import android.content.Intent;
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
import com.example.x.karateklub.area_clan.MainClanActivity;
import com.example.x.karateklub.area_clan.ViewPagerAdapter;


public class ClanarineClanTabFragment extends Fragment {
    public  static ClanarineClanTabFragment newInstance()
    {
        return new ClanarineClanTabFragment();
    }
    private ImageButton btnBack;


    public ClanarineClanTabFragment() {
        // Required empty public constructor
    }


    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        final View view= inflater.inflate(R.layout.layout_clanarine_tab_clan, container, false);


        TabLayout tabLayout;
        final ViewPager viewPager;
       final ViewPagerAdapter adapter;
        btnBack=view.findViewById(R.id.btnBackClanarineClanTab);

        tabLayout = view.findViewById(R.id.tablayout_id);
        viewPager = view.findViewById(R.id.viewpager_id);
        android.support.v4.app.FragmentManager fragmentManager=this.getChildFragmentManager();


    adapter = new ViewPagerAdapter(fragmentManager);

    adapter.AddFragment(new ClanarineIzmireneClanarineClanFragment(), "Izmirene");
        adapter.AddFragment(new ClanarineNeizmireneClanarineClanFragment(), "Neizmirene");

        viewPager.setAdapter(adapter);

tabLayout.setupWithViewPager(viewPager);
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
}
