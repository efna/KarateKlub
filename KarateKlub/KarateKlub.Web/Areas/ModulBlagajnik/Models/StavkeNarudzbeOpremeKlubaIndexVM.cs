using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulBlagajnik.Models
{
    public class StavkeNarudzbeOpremeKlubaPodaci
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int NarudzbaOpremeKlubaId { get; set; }
        public int VrstaOpremeKlubaId { get; set; }
        public int KolicinaNabavljeneOpreme { get; set; }
        public int JedinicaMjereId { get; set; }
        public string MarkaNabavljeneOpreme { get; set; }
        public bool IsWkfEkfApproved { get; set; }
        public string VrstaOpreme { get; set; }
        public string JedinicaMjere { get; set; }
    }
    public class StavkeNarudzbeOpremeKlubaIndexVM
    {
        public List<StavkeNarudzbeOpremeKlubaPodaci> stavkeNarudzbeOpremeKluba;
        public int narudzbaId { get; set; }
    }
}