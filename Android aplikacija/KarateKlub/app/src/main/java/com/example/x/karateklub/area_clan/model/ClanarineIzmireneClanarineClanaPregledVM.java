package com.example.x.karateklub.area_clan.model;

import java.io.Serializable;
import java.util.Date;
import java.util.List;

public class ClanarineIzmireneClanarineClanaPregledVM implements Serializable {
    public static class Row implements Serializable{
        public int id;
        public String naziv;
        public String mjestoUplate;
        public String iznos;
        public Date datumUplate;
        public Date datumOd;
        public Date datumDo;
        public String brojPriznanice;

    }



        public List<Row> rows;

}
