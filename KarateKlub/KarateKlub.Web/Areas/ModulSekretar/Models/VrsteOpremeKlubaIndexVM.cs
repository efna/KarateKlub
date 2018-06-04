using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class VrstaOpremePodaci
    {
        public int Id { get; set; }
        public string Naziv { get; set; }

    }
    public class VrsteOpremeKlubaIndexVM
    {
        public List<VrstaOpremePodaci> vrsteOpreme { get; set; }
    }
}