using KarateKlub.Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateKlub.Data.Models
{
    public class StavkeNarudzbeOpremeKluba : IEntity
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int NarudzbaOpremeKlubaId { get; set; }
        public virtual NarudzbeOpremeKluba NarudzbaOpremeKluba { get; set; }
        public int VrstaOpremeKlubaId { get; set; }
        public virtual VrsteOpremeKluba VrstaOpremeKluba { get; set; }
        public int KolicinaNabavljeneOpreme { get; set; }
        public int JedinicaMjereId { get; set; }
        public virtual JediniceMjere JedinicaMjere { get; set; }
        public string MarkaNabavljeneOpreme { get; set; }
        public bool IsWkfEkfApproved { get; set; }

    }
}
