using KarateKlub.Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateKlub.Data.Models
{
    public class ClanoviUpravnogOdbora:IEntity
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int OsobaId { get; set; }
        public virtual Osoba Osoba { get; set; }
        public int UlogeClanovaUpravnogOdboraId{ get; set; }
        public virtual UlogeClanovaUpravnogOdbora UlogeClanovaUpravnogOdbora { get; set; }
        public DateTime DatumIzglasavanja { get; set; }
        public bool Aktivan { get; set; }
    }
}
