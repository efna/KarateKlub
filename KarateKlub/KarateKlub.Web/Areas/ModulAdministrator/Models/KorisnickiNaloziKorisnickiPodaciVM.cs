using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulAdministrator.Models
{
    public class KorisnickiNaloziKorisnickiPodaciVM
    {
        public int OsobaId { get; set; }
        public int NalogId { get; set; }
        public string Lozinka { get; set; }
        public string StaraLozinka { get; set; }
        public string NovaLozinka { get; set; }
        public string PotvrdaLozinke { get; set; }
        public string KorisnickoIme { get; set; }
        public bool isAktivanNalog { get; set; }
        public int KorisnickaUlogaId { get; set; }
        public List<SelectListItem> KorisnickeUloge { get; set; }
        public int aktivnost { get; set; }
    }
}