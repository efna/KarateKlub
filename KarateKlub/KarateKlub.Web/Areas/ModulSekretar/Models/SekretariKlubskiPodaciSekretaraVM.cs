using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class SekretariKlubskiPodaciSekretaraVM
    {
        public bool isAktivnaOsoba { get; set; }
        public int OsobaId { get; set; }
        public string DatumZaposlenja { get; set; }
        public int aktivan { get; set; }
    }
}