using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulAdministrator.Models
{
    public class KorisnickiNaloziDodajKorisnickiNalogVM
    {
        public int OsobaId { get; set; }
        public string OsobaImePrezime { get; set; }
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public string PotvrdaLozinke { get; set; }
        public int KorisnickaUlogaId { get; set; }
    }
}