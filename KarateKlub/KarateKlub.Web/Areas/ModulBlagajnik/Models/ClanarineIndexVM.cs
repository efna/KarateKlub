using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulBlagajnik.Models
{
    public class ClanarinaPodaci
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public string Naziv { get; set; }
        public DateTime DatumOd { get; set; }
        public DateTime DatumDo { get; set; }
    }
    public class ClanarineIndexVM
    {
        public List<ClanarinaPodaci> clanarine;
    }
}