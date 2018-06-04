using KarateKlub.Data;
using KarateKlub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{

    public class RegistracijaKlubaPodaci
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public string Naziv { get; set; }
        public DateTime DatumRegistracijeKluba { get; set; }
        public DateTime DatumIstekaRegistracijeKluba { get; set; }
        public int SavezId { get; set; }
        public string Savez { get; set; }
        public int OsobaId { get; set; }
        public string Korisnik { get; set; }
        public TroskoviRegracijeKluba trosakRegistracije { get; set; }

    }
    public class RegistracijeKlubaIndexVM
    {
        MojContext ctx = new MojContext();
        public List<RegistracijaKlubaPodaci> registracijeKluba;
        public List<RegistracijaKlubaPodaci> troskoviRegistracije;

        public int savez { get; set; }
        public List<SelectListItem> savezi { get; set; }
        public RegistracijeKlubaIndexVM(List<TroskoviRegracijeKluba> troskoviRegistracijeKluba)
        {
            troskoviRegistracije = troskoviRegistracijeKluba.Select(x => new RegistracijaKlubaPodaci
            {
                Id = x.Id,
                isDeleted = x.isDeleted,
                Naziv = x.RegistracijaKluba.Naziv,
                DatumIstekaRegistracijeKluba = x.RegistracijaKluba.DatumIstekaRegistracijeKluba,
                DatumRegistracijeKluba = x.RegistracijaKluba.DatumRegistracijeKluba,
                SavezId = x.RegistracijaKluba.SavezId,
                Savez = x.RegistracijaKluba.Savez.Naziv,
                OsobaId = x.RegistracijaKluba.OsobaId,
                Korisnik = x.RegistracijaKluba.Osoba.Ime + " " + x.RegistracijaKluba.Osoba.Prezime,
                trosakRegistracije = x
            }).ToList();

        }
        public RegistracijeKlubaIndexVM(List<RegistracijeKluba> registracije, int savez)
        {
            registracijeKluba = registracije.Select(x => new RegistracijaKlubaPodaci
            {
                Id = x.Id,
                isDeleted = x.isDeleted,
                Naziv = x.Naziv,
                DatumIstekaRegistracijeKluba = x.DatumIstekaRegistracijeKluba,
                DatumRegistracijeKluba = x.DatumRegistracijeKluba,
                SavezId = x.SavezId,
                Savez = x.Savez.Naziv,
                OsobaId = x.OsobaId,
                Korisnik = x.Osoba.Ime + " " + x.Osoba.Prezime,
                trosakRegistracije = ctx.TroskoviRegracijeKluba.Where(y => y.isDeleted == false && y.RegistracijaKlubaId == x.Id).FirstOrDefault()
            }).ToList();
            savezi = new List<SelectListItem>();
            savezi = BindSaveze();
            int vrijednost = 0;
            savezi.Insert(0, new SelectListItem { Value =vrijednost.ToString() , Text = "Sve registracije" });


            this.savez = savez;
        }

        private List<SelectListItem> BindSaveze()
        {
            return ctx.Savezi.Where(x => x.isDeleted == false).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv }).ToList();

        }
    }
}