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
    [Autorizacija(true, false, false, false, false)]

    public class TreneriController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulAdministrator/Treneri
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Dodaj(int aktivan)
        {
            TreneriDodajVM model = new TreneriDodajVM
            {
                aktivnost = aktivan,
                FunkcijaTrenera = BindFunkcijeTrenera(),
                ZvanjaUKarateu = BindZvanjaUKarateu()

            };
            model.ZvanjaUKarateu.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite zvanje-" });
            model.FunkcijaTrenera.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite funkciju-" });
            return View("Dodaj", model);
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
            KorisnickiNalozi nalog = new KorisnickiNalozi();
            int ulogaId = ctx.KorisnickeUloge.Where(x => x.Naziv.Contains("ren")).FirstOrDefault().Id;
            nalog.isDeleted = false;
            nalog.isAktivanNalog = true;
            nalog.OsobaId = OsobaId;
            nalog.KorisnickaUlogaId = ulogaId;
            nalog.KorisnickoIme = model.KorisnickoIme;
            nalog.Lozinka = model.Lozinka;
            ctx.KorisnickiNalozi.Add(nalog);
            ctx.SaveChanges();
            return RedirectToAction("Index","KorisnickiNalozi", new { aktivan = model.aktivnost, uloga = 0, brojTaba = 4 });

        }
    }
}