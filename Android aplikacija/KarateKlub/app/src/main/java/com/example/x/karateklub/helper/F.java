package com.example.x.karateklub.helper;
import android.app.Activity;
import android.view.View;

import java.text.DecimalFormat;
import java.text.SimpleDateFormat;
import java.util.Date;
public class F {

        public static <T> T findView(View view, int id, Class<T> tClass)
        {
            final View viewById = view.findViewById(id);
            return (T) viewById;
        }

        public static <T> T findView(Activity activity, int id, Class<T> tClass)
        {
            final View viewById = activity.findViewById(id);
            return (T) viewById;
        }

        public static String date_ddMMyyy(Date datum)
        {
            if (datum == null)
                return "";
            final SimpleDateFormat sdf = new SimpleDateFormat("dd.MM.yyyy");
            return sdf.format(datum);
        }

        public static String decimal_0_00(Float value)
        {
            if (value == null)
                return "";
            DecimalFormat df = new DecimalFormat("#.##");
            return df.format(value);
        }
    }

