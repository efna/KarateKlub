using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class ObnoveLicenciUrediVM
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int SeminarId { get; set; }
        public int StecenaLicencaId { get; set; }
        public string DatumObnove { get; set; }
        public string DatumVazenja { get; set; }
        public int aktivnost { get; set; }
    }
}