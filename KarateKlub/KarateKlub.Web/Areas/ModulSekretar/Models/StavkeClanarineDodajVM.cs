using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class StavkeClanarineDodajVM
    {
        public int ClanarinaId { get; set; }
        public List<int> ClanoviKlubaId { get; set; }
        public List<SelectListItem> clanoviKluba { get; set; }
        public string BrojPriznanice { get; set; }
        public decimal IznosKMBrojevima { get; set; }
        public string IznosKMSlovima { get; set; }
        public string MjestoUplate { get; set; }
        public string DatumUplate { get; set; }
        public bool isIzmirenaClanarina { get; set; }
        public int izmirena { get; set; }
    }
}