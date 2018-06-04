using KarateKlub.Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateKlub.Data.Models
{
    public class KlubskaOprema:IEntity
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int VrstaOpremeKlubaId { get; set; }
        public virtual VrsteOpremeKluba VrstaOpremeKluba { get; set; }
        public int Kolicina { get; set; }
        public int JedinicaMjereId { get; set; }
        public virtual JediniceMjere JedinicaMjere { get; set; }
    }
}
