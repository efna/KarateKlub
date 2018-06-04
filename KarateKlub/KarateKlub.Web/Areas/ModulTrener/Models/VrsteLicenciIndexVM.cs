using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulTrener.Models
{
     public class VrstaLicencePodaci
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
    }
    public class VrsteLicenciIndexVM
    {
        public List<VrstaLicencePodaci> vrsteLicenci { get; set; }
    }
}