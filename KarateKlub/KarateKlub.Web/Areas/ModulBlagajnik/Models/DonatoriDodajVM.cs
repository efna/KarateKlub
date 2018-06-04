using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulBlagajnik.Models
{
    public class DonatoriDodajVM
    {
        public int VrstaLicaId { get; set; }
        public List<SelectListItem> vrsteLica { get; set; }
        public string Naziv { get; set; }
        public string ImeOsobe { get; set; }
        public string PrezimeOsobe { get; set; }
        public string KontaktTelefon { get; set; }
        public string Email { get; set; }
        public string BrojPriznanice { get; set; }
        public string IznosKMBrojevima { get; set; }
        public string IznosKMSlovima { get; set; }
        public string DatumUplate { get; set; }
        public string Obrazlozenje { get; set; }
    }
}