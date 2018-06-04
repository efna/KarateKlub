using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class SjedniciceUpravnogOdboraPodaci
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DatumOdrzavanja { get; set; }
        public string Svrha { get; set; }
        public string Obrazlozenje { get; set; }
    }
    public class SjedniceUpravnogOdboraIndexVM
    {
        public List<SjedniciceUpravnogOdboraPodaci> sjedniceUO; 
    }
}