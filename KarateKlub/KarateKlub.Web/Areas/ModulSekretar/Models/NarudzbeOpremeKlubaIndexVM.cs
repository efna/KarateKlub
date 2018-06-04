using KarateKlub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
public class TroskoviNarudzbePodaci
    {
        public DateTime DatumUplate { get; set; }
        public decimal IznosKMBrojevi { get; set; }
        public string IznosKMSlova { get; set; }
        public string Obrazlozenje { get; set; }
        public NarudzbeOpremeKlubaPodaci narudzba { get; set; }
    }
    public class NarudzbeOpremeKlubaPodaci
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int OsobaId { get; set; }
        public string NazivNarudzbeOpreme { get; set; }
        public DateTime DatumNabavke { get; set; }
        public string Obrazlozenje { get; set; }
        public string Korisnik { get; set; }
        public TroskoviNarudzbePodaci trosak { get; set; }
    }

    public class NarudzbeOpremeKlubaIndexVM
    {
        public List<NarudzbeOpremeKlubaPodaci> narudzbeOpremeKluba;
        public List<TroskoviNarudzbePodaci> troskoviNarudzbe;


    }
}