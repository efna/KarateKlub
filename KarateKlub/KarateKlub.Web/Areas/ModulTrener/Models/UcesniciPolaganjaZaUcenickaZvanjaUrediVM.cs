using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulTrener.Models
{
    public class UcesniciPolaganjaZaUcenickaZvanjaUrediVM
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int PolaganjeUcenickaZvanjaId { get; set; }
        public int ClanKlubaId { get; set; }
        public List<SelectListItem> clanoviKluba { get; set; }
        public int ZvanjeUKarateuId { get; set; }
        public List<SelectListItem> zvanjaUKarateu { get; set; }
    }
}