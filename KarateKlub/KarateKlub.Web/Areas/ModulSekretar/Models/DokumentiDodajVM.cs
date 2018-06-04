using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class DokumentiDodajVM
    {
        public int stavkaId { get; set; }
        public string NazivDokumenta { get; set; }
        public string TipDokumenta { get; set; }
        public byte[] Dokument { get; set; }
        public HttpPostedFileBase d { get; set; }
    }
}