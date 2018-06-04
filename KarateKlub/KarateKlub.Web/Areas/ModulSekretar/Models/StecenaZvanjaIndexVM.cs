using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class StecenoZvanjePodaci
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int OsobaId { get; set; }
        public string Osoba { get; set; }
        public int ZvanjeUKarateuId { get; set; }
        public string ZvanjeUKarateu { get; set; }
        public DateTime DatumSticanja { get; set; }
        public string Mjesto { get; set; }
        public string Organizator { get; set; }
    }
    public class StecenaZvanjaIndexVM
    {
        public List<StecenoZvanjePodaci> stecenaZvanja;
    }
}