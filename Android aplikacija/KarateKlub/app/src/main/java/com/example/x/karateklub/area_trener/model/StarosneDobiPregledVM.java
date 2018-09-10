package com.example.x.karateklub.area_trener.model;

import java.io.Serializable;
import java.util.List;

public class StarosneDobiPregledVM implements Serializable {
    public  static class Row implements  Serializable{

        public int id;
        public String naziv;
    }
    public List<Row> rows;
}
