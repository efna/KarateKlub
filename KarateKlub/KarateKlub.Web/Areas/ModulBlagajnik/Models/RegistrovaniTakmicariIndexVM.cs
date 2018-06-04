using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulBlagajnik.Models
{
    public class RegistrovaniTakmicarPodaci
    {
        public int Id { get; set; }
        public string Takmicar { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string MjestoRodjenja { get; set; }
        public string JMBG { get; set; }
        public string ZvanjeUKarateu { get; set; }
        public string StarosnaDob { get; set; }

    };


    public class RegistrovaniTakmicariIndexVM
    {
        public List<RegistrovaniTakmicarPodaci> registrovaniTakmicari { get; set; }
        public int registracijaTakmicaraId { get; set; }

    }
}