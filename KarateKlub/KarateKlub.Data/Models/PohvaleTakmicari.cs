using KarateKlub.Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateKlub.Data.Models
{
   public class PohvaleTakmicari:IEntity
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int TakmicarId { get; set; }
        public virtual Takmicari Takmicar { get; set; }
        public string DodjeljenoOd { get; set; }
        public string DodjeljenoZbog { get; set; }
        public DateTime DatumDodjele { get; set; }
        public string MjestoDodjele { get; set; }
        public string Obrazlozenje { get; set; }
    }
}
