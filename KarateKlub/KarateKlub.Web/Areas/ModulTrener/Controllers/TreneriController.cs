using KarateKlub.Data;
using KarateKlub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KarateKlub.Web.Areas.ModulTrener.Models;
using KarateKlub.Web.Helper;

namespace KarateKlub.Web.Areas.ModulTrener.Controllers
{
    [Autorizacija(false, false, false, true, false)]

    public class TreneriController : Controller
    {
        MojContext ctx = new MojContext();

        // GET: ModulTrener/Treneri
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UpravljanjeDetaljimaTrenera(int osobaId,int brojTaba = 1, int aktivnostLicence = 0)
        {
            Osoba osoba = ctx.Osoba.Where(x => x.Id == osobaId).FirstOrDefault();
            ViewData["osobaId"] = osobaId;
            ViewData["tab"] = brojTaba;
            ViewData["osoba"] = osoba;
            ViewData["aktivnostLicence"] = aktivnostLicence;

            return View("UpravljanjeDetaljimaTrenera");
        }
        public ActionResult GoToUpravljanjeDetaljimaTrenera(int osobaId, int aktivnost)
        {

            return RedirectToAction("UpravljanjeDetaljimaTrenera", new { osobaId = osobaId,brojTaba = 3, aktivnostLicence = aktivnost });
        }
        public ActionResult Detalji(int osobaId)
        {
           
            ViewData["osobaId"] = osobaId;
            Treneri trener = ctx.Treneri.Where(x => x.OsobaId == osobaId && x.isDeleted == false).FirstOrDefault();
            Osoba osoba = ctx.Osoba.Where(x => x.Id == osobaId).FirstOrDefault();
            TreneriDetaljiVM model = new TreneriDetaljiVM
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
                DatumZaposlenja = trener.DatumZaposlenja,
                ZvanjeUKarateu = trener.ZvanjeUKarateu.Naziv,
                FunkcijaTrenera = trener.FunkcijaTrenera.Naziv
            };
            return View("Detalji", model);
        }
        public ActionResult GetImage(int osobaId)
        {
            Osoba osoba = ctx.Osoba.Where(x => x.Id == osobaId).FirstOrDefault();
            return File(osoba.Slika, osoba.TipSlike);

        }
        public ActionResult KorisnickiPodaci(int osobaId)
        {
            KorisnickiNalozi nalog = ctx.KorisnickiNalozi.Where(x => x.OsobaId == osobaId && x.isDeleted == false).FirstOrDefault();
            TreneriKorisnickiPodaciVM model = new TreneriKorisnickiPodaciVM
            {
                OsobaId = nalog.OsobaId,
                NalogId = nalog.Id,
                KorisnickoIme = nalog.KorisnickoIme,
                TrenutnaLozinka = nalog.Lozinka
            };

            return View("KorisnickiPodaci", model);
        }
        [HttpPost]
        public ActionResult SpremiKorisnickePodatke(TreneriKorisnickiPodaciVM model)
        {
            KorisnickiNalozi nalog = ctx.KorisnickiNalozi.Where(x => x.OsobaId == model.OsobaId && x.Id == model.NalogId).FirstOrDefault();
            if (model.NovaLozinka != null)
            {
                nalog.Lozinka = model.NovaLozinka;
                ctx.SaveChanges();
            }
            return RedirectToAction("UpravljanjeDetaljimaTrenera", new { osobaId = model.OsobaId,aktivan=0,brojTaba = 1 });
        }
    }
}