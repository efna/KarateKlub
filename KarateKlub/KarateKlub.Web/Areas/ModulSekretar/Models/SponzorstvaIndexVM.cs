using KarateKlub.Data;
using KarateKlub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class SponzorstvoPodaci
    { 
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int KorisnikId { get; set; }
        public string Korisnik { get; set; }
        public int SponzorId { get; set; }
        public string Sponzor { get; set; }
        public DateTime DatumUplate { get; set; }
        public decimal IznosKMBrojevima { get; set; }
        public string IznosKMSlovima { get; set; }
        public string Obrazlozenje { get; set; }
        
    }

    public class SponzorstvaIndexVM
    {
        MojContext ctx = new MojContext();
        public List<SponzorstvoPodaci> sponzorstva;
        public int sponzorId;
        public SponzorstvaIndexVM(int sponzorId)
        {
            sponzorstva = ctx.Sponzorstva.Where(x => x.SponzorId == sponzorId && x.isDeleted == false).Select(x => new SponzorstvoPodaci
            {
                Id = x.Id,
                isDeleted = x.isDeleted,
                SponzorId = x.SponzorId,
                DatumUplate = x.DatumUplate,
                IznosKMBrojevima = x.IznosKMBrojevima,
                IznosKMSlovima = x.IznosKMSlovima,
                Obrazlozenje = x.Obrazlozenje,
                KorisnikId = x.OsobaId,
                Korisnik = x.Osoba.Ime + " " + x.Osoba.Prezime,
                Sponzor=x.Sponzor.Naziv
            }).ToList();
            this.sponzorId = sponzorId;
         
    }
        public SponzorstvaIndexVM(List<Sponzorstva> listaSponzorstva)
        {

            sponzorstva = listaSponzorstva.Select(x => new SponzorstvoPodaci
            {
                Id = x.Id,
                isDeleted = x.isDeleted,
                SponzorId = x.SponzorId,
                DatumUplate = x.DatumUplate,
                IznosKMBrojevima = x.IznosKMBrojevima,
                IznosKMSlovima = x.IznosKMSlovima,
                Obrazlozenje = x.Obrazlozenje,
                KorisnikId = x.OsobaId,
                Korisnik = x.Osoba.Ime + " " + x.Osoba.Prezime,
                Sponzor=x.Sponzor.Naziv
            }).ToList();

        }
    }
}