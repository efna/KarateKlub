package com.example.x.karateklub.area_clan.model;

import java.io.Serializable;
import java.util.Date;
import java.util.List;

public class RezultatiTakmicenjaClanPregledVM implements Serializable {
    public static class Row implements Serializable
    {
        public String nazivTakmicenja;
        public String mjestoOdrzavanja;
        public Date datumOdrzavaja;
        public String osvojenoMjesto;
        public String kategorija;
        public String uzrast;
        public String disciplina;
    }

        public List<Row> rows;

}
