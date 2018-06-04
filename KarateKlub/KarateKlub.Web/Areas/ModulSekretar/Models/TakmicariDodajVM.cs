using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class TakmicariDodajVM
    {
        public string RegistarskiBroj { get; set; }
        public List<SelectListItem> clanoviKluba { get; set; }
        public int ClanKlubaId { get; set; }
        public string Obrazlozenje { get; set; }
        public int aktivnost { get; set; }
    }
}