using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class RazduzenaOpremaKlubaClanoviDodajVM
    {
      
        public bool isDeleted { get; set; }
        public int ZaduzenjeOpremeKlubaClanovimaId { get; set; }
        public bool isRazduzeno { get; set; }
        public string DatumRazduzenjaOpreme { get; set; }
        public int aktivnost { get; set; }
    }
}