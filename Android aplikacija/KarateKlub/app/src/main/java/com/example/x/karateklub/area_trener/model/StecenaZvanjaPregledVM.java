package com.example.x.karateklub.area_trener.model;

import java.io.Serializable;
import java.util.Date;
import java.util.List;

public class StecenaZvanjaPregledVM implements Serializable {
    public  static class Row implements  Serializable{
        public String ime;
        public String prezime;
        public String imeRoditelja;
        public String nazivZvanja;
        public String mjestoSticanja;
        public Date datumSticanja;
        public String organizator;
    }
    public List<Row> rows;
}
