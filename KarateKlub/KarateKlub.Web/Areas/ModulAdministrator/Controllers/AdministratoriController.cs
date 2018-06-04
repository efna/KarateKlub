using KarateKlub.Data;
using KarateKlub.Data.Models;
using KarateKlub.Web.Areas.ModulAdministrator.Models;
using KarateKlub.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulAdministrator.Controllers
{
    [Autorizacija(true, false, false, false,false)]

    public class AdministratoriController : Controller
    {
        MojContext ctx = new MojContext();

        // GET: ModulAdministrator/Administratori
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetImage(int osobaId)
        {
            Osoba osoba = ctx.Osoba.Where(x => x.Id == osobaId).FirstOrDefault();
            return File(osoba.Slika, osoba.TipSlike);

        }

        public ActionResult UpravljanjeDetaljimaAdministratora(int osobaId, int brojTaba = 1)
        {
            Osoba osoba = ctx.Osoba.Where(x => x.Id == osobaId).FirstOrDefault();
            ViewData["osobaId"] = osobaId;
            ViewData["tab"] = brojTaba;
            ViewData["osoba"] = osoba;

            return View("UpravljanjeDetaljimaAdministratora");
        }
        public ActionResult Detalji(int osobaId)
        {
            ViewData["osobaId"] = osobaId;

            Osoba osoba = ctx.Osoba.Where(x => x.Id == osobaId).FirstOrDefault();
            Administratori administrator = ctx.Administratori.Where(x => osobaId == osobaId && x.isDeleted == false).FirstOrDefault();
            AdministratoriDetaljiVM model = new AdministratoriDetaljiVM
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
                NazivSlike = osoba.NazivSlike
            };
            return View("Detalji", model);
        }
        public ActionResult KorisnickiPodaci(int osobaId)
        {
            KorisnickiNalozi nalog = ctx.KorisnickiNalozi.Where(x => x.OsobaId == osobaId && x.isDeleted == false).FirstOrDefault();
            AdministratoriKorisnickiPodaciVM model = new AdministratoriKorisnickiPodaciVM
            {
                OsobaId = nalog.OsobaId,
                NalogId = nalog.Id,
                KorisnickoIme = nalog.KorisnickoIme,
                TrenutnaLozinka = nalog.Lozinka
            };

            return View("KorisnickiPodaci", model);
        }
        [HttpPost]
        public ActionResult SpremiKorisnickePodatke(AdministratoriKorisnickiPodaciVM model)
        {
            KorisnickiNalozi nalog = ctx.KorisnickiNalozi.Where(x => x.OsobaId == model.OsobaId && x.Id == model.NalogId).FirstOrDefault();
            if (model.NovaLozinka != null)
            {
                nalog.Lozinka = model.NovaLozinka;
                ctx.SaveChanges();
            }
            return RedirectToAction("Detalji", new { osobaId = model.OsobaId});
        }
        public ActionResult Dodaj(int aktivan)
        {
            AdministratoriDodajVM model = new AdministratoriDodajVM
            {
                aktivnost = aktivan

            };
            return View("Dodaj", model);
        }
        public DateTime KonvertujUDatum_dd_mm_yyyy(string datum)
        {
            int dan = Convert.ToInt32(datum.Substring(0, 2));
            int mjesec = Convert.ToInt32(datum.Substring(3, 2));
            int godina = Convert.ToInt32(datum.Substring(6, 4));
            DateTime uneseniDatum = new DateTime(godina, mjesec, dan);
            return uneseniDatum;
        }
        public ActionResult SpremiNovogAdministratora(AdministratoriDodajVM model)
        {
            Osoba osoba = new Osoba();
            osoba.isDeleted = false;
            osoba.isAktivnaOsoba = true;
            osoba.Ime = model.Ime;
            osoba.Prezime = model.Prezime;
            osoba.ImeRoditelja = model.ImeRoditelja;
            osoba.JMBG = model.JMBG;
            osoba.DatumRodjenja = KonvertujUDatum_dd_mm_yyyy(model.DatumRodjenja);
            osoba.MjestoRodjenja = model.MjestoRodjenja;
            osoba.Spol = model.Spol;
            osoba.KontaktTelefon = model.KontaktTelefon;
            osoba.Email = model.Email;
            osoba.isAdministrator = true;
            osoba.isBlagajnik = false;
            osoba.isSekretar = false;
            osoba.isClanKluba = false;
            osoba.isTrener = false;
            osoba.isClanUpravnogOdbora = false;
            osoba.isKnjigovodja = false;
            if (model.s == null)
            {
                osoba.NazivSlike = null;
                osoba.TipSlike = null;
                osoba.Slika = null;


            }
            else
            {
                osoba.NazivSlike = model.s.FileName;
                osoba.TipSlike = model.s.ContentType;

                byte[] slika = new byte[model.s.ContentLength];
                model.s.InputStream.Read(slika, 0, model.s.ContentLength);
                osoba.Slika = slika;
            }

            ctx.Osoba.Add(osoba);
            ctx.SaveChanges();
            int OsobaId = ctx.Osoba.ToList().OrderByDescending(x => x.Id).FirstOrDefault().Id;
            Administratori administrator = new Administratori();
            administrator.OsobaId = OsobaId;
            administrator.isDeleted = false;
            ctx.Administratori.Add(administrator);
            ctx.SaveChanges();
            KorisnickiNalozi nalog = new KorisnickiNalozi();
            int ulogaId = ctx.KorisnickeUloge.Where(x => x.Naziv.Contains("min")).FirstOrDefault().Id;
            nalog.isDeleted = false;
            nalog.isAktivanNalog = true;
            nalog.OsobaId = OsobaId;
            nalog.KorisnickaUlogaId = ulogaId;
            nalog.KorisnickoIme = model.KorisnickoIme;
            nalog.Lozinka = model.Lozinka;
            ctx.KorisnickiNalozi.Add(nalog);
            ctx.SaveChanges();
            return RedirectToAction("Index","KorisnickiNalozi", new { aktivan = model.aktivnost, uloga = 0, brojTaba = 1 });


        }
    }
}