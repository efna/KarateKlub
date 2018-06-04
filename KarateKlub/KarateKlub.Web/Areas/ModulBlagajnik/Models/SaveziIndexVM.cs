using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulBlagajnik.Models
{
    public class SaveziPodaci
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
    }
    public class SaveziIndexVM
    {
        public List<SaveziPodaci> savezi { get; set; }
    }
}