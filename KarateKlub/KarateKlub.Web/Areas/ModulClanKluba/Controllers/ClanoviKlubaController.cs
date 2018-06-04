using KarateKlub.Data;
using KarateKlub.Data.Models;
using KarateKlub.Web.Areas.ModulClanKluba.Models;
using KarateKlub.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulClanKluba.Controllers
{
    [Autorizacija(false, false, true, false, false)]

    public class ClanoviKlubaController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulClanKluba/ClanoviKluba
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UpravljanjeKlubskomKnjizicom(int brojTaba = 1, int aktivnostLicence = 0, int brojTabaUplate = 1, int izmirena = 0)
        {
            int osobaId = Autentifikacija.GetLogiraniKorisnik(HttpContext).OsobaId;
            Osoba osoba = ctx.Osoba.Where(x => x.Id == osobaId).FirstOrDefault();
            ViewData["osobaId"] = osobaId;
            ViewData["tab"] = brojTaba;
            ViewData["osoba"] = osoba;
            ViewData["aktivnostLicence"] = aktivnostLicence;
            ViewData["brojTabaUplate"] = brojTabaUplate;
            ViewData["izmirena"] = izmirena;


            return View("UpravljanjeKlubskomKnjizicom");
        }
        public ActionResult Detalji(int osobaId)
        {
      
            ViewData["osobaId"] = osobaId;

            Osoba osoba = ctx.Osoba.Where(x => x.Id == osobaId).FirstOrDefault();
            ClanoviKluba clan = ctx.ClanoviKluba.Where(x => x.OsobaId == osobaId && x.isDeleted == false).FirstOrDefault();
            ClanoviKlubaDetaljiVM model = new ClanoviKlubaDetaljiVM
            {
                isAktivnaOsoba = osoba.isAktivnaOsoba,
                OsobaId = osobaId,
                Ime = osoba.Ime,
                Prezime = osoba.Prezime,
                ImeRoditelja = osoba.ImeRoditelja,
                DatumRodjenja = osoba.DatumRodjenja,
                MjestoRodjenja = osoba.MjestoRodjenja,
                JMBG = osoba.JMBG,
                Spol = osoba.Spol,
                KontaktTelefon = osoba.KontaktTelefon,
                Email = osoba.Email,
                Slika = osoba.Slika,
                TipSlike = osoba.TipSlike,
                NazivSlike = osoba.NazivSlike,
                DatumUpisa = clan.DatumUpisa,
                ZvanjeUKarateu = clan.ZvanjeUKarateu.Naziv,
                StarosnaDob = clan.StarosnaDob.Naziv
            };
            return View("Detalji", model);
        }
        public ActionResult GetImage(int osobaId)
        {
            Osoba osoba = ctx.Osoba.Where(x => x.Id == osobaId).FirstOrDefault();
            return File(osoba.Slika, osoba.TipSlike);

        }
        public ActionResult GoToUpravljanjeKlubskomKnjizicom(int aktivnost)
        {

            return RedirectToAction("UpravljanjeKlubskomKnjizicom", new {brojTaba = 3, aktivnostLicence = aktivnost });
        }
        public ActionResult GoToUpravljanjeUplatamaClana(int izmirena)
        {

            return RedirectToAction("UpravljanjeKlubskomKnjizicom", new {brojTaba = 4, aktivnostLicence = 0, brojTabaUplate = 1, izmirena = izmirena });
        }
        public ActionResult GoToUpravljanjeUplatamaClana2(int izmirena)
        {

          return RedirectToAction("UpravljanjeKlubskomKnjizicom", new {brojTaba = 4, aktivnostLicence = 0, brojTabaUplate = 2, izmirena = izmirena });
        }
        public ActionResult KorisnickiPodaci(int osobaId)
        {
            KorisnickiNalozi nalog = ctx.KorisnickiNalozi.Where(x => x.OsobaId == osobaId && x.isDeleted == false).FirstOrDefault();
            ClanoviKlubaKorisnickiPodaciVM model = new ClanoviKlubaKorisnickiPodaciVM
            {
                OsobaId = nalog.OsobaId,
                NalogId = nalog.Id,
                KorisnickoIme = nalog.KorisnickoIme,
                TrenutnaLozinka = nalog.Lozinka
            };

            return View("KorisnickiPodaci", model);
        }
        [HttpPost]
        public ActionResult SpremiKorisnickePodatke(ClanoviKlubaKorisnickiPodaciVM model)
        {
            KorisnickiNalozi nalog = ctx.KorisnickiNalozi.Where(x => x.OsobaId == model.OsobaId && x.Id == model.NalogId).FirstOrDefault();
            if (model.NovaLozinka != null)
            {
                nalog.Lozinka = model.NovaLozinka;
                ctx.SaveChanges();
            }
            return RedirectToAction("Index","Home");
        }
    }
}