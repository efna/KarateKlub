using KarateKlub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulBlagajnik.Models
{

    public class ClanoviKlubaPregledClanovaKlubaPoPojasevimaVM
    {
        public List<ClanPodaci> clanovi;
        public int zvanje { get; set; }
        public List<SelectListItem> Zvanja { get; set; }

        public ClanoviKlubaPregledClanovaKlubaPoPojasevimaVM(List<ClanoviKluba> listaClanova, int zvanje)
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
            Zvanja = new List<SelectListItem>();
            Zvanja.Add(new SelectListItem { Text = "Početnici", Value = "0" });
            Zvanja.Add(new SelectListItem { Text = "Žuti pojas(V kyu)", Value = "1" });
            Zvanja.Add(new SelectListItem { Text = "Oranž pojas(IV kyu)", Value = "2" });
            Zvanja.Add(new SelectListItem { Text = "Zeleni pojas(V kyu)", Value = "3" });
            Zvanja.Add(new SelectListItem { Text = "Plavi pojas(II kyu)", Value = "4" });
            Zvanja.Add(new SelectListItem { Text = "Smeđi pojas(I kyu)", Value = "5" });
            Zvanja.Add(new SelectListItem { Text = "Majstori karatea", Value = "6" });

            this.zvanje = zvanje;
        }
    }
}