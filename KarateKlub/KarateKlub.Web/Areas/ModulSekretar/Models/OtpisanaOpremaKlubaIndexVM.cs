using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class OtpisanaOpremaKlubaPodaci
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int VrstaOpremeKlubaId { get; set; }
        public int Kolicina { get; set; }
        public int JedinicaMjereId { get; set; }
        public string VrstaOpreme { get; set; }
        public string JedinicaMjere { get; set; }
        public DateTime DatumOtpisa { get; set; }
        public string Obrazlozenje { get; set; }

    }
    public class OtpisanaOpremaKlubaIndexVM
    {
        public List<OtpisanaOpremaKlubaPodaci> otpisanaOpremaKluba;

    }
}