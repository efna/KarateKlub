using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulClanKluba.Models
{
    public class PohvalaTakmicaraPodaci
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int TakmicarId { get; set; }
        public string DodjeljenoOd { get; set; }
        public string DodjeljenoZbog { get; set; }
        public DateTime DatumDodjele { get; set; }
        public string MjestoDodjele { get; set; }
        public string Obrazlozenje { get; set; }
    }
    public class PohvaleTakmicariIndexVM
    {
        public List<PohvalaTakmicaraPodaci> pohvaleTakmicara;
    }
}