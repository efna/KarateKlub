using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class UpisiDodajVM
    {
        public string Naziv { get; set; }
        public string DatumOd { get; set; }
        public string DatumDo { get; set; }
    }
}