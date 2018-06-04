using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class KlubskaOpremaUrediVM
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int VrstaOpremeKlubaId { get; set; }
        public List<SelectListItem> vrsteOpremeKluba { get; set; }
        public string Kolicina { get; set; }
        public int JedinicaMjereId { get; set; }
        public List<SelectListItem> jediniceMjere { get; set; }
    }
}