using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class ClanoviKlubaKlubskiPodaciClanaVM
    {
        public bool isAktivnaOsoba { get; set; }
        public int OsobaId { get; set; }
        public string DatumUpisa { get; set; }
        public int starosnaDobId { get; set; }
        public List<SelectListItem> starosneDobi { get; set; }
        public int ZvanjeUKarateuId { get; set; }
        public List<SelectListItem> ZvanjaUKarateu { get; set; }
        public int aktivan { get; set; }    }
}