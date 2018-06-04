using KarateKlub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulAdministrator.Models
{
    public class NaloziNaCekanjuPodaci
    {
        public int OsobaId { get; set; }
        public string OsobaImePrezime { get; set; }
        public Osoba Osoba { get; set; }

    }
    public class KorisnickiNaloziPrikazNalogaNaCekanjuVM
    {
        public List<NaloziNaCekanjuPodaci> naloziNaCekanju;
        
        public KorisnickiNaloziPrikazNalogaNaCekanjuVM(List<Osoba> listaOsoba)
        {
            naloziNaCekanju = listaOsoba.Select(x => new NaloziNaCekanjuPodaci
            {
              OsobaId=x.Id,
              OsobaImePrezime=x.Ime+" ("+x.ImeRoditelja+") "+x.Prezime,
              Osoba=x
            }).ToList();
     
        }

    }



}