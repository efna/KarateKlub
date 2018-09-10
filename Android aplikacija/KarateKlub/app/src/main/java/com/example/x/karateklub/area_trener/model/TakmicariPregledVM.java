package com.example.x.karateklub.area_trener.model;

import java.io.Serializable;
import java.util.List;

public class TakmicariPregledVM implements Serializable {
    public static class Row implements Serializable{
        public int id;
        public String takmicar;
        public String regBroj;
        public String slika;
    }
    public List<Row> rows;
}
