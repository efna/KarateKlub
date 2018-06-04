using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class StecenaLicencaPodaci
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int VrstaLicenciId { get; set; }
        public string VrstaLicence { get; set; }
        public int SeminarId { get; set; }
        public string Seminar { get; set; }
        public int NivoLicenceId { get; set; }
        public string NivoLicence { get; set; }
        public int OsobaId { get; set; }
        public string Osoba { get; set; }
        public string StecenoZvanje { get; set; }
        public DateTime DatumSticanja { get; set; }
        public DateTime DatumVazenja { get; set; }
        public string Obrazlozenje { get; set; }
        public bool isAktivnaLicenca { get; set; }
   
    }
    public class SteceneLicenceIndexVM
    {
        public List<StecenaLicencaPodaci> steceneLicence;
    }
}