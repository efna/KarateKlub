package com.example.x.karateklub.area_blagajnik.model;

import java.io.Serializable;
import java.util.Date;
import java.util.List;

public class IzmireneClanarinePregledVM implements Serializable {
    public  static  class Row implements Serializable{
        public int clanarinaId;
        public int clanKlubaId;
        public String clanKluba;
        public String brojPriznanice;
        public String iznosBrojevima;
        public String mjestoUplate;
        public Date datumUplate;
        public Date datumOd;
        public Date datumDo;
    }
 public List<Row> rows;
}
