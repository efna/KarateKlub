using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{

    public class DokumentPodaci
    {
        public int Id { get; set; }
        public int stavkaId { get; set; }
        public string NazivDokumenta { get; set; }
        public string TipDokumenta{ get; set; }
        public byte[] Dokument { get; set; }
        public string PodBroj { get; set; }
    }
    public class DokumentiIndexVM
    {
       public List<DokumentPodaci> listaDokumenata;
        public int protokolId { get; set; }
        public int stavkaId { get; set; }
    }
}