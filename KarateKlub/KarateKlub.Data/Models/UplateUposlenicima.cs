using KarateKlub.Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateKlub.Data.Models
{
   public class UplateUposlenicima:IEntity
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int OsobaId { get; set; }
        public virtual Osoba Osoba { get; set; }
        public DateTime DatumUplate { get; set; }
        public DateTime DatumOd { get; set; }
        public DateTime DatumDo { get; set; }
        public decimal IznosKMBrojevima { get; set; }
        public string IznosKMSLovima { get; set; }
        public string SvrhaUplate{ get; set; }
        public string Obrazlozenje { get; set; }
    }
}
