using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class UlogaPodaci
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
    }
    public class UlogeClanovaUpravnogOdboraIndexVM
    {
        public List<UlogaPodaci> uloge { get; set; }
    }
}