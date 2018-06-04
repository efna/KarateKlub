using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulAdministrator.Models
{

    public class KorisnickiNaloziDetaljiVM
    {
        public int NalogId { get; set; }
        public int OsobaId { get; set; }
        public int KorisnickaUlogaId { get; set; }
        public bool isAktivanNalog { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string ImeRoditelja { get; set; }
        public string JMBG { get; set; }
        public string Spol { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string MjestoRodjenja { get; set; }
        public string KontaktTelefon { get; set; }
        public string Email { get; set; }
        public string NazivSlike { get; set; }
        public string TipSlike { get; set; }
        public byte[] Slika { get; set; }
        public HttpPostedFileBase s { get; set; }
        public string KorisnickaUloga { get; set; }
        public int aktivnost { get; set; }
    }
}