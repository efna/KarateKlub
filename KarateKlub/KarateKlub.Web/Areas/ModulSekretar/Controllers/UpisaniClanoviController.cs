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

    public class UpisaniClanoviController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulSekretar/UpisaniClanovi
        public ActionResult Index(int upisId)
        {
            UpisaniClanoviIndexVM model = new UpisaniClanoviIndexVM {
                upisaniClanovi=ctx.UpisaniClanovi.Where(x=>x.isDeleted==false && x.UpisId==upisId).Select(x=>new UpisaniClanPodaci {
                    Id=x.Id,
                    isDeleted=x.isDeleted,
                    ClanKluba=x.ClanKluba.Osoba.Ime+" ("+x.ClanKluba.Osoba.ImeRoditelja+" )"+x.ClanKluba.Osoba.Prezime,
                    JMBG=x.ClanKluba.Osoba.JMBG,
                    Spol=x.ClanKluba.Osoba.Spol,
                    DatumRodjenja=x.ClanKluba.Osoba.DatumRodjenja,
                    MjestoRodjenja=x.ClanKluba.Osoba.MjestoRodjenja,
                    KontaktTelefon=x.ClanKluba.Osoba.KontaktTelefon,
                    Email=x.ClanKluba.Osoba.Email,
                    ZvanjeUKarateuId=x.ClanKluba.ZvanjeUKarateuId,
                    ZvanjeUKarateu=x.ClanKluba.ZvanjeUKarateu.Naziv,
                    StarosnaDob=x.ClanKluba.StarosnaDob.Naziv,
                    StarosnaDobId=x.ClanKluba.StarosnaDobId,
                    DatumUpisa=x.ClanKluba.DatumUpisa
                }).ToList()
            };
            ViewData["upisId"] = upisId;
            return View(model);
        }
        public DateTime KonvertujUDatum_dd_mm_yyyy(string datum)
        {

            int dan = Convert.ToInt32(datum.Substring(0, 2));
            int mjesec = Convert.ToInt32(datum.Substring(3, 2));
            int godina = Convert.ToInt32(datum.Substring(6, 4));
            DateTime uneseniDatum = new DateTime(godina, mjesec, dan);
            return uneseniDatum;


        }
        public ActionResult Dodaj(int upisId)
        {
            UpisaniClanoviDodajVM model = new UpisaniClanoviDodajVM {
                StarosneDobi=BindStarosneDobi(),
                ZvanjaUKarateu=BindZvanjaUKarateu(),
                upisId=upisId
            };
            model.ZvanjaUKarateu.Insert(0, new SelectListItem { Value = null, Text = "-Odaberi zvanje-" });
            model.StarosneDobi.Insert(0, new SelectListItem { Value = null, Text = "-Odaberi starosnu dob-" });

            return View("Dodaj", model);
        }
        public ActionResult SpremiNovogUpisanogClana(UpisaniClanoviDodajVM model)
        {
            Osoba osoba = new Osoba();
            osoba.isDeleted = false;
            osoba.isAktivnaOsoba = true;
            osoba.Ime = model.Ime;
            osoba.Prezime = model.Prezime;
            osoba.Spol = model.Spol;
            osoba.ImeRoditelja = model.ImeRoditelja;
            osoba.DatumRodjenja = KonvertujUDatum_dd_mm_yyyy(model.DatumRodjenja);
            osoba.MjestoRodjenja = model.MjestoRodjenja;
            osoba.KontaktTelefon = model.KontaktTelefon;
            osoba.JMBG = model.JMBG;
            osoba.Email = model.Email;
            osoba.isAdministrator = false;
            osoba.isBlagajnik = false;
            osoba.isSekretar = false;
            osoba.isTrener = false;
            osoba.isKnjigovodja = false;
            osoba.isClanUpravnogOdbora = false;
            osoba.isClanKluba = true;
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

            ClanoviKluba clanKluba = new ClanoviKluba();
            clanKluba.isDeleted = false;
            clanKluba.OsobaId = OsobaId;
            if(model.DatumUpisa!=null)
            clanKluba.DatumUpisa = KonvertujUDatum_dd_mm_yyyy(model.DatumUpisa);
            clanKluba.ZvanjeUKarateuId = model.ZvanjeUKarateuId;
            clanKluba.StarosnaDobId = model.StarosnaDobId;
            ctx.ClanoviKluba.Add(clanKluba);
            ctx.SaveChanges();
            int ClanKlubaId = ctx.ClanoviKluba.ToList().OrderByDescending(x => x.Id).FirstOrDefault().Id;
            UpisaniClanovi upisaniClan = new UpisaniClanovi();
            upisaniClan.isDeleted = false;
            upisaniClan.UpisId = model.upisId;
            upisaniClan.ClanKlubaId = ClanKlubaId;
            ctx.UpisaniClanovi.Add(upisaniClan);
            ctx.SaveChanges();

            Upisnine upisnina = new Upisnine();
            upisnina.isDeleted = false;
            upisnina.isIzmirenaUpisnina = false;
            upisnina.DatumUplate = null;
            upisnina.UpisId = model.upisId;
            upisnina.ClanKlubaId = ClanKlubaId;
            ctx.Upisnine.Add(upisnina);
            ctx.SaveChanges();
            return RedirectToAction("Index", "UpravljanjePodacimaUpisa", new { upisId=model.upisId,brojTaba = 1 });
            
        }

        private List<SelectListItem> BindZvanjaUKarateu()
        {
            return ctx.ZvanjaUKarateu.Where(x => x.isDeleted == false).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv }).ToList();

        }

        private List<SelectListItem> BindStarosneDobi()
        {
            return ctx.StarosneDobi.Where(x=>x.isDeleted==false).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv }).ToList();

        }
        public ActionResult Uredi(int upisaniClanId)
        {
            UpisaniClanovi upisaniClan = ctx.UpisaniClanovi.Where(x => x.Id == upisaniClanId).FirstOrDefault();

            UpisaniClanoviUrediVM model = new UpisaniClanoviUrediVM {
               Id=upisaniClanId,
               isDeleted=upisaniClan.ClanKluba.Osoba.isDeleted,
               Ime=upisaniClan.ClanKluba.Osoba.Ime,
               Prezime= upisaniClan.ClanKluba.Osoba.Prezime,
               ImeRoditelja=upisaniClan.ClanKluba.Osoba.ImeRoditelja,
               JMBG=upisaniClan.ClanKluba.Osoba.JMBG,
               Spol=upisaniClan.ClanKluba.Osoba.Spol,
              DatumRodjenja=upisaniClan.ClanKluba.Osoba.DatumRodjenja.ToString("dd.MM.yyyy"),
              MjestoRodjenja=upisaniClan.ClanKluba.Osoba.MjestoRodjenja,
              KontaktTelefon=upisaniClan.ClanKluba.Osoba.KontaktTelefon,
              Email=upisaniClan.ClanKluba.Osoba.Email,
              DatumUpisa=upisaniClan.ClanKluba.DatumUpisa.ToString("dd.MM.yyyy"),
              ZvanjeUKarateuId=upisaniClan.ClanKluba.ZvanjeUKarateuId,
              StarosnaDobId=upisaniClan.ClanKluba.StarosnaDobId,
              ZvanjaUKarateu=BindZvanjaUKarateu(),
              StarosneDobi=BindStarosneDobi(),
              upisId=upisaniClan.UpisId

            };
            model.ZvanjaUKarateu.Insert(0, new SelectListItem { Value = null, Text = "-Odaberi zvanje-" });
            model.StarosneDobi.Insert(0, new SelectListItem { Value = null, Text = "-Odaberi starosnu dob-" });


            return View("Uredi", model);
        }
        public ActionResult SpremiIzmjenuUpisanogClana(UpisaniClanoviUrediVM model)
        {

            int clanKlubaId = ctx.UpisaniClanovi.Where(x => x.Id == model.Id).Select(x => x.ClanKlubaId).FirstOrDefault();
            ClanoviKluba clanKluba = ctx.ClanoviKluba.Where(x => x.Id == clanKlubaId).FirstOrDefault();
            int osobaId = clanKluba.OsobaId;
            Osoba osoba = ctx.Osoba.Where(x => x.Id == osobaId).FirstOrDefault();
            osoba.Ime = model.Ime;
            osoba.Prezime = model.Prezime;
            osoba.Spol = model.Spol;
            osoba.ImeRoditelja = model.ImeRoditelja;
            osoba.DatumRodjenja = KonvertujUDatum_dd_mm_yyyy(model.DatumRodjenja);
            osoba.MjestoRodjenja = model.MjestoRodjenja;
            osoba.KontaktTelefon = model.KontaktTelefon;
            osoba.JMBG = model.JMBG;
            osoba.Email = model.Email;
            ctx.SaveChanges();
            int OsobaId = ctx.Osoba.ToList().OrderByDescending(x => x.Id).FirstOrDefault().Id;


            if (model.DatumUpisa != null)
            clanKluba.DatumUpisa = KonvertujUDatum_dd_mm_yyyy(model.DatumUpisa);
            clanKluba.ZvanjeUKarateuId = model.ZvanjeUKarateuId;
            clanKluba.StarosnaDobId = model.StarosnaDobId;
            ctx.SaveChanges();

            return RedirectToAction("Index", "UpravljanjePodacimaUpisa", new { upisId = model.upisId, brojTaba = 1 });
        }
        public JsonResult Obrisi(int upisaniClanId)
        {
            UpisaniClanovi upisaniClan = ctx.UpisaniClanovi.Where(x => x.Id == upisaniClanId).FirstOrDefault();
            int osobaId = upisaniClan.ClanKluba.OsobaId;
            int clanKlubaId = upisaniClan.ClanKlubaId;
            Osoba osoba = ctx.Osoba.Where(x => x.Id == osobaId).FirstOrDefault();
            ClanoviKluba clanKluba = ctx.ClanoviKluba.Where(x => x.Id == clanKlubaId).FirstOrDefault();
            Upisnine upisnina = ctx.Upisnine.Where(x => x.ClanKlubaId == clanKlubaId).FirstOrDefault();
            if (upisnina != null)
                upisnina.isDeleted = true;
            if (upisaniClan != null)
                upisaniClan.isDeleted = true;
            if (clanKluba != null)
                clanKluba.isDeleted = true;

            if (osoba != null)
            {
                osoba.isDeleted = true;
                osoba.isAktivnaOsoba = false;
            }
            ctx.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}