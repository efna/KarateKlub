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

    public class ClanoviUpravnogOdboraController : Controller
    {
        MojContext ctx = new MojContext();
        
        // GET: ModulSekretar/ClanoviUpravnogOdbora
        public ActionResult Index(int aktivnost=0,int brojTaba=1)
        {
            List<ClanoviUpravnogOdbora> clanovi = new List<ClanoviUpravnogOdbora>();
            if (aktivnost == 0) {
                clanovi = ctx.ClanoviUpravnogOdbora.Where(x => x.isDeleted == false && x.Aktivan == true).ToList();
            }
            else if(aktivnost == 1)
            {
                clanovi = ctx.ClanoviUpravnogOdbora.Where(x => x.isDeleted == false && x.Aktivan == false).ToList();

            }
            else
            {
                clanovi = ctx.ClanoviUpravnogOdbora.Where(x => x.isDeleted == false).ToList();

            }
            ClanoviUpravnogOdboraIndexVM model = new ClanoviUpravnogOdboraIndexVM(clanovi,aktivnost);
            ViewData["tab"] = brojTaba;

            ViewData["aktivnost"] = aktivnost;
            return View(model);
        }

        public ActionResult GetImage(int clanId)
        {
            ClanoviUpravnogOdbora clan = ctx.ClanoviUpravnogOdbora.Where(x => x.Id == clanId).FirstOrDefault();
            return File(clan.Osoba.Slika, clan.Osoba.TipSlike);

        }
        [HttpPost]
        public ActionResult DodajSliku(ClanoviUpravnogOdboraDetaljiVM model)
        {
            if (model.s == null)
            {
                return RedirectToAction("Detalji", new { clanId = model.ClanId,aktivnost=model.aktivnost});

            }
            else
            {
                ClanoviUpravnogOdbora clan = ctx.ClanoviUpravnogOdbora.Where(y => y.Id == model.ClanId).FirstOrDefault();
                clan.Osoba.NazivSlike = model.s.FileName;
                clan.Osoba.TipSlike = model.s.ContentType;

                byte[] slika = new byte[model.s.ContentLength];
                model.s.InputStream.Read(slika, 0, model.s.ContentLength);
                clan.Osoba.Slika = slika;
                ctx.SaveChanges();
            }  
            
            return RedirectToAction("Detalji", new { clanId = model.ClanId,aktivnost=model.aktivnost});
        }

        public ActionResult Detalji(int clanId,int aktivnost)
        {
            ClanoviUpravnogOdbora clan = ctx.ClanoviUpravnogOdbora.Where(x => x.Id == clanId).FirstOrDefault();
            ClanoviUpravnogOdboraDetaljiVM model = new ClanoviUpravnogOdboraDetaljiVM
            {

                ClanId=clan.Id,
                isAktivan = clan.Aktivan,
                OsobaId = clan.OsobaId,
                Ime = clan.Osoba.Ime,
                FunkcijaClanaId = clan.UlogeClanovaUpravnogOdboraId,
                Prezime = clan.Osoba.Prezime,
                ImeRoditelja = clan.Osoba.ImeRoditelja,
                DatumRodjenja = clan.Osoba.DatumRodjenja,
                MjestoRodjenja = clan.Osoba.MjestoRodjenja,
                JMBG = clan.Osoba.JMBG,
                Spol = clan.Osoba.Spol,
                KontaktTelefon = clan.Osoba.KontaktTelefon,
                Email = clan.Osoba.Email,
                Slika = clan.Osoba.Slika,
                TipSlike = clan.Osoba.TipSlike,
                NazivSlike = clan.Osoba.NazivSlike,
                funckijaClanaUO = clan.UlogeClanovaUpravnogOdbora.Naziv,
                DatumIzglasavanja=clan.DatumIzglasavanja,
                aktivnost=aktivnost
        
            };


            return View("Detalji", model);
        }

        public ActionResult PodaciClana(int clanId,int aktivnost)
        {
            ClanoviUpravnogOdbora clan = ctx.ClanoviUpravnogOdbora.Where(x => x.Id == clanId).FirstOrDefault();

            ClanoviUpravnogOdboraPodaciClanaVM model = new ClanoviUpravnogOdboraPodaciClanaVM
            {
                ClanId = clan.Id,
                OsobaId = clan.OsobaId,
                FunkcijaClanaId = clan.UlogeClanovaUpravnogOdboraId,
                isAktivanClan = clan.Aktivan,
                Ime = clan.Osoba.Ime,
                Prezime = clan.Osoba.Prezime,
                ImeRoditelja = clan.Osoba.ImeRoditelja,
                JMBG = clan.Osoba.JMBG,
                Spol = clan.Osoba.Spol,
                DatumRodjenja = clan.Osoba.DatumRodjenja.ToString("dd.MM.yyyy"),
                MjestoRodjenja = clan.Osoba.MjestoRodjenja,
                KontaktTelefon = clan.Osoba.KontaktTelefon,
                Email = clan.Osoba.Email,
                FunkcijaClana = clan.UlogeClanovaUpravnogOdbora.Naziv,
                listaFunkcijaClana = BindFunkcijeClana(),
                DatumIzglasavanja = clan.DatumIzglasavanja.ToString("dd.MM.yyyy"),
                aktivnost=aktivnost

            };
            model.listaFunkcijaClana.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite ulogu-" });

            return View("PodaciClana", model);
        }

        private List<SelectListItem> BindFunkcijeClana()
        {
            return ctx.UlogeClanovaUpravnogOdbora.Where(x=>x.isDeleted==false).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv }).ToList();
         
        }

        public DateTime KonvertujUDatum_dd_mm_yyyy(string datum)
        {

            int dan = Convert.ToInt32(datum.Substring(0, 2));
            int mjesec = Convert.ToInt32(datum.Substring(3, 2));
            int godina = Convert.ToInt32(datum.Substring(6, 4));
            DateTime uneseniDatum = new DateTime(godina, mjesec, dan);
            return uneseniDatum;


        }

        [HttpPost]
        public ActionResult SpremiIzmjenePodataka(ClanoviUpravnogOdboraPodaciClanaVM model)
        {

    
        ClanoviUpravnogOdbora clan = ctx.ClanoviUpravnogOdbora.Where(x => x.Id == model.ClanId).FirstOrDefault();
            clan.Osoba.Ime = model.Ime;
            clan.Osoba.Prezime = model.Prezime;
            clan.Osoba.ImeRoditelja = model.ImeRoditelja;
            clan.Osoba.JMBG = model.JMBG;
            clan.Osoba.Spol = model.Spol;
            clan.Osoba.DatumRodjenja = KonvertujUDatum_dd_mm_yyyy(model.DatumRodjenja);
            clan.Osoba.MjestoRodjenja = model.MjestoRodjenja;
            clan.Osoba.KontaktTelefon = model.KontaktTelefon;
            clan.Osoba.Email = model.Email;
            clan.UlogeClanovaUpravnogOdboraId = model.FunkcijaClanaId;
            clan.DatumIzglasavanja = KonvertujUDatum_dd_mm_yyyy(model.DatumIzglasavanja);
            ctx.SaveChanges();

            return RedirectToAction("Detalji", new { clanId = model.ClanId,aktivnost=model.aktivnost });
        }
        public ActionResult DeaktivirajClana(int clanId,int aktivnost)
        {
            ClanoviUpravnogOdbora clan = ctx.ClanoviUpravnogOdbora.Where(x => x.Id == clanId).FirstOrDefault();
            if (clan != null)
            {
                clan.Aktivan = false;
                clan.Osoba.isAktivnaOsoba = false;
                ctx.SaveChanges();
            }
            return RedirectToAction("Detalji", new { clanId=clanId,aktivnost=aktivnost });
        }

        public ActionResult AktivirajClana(int clanId,int aktivnost)
        {
            ClanoviUpravnogOdbora clan = ctx.ClanoviUpravnogOdbora.Where(x => x.Id == clanId).FirstOrDefault();

            if (clan != null)
            {
                clan.Aktivan = true;
                clan.Osoba.isAktivnaOsoba = true;
                ctx.SaveChanges();
            }
            return RedirectToAction("Detalji", new { clanId=clanId,aktivnost=aktivnost });
        }

        public ActionResult Dodaj(int aktivnost)
        {
            ClanoviUpravnogOdboraDodajVM model = new ClanoviUpravnogOdboraDodajVM {
                listaFunkcijaClana = BindFunkcijeClana(),
                aktivnost = aktivnost,
                DatumRodjenja = "",
                DatumIzglasavanja=""

            };
            model.listaFunkcijaClana.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite ulogu-" });

            return View("Dodaj", model);
        }

        [HttpPost]
        public ActionResult SpremiNovogClana(ClanoviUpravnogOdboraDodajVM model){
            Osoba osoba = new Osoba();
            osoba.isAktivnaOsoba = true;
            osoba.isDeleted = false;
            osoba.Ime = model.Ime;
            osoba.Prezime = model.Prezime;
            osoba.DatumRodjenja = KonvertujUDatum_dd_mm_yyyy(model.DatumRodjenja);
            osoba.MjestoRodjenja = model.MjestoRodjenja;
            osoba.JMBG = model.JMBG;
            osoba.ImeRoditelja = model.ImeRoditelja;
            osoba.Spol = model.Spol;
            osoba.KontaktTelefon = model.KontaktTelefon;
            osoba.Email = model.Email;
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

            ClanoviUpravnogOdbora clan = new ClanoviUpravnogOdbora();
            clan.Aktivan = true;
            clan.isDeleted = false;
            clan.OsobaId =OsobaId;
            clan.UlogeClanovaUpravnogOdboraId = model.FunkcijaClanaId;
            clan.DatumIzglasavanja = KonvertujUDatum_dd_mm_yyyy(model.DatumIzglasavanja);
            ctx.ClanoviUpravnogOdbora.Add(clan);
            ctx.SaveChanges();

            return RedirectToAction("Index","UpravljanjeUpravnimOdborom",new {aktivnost=model.aktivnost,brojtaba=1 });
        }
        public JsonResult Obrisi(int clanId)
        {
            ClanoviUpravnogOdbora clan = ctx.ClanoviUpravnogOdbora.Where(x => x.Id == clanId).FirstOrDefault();
            clan.isDeleted = true;
            Osoba osoba = ctx.Osoba.Where(x => x.Id == clan.OsobaId).FirstOrDefault();
            osoba.isDeleted = true;
            ctx.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

    }
}