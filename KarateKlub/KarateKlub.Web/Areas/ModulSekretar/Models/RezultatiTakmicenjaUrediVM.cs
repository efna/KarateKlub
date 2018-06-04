using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class RezultatiTakmicenjaUrediVM
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int TakmicenjeId { get; set; }
        public int TakmicarId { get; set; }
        public List<SelectListItem> takmicari { get; set; }
        public int DisciplinaTakmicenjaId { get; set; }
        public List<SelectListItem> disciplinaTakmicenja { get; set; }
        public int StarosnaDobId { get; set; }
        public List<SelectListItem> StarosneDobi { get; set; }
        public string Kategorija { get; set; }
        public string BrojTakmicaraUKategoriji { get; set; }
        public string BrojPobjeda { get; set; }
        public string BrojPoraza { get; set; }
        public int OsvojenoMjestoNaTakmicenjuId { get; set; }
        public List<SelectListItem> osvojenaMjestaNaTakmicenju { get; set; }
        public string Obrazlozenje { get; set; }
    }
}