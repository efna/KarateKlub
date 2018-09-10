package com.example.x.karateklub.area_trener.model;

import java.io.Serializable;
import java.util.Date;
import java.util.List;

public class ClanoviKubaPregledClanovaVM implements Serializable {
    public static class Row implements Serializable {
        public int id;
        public String ime;
        public String prezime;
        public String imeRoditelja;
        public String spol;
        public Date datumRodjenja;
        public String mjestoRodjenja;
        public String slika;
        public String kontaktTelefon;

    }

    public List<Row> rows;
}
