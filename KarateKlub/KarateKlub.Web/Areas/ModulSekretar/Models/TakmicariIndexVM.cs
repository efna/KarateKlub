using KarateKlub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class TakmicarPodaci
    {

        public int Id { get; set; }
        public string Takmicar { get; set; }
        public string JMBG { get; set; }
        public string Spol { get; set; }
        public string RegistarskiBroj { get; set; }
        public string StarosnaDob { get; set; }
        public int StarosnaDobId { get; set; }
        public string Obrazlozenje { get; set; }
        public bool isAktivanTakmicar { get; set; }
    }

    public class TakmicariIndexVM
    {
        public List<TakmicarPodaci> listaTakmicara;
        public int aktivnost { get; set; }
        public List<SelectListItem> Aktivnost { get; set; }

        public TakmicariIndexVM(List<Takmicari> takmicari, int aktivnost)
        {
            listaTakmicara = takmicari.Select(x => new TakmicarPodaci
            {
                Id=x.Id,
                Takmicar=x.ClanKluba.Osoba.Ime+" ("+x.ClanKluba.Osoba.ImeRoditelja+") "+x.ClanKluba.Osoba.Prezime,
                JMBG=x.ClanKluba.Osoba.JMBG,
                Spol=x.ClanKluba.Osoba.Spol,
                RegistarskiBroj=x.RegistarskiBroj,
                StarosnaDob=x.ClanKluba.StarosnaDob.Naziv,
                StarosnaDobId=x.ClanKluba.StarosnaDobId,
                Obrazlozenje=x.Obrazlozenje,
                isAktivanTakmicar=x.isAktivanTakmicar

            }).ToList();
            Aktivnost = new List<SelectListItem>();
            Aktivnost.Add(new SelectListItem { Text = "Aktivni takmičari", Value = "0" });
            Aktivnost.Add(new SelectListItem { Text = "Neaktivni takmičari", Value = "1" });

            Aktivnost.Add(new SelectListItem { Text = "Svi takmičari", Value = "3" });
            this.aktivnost = aktivnost;
        }

    }
}
 