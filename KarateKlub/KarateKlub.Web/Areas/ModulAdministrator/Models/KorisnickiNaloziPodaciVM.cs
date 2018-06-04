using KarateKlub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulAdministrator.Models
{
    public class KorisnickiNaloziPodaciVM
    {
        public KorisnickiNalozi korisnickiNalog { get; set; }
        public string Osoba { get; set; }
        public string korisnickaUloga { get; set; }
        public bool isAktivanNalog { get; set; }
    }

}