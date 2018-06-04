using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{

    public class StavkeProtokolaPodaci
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public int ProtokolId { get; set; }
        public string BrojProtokola { get; set; }
        public string Predmet { get; set; }
        public string PodBroj { get; set; }
        public string DatumPrijema { get; set; }
        public string Posiljaoc { get; set; }
        public string MjestoPosiljke { get; set; }
        public string BrojPosiljke { get; set; }
        public string DatumPosiljke { get; set; }
        public string OrganizacionaJedinica { get; set; }
        public string DatumRazvoda { get; set; }
        public string Oznaka { get; set; }
    }
    
    public class StavkeProtokolaIndexVM
    {
        public List<StavkeProtokolaPodaci> listaStavkiProtokola;
        public int protokolId { get; set; }
    }
}