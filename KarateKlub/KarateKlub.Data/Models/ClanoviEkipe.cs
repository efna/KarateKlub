using KarateKlub.Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateKlub.Data.Models
{
   public class ClanoviEkipe:IEntity
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int EkipaId { get; set; }
        public virtual Ekipa Ekipa { get; set; }
        public int TakmicarId { get; set; }
        public virtual Takmicari Takmicar { get; set; }

    }
}
