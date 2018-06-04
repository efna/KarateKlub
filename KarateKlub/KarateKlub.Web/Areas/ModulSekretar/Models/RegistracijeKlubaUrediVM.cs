using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class RegistracijeKlubaUrediVM
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public string Naziv { get; set; }
        public string DatumRegistracijeKluba { get; set; }
        public string DatumIstekaRegistracijeKluba { get; set; }
        public int SavezId { get; set; }
        public List<SelectListItem> savezi { get; set; }
        public int OsobaId { get; set; }
        public string DatumUplate { get; set; }
        public string IznosKMBrojevima { get; set; }
        public string IznosKMSlovima { get; set; }
        public string Obrazlozenje { get; set; }
        public int savez { get; set; }
    }
}