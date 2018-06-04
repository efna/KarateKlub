using KarateKlub.Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateKlub.Data.Models
{
   public class RezultatiPolaganjaUcenickaZvanja:IEntity
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int PolaganjeUcenickaZvanjaId { get; set; }
        public virtual PolaganjaUcenickaZvanja PolaganjeUcenickaZvanja { get; set; }
        public int UcesnikPolaganjaZaUcenickaZvanjaId { get; set; }
        public virtual UcesniciPolaganjaZaUcenickaZvanja UcesnikPolaganjaZaUcenickaZvanja { get; set; }
        public bool isPolozio { get; set; }



    }
}
