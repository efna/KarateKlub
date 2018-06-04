using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
   

    public class SjedniceUpravnogOdboraDetaljiVM
    {
        //public List<PrisutvaNaSjednicamaPodaci> prisustvaNaSjednici { get; set; }
        public SjedniciceUpravnogOdboraPodaci sjednica { get; set; }
        //public List<OdlukeUpravnogOdboraPodaci> odlukeUpravnogOdbora { get; set; }
        public int sjednicaId { get; set; }
    }
}