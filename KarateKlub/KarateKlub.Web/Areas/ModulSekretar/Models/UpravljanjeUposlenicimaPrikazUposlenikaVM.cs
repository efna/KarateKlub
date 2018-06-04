using KarateKlub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class UposlenikPodaci
    {
        public int osobaId { get; set; }
        public string imePrezime { get; set; }
        public bool aktivan { get; set; }
        public string funkcija { get; set; }
        public Osoba osoba { get; set; }

    }
    public class UpravljanjeUposlenicimaPrikazUposlenikaVM
    {
        public List<UposlenikPodaci> uposlenici;
        public int aktivan { get; set; }
        public List<SelectListItem> Aktivnost { get; set; }
        public int uloga { get; set; }

        public UpravljanjeUposlenicimaPrikazUposlenikaVM(List<Osoba> listaUposlenika, int aktivan, int uloga)
        {
            uposlenici = listaUposlenika.Select(x => new UposlenikPodaci
            {
                osoba = x,
                osobaId=x.Id,
                imePrezime = x.Ime + " ("+x.ImeRoditelja+") "+ x.Prezime,
                aktivan = x.isAktivnaOsoba
            }).ToList();
            Aktivnost = new List<SelectListItem>();
            Aktivnost.Add(new SelectListItem { Text = "Svi", Value = "0" });
            Aktivnost.Add(new SelectListItem { Text = "Aktivno osoblje", Value = "1" });
            Aktivnost.Add(new SelectListItem { Text = "Neaktivno osoblje", Value = "2" });
            this.aktivan = aktivan;
            this.uloga = uloga;
        }
    }
}