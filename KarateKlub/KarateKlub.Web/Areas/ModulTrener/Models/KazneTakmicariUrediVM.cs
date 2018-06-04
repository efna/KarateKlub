using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulTrener.Models
{
    public class KazneTakmicariUrediVM
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int TakmicarId { get; set; }
        public string DodjeljenoOd { get; set; }
        public string DodjeljenoZbog { get; set; }
        public string DatumSticanja { get; set; }
        public string DatumIsteka { get; set; }
        public string MjestoSticanja { get; set; }
        public string Obrazlozenje { get; set; }
        public int aktivnost { get; set; }
    }
}