using KarateKlub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class DonacijePodaci
    {
        public int OsobaId { get; set; }
        public string Korisnik { get; set; }
        public int DonatorId { get; set; }
        public string Donator { get; set; }
        public string BrojUplatnice { get; set; }
        public decimal IznosKMBrojevima { get; set; }
        public string IznosKMSlovima { get; set; }
        public DateTime DatumUplate { get; set; }
        public string Obrazlozenje { get; set; }
        public string NazivDonatora { get; set; }
    }
    public class DonatorPodaci
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int VrstaLicaId { get; set; }
        public string VrstaLica { get; set; }
        public string Naziv { get; set; }
        public string Donator { get; set; }
        public string KontaktTelefon { get; set; }
        public string Email { get; set; }
        public DonacijaPodaci donacija { get; set; }
    }
    public class DonatoriIndexVM
    {
        public List<DonatorPodaci> listaDonatora;
  
    }
}