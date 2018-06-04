using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulAdministrator.Models
{
    public class KorisnikPodaci
    {
        public int KorisnikId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string ImeRoditelja { get; set; }
        public string JMBG { get; set; }

    }
    public class KorisniciPrikazVM
    {
        public List<KorisnikPodaci> listaKorisnika { get; set; }

    }
}