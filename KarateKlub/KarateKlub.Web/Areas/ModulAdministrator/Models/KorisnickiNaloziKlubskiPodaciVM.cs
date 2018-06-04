using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulAdministrator.Models
{
    public class KorisnickiNaloziKlubskiPodaciVM
    {
        public int OsobaId { get; set; }
        public int KorisnickaUlogaId { get; set; }
        public string DatumZaposlenja { get; set; }
        public int funkcijaTreneraId { get; set; }
        public List<SelectListItem> FunckijeTrenera { get; set; }
        public int StarosnaDobId { get; set; }
        public List<SelectListItem> StarosneDobi { get; set; }
        public int ZvanjeUKarateuId { get; set; }
        public List<SelectListItem> ZvanjaUKarateu { get; set; }
        public string DatumUpisa { get; set; }
        public string KorisnickaUloga { get; set; }
        public bool isAktivanNalog { get; set; }
        public int aktivnost { get; set; }
    }
}