using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulClanKluba.Models
{
    public class ZabranaTakmicaraPodaci
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int TakmicarId { get; set; }
        public string Takmicar { get; set; }
        public string DodjeljenoOd { get; set; }
        public string DodjeljenoZbog { get; set; }
        public DateTime DatumSticanja { get; set; }
        public DateTime DatumIsteka { get; set; }
        public string MjestoSticanja { get; set; }
        public string Obrazlozenje { get; set; }
    }
    public class ZabraneTakmicariIndexVM
    {
        public List<ZabranaTakmicaraPodaci> zabraneTakmicara;
    }
}