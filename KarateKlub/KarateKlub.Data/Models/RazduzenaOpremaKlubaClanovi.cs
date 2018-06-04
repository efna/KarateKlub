using KarateKlub.Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateKlub.Data.Models
{
    public class RazduzenaOpremaKlubaClanovi:IEntity
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int ZaduzenjeOpremeKlubaClanovimaId { get; set; }
        public virtual ZaduzenjeOpremeKlubaClanovima ZaduzenjeOpremeKlubaClanovima { get; set; }
        public bool isRazduzeno { get; set; }
        public DateTime DatumRazduzenjaOpreme { get; set; }
    }
}
