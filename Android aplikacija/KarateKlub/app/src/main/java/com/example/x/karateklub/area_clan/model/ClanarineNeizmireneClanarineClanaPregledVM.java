package com.example.x.karateklub.area_clan.model;

import java.io.Serializable;
import java.util.Date;
import java.util.List;

public class ClanarineNeizmireneClanarineClanaPregledVM implements Serializable {
    public static class Row implements Serializable{
        public int id;
        public String naziv;
        public Date datumOd;
        public Date datumDo;
    }

        public List<Row> rows;
}
