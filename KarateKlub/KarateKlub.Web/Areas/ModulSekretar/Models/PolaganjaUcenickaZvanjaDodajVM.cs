using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class PolaganjaUcenickaZvanjaDodajVM
    {
        public string DatumPolaganja { get; set; }
        public string MjestoPolaganja { get; set; }
        public string OrganizatorPolaganja { get; set; }
        public string Obrazlozenje { get; set; }
   
    }
}