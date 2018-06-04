using KarateKlub.Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateKlub.Data.Models
{
  public class UcesniciPolaganjaZaUcenickaZvanja:IEntity
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int PolaganjeUcenickaZvanjaId { get; set; }
        public virtual PolaganjaUcenickaZvanja PolaganjeUcenickaZvanja { get; set; }
        public int ClanKlubaId { get; set; }
        public virtual ClanoviKluba ClanKluba { get; set; }
        public int ZvanjeUKarateuId { get; set; }
        public virtual ZvanjaUKarateu ZvanjeUKarateu{ get; set; }


    }
}
