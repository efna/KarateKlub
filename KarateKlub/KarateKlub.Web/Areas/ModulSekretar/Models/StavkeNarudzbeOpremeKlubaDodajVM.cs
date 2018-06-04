using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class StavkeNarudzbeOpremeKlubaDodajVM
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int NarudzbaOpremeKlubaId { get; set; }
        public int VrstaOpremeKlubaId { get; set; }
        public string KolicinaNabavljeneOpreme { get; set; }
        public int JedinicaMjereId { get; set; }
        public string MarkaNabavljeneOpreme { get; set; }
        public bool IsWkfEkfApproved { get; set; }
        public List<SelectListItem> vrsteOpremeKluba { get; set; }
        public List<SelectListItem> jediniceMjere { get; set; }
    }
}