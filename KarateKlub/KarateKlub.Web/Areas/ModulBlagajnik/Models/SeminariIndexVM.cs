using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulBlagajnik.Models
{
    public class SeminarPodaci
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public string NazivSeminara { get; set; }
        public string MjestoOdrzavanjaSeminara { get; set; }
        public DateTime DatumOdrzavanjaSeminaraOd { get; set; }
        public DateTime DatumOdrzavanjaSeminaraDo { get; set; }
        public string OrganizatorSeminara { get; set; }
        public string Obrazlozenje { get; set; }
    }
    public class SeminariIndexVM
    {
        public List<SeminarPodaci> seminari;
    }
}