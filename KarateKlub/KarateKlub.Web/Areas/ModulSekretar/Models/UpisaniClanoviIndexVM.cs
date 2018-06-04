using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class UpisaniClanPodaci
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public string ClanKluba { get; set; }
        public string JMBG { get; set; }
        public string Spol { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string MjestoRodjenja { get; set; }
        public string KontaktTelefon { get; set; }
        public string Email { get; set; }
        public DateTime DatumUpisa { get; set; }
        public int ZvanjeUKarateuId { get; set; }
        public string ZvanjeUKarateu { get; set; }
        public int StarosnaDobId { get; set; }
        public string StarosnaDob { get; set; }
        
    }
    public class UpisaniClanoviIndexVM
    {
        public List<UpisaniClanPodaci> upisaniClanovi;
    }
}