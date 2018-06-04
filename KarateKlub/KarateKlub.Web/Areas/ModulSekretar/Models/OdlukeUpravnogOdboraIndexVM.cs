using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{

    public class OdlukeUpravnogOdboraPodaci
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public string DonesenaOdluka { get; set; }
        public string Obrazlozenje { get; set; }
        public int SjednicaUpravnogOdboraId { get; set; }

    }
    public class OdlukeUpravnogOdboraIndexVM
    {
        public List<OdlukeUpravnogOdboraPodaci> odlukeUpravnogOdbora;
        public int SjednicaId { get; set; }
    }
}