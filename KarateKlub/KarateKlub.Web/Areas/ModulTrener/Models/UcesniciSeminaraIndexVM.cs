using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulTrener.Models
{
    public class UcesnikSeminaraPodaci
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int SeminariId { get; set; }
        public int OsobaId { get; set; }
        public string Osoba { get; set; }
        public string Spol { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string MjestoRodjenja { get; set; }
        public string KontaktTelefon { get; set; }
        public string Email { get; set; }



    }
    public class UcesniciSeminaraIndexVM
    {
        public List<UcesnikSeminaraPodaci> ucesniciSeminara;
    }
}