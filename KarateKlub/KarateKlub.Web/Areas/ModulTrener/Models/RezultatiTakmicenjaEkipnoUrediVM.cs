using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulTrener.Models
{
    public class RezultatiTakmicenjaEkipnoUrediVM
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int TakmicenjeId { get; set; }
        public int DisciplinaTakmicenjaId { get; set; }
        public List<SelectListItem> disciplinaTakmicenja { get; set; }
        public int StarosnaDobId { get; set; }
        public List<SelectListItem> StarosneDobi { get; set; }
        public string Kategorija { get; set; }
        public string BrojEkipaUKategoriji { get; set; }
        public string BrojPobjeda { get; set; }
        public string BrojPoraza { get; set; }
        public int OsvojenoMjestoNaTakmicenjuId { get; set; }
        public List<SelectListItem> osvojenaMjestaNaTakmicenju { get; set; }
        public string Obrazlozenje { get; set; }
        public string NazivEkipe { get; set; }
        public List<int> clanoviEkipeId { get; set; }
        public List<SelectListItem> clanoviEkipe { get; set; }
        public int EkipaId { get; set; }
    }
}