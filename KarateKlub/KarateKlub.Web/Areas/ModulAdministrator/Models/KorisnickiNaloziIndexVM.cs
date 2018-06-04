using KarateKlub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulAdministrator.Models
{

    public class KorisnickiNaloziIndexVM
    {
        public List<KorisnickiNaloziPodaciVM> korisnickiNalozi;
        public int aktivan { get; set; }
        public List<SelectListItem> Aktivnost { get; set; }
        public int uloga { get; set; }
      

        public KorisnickiNaloziIndexVM(List<KorisnickiNalozi> nalozi,int aktivan,int uloga)
        {
            korisnickiNalozi = nalozi.Select(x => new KorisnickiNaloziPodaciVM {
                korisnickiNalog=x,
                Osoba=x.Osoba.Ime+" "+x.Osoba.Prezime,
                korisnickaUloga=x.KorisnickaUloga.Naziv,
                isAktivanNalog=x.isAktivanNalog
             
            }).ToList();
            Aktivnost = new List<SelectListItem>();
            Aktivnost.Add(new SelectListItem { Text = "Svi nalozi", Value = "0" });
            Aktivnost.Add(new SelectListItem { Text = "Aktivni nalozi", Value = "1" });
            Aktivnost.Add(new SelectListItem { Text = "Neaktivni nalozi", Value = "2" });
            this.aktivan = aktivan;
            this.uloga = uloga;
        }

      

    }




}