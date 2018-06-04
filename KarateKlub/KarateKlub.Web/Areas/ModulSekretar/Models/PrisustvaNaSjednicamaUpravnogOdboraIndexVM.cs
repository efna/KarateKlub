using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{



    public class PrisutvaNaSjednicamaPodaci
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int ClanUpravnogOdboraId { get; set; }
        public bool Prisutan { get; set; }
        public int SjednicaUpravnogOdboraId { get; set; }
        public string ImePrezime { get; set; }
        public string Funkcija { get; set; }
    }

    public class PrisustvaNaSjednicamaUpravnogOdboraIndexVM
    {
        public List<PrisutvaNaSjednicamaPodaci> prisustvaNaSjednici { get; set; }
        public int sjednicaId { get; set; }

    }

}