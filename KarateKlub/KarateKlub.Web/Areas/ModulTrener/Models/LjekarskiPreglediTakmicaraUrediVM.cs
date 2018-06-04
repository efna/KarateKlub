using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulTrener.Models
{
    public class LjekarskiPreglediTakmicaraUrediVM
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int TakmicarId { get; set; }
        public string DatumLjekarskogPregleda { get; set; }
        public string Dijagnoza { get; set; }
        public string ImeDoktora { get; set; }
        public string PrezimeDoktora { get; set; }
        public string Titula { get; set; }
        public int aktivnost { get; set; }
    }
}