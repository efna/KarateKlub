using KarateKlub.Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateKlub.Data.Models
{
   public class ZaduzenjeOpremeKlubaClanovima:IEntity
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int ClanKlubaId { get; set; }
        public  virtual ClanoviKluba ClanKluba { get; set; }
        public int VrstaOpremeKlubaId { get; set; }
        public virtual VrsteOpremeKluba VrstaOpremeKluba { get; set; }
        public int JedinicaMjereId { get; set; }
        public virtual JediniceMjere JedinicaMjere { get; set; }
        public DateTime DatumZaduzenjaOpreme { get; set; }
        public string Obrazlozenje { get; set; }
        public bool isAktivnoZaduzenje { get; set; }
    }
}
