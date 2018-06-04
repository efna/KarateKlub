using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class ObnovaLicencePodaci
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int SeminarId { get; set; }
        public string Osoba { get; set; }
        public int StecenaLicencaId { get; set; }
        public string StecenoZvanje { get; set; }
        public string VrstaLicence { get; set; }
        public string NivoLicence { get; set; }
        public DateTime DatumObnove { get; set; }
        public DateTime DatumVazenja { get; set; }
        public DateTime DatumSticanja { get; set; }
    }
    public class ObnoveLicenciIndexVM
    {
        public List<ObnovaLicencePodaci> obnoveLicenci;
    }
}