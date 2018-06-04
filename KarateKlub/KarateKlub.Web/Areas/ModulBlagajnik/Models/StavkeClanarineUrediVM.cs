using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulBlagajnik.Models
{
    public class StavkeClanarineUrediVM
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int ClanarinaId { get; set; }
        public int ClanKlubaId { get; set; }
        public List<SelectListItem> clanoviKluba { get; set; }
        public string BrojPriznanice { get; set; }
        public string IznosKMBrojevima { get; set; }
        public string IznosKMSlovima { get; set; }
        public string MjestoUplate { get; set; }
        public string DatumUplate { get; set; }
        public bool isIzmirenaClanarina { get; set; }
        public int izmirena { get; set; }
    }
}