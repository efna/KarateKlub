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

    public class UpisiController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulSekretar/Upisi
        public ActionResult Index()
        {
            UpisiIndexVM model = new UpisiIndexVM
            {
                upisi = ctx.Upisi.Where(x => x.isDeleted == false).Select(x => new UpisPodaci
                {
                    Id = x.Id,
                    isDeleted = x.isDeleted,
                    DatumDo = x.DatumDo,
                    DatumOd = x.DatumOd,
                    Naziv = x.Naziv
                }).ToList()
            };
            return View(model);
        }
        public ActionResult Dodaj()
        {
            UpisiDodajVM model = new UpisiDodajVM
            {
            };
            return View("Dodaj", model);
        }

        private List<SelectListItem> BindClanoveKluba()
        {

                return ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Osoba.Ime + " (" + x.Osoba.ImeRoditelja + ") " + x.Osoba.Prezime }).ToList();
        }

        public DateTime KonvertujUDatum_dd_mm_yyyy(string datum)
        {

            int dan = Convert.ToInt32(datum.Substring(0, 2));
            int mjesec = Convert.ToInt32(datum.Substring(3, 2));
            int godina = Convert.ToInt32(datum.Substring(6, 4));
            DateTime uneseniDatum = new DateTime(godina, mjesec, dan);
            return uneseniDatum;


        }
        public ActionResult SpremiNoviUpis(UpisiDodajVM model)
        {
            Upisi upis = new Upisi();
            upis.isDeleted = false;
            upis.Naziv = model.Naziv;
            upis.DatumOd = KonvertujUDatum_dd_mm_yyyy(model.DatumOd);
            upis.DatumDo = KonvertujUDatum_dd_mm_yyyy(model.DatumDo);
            ctx.Upisi.Add(upis);
            ctx.SaveChanges();
           

            return RedirectToAction("Index", "UpravljanjeUpisima", new { brojTaba = 1 });
        }
        public ActionResult Uredi(int upisId)
        {
            Upisi upis = ctx.Upisi.Where(x => x.Id == upisId).FirstOrDefault();
            UpisiUrediVM model = new UpisiUrediVM
            {
                Id = upisId,
                isDeleted = upis.isDeleted,
                Naziv = upis.Naziv,
                DatumOd = upis.DatumOd.ToString("dd.MM.yyyy"),
                DatumDo = upis.DatumDo.ToString("dd.MM.yyyy"),
                clanoviKluba=BindClanoveKluba()
            };
            return View("Uredi", model);
        }
        public ActionResult SpremiIzmjenuUpisa(UpisiUrediVM model)
        {
            Upisi upis = ctx.Upisi.Where(x => x.Id == model.Id).FirstOrDefault();

            upis.Naziv = model.Naziv;
            upis.DatumOd = KonvertujUDatum_dd_mm_yyyy(model.DatumOd);
            upis.DatumDo = KonvertujUDatum_dd_mm_yyyy(model.DatumDo);
            ctx.SaveChanges();
            return RedirectToAction("Index", "UpravljanjeUpisima", new { brojTaba = 1 });


        }
        public JsonResult Obrisi(int upisId)
        {
            Upisi upis = ctx.Upisi.Where(x => x.Id == upisId).FirstOrDefault();
            List<Upisnine> upisnine = ctx.Upisnine.Where(x => x.isDeleted == false && x.UpisId == upisId).ToList();
            List<UpisaniClanovi> upisaniClanovi = ctx.UpisaniClanovi.Where(x => x.isDeleted == false && x.UpisId == upisId).ToList();

            for (int i = 0; i < upisnine.Count(); i++)
            {
                upisnine[i].isDeleted = true;
                ctx.SaveChanges();
            }
            for (int i = 0; i < upisaniClanovi.Count(); i++)
            {
                upisaniClanovi[i].isDeleted = true;
                ctx.SaveChanges();
            }
            upis.isDeleted = true;
            ctx.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}