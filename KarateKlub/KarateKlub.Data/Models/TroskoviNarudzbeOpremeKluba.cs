using KarateKlub.Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateKlub.Data.Models
{
   public class TroskoviNarudzbeOpremeKluba:IEntity
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int NarudzbaOpremeKlubaId { get; set; }
        public virtual NarudzbeOpremeKluba NarudzbaOpremeKluba { get; set; }
        public decimal IznosKMBrojevima { get; set; }
        public string IznosKMSlovima { get; set; }
        public DateTime DatumUplate { get; set; }
        public string Obrazlozenje { get; set; }

    }
}
