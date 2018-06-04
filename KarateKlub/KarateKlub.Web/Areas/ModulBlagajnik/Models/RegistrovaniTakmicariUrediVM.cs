using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulBlagajnik.Models
{
    public class RegistrovaniTakmicariUrediVM
    {
        public int isDeleted { get; set; }
        public List<int> ClanKlubaId { get; set; }
        public List<SelectListItem> takmicariKluba { get; set; }
        public int RegistracijaTakmicaraId { get; set; }
        public int savez { get; set; }
    }
}