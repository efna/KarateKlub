using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class ZaduzenjeOpremeKlubaClanovimaDodajVM
    {
    
        public int ClanKlubaId { get; set; }
        public List<SelectListItem> clanoviKluba { get; set; }
        public int VrstaOpremeKlubaId { get; set; }
        public List<SelectListItem> vrsteOpremeKluba { get; set; }
        public int JedinicaMjereId { get; set; }
        public List<SelectListItem> jediniceMjere { get; set; }
        public string DatumZaduzenjaOpreme { get; set; }
        public string Obrazlozenje { get; set; }
        public int aktivnost { get; set; }
    }
}