using KarateKlub.Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateKlub.Data.Models
{
  public class ClanoviKluba:IEntity
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public DateTime DatumUpisa { get; set; }
        public int ZvanjeUKarateuId { get; set; }
        public virtual ZvanjaUKarateu ZvanjeUKarateu { get; set; }
        public int StarosnaDobId { get; set; }
        public virtual StarosneDobi StarosnaDob { get; set; }

        public int OsobaId { get; set; }
        public virtual Osoba Osoba { get; set; }
    }
}
