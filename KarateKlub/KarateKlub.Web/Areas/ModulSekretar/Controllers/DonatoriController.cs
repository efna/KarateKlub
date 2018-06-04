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

    public class DonatoriController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulSekretar/Donatori
        public ActionResult Index()
        {

            DonatoriIndexVM model = new DonatoriIndexVM
            {

                listaDonatora = ctx.Donatori.Where(x=>x.isDeleted==false).Select(x => new DonatorPodaci
                {
                    Id = x.Id,
                    isDeleted = x.isDeleted,
                    VrstaLicaId = x.VrstaLicaId,
                    VrstaLica = x.VrstaLica.Naziv,
                    Naziv = x.Naziv,
                    Donator = x.ImeOsobe + " " + x.PrezimeOsobe,
                    KontaktTelefon = x.KontaktTelefon,
                    Email = x.Email,
                    donacija =ctx.Donacije.Where(y=>y.DonatorId==x.Id && y.isDeleted==false).Select(y=>new DonacijaPodaci {
                        OsobaId=y.OsobaId,
                        Korisnik=y.Osoba.Ime+" "+y.Osoba.Prezime,
                        DonatorId=y.DonatorId,
                        Donator=y.Donator.Naziv,
                        BrojUplatnice=y.BrojUplatnice,
                        IznosKMBrojevima=y.IznosKMBrojevima,
                        IznosKMSlovima=y.IznosKMSlovima,
                        DatumUplate=y.DatumUplate,
                        Obrazlozenje=y.Obrazlozenje,
                        Naziv=y.Donator.Naziv

                    }).FirstOrDefault()
                
                }).ToList()
                
      

            };
              return View(model);

     
    }


        public ActionResult GoToUpravljanjeSponzorstvimaDonacijama()
        {
            return RedirectToAction("Index", "UpravljanjeSponzorstvimaDonacijama", new { brojTaba = 2, aktivnost = 0 });
        }

        public ActionResult Dodaj()
        {
            DonatoriDodajVM model = new DonatoriDodajVM
            {
                vrsteLica = BindVrsteLica(),
            };
            model.vrsteLica.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite vrstu lica-" });

            return View("Dodaj", model);
        }

        private List<SelectListItem> BindVrsteLica()
        {
            return ctx.VrsteLica.Where(x => x.isDeleted == false).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv }).ToList();

        }
        public DateTime KonvertujUDatum_dd_mm_yyyy(string datum)
        {

            int dan = Convert.ToInt32(datum.Substring(0, 2));
            int mjesec = Convert.ToInt32(datum.Substring(3, 2));
            int godina = Convert.ToInt32(datum.Substring(6, 4));
            DateTime uneseniDatum = new DateTime(godina, mjesec, dan);
            return uneseniDatum;


        }

        public ActionResult SpremiNovogDonatora(DonatoriDodajVM model)
        {
            int korisnikId = Autentifikacija.GetLogiraniKorisnik(HttpContext).Osoba.Id;
            Donatori donator = new Donatori();
            donator.isDeleted = false;
            donator.Naziv = model.Naziv;
            donator.ImeOsobe = model.ImeOsobe;
            donator.PrezimeOsobe = model.PrezimeOsobe;
            donator.KontaktTelefon = model.KontaktTelefon;
            donator.Email = model.Email;
            donator.VrstaLicaId = model.VrstaLicaId;
            ctx.Donatori.Add(donator);
            ctx.SaveChanges();
            int donatorId = ctx.Donatori.ToList().OrderByDescending(x => x.Id).FirstOrDefault().Id;
            Donacije donacija = new Donacije();
            donacija.isDeleted = false;
            donacija.DonatorId = donatorId;
            donacija.BrojUplatnice = model.BrojPriznanice;
            donacija.IznosKMBrojevima = Convert.ToDecimal(model.IznosKMBrojevima);
            donacija.IznosKMSlovima = model.IznosKMSlovima;
            donacija.DatumUplate = KonvertujUDatum_dd_mm_yyyy(model.DatumUplate);
            donacija.Obrazlozenje = model.Obrazlozenje;
            donacija.OsobaId = korisnikId;
            ctx.Donacije.Add(donacija);
            ctx.SaveChanges();
            return RedirectToAction("Index", "UpravljanjeSponzorstvimaDonacijama", new { brojTaba = 2, aktivnost =0 });
        }

        public ActionResult Uredi(int donatorId)
        {
            Donatori donator = ctx.Donatori.Where(x => x.Id == donatorId).FirstOrDefault();
            Donacije donacija = ctx.Donacije.Where(x => x.DonatorId == donatorId && x.isDeleted == false).FirstOrDefault();
            DonatoriUrediVM model = new DonatoriUrediVM
            {
                Id = donatorId,
                isDeleted = donator.isDeleted,
                Naziv = donator.Naziv,
                ImeOsobe = donator.ImeOsobe,
                PrezimeOsobe = donator.PrezimeOsobe,
                KontaktTelefon = donator.KontaktTelefon,
                Email = donator.Email,
                VrstaLicaId = donator.VrstaLicaId,
                vrsteLica = BindVrsteLica(),
                DatumUplate=donacija.DatumUplate.ToString("dd.MM.yyyy"),
                BrojPriznanice=donacija.BrojUplatnice,
                IznosKMBrojevima=donacija.IznosKMBrojevima.ToString(),
                IznosKMSlovima=donacija.IznosKMSlovima,
                Obrazlozenje=donacija.Obrazlozenje
            };
            model.vrsteLica.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite vrstu lica-" });

            return View("Uredi", model);
        }

        public ActionResult SpremiIzmjenuDonatora(DonatoriUrediVM model)
        {
            Donatori donator = ctx.Donatori.Where(x => x.Id == model.Id).FirstOrDefault();
            Donacije donacija = ctx.Donacije.Where(x => x.DonatorId == model.Id && x.isDeleted == false).FirstOrDefault();

            donator.VrstaLicaId = model.VrstaLicaId;
            donator.ImeOsobe = model.ImeOsobe;
            donator.PrezimeOsobe = model.PrezimeOsobe;
            donator.KontaktTelefon = model.KontaktTelefon;
            donator.Email = model.Email;
            donator.Naziv = model.Naziv;
            donacija.DatumUplate = KonvertujUDatum_dd_mm_yyyy(model.DatumUplate);
            donacija.Obrazlozenje = model.Obrazlozenje;
            donacija.IznosKMBrojevima =Convert.ToDecimal(model.IznosKMBrojevima);
            donacija.IznosKMSlovima = model.IznosKMSlovima;
            donacija.BrojUplatnice = model.BrojPriznanice;
            ctx.SaveChanges();
            return RedirectToAction("Index", "UpravljanjeSponzorstvimaDonacijama", new { brojTaba = 2, aktivnost=0 });

        }

        public JsonResult Obrisi(int donatorId)
        {

            Donatori donator = ctx.Donatori.Where(x => x.Id == donatorId).FirstOrDefault();
            Donacije donacija = ctx.Donacije.Where(x => x.DonatorId == donatorId && x.isDeleted == false).FirstOrDefault();
            if (donacija != null)
                donacija.isDeleted = true;
            if (donator != null)
                donator.isDeleted = true;
            ctx.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}