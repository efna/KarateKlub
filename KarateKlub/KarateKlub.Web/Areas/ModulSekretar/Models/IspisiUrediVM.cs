using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class IspisiUrediVM
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int ClanKlubaId { get; set; }
        public List<SelectListItem> ClanoviKluba { get; set; }
        public string DatumIspisa { get; set; }
        public string RazlogIspisa { get; set; }
    }
}