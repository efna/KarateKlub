using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class StecenaZvanjaUrediVM
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int OsobaId { get; set; }
        public List<SelectListItem> Osobe{ get; set; }
        public int ZvanjeUKarateuId { get; set; }
        public List<SelectListItem> ZvanjaUKarateu { get; set; }
        public string DatumSticanja { get; set; }
        public string Mjesto { get; set; }
        public string Organizator { get; set; }
    }
}