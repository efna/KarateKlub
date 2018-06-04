using KarateKlub.Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateKlub.Data.Models
{
   public class ObnoveLicenci:IEntity
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int SeminarId { get; set; }
        public virtual Seminari Seminar { get; set; }
        public int StecenaLicencaId { get; set; }
        public virtual SteceneLicence StecenaLicenca { get; set; }
        public DateTime DatumObnove { get; set; }
        public DateTime DatumVazenja { get; set; }

    }
}
