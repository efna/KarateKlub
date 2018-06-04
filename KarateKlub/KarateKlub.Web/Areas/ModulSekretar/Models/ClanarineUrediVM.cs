using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class ClanarineUrediVM
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public string Naziv { get; set; }
        public string DatumOd { get; set; }
        public string DatumDo { get; set; }
    }
}