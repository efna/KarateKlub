using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulTrener.Models
{
    public class NagradeTakmicariDodajVM
    {
        public int TakmicarId { get; set; }
        public string DodjeljenoOd { get; set; }
        public string DodjeljenoZbog { get; set; }
        public string DatumDodjele { get; set; }
        public string MjestoDodjele { get; set; }
        public string Obrazlozenje { get; set; }
        public int aktivnost { get; set; }
    }
}