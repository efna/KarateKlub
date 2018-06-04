using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{

    public class ProtokolPodaci
    {
        public int Id { get; set; }
        public int OsobaId { get; set; }
        public string OsobaImePrezime { get; set; }
        public bool isDeleted { get; set; }
        public string Naziv { get; set; }
        public int Godina { get; set; }

    }
    public class ProtokoliIndexVM
    {
        public List<ProtokolPodaci> listaProtokola;
   
    }
}