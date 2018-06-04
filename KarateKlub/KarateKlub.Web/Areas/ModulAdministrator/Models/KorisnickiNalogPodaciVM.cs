using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulAdministrator.Models
{
    public class KorisnickiNalogPodaciVM
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string ImeRoditelja { get; set; }
        public string JMBG { get; set; }
        public string Spol { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string MjestoRodjenja { get; set; }
        public string KontaktTelefon { get; set; }
        public string Email { get; set; }
        public bool isAdministrator { get; set; }
        public bool isTrener { get; set; }
        public bool isBlagajnik { get; set; }
        public bool isSekretar { get; set; }
        public bool isClanKluba { get; set; }
        public bool isClanUpravnogOdbora { get; set; }
        public bool isKnjigovodja { get; set; }
        public bool isAktivnaOsoba { get; set; }
        public DateTime DatumZaposlenja { get; set; }
        public int ZvanjeUKarateuId { get; set; }
        public List<SelectListItem> ZvanjaUKarateu { get; set; }
        public int FunkcijaTreneraId { get; set; }
        public List<SelectListItem> FunkcijaTrenera { get; set; }
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public string PotvrdaLozinke { get; set; }
        public int KorisnickaUlogaId { get; set; }
        public List<SelectListItem> KorisnickeUloge { get; set; }
        public DateTime DatumUpisa { get; set; }
        public int StarosnaDobId { get; set; }
        public List<SelectListItem> StarosneDobi { get; set; }
      
    }
}