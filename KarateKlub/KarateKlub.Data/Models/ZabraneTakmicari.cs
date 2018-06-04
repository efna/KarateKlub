using KarateKlub.Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateKlub.Data.Models
{
    public class ZabraneTakmicari:IEntity
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int TakmicarId { get; set; }
        public virtual Takmicari Takmicar { get; set; }
        public string DodjeljenoOd { get; set; }
        public string DodjeljenoZbog { get; set; }
        public DateTime DatumSticanja { get; set; }
        public DateTime DatumIsteka { get; set; }
        public string MjestoSticanja { get; set; }
        public string Obrazlozenje { get; set; }
    }
}
