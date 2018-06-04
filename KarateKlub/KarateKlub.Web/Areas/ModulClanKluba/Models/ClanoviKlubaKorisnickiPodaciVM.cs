using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulClanKluba.Models
{
    public class ClanoviKlubaKorisnickiPodaciVM
    {
        public int OsobaId { get; set; }
        public int NalogId { get; set; }
        public string TrenutnaLozinka { get; set; }
        public string StaraLozinka { get; set; }
        public string NovaLozinka { get; set; }
        public string PotvrdaLozinke { get; set; }
        public string KorisnickoIme { get; set; }
    }
}