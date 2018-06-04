using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class KlubskaOpremaPodaci
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int VrstaOpremeKlubaId { get; set; }
        public int Kolicina { get; set; }
        public int JedinicaMjereId { get; set; }
        public string VrstaOpreme { get; set; }
        public string JedinicaMjere { get; set; }
    }
    public class KlubskaOpremaIndexVM
    {
        public List<KlubskaOpremaPodaci> klubskaOprema;
    }
}