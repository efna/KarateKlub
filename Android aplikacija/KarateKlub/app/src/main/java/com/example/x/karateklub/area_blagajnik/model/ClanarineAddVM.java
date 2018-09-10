package com.example.x.karateklub.area_blagajnik.model;

import java.io.Serializable;

public class ClanarineAddVM implements Serializable {
    public String naziv;
    public String datumOd;
    public String datumDo;

    public ClanarineAddVM(String naziv, String datumOd, String datumDo) {

        this.naziv = naziv;
        this.datumOd = datumOd;
        this.datumDo = datumDo;
    }
}
