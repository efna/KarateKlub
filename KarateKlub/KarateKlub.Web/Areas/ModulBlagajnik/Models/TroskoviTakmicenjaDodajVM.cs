using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulBlagajnik.Models
{
    public class TroskoviTakmicenjaDodajVM
    {
        public string Naziv { get; set; }
        public int TakmicenjeId { get; set; }
        public string IznosKMBrojevima { get; set; }
        public string IznosKMSlovima { get; set; }
        public string DatumUplate { get; set; }
        public string Obrazlozenje { get; set; }
    }
}