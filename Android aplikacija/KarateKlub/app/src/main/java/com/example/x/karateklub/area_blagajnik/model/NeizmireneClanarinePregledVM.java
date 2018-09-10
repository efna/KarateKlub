package com.example.x.karateklub.area_blagajnik.model;

import java.io.Serializable;
import java.util.Date;
import java.util.List;

public class NeizmireneClanarinePregledVM implements Serializable {
    public static class Row implements Serializable{

        public int stavkaId;
        public int clanarinaId;
        public int clanKlubaId;
        public String clanKluba;
        public Date datumOd;
        public Date datumDo;
    }
public List<Row> rows;
}
