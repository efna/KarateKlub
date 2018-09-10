package com.example.x.karateklub.area_trener.model;

import java.io.Serializable;
import java.util.Date;
import java.util.List;

public class TakmicenjaPregledVM implements Serializable {
    public  static class Row implements  Serializable{
        public int id;
        public String nazivTakmicenja;
        public Date datumOdrzavanjaTakmicenja;
        public String mjestoOdrzavanjaTakmicenja;
        public String organizatorTakmicenja;
        public int savezId;
        public String nazivSaveza;

    }
    public List<Row> rows;
}
