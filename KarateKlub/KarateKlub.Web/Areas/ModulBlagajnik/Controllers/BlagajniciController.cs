using KarateKlub.Data;
using KarateKlub.Data.Models;
using KarateKlub.Web.Areas.ModulBlagajnik.Models;
using KarateKlub.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulBlagajnik.Controllers
{
    [Autorizacija(false, false, false, false, true)]

    public class BlagajniciController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulBlagajnik/Blagajnici
        public ActionResult Index()
        {
            return View();
        }
       
        public ActionResult GetImage(int osobaId)
        {
            Osoba osoba = ctx.Osoba.Where(x => x.Id == osobaId).FirstOrDefault();
            return File(osoba.Slika, osoba.TipSlike);

        }

        public ActionResult UpravljanjeDetaljimaBlagajnika(int osobaId, int brojTaba = 1)
        {
            Osoba osoba = ctx.Osoba.Where(x => x.Id == osobaId).FirstOrDefault();
            ViewData["osobaId"] = osobaId;
            ViewData["tab"] = brojTaba;
            ViewData["osoba"] = osoba;

            return View("UpravljanjeDetaljimaBlagajnika");
        }
        public ActionResult Detalji(int osobaId)
        {
            ViewData["osobaId"] = osobaId;

            Osoba osoba = ctx.Osoba.Where(x => x.Id == osobaId).FirstOrDefault();
            Blagajnici blagajnik = ctx.Blagajnici.Where(x => osobaId == osobaId && x.isDeleted == false).FirstOrDefault();
            BlagajniciDetaljiVM model = new BlagajniciDetaljiVM
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
                DatumZaposlenja = blagajnik.DatumZaposlenja
            };
            return View("Detalji", model);
        }
 
        public ActionResult KorisnickiPodaci(int osobaId)
        {
            KorisnickiNalozi nalog = ctx.KorisnickiNalozi.Where(x => x.OsobaId == osobaId && x.isDeleted == false).FirstOrDefault();
            BlagajniciKorisnickiPodaciVM model = new BlagajniciKorisnickiPodaciVM
            {
                OsobaId = nalog.OsobaId,
                NalogId = nalog.Id,
                KorisnickoIme = nalog.KorisnickoIme,
                TrenutnaLozinka = nalog.Lozinka
            };

            return View("KorisnickiPodaci", model);
        }
        [HttpPost]
        public ActionResult SpremiKorisnickePodatke(BlagajniciKorisnickiPodaciVM model)
        {
            KorisnickiNalozi nalog = ctx.KorisnickiNalozi.Where(x => x.OsobaId == model.OsobaId && x.Id == model.NalogId).FirstOrDefault();
            if (model.NovaLozinka != null)
            {
                nalog.Lozinka = model.NovaLozinka;
                ctx.SaveChanges();
            }
            return RedirectToAction("UpravljanjeDetaljimaBlagajnika", new { osobaId = model.OsobaId, brojTaba = 1 });
        }
    }
}