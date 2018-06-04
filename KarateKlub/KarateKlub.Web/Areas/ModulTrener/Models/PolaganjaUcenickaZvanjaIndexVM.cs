using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulTrener.Models
{
    public class PolaganjeUcenickaZvanjaPodaci
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public DateTime DatumPolaganja { get; set; }
        public string MjestoPolaganja { get; set; }
        public string OrganizatorPolaganja { get; set; }
        public string Obrazlozenje { get; set; }
    }

    public class PolaganjaUcenickaZvanjaIndexVM
    {
        public List<PolaganjeUcenickaZvanjaPodaci> polaganjaUcenickaZvanja;
    }
}