package com.example.x.karateklub.area_trener.model;

import java.io.Serializable;

public class TakmicenjeAddVM implements Serializable {
    public String naziv;
    public String mjestoOdrzavanja;
    public String datumOdrzavanja;
    public String organizator;
    public int savezId;

    public TakmicenjeAddVM(String naziv,String mjestoOdrzavanja,String datumOdrzavanja,String organizator,int savezId){
        this.naziv=naziv;
        this.mjestoOdrzavanja=mjestoOdrzavanja;
        this.datumOdrzavanja=datumOdrzavanja;
        this.organizator=organizator;
        this.savezId=savezId;

    }
}
