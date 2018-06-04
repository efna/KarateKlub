using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class OtpisanaOpremaKlubaDodajVM
    {
        public int VrstaOpremeKlubaId { get; set; }
        public List<SelectListItem> vrsteOpremeKluba { get; set; }
        public string OtpisanaKolicina { get; set; }
        public int JedinicaMjereId { get; set; }
        public List<SelectListItem> jediniceMjere { get; set; }
        public string DatumOtpisaOpreme { get; set; }
        public string Obrazlozenje { get; set; }
    }
}