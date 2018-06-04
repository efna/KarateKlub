using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulTrener.Models
{
    public class LjekarskiPreglediTakmicaraPodaci
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int TakmicarId { get; set; }
        public DateTime DatumLjekarskogPregleda { get; set; }
        public string Dijagnoza { get; set; }
        public string Doktor { get; set; }
    }
    public class LjekarskiPreglediTakmicaraIndexVM
    {
        public List<LjekarskiPreglediTakmicaraPodaci> ljekarskiPregledi;
        public string Takmicar { get; set; }
        public int takmicarId { get; set; }
    }
}