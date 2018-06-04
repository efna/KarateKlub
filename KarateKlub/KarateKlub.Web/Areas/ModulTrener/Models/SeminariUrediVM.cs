using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulTrener.Models
{
    public class SeminariUrediVM
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public string NazivSeminara { get; set; }
        public string MjestoOdrzavanjaSeminara { get; set; }
        public string DatumOdrzavanjaSeminaraOd { get; set; }
        public string DatumOdrzavanjaSeminaraDo { get; set; }
        public string OrganizatorSeminara { get; set; }
        public string Obrazlozenje { get; set; }
        public List<int> ucesniciId { get; set; }
        public List<SelectListItem> ucesnici { get; set; }

    }
}