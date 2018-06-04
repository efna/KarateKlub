using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulBlagajnik.Models
{
    public class UpisiUrediVM
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public string Naziv { get; set; }
        public string DatumOd { get; set; }
        public string DatumDo { get; set; }
        public List<int> clanoviKlubaId { get; set; }
        public List<SelectListItem> clanoviKluba { get; set; }
    }
}