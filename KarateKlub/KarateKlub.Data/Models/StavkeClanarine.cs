using KarateKlub.Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateKlub.Data.Models
{
   public class StavkeClanarine:IEntity
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int ClanarinaId { get; set; }
        public virtual Clanarine Clanarina { get; set; }
        public int ClanKlubaId { get; set; }
        public virtual ClanoviKluba ClanKluba { get; set; }
        public string BrojPriznanice { get; set; }
        public decimal IznosKMBrojevima { get; set; }
        public string IznosKMSlovima { get; set; }
        public string MjestoUplate { get; set; }
        public DateTime? DatumUplate { get; set; }
        public bool isIzmirenaClanarina { get; set; }
    }
}
