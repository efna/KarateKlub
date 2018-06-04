using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class IspisiDodajVM
    {
      
        public int ClanKlubaId { get; set; }
        public List<SelectListItem> ClanoviKluba { get; set; }
        public string DatumIspisa { get; set; }
        public string RazlogIspisa { get; set; }
    }
}