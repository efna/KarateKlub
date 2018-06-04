using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class ClanoviUpravnogOdboraPodaciClanaVM
    {
        public int ClanId { get; set; }
        public int OsobaId { get; set; }
        public int FunkcijaClanaId { get; set; }
        public bool isAktivanClan { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string ImeRoditelja { get; set; }
        public string JMBG { get; set; }
        public string Spol { get; set; }
        public string DatumRodjenja { get; set; }
        public string MjestoRodjenja { get; set; }
        public string KontaktTelefon { get; set; }
        public string Email { get; set; }
        public string FunkcijaClana { get; set; }
        public List<SelectListItem> listaFunkcijaClana { get; set; }
        public string DatumIzglasavanja { get; set; }
        public int aktivnost { get; set; }
    }
}