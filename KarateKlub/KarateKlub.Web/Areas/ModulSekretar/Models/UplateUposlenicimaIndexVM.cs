using KarateKlub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class UplataUposlenikaPodaci
    {
        public int Id { get; set; }
        public Osoba osoba { get; set; }
        public string Osoba { get; set; }
        public string JMBG { get; set; }
        public DateTime DatumUplate { get; set; }
        public DateTime DatumOd { get; set; }
        public DateTime DatumDo { get; set; }
        public decimal IznosKMBrojevima { get; set; }
        public string IznosKMSLovima { get; set; }
        public string SvrhaUplate { get; set; }
        public string Obrazlozenje { get; set; }

    }
    public class UplateUposlenicimaIndexVM
    {
        public Osoba uposlenik { get; set; }
        public List<UplataUposlenikaPodaci> uplateUposlenicima;
      
    }
}