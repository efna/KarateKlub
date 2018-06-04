using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class SponzoriUrediVM
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int VrstaLicaId { get; set; }
        public List<SelectListItem> vrsteLica { get; set; }
        public string Naziv { get; set; }
        public string ImeKontaktOsobe { get; set; }
        public string PrezimeKontaktOsobe { get; set; }
        public string KontaktTelefon { get; set; }
        public string Email { get; set; }
        public bool isAktivan { get; set; }
        public int aktivnost { get; set; }
    }
}