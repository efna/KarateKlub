using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class ParticipacijeZaPolaganjeUcenickaZvanjaDodajVM
    {
        public int PolaganjeUcenickaZvanjaId { get; set; }
        public int UcesnikPolaganjaZaUcenickaZvanjaId { get; set; }
        public List<SelectListItem> ucesniciPolaganja { get; set; }
        public string IznosKMBrojevima { get; set; }
        public string IznosKMSlovima { get; set; }
        public string DatumUplate { get; set; }
        public string BrojPriznanice { get; set; }
        public string Ucesnik { get; set; }
    }
}