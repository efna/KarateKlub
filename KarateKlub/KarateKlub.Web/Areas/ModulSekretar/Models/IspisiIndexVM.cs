using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class IspisPodaci
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int ClanKlubaId { get; set; }
        public string ClanKluba { get; set; }
        public string JMBG { get; set; }
        public DateTime DatumIspisa { get; set; }
        public string RazlogIspisa { get; set; }
    }
    public class IspisiIndexVM
    {
        public List<IspisPodaci> ispisi { get; set; }
    }
}