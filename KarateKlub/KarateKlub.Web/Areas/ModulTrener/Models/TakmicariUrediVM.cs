using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulTrener.Models
{
    public class TakmicariUrediVM
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public string RegistarskiBroj { get; set; }
        public int ClanKlubaId { get; set; }
        public bool isAktivanTakmicar { get; set; }
        public string Obrazlozenje { get; set; }
        public int aktivnost { get; set; }
    }
}