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

    public class KnjigovodjeController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulSekretar/Knjigovodje
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Dodaj(int aktivan)
        {
            KnjigovodjeDodajVM model = new KnjigovodjeDodajVM
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

        public ActionResult SpremiNovogKnjigovodju(KnjigovodjeDodajVM model)
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
            osoba.isBlagajnik = false;
            osoba.isSekretar = false;
            osoba.isClanKluba = false;
            osoba.isTrener = false;
            osoba.isClanUpravnogOdbora = false;
            osoba.isKnjigovodja = true;
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
            Knjigovodje knjigovodja = new Knjigovodje();
            knjigovodja.OsobaId = OsobaId;
            knjigovodja.isDeleted = false;
            knjigovodja.DatumZaposlenja = KonvertujUDatum_dd_mm_yyyy(model.DatumZaposlenja);
            ctx.Knjigovodje.Add(knjigovodja);
            ctx.SaveChanges();
            return RedirectToAction("GoToPrikazKnjigovodja", "UpravljanjeUposlenicima", new { aktivan = model.aktivan });
        }

        public JsonResult Obrisi(int osobaId)
        {
            Osoba osoba = ctx.Osoba.Where(x => x.Id == osobaId).FirstOrDefault();
            Knjigovodje knjigovodja = ctx.Knjigovodje.Where(x => x.OsobaId == osobaId).FirstOrDefault();
            if (knjigovodja != null)
                knjigovodja.isDeleted = true;
            if (osoba != null)
                osoba.isDeleted = true;
            ctx.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpravljanjeDetaljimaKnjigovodje(int osobaId, int aktivan, int brojTaba = 1)
        {
            Osoba osoba = ctx.Osoba.Where(x => x.Id == osobaId).FirstOrDefault();
            ViewData["osobaId"] = osobaId;
            ViewData["aktivan"] = aktivan;
            ViewData["tab"] = brojTaba;
            ViewData["osoba"] = osoba;

            return View("UpravljanjeDetaljimaKnjigovodje");
        }
        public ActionResult Detalji(int osobaId, int aktivan)
        {
            ViewData["aktivan"] = aktivan;
            ViewData["osobaId"] = osobaId;

            Osoba osoba = ctx.Osoba.Where(x => x.Id == osobaId).FirstOrDefault();
            Knjigovodje knjigovodja = ctx.Knjigovodje.Where(x => x.OsobaId == osobaId && x.isDeleted == false).FirstOrDefault();
            KnjigovodjeDetaljiVM model = new KnjigovodjeDetaljiVM
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
                DatumZaposlenja=knjigovodja.DatumZaposlenja
            };
            return View("Detalji", model);
        }
        public ActionResult GetImage(int osobaId)
        {
            Osoba osoba = ctx.Osoba.Where(x => x.Id == osobaId).FirstOrDefault();
            return File(osoba.Slika, osoba.TipSlike);

        }
        [HttpPost]
        public ActionResult DodajSliku(KnjigovodjeDetaljiVM model)
        {
            if (model.s == null)
            {
                return RedirectToAction("UpravljanjeDetaljimaKnjigovodje", new { osobaId = model.OsobaId, aktivan = model.aktivan, brojTaba = 1 });

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

            return RedirectToAction("UpravljanjeDetaljimaKnjigovodje", new { osobaId = model.OsobaId, aktivan = model.aktivan, brojTaba = 1 });

        }

        public ActionResult DeaktivirajKnjigovodju(int osobaId, int aktivan)
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
            return RedirectToAction("UpravljanjeDetaljimaKnjigovodje", new { osobaId = osobaId, aktivan = aktivan, brojTaba = 1 });

        }

        public ActionResult AktivirajKnjigovodju(int osobaId, int aktivan)
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
            return RedirectToAction("UpravljanjeDetaljimaKnjigovodje", new { osobaId = osobaId, aktivan = aktivan, brojTaba = 1 });

        }
        public string KonvertujUString_mm_dd_yyyy(string datum)
        {
            string dan = datum.Substring(0, 2);
            string mjesec = datum.Substring(3, 2);
            string godina = datum.Substring(6, 4);
            string uneseniDatum = dan + "/" + mjesec + "/" + godina;
            return uneseniDatum;
        }


        public ActionResult LicniPodaciKnjigovodje(int osobaId, int aktivan)
        {

            Osoba osoba = ctx.Osoba.Where(x => x.Id == osobaId).FirstOrDefault();
            KnjigovodjeLicniPodaciKnjigovodjeVM model = new KnjigovodjeLicniPodaciKnjigovodjeVM
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

            return View("LicniPodaciKnjigovodje", model);
        }
        [HttpPost]
        public ActionResult SpremiLicnePodatkeKnjigovodje(KnjigovodjeLicniPodaciKnjigovodjeVM model)
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

            return RedirectToAction("UpravljanjeDetaljimaKnjigovodje", new { osobaId = model.OsobaId, aktivan = model.aktivan, brojTaba = 1 });

        }

        public ActionResult KlubskiPodaciKnjigovodje(int osobaId, int aktivan)
        {
            Osoba osoba = ctx.Osoba.Where(x => x.Id == osobaId).FirstOrDefault();
            Knjigovodje knjigovodja = ctx.Knjigovodje.Where(x => x.OsobaId == osobaId).FirstOrDefault();
            KnjigovodjeKlubskiPodaciKnjigovodjeVM model = new KnjigovodjeKlubskiPodaciKnjigovodjeVM
            {
                DatumZaposlenja = KonvertujUString_mm_dd_yyyy(knjigovodja.DatumZaposlenja.ToString()),
                aktivan = aktivan,
                isAktivnaOsoba = osoba.isAktivnaOsoba
            };

            return View("KlubskiPodaciKnjigovodje", model);

        }
        [HttpPost]
        public ActionResult SpremiKlubskePodatkeKnjigovodje(KnjigovodjeKlubskiPodaciKnjigovodjeVM model)
        {
            Osoba osoba = ctx.Osoba.Where(x => x.Id == model.OsobaId).FirstOrDefault();
            Knjigovodje knjigovodja = ctx.Knjigovodje.Where(x => x.OsobaId == model.OsobaId).FirstOrDefault();

            knjigovodja.DatumZaposlenja = KonvertujUDatum_dd_mm_yyyy(model.DatumZaposlenja);

            ctx.SaveChanges();
            return RedirectToAction("UpravljanjeDetaljimaKnjigovodje", new { osobaId = model.OsobaId, aktivan = model.aktivan, brojTaba = 1 });

        }
    }
}