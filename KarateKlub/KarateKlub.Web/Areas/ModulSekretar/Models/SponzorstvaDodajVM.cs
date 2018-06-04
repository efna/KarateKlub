using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class SponzorstvaDodajVM
    {
        public int KorisnikId { get; set; }
        public int SponzorId { get; set; }
        public string DatumUplate { get; set; }
        public string IznosKMBrojevima { get; set; }
        public string IznosKMSlovima { get; set; }
        public string Obrazlozenje { get; set; }
        public int aktivnost { get; set; }
    }
}