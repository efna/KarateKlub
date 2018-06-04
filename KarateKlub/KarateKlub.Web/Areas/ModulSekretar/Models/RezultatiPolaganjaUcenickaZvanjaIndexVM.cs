using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class RezultatPolaganjaZaUcenickaZvanjaPodaci
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int PolaganjeUcenickaZvanjaId { get; set; }
        public int UcesnikPolaganjaZaUcenickaZvanjaId { get; set; }
        public string Ucesnik { get; set; }
        public string Zvanje { get; set; }
        public bool isPolozio { get; set; }

    }
    public class RezultatiPolaganjaUcenickaZvanjaIndexVM
    {
        public List<RezultatPolaganjaZaUcenickaZvanjaPodaci> rezultatiPolaganja { get; set; }
        public int polaganjeId { get; set; }
    }
}