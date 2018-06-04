using KarateKlub.Data;
using KarateKlub.Data.Models;
using KarateKlub.Web.Areas.ModulSekretar.Models;
using KarateKlub.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulSekretar.Controllers
{
    [Autorizacija(false, true, false, false, false)]

    public class BlagajniciController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulSekretar/Blagajnici
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Dodaj(int aktivan)
        {
            BlagajniciDodajVM model = new BlagajniciDodajVM
            {
                aktivan = aktivan

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

        public ActionResult SpremiNovogBlagajnika(BlagajniciDodajVM model)
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
            osoba.isAdministrator = false;
            osoba.isBlagajnik = true;
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
            Blagajnici blagajnik = new Blagajnici();
            blagajnik.OsobaId = OsobaId;
            blagajnik.isDeleted = false;
            blagajnik.DatumZaposlenja = KonvertujUDatum_dd_mm_yyyy(model.DatumZaposlenja);
            ctx.Blagajnici.Add(blagajnik);
            ctx.SaveChanges();
            return RedirectToAction("GoToPrikazBlagajnika", "UpravljanjeUposlenicima", new { aktivan = model.aktivan });
        }

        public JsonResult Obrisi(int osobaId)
        {
            Osoba osoba = ctx.Osoba.Where(x => x.Id == osobaId).FirstOrDefault();
            Blagajnici blagajnik = ctx.Blagajnici.Where(x => x.OsobaId == osobaId).FirstOrDefault();
            if (blagajnik != null)
                blagajnik.isDeleted = true;
            if (osoba != null)
                osoba.isDeleted = true;
            ctx.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }


        public ActionResult UpravljanjeDetaljimaBlagajnika(int osobaId, int aktivan, int brojTaba = 1)
        {
            Osoba osoba = ctx.Osoba.Where(x => x.Id == osobaId).FirstOrDefault();
            ViewData["osobaId"] = osobaId;
            ViewData["aktivan"] = aktivan;
            ViewData["tab"] = brojTaba;
            ViewData["osoba"] = osoba;

            return View("UpravljanjeDetaljimaBlagajnika");
        }
        public ActionResult Detalji(int osobaId, int aktivan)
        {
            ViewData["aktivan"] = aktivan;
            ViewData["osobaId"] = osobaId;

            Osoba osoba = ctx.Osoba.Where(x => x.Id == osobaId).FirstOrDefault();
            Blagajnici blagajnik = ctx.Blagajnici.Where(x => x.OsobaId == osobaId && x.isDeleted == false).FirstOrDefault();
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
                aktivan = aktivan,
                DatumZaposlenja=blagajnik.DatumZaposlenja
            };
            return View("Detalji", model);
        }
        public ActionResult GetImage(int osobaId)
        {
            Osoba osoba = ctx.Osoba.Where(x => x.Id == osobaId).FirstOrDefault();
            return File(osoba.Slika, osoba.TipSlike);

        }
        [HttpPost]
        public ActionResult DodajSliku(BlagajniciDetaljiVM model)
        {
            if (model.s == null)
            {
                return RedirectToAction("UpravljanjeDetaljimaBlagajnika", new { osobaId = model.OsobaId, aktivan = model.aktivan, brojTaba = 1 });

            }
            else
            {
                Osoba osoba = ctx.Osoba.Where(y => y.Id == model.OsobaId).FirstOrDefault();
                osoba.NazivSlike = model.s.FileName;
                osoba.TipSlike = model.s.ContentType;

                byte[] slika = new byte[model.s.ContentLength];
                model.s.InputStream.Read(slika, 0, model.s.ContentLength);
                osoba.Slika = slika;
                ctx.SaveChanges();
            }

            return RedirectToAction("UpravljanjeDetaljimaBlagajnika", new { osobaId = model.OsobaId, aktivan = model.aktivan, brojTaba = 1 });

        }

        public ActionResult DeaktivirajBlagajnika(int osobaId, int aktivan)
        {
            Osoba osoba = ctx.Osoba.Where(x => x.Id == osobaId).FirstOrDefault();
            if (osoba != null)
            {
                osoba.isAktivnaOsoba = false;
                KorisnickiNalozi nalog = ctx.KorisnickiNalozi.Where(x => x.OsobaId == osobaId).FirstOrDefault();
                if (nalog != null)
                    nalog.isAktivanNalog = false;
                ctx.SaveChanges();
            }
            return RedirectToAction("UpravljanjeDetaljimaBlagajnika", new { osobaId = osobaId, aktivan = aktivan, brojTaba = 1 });

        }

        public ActionResult AktivirajBlagajnika(int osobaId, int aktivan)
        {
            Osoba osoba = ctx.Osoba.Where(x => x.Id == osobaId).FirstOrDefault();
            if (osoba != null)
            {
                osoba.isAktivnaOsoba = true;
                KorisnickiNalozi nalog = ctx.KorisnickiNalozi.Where(x => x.OsobaId == osobaId).FirstOrDefault();
                if (nalog != null)
                    nalog.isAktivanNalog = true;
                ctx.SaveChanges();
            }
            return RedirectToAction("UpravljanjeDetaljimaBlagajnika", new { osobaId = osobaId, aktivan = aktivan, brojTaba = 1 });

        }
        public string KonvertujUString_mm_dd_yyyy(string datum)
        {
            string dan = datum.Substring(0, 2);
            string mjesec = datum.Substring(3, 2);
            string godina = datum.Substring(6, 4);
            string uneseniDatum = dan + "/" + mjesec + "/" + godina;
            return uneseniDatum;
        }


        public ActionResult LicniPodaciBlagajnika(int osobaId, int aktivan)
        {

            Osoba osoba = ctx.Osoba.Where(x => x.Id == osobaId).FirstOrDefault();
            BlagajniciLicniPodaciBlagajnikaVM model = new BlagajniciLicniPodaciBlagajnikaVM
            {
                OsobaId = osobaId,
                isAktivnaOsoba = osoba.isAktivnaOsoba,
                Ime = osoba.Ime,
                Prezime = osoba.Prezime,
                ImeRoditelja = osoba.ImeRoditelja,
                JMBG = osoba.JMBG,
                Spol = osoba.Spol,
                DatumRodjenja = KonvertujUString_mm_dd_yyyy(osoba.DatumRodjenja.ToString()),
                MjestoRodjenja = osoba.MjestoRodjenja,
                KontaktTelefon = osoba.KontaktTelefon,
                Email = osoba.Email,
                aktivan = aktivan
            };

            return View("LicniPodaciBlagajnika", model);
        }
        [HttpPost]
        public ActionResult SpremiLicnePodatkeBlagajnika(BlagajniciLicniPodaciBlagajnikaVM model)
        {
            Osoba osoba = ctx.Osoba.Where(x => x.Id == model.OsobaId).FirstOrDefault();

            osoba.Ime = model.Ime;
            osoba.Prezime = model.Prezime;
            osoba.ImeRoditelja = model.ImeRoditelja;
            osoba.DatumRodjenja = KonvertujUDatum_dd_mm_yyyy(model.DatumRodjenja);
            osoba.MjestoRodjenja = model.MjestoRodjenja;
            osoba.KontaktTelefon = model.KontaktTelefon;
            osoba.JMBG = model.JMBG;
            osoba.Email = model.Email;
            osoba.Spol = model.Spol;

            ctx.SaveChanges();

            return RedirectToAction("UpravljanjeDetaljimaBlagajnika", new { osobaId = model.OsobaId, aktivan = model.aktivan, brojTaba = 1 });

        }

        public ActionResult KlubskiPodaciBlagajnika(int osobaId, int aktivan)
        {
            Osoba osoba = ctx.Osoba.Where(x => x.Id == osobaId).FirstOrDefault();
            Blagajnici blagajnik = ctx.Blagajnici.Where(x => x.OsobaId == osobaId).FirstOrDefault();
            BlagajniciKlubskiPodaciBlagajnikaVM model = new BlagajniciKlubskiPodaciBlagajnikaVM
            {
                DatumZaposlenja = KonvertujUString_mm_dd_yyyy(blagajnik.DatumZaposlenja.ToString()),
                aktivan = aktivan,
                isAktivnaOsoba = osoba.isAktivnaOsoba
            };

            return View("KlubskiPodaciBlagajnika", model);

        }
        [HttpPost]
        public ActionResult SpremiKlubskePodatkeBlagajnika(BlagajniciKlubskiPodaciBlagajnikaVM model)
        {
            Osoba osoba = ctx.Osoba.Where(x => x.Id == model.OsobaId).FirstOrDefault();
            Blagajnici blagajnik = ctx.Blagajnici.Where(x => x.OsobaId == model.OsobaId).FirstOrDefault();

            blagajnik.DatumZaposlenja = KonvertujUDatum_dd_mm_yyyy(model.DatumZaposlenja);

            ctx.SaveChanges();
            return RedirectToAction("UpravljanjeDetaljimaBlagajnika", new { osobaId = model.OsobaId, aktivan = model.aktivan, brojTaba = 1 });

        }
    }
}