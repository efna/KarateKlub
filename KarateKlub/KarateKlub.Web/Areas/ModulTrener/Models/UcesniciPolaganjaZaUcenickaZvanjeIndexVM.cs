using KarateKlub.Data;
using KarateKlub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulTrener.Models
{
    public class UcesnikPolaganjaPodaci
    {
        public int Id { get; set; }
        public string Ucesnik { get; set; }
        public string JMBG { get; set; }
        public string zvanjeUKarateu { get; set; }
        public int polaganjeId { get; set; }
    }

    public class UcesniciPolaganjaZaUcenickaZvanjeIndexVM
    {
        MojContext ctx = new MojContext();
        public List<UcesnikPolaganjaPodaci> ucesniciPolaganja;
        public int zvanje { get; set; }
        public List<SelectListItem> ZvanjaUKarateu { get; set; }

        public UcesniciPolaganjaZaUcenickaZvanjeIndexVM(List<UcesniciPolaganjaZaUcenickaZvanja> ucesnici, int zvanje)
        {
            ucesniciPolaganja = ucesnici.Select(x => new UcesnikPolaganjaPodaci
            {
                Id = x.Id,
                Ucesnik = x.ClanKluba.Osoba.Ime + " (" + x.ClanKluba.Osoba.ImeRoditelja + ") " + x.ClanKluba.Osoba.Prezime,
                JMBG = x.ClanKluba.Osoba.JMBG,
                zvanjeUKarateu = x.ZvanjeUKarateu.Naziv,
                polaganjeId = x.PolaganjeUcenickaZvanjaId

            }).ToList();
            ZvanjaUKarateu = BindZvanja();
            ZvanjaUKarateu.Add(new SelectListItem { Text = "Svi učesnici", Value = "0" });

            this.zvanje = zvanje;
        }

        private List<SelectListItem> BindZvanja()
        {
            return ctx.ZvanjaUKarateu.Where(x => x.isDeleted == false).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv }).ToList();

        }
    }
}