using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulBlagajnik.Models
{
    public class UpisnineDodajVM
    {
        public int UpisId { get; set; }
        public List<int> UpisaniClanoviId { get; set; }
        public List<SelectListItem> UpisaniClanovi { get; set; }
        public string BrojPriznanice { get; set; }
        public decimal IznosKMBrojevima { get; set; }
        public string IznosKMSlovima { get; set; }
        public string MjestoUplate { get; set; }
        public string DatumUplate { get; set; }
        public bool isIzmirenaUpisnina { get; set; }
        public int izmirena { get; set; }
    }
}