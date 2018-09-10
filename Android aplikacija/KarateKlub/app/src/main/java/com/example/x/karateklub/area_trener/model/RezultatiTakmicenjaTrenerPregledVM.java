package com.example.x.karateklub.area_trener.model;

import java.io.Serializable;
import java.util.List;

public class RezultatiTakmicenjaTrenerPregledVM implements Serializable {
    public  static class Row implements  Serializable{
        public int id;
        public int takmicarId;
        public String takmicar;
        public String starosnaDob;
        public String kategorija;
        public String disciplina;
        public String osvojenoMjesto;
        public int brojTakmicaraUKategoriji;
        public int brojPobjeda;
        public int brojPoraza;


    }
public List<Row> rows;

}
