using KarateKlub.Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateKlub.Data.Models
{
    public class SteceneLicence:IEntity
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int VrstaLicenciId { get; set; }
        public virtual VrsteLicenci VrstaLicenci { get; set; }
        public int SeminarId { get; set; }
        public virtual Seminari Seminar { get; set; }
        public int NivoLicenceId { get; set; }
        public virtual NivoLicence NivoLicence { get; set; }
        public int OsobaId { get; set; }
        public virtual Osoba Osoba { get; set; }
        public string StecenoZvanje { get; set; }
        public DateTime DatumSticanja { get; set; }
        public DateTime DatumVazenja { get; set; }
        public string Obrazlozenje { get; set; }
        public bool isAktivnaLicenca { get; set; }
    }
}
