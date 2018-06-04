using KarateKlub.Data;
using KarateKlub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class OsobaPodaci
    {
        public int Id { get; set; }
        public string Osoba { get; set; }
        public string JMBG { get; set; }
        public Osoba osoba { get; set; }

    }
    public class OsobaIndexVM
    {
        MojContext ctx = new MojContext();

        public List<OsobaPodaci> osobe;
        public int funkcijaOsobe { get; set; }
        public List<SelectListItem> FunkcijeOsoba { get; set; }
        
        public OsobaIndexVM(List<Osoba> osobe, int funkcijaOsobe)
        {
            this.osobe = osobe.Select(x => new OsobaPodaci
            {
                osoba = x,
          Id=x.Id,
          Osoba=x.Ime+" ("+x.ImeRoditelja+") "+x.Prezime,
          JMBG=x.JMBG
            }).ToList();

            FunkcijeOsoba = new List<SelectListItem>();
            FunkcijeOsoba.Add(new SelectListItem { Text = "Svi", Value = "0" });
            FunkcijeOsoba.Add(new SelectListItem { Text = "Treneri", Value = "1" });
            FunkcijeOsoba.Add(new SelectListItem { Text = "Sekretari", Value = "2" });
            FunkcijeOsoba.Add(new SelectListItem { Text = "Blagajnici", Value = "3" });
            FunkcijeOsoba.Add(new SelectListItem { Text = "Knjigovođe", Value = "4" });
            FunkcijeOsoba.Add(new SelectListItem { Text = "Članovi UO", Value = "5" });

            this.funkcijaOsobe = funkcijaOsobe;
        }
    }
}