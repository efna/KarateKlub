using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class SjedniceUpravnogOdboraUrediVM
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Svrha{ get; set; }
        public string Obrazlozenje { get; set; }
        public string DatumOdrzavanja { get; set; }
    }
}