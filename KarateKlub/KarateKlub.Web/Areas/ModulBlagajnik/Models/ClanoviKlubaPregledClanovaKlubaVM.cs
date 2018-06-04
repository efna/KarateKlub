using KarateKlub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulBlagajnik.Models
{
    public class ClanPodaci
    {
        public int zvanjeId { get; set; }
        public int osobaId { get; set; }
        public int clanId { get; set; }
        public string Clan { get; set; }
        public string Zvanje { get; set; }
        public string StarosnaDob { get; set; }
        public string Spol { get; set; }
        public bool isAktivnaOsoba { get; set; }
    }
    public class ClanoviKlubaPregledClanovaKlubaVM
    {
        public List<ClanPodaci> clanovi;
        public int aktivan { get; set; }
        public List<SelectListItem> Aktivnost { get; set; }

        public ClanoviKlubaPregledClanovaKlubaVM(List<ClanoviKluba> listaClanova, int zvanje)
        {
            clanovi = listaClanova.Select(x => new ClanPodaci
            {
                zvanjeId = x.ZvanjeUKarateuId,
                osobaId = x.OsobaId,
                clanId = x.Id,
                Clan = x.Osoba.Ime + " (" + x.Osoba.ImeRoditelja + ") " + x.Osoba.Prezime,
                Zvanje = x.ZvanjeUKarateu.Naziv,
                StarosnaDob = x.StarosnaDob.Naziv,
                Spol = x.Osoba.Spol,
                isAktivnaOsoba = x.Osoba.isAktivnaOsoba
            }).ToList();
            Aktivnost = new List<SelectListItem>();
            Aktivnost.Add(new SelectListItem { Text = "Svi članovi", Value = "0" });
            Aktivnost.Add(new SelectListItem { Text = "Aktivni članovi", Value = "1" });
            Aktivnost.Add(new SelectListItem { Text = "Neaktivni članovi", Value = "2" });


            this.aktivan = aktivan;
        }
    }
}