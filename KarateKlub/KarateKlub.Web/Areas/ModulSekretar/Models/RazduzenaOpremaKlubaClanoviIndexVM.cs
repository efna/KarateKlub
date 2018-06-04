using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{

    public class RazduzenaOpremKlubaClanoviPodaci
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int ZaduzenjeOpremeKlubaClanovimaId { get; set; }
        public bool isRazduzeno { get; set; }
        public DateTime DatumRazduzenjaOpreme { get; set; }
    }
    public class RazduzenaOpremaKlubaClanoviIndexVM
    {
        public List<RazduzenaOpremaKlubaClanoviIndexVM> razduzenaOpremaKlubaClanovi;
    }
}