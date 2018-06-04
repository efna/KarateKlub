using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulBlagajnik.Models
{
    public class SponzorstvaUrediVM
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int KorisnikId { get; set; }
        public int SponzorId { get; set; }
        public string DatumUplate { get; set; }
        public string IznosKMBrojevima { get; set; }
        public string IznosKMSlovima { get; set; }
        public string Obrazlozenje { get; set; }
        public int aktivnost { get; set; }
    }
}