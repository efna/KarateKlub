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

    public class TreneriController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulSekretar/Treneri
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dodaj(int aktivan)
        {
            TreneriDodajVM model = new TreneriDodajVM{
                aktivan=aktivan,
                FunkcijaTrenera=BindFunkcijeTrenera(),
                ZvanjaUKarateu=BindZvanjaUKarateu()

            };
            model.ZvanjaUKarateu.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite zvanje-" });
            model.FunkcijaTrenera.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite funkciju-" });
            return View("Dodaj",model);
        }

        private List<SelectListItem> BindFunkcijeTrenera()
        {
            return ctx.FunkcijeTrenera.Where(x => x.isDeleted == false).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv }).ToList();
        }

        private List<SelectListItem> BindZvanjaUKarateu()
        {
            return ctx.ZvanjaUKarateu.Where(x => x.isDeleted == false).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv }).ToList();
        }

        public DateTime KonvertujUDatum_dd_mm_yyyy(string datum)
        {
            int dan = Convert.ToInt32(datum.Substring(0, 2));
            int mjesec = Convert.ToInt32(datum.Substring(3, 2));
            int godina = Convert.ToInt32(datum.Substring(6, 4));
            DateTime uneseniDatum = new DateTime(godina, mjesec, dan);
            return uneseniDatum;
        }

        public ActionResult SpremiNovogTrenera(TreneriDodajVM model)
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
            osoba.isTrener = true;
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
            Treneri trener = new Treneri();
            trener.OsobaId = OsobaId;
            trener.isDeleted = false;
            trener.DatumZaposlenja = KonvertujUDatum_dd_mm_yyyy(model.DatumZaposlenja);
            trener.FunkcijaTreneraId = model.FunkcijaTreneraId;
            trener.ZvanjeUKarateuId = model.ZvanjeUKarateuId;
            ctx.Treneri.Add(trener);
            ctx.SaveChanges();

            return RedirectToAction("GoToPrikazTrenera","UpravljanjeUposlenicima",new {aktivan=model.aktivan });
        }

        public JsonResult Obrisi(int osobaId)
        {
            Osoba osoba = ctx.Osoba.Where(x => x.Id == osobaId).FirstOrDefault();
            Treneri trener = ctx.Treneri.Where(x => x.OsobaId == osobaId).FirstOrDefault();
            if (trener != null)
                trener.isDeleted = true;
            if(osoba!=null)
            osoba.isDeleted = true;
            ctx.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpravljanjeDetaljimaTrenera(int osobaId,int aktivan, int brojTaba = 1, int aktivnostLicence=0)
        {
            Osoba osoba = ctx.Osoba.Where(x => x.Id == osobaId).FirstOrDefault();
            ViewData["osobaId"] = osobaId;
            ViewData["aktivan"] = aktivan;
            ViewData["tab"] = brojTaba;
            ViewData["osoba"] = osoba;
            ViewData["aktivnostLicence"] = aktivnostLicence;

            return View("UpravljanjeDetaljimaTrenera");
        }
        public ActionResult GoToUpravljanjeDetaljimaTrenera(int osobaId,int aktivan,int aktivnost)
        {

            return RedirectToAction("UpravljanjeDetaljimaTrenera", new { osobaId = osobaId, aktivan = aktivan, brojTaba = 3, aktivnostLicence = aktivnost });
        }
        public ActionResult Detalji(int osobaId,int aktivan)
        {
            ViewData["aktivan"] = aktivan;
            ViewData["osobaId"] = osobaId;
            Treneri trener = ctx.Treneri.Where(x => x.OsobaId == osobaId && x.isDeleted == false).FirstOrDefault();
            Osoba osoba = ctx.Osoba.Where(x => x.Id == osobaId).FirstOrDefault();
            TreneriDetaljiVM model = new TreneriDetaljiVM
            {
                isAktivnaOsoba=osoba.isAktivnaOsoba,
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
                aktivan=aktivan,
                DatumZaposlenja=trener.DatumZaposlenja,
                ZvanjeUKarateu=trener.ZvanjeUKarateu.Naziv,
                FunkcijaTrenera=trener.FunkcijaTrenera.Naziv
            };
            return View("Detalji", model);
        }
        public ActionResult GetImage(int osobaId)
        {
            Osoba osoba = ctx.Osoba.Where(x => x.Id == osobaId).FirstOrDefault();
            return File(osoba.Slika, osoba.TipSlike);

        }
        [HttpPost]
        public ActionResult DodajSliku(TreneriDetaljiVM model)
        {
            if (model.s == null)
            {
                return RedirectToAction("UpravljanjeDetaljimaTrenera", new { osobaId = model.OsobaId,aktivan=model.aktivan,brojTaba=1 });

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

            return RedirectToAction("UpravljanjeDetaljimaTrenera", new { osobaId = model.OsobaId, aktivan = model.aktivan, brojTaba = 1 });

        }

        public ActionResult DeaktivirajTrenera(int osobaId,int aktivan)
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
            return RedirectToAction("UpravljanjeDetaljimaTrenera", new { osobaId = osobaId, aktivan = aktivan, brojTaba = 1 });

        }

        public ActionResult AktivirajTrenera(int osobaId,int aktivan)
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
            return RedirectToAction("UpravljanjeDetaljimaTrenera", new { osobaId = osobaId, aktivan = aktivan, brojTaba = 1 });

        }
        public string KonvertujUString_mm_dd_yyyy(string datum)
        {
            string dan = datum.Substring(0, 2);
            string mjesec = datum.Substring(3, 2);
            string godina = datum.Substring(6, 4);
            string uneseniDatum = dan + "/" + mjesec + "/" + godina;
            return uneseniDatum;
        }


        public ActionResult LicniPodaciTrenera(int osobaId, int aktivan)
        {

            Osoba osoba = ctx.Osoba.Where(x => x.Id == osobaId).FirstOrDefault();
            TreneriLicniPodaciTreneraVM model = new TreneriLicniPodaciTreneraVM
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
                aktivan=aktivan
            };

            return View("LicniPodaciTrenera", model);
        }
        [HttpPost]
        public ActionResult SpremiLicnePodatkeTrenera(TreneriLicniPodaciTreneraVM model)
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

            return RedirectToAction("UpravljanjeDetaljimaTrenera", new { osobaId = model.OsobaId, aktivan = model.aktivan, brojTaba = 1 });

        }

        public ActionResult KlubskiPodaciTrenera(int osobaId,int aktivan)
        {
            Osoba osoba = ctx.Osoba.Where(x => x.Id == osobaId).FirstOrDefault();
            Treneri trener = ctx.Treneri.Where(x => x.OsobaId == osobaId).FirstOrDefault();
            TreneriKlubskiPodaciTreneraVM model = new TreneriKlubskiPodaciTreneraVM
            {
                DatumZaposlenja = KonvertujUString_mm_dd_yyyy(trener.DatumZaposlenja.ToString()),
                funkcijaTreneraId = trener.FunkcijaTreneraId,
                FunckijeTrenera = BindFunkcijeTrenera(),
                ZvanjeUKarateuId = trener.ZvanjeUKarateuId,
                ZvanjaUKarateu = BindZvanjaUKarateu(),
                aktivan = aktivan,
                isAktivnaOsoba=osoba.isAktivnaOsoba
                };
                model.ZvanjaUKarateu.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite zvanje-" });
                model.FunckijeTrenera.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite funkciju-" });

            return View("KlubskiPodaciTrenera",model);

        }
        [HttpPost]
        public ActionResult SpremiKlubskePodatkeTrenera(TreneriKlubskiPodaciTreneraVM model)
        {
            Osoba osoba = ctx.Osoba.Where(x => x.Id == model.OsobaId).FirstOrDefault();
            Treneri trener = ctx.Treneri.Where(x => x.OsobaId == model.OsobaId).FirstOrDefault();
            
                trener.DatumZaposlenja = KonvertujUDatum_dd_mm_yyyy(model.DatumZaposlenja);
                trener.FunkcijaTreneraId = model.funkcijaTreneraId;
                trener.ZvanjeUKarateuId = model.ZvanjeUKarateuId;
                ctx.SaveChanges();
            return RedirectToAction("UpravljanjeDetaljimaTrenera", new { osobaId = model.OsobaId, aktivan = model.aktivan, brojTaba = 1 });

        }


    }
}