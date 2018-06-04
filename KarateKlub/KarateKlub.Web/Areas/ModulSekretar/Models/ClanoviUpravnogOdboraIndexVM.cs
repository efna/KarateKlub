using KarateKlub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class ClanoviUpravnogOdboraPodaci
    {
        public int osobaId { get; set; }
        public int clanId { get; set; }
        public string uloga { get; set; }
        public string ImePrezime { get; set; }
        public DateTime datumIzglasavanja { get; set; }
        public int aktivan { get; set; }
        public bool isAktivanClan{ get; set; }
       
    }

    public class ClanoviUpravnogOdboraIndexVM
    {
        public List<ClanoviUpravnogOdboraPodaci> clanoviUO;
        public int aktivnost { get; set; }
        public List<SelectListItem> Aktivnost { get; set; }

        public ClanoviUpravnogOdboraIndexVM(List<ClanoviUpravnogOdbora> clanovi, int aktivnost)
        {
            clanoviUO = clanovi.Select(x => new ClanoviUpravnogOdboraPodaci
            {
                osobaId = x.OsobaId,
                clanId=x.Id,
                ImePrezime=x.Osoba.Ime+" "+x.Osoba.Prezime,
                uloga = x.UlogeClanovaUpravnogOdbora.Naziv,
                datumIzglasavanja = x.DatumIzglasavanja,
                isAktivanClan = x.Aktivan

            }).ToList();
            Aktivnost = new List<SelectListItem>();
            Aktivnost.Add(new SelectListItem { Text = "Aktivni članovi", Value = "0" });
            Aktivnost.Add(new SelectListItem { Text = "Neaktivni članovi", Value = "1" });

            Aktivnost.Add(new SelectListItem { Text = "Svi članovi", Value = "3" });
            this.aktivnost = aktivnost;
        }
    }
}