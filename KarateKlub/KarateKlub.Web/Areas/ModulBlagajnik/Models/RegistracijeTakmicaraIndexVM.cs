using KarateKlub.Data;
using KarateKlub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulBlagajnik.Models
{
    public class RegistracijaTakmicaraPodaci
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public string Naziv { get; set; }
        public DateTime DatumRegistracijeTakmicara { get; set; }
        public DateTime DatumIstekaRegistracijeTakmicara { get; set; }
        public int SavezId { get; set; }
        public string Savez { get; set; }
        public int OsobaId { get; set; }
        public string Korisnik { get; set; }
        public TroskoviRegistracijeTakmicara trosakRegistracijeTakmicara { get; set; }

    }
    public class RegistracijeTakmicaraIndexVM
    {
        MojContext ctx = new MojContext();
        public List<RegistracijaTakmicaraPodaci> troskoviRegistracije;
        public List<RegistracijaTakmicaraPodaci> registracijeTakmicara;

        public int savez { get; set; }
        public List<SelectListItem> savezi { get; set; }
        public RegistracijeTakmicaraIndexVM(List<TroskoviRegistracijeTakmicara> troskoviRegistracijeTakmicara)
        {
            troskoviRegistracije = troskoviRegistracijeTakmicara.Select(x => new RegistracijaTakmicaraPodaci
            {
                Id = x.Id,
                isDeleted = x.isDeleted,
                Naziv = x.RegistracijaTakmicara.Naziv,
                DatumIstekaRegistracijeTakmicara = x.RegistracijaTakmicara.DatumIstekaRegistracijeTakmicara,
                DatumRegistracijeTakmicara = x.RegistracijaTakmicara.DatumRegistracijeTakmicara,
                SavezId = x.RegistracijaTakmicara.SavezId,
                Savez = x.RegistracijaTakmicara.Savez.Naziv,
                OsobaId = x.RegistracijaTakmicara.OsobaId,
                Korisnik = x.RegistracijaTakmicara.Osoba.Ime + " " + x.RegistracijaTakmicara.Osoba.Prezime,
                trosakRegistracijeTakmicara = x
            }).ToList();
        }


        public RegistracijeTakmicaraIndexVM(List<RegistracijeTakmicara> registracije, int savez)
        {
            registracijeTakmicara = registracije.Select(x => new RegistracijaTakmicaraPodaci
            {
                Id = x.Id,
                isDeleted = x.isDeleted,
                Naziv = x.Naziv,
                DatumIstekaRegistracijeTakmicara = x.DatumIstekaRegistracijeTakmicara,
                DatumRegistracijeTakmicara = x.DatumRegistracijeTakmicara,
                SavezId = x.SavezId,
                Savez = x.Savez.Naziv,
                OsobaId = x.OsobaId,
                Korisnik = x.Osoba.Ime + " " + x.Osoba.Prezime,
                trosakRegistracijeTakmicara = ctx.TroskoviRegistracijeTakmicara.Where(y => y.isDeleted == false && y.RegistracijaTakmicaraId == x.Id).FirstOrDefault()
            }).ToList();
            savezi = new List<SelectListItem>();
            savezi = BindSaveze();
            int vrijednost = 0;
            savezi.Insert(0, new SelectListItem { Value = vrijednost.ToString(), Text = "Sve registracije" });


            this.savez = savez;
        }

        private List<SelectListItem> BindSaveze()
        {
            return ctx.Savezi.Where(x => x.isDeleted == false).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv }).ToList();

        }
    }
}