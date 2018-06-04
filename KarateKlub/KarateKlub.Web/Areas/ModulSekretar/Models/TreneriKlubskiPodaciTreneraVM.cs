using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class TreneriKlubskiPodaciTreneraVM
    {
        public bool isAktivnaOsoba { get; set; }
        public int OsobaId { get; set; }
        public string DatumZaposlenja { get; set; }
        public int funkcijaTreneraId { get; set; }
        public List<SelectListItem> FunckijeTrenera { get; set; }
        public int ZvanjeUKarateuId { get; set; }
        public List<SelectListItem> ZvanjaUKarateu { get; set; }
        public int aktivan { get; set; }
    }
}