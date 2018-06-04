using KarateKlub.Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateKlub.Data.Models
{
    public class ParticipacijeZaPolaganjeUcenickaZvanja:IEntity
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int PolaganjeUcenickaZvanjaId { get; set; }
        public virtual PolaganjaUcenickaZvanja PolaganjeUcenickaZvanja { get; set; }
        public int UcesnikPolaganjaZaUcenickaZvanjaId { get; set; }
        public virtual UcesniciPolaganjaZaUcenickaZvanja UcesnikPolaganjaZaUcenickaZvanja { get; set; }
        public decimal IznosKMBrojevima { get; set; }
        public string IznosKMSlovima { get; set; }
        public DateTime DatumUplate { get; set; }
        public string BrojPriznanice { get; set; }
    }
}
