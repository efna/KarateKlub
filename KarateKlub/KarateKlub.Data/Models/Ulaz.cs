using KarateKlub.Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateKlub.Data.Models
{
    public class Ulaz:IEntity
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public string Naziv { get; set; }
        public decimal IznosKMSBrojevima { get; set; }
        public string IznosKMSlovima { get; set; }
        public DateTime  Datum { get; set; }
        public string Obrazlozenje { get; set; }
        public int OsobaId { get; set; }
        public virtual Osoba Osoba { get; set; }
    }
}
