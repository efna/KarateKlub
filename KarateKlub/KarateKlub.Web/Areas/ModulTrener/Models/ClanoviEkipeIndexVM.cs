using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulTrener.Models
{
    public class ClanEkipePodaci
    {
        public int Id { get; set; }
        public int EkipaId { get; set; }
        public int TakmicarId { get; set; }
        public string Takmicar { get; set; }
        public string ZvanjeUKarateu { get; set; }
        public string StarosnaDob { get; set; }

    }
    public class ClanoviEkipeIndexVM
    {
        public List<ClanEkipePodaci> clanoviEkipe { get; set; }
    }
}