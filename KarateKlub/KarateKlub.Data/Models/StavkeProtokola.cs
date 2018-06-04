using KarateKlub.Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateKlub.Data.Models
{
    public class StavkeProtokola:IEntity
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int ProtokolId { get; set; }
        public virtual Protokol Protokol { get; set; }
        public string BrojProtokola { get; set; }
        public string Predmet { get; set; }
        public string PodBroj { get; set; }
        public DateTime DatumPrijema { get; set; }
        public string Posiljaoc { get; set; }
        public string BrojPosiljke { get; set; }
        public DateTime? DatumPosiljke { get; set; }
        public string MjestoPosiljke { get; set; }
        public string OrganizacionaJedinica { get; set; }
        public DateTime? DatumRazvoda { get; set; }
        public string Oznaka { get; set; }

    }
}
