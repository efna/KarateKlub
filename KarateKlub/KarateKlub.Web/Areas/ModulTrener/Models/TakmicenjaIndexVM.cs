using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulTrener.Models
{
    public class TakmicenjePodaci
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public string NazivTakmicenja { get; set; }
        public DateTime DatumOdrzavanjaTakmicenja { get; set; }
        public string MjestoOdrzavanjaTakmicenja { get; set; }
        public string OrganizatorTakmicenja { get; set; }
        public int SavezId { get; set; }
        public string Savez { get; set; }
    }
    public class TakmicenjaIndexVM
    {
        public List<TakmicenjePodaci> takmicenja { get; set; }
    }
}