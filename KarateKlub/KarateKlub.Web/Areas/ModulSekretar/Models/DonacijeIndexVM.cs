using KarateKlub.Data;
using KarateKlub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class DonacijaPodaci
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
        public string Naziv { get; set; }
    }
    public class DonacijeIndexVM
    {
        MojContext ctx = new MojContext();
        public DonacijaPodaci donacija;
        public List<DonacijaPodaci> listaDonacija { get; set; }

        public DonacijeIndexVM() { }
        public DonacijeIndexVM(List<Donacije> donacije)
        {
            listaDonacija = ctx.Donacije.Where(x => x.isDeleted == false).Select(x => new DonacijaPodaci
            {
                OsobaId = x.OsobaId,
                Korisnik = x.Osoba.Ime + " " + x.Osoba.Prezime,
                DonatorId = x.DonatorId,
                Donator = x.Donator.ImeOsobe + " " + x.Donator.PrezimeOsobe,
                BrojUplatnice = x.BrojUplatnice,
                IznosKMBrojevima = x.IznosKMBrojevima,
                IznosKMSlovima = x.IznosKMSlovima,
                Obrazlozenje = x.Obrazlozenje,
                DatumUplate = x.DatumUplate,
                Naziv = x.Donator.Naziv
            }).ToList();

        }

    }
}