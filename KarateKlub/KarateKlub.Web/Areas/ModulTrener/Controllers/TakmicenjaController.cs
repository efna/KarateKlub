using KarateKlub.Data;
using KarateKlub.Data.Models;
using KarateKlub.Web.Areas.ModulTrener.Models;
using KarateKlub.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulTrener.Controllers
{
    [Autorizacija(false, false, false, true, false)]

    public class TakmicenjaController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulTrener/Takmicenja
        public ActionResult Index()
        {
            TakmicenjaIndexVM model = new TakmicenjaIndexVM
            {
                takmicenja = ctx.Takmicenja.Where(x => x.isDeleted == false).Select(x => new TakmicenjePodaci
                {
                    Id = x.Id,
                    isDeleted = x.isDeleted,
                    NazivTakmicenja = x.NazivTakmicenja,
                    DatumOdrzavanjaTakmicenja = x.DatumOdrzavanjaTakmicenja,
                    MjestoOdrzavanjaTakmicenja = x.MjestoOdrzavanjaTakmicenja,
                    OrganizatorTakmicenja = x.OrganizatorTakmicenja,
                    Savez = x.Savez.Naziv,
                    SavezId = x.SavezId

                }).ToList()

            };
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
        public ActionResult Dodaj()
        {
            TakmicenjaDodajVM model = new TakmicenjaDodajVM
            {
                savezi = BindSaveze()
            };
            model.savezi.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite savez-" });

            return View("Dodaj", model);
        }

        private List<SelectListItem> BindSaveze()
        {
            return ctx.Savezi.Where(x => x.isDeleted == false).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv }).ToList();

        }

        public ActionResult SpremiNovoTakmicenje(TakmicenjaDodajVM model)
        {
            Takmicenja takmicenje = new Takmicenja();
            takmicenje.isDeleted = false;
            takmicenje.MjestoOdrzavanjaTakmicenja = model.MjestoOdrzavanjaTakmicenja;
            takmicenje.NazivTakmicenja = model.NazivTakmicenja;
            takmicenje.OrganizatorTakmicenja = model.OrganizatorTakmicenja;
            takmicenje.SavezId = model.SavezId;
            if (model.DatumOdrzavanjaTakmicenja != null)
                takmicenje.DatumOdrzavanjaTakmicenja = KonvertujUDatum_dd_mm_yyyy(model.DatumOdrzavanjaTakmicenja);
            ctx.Takmicenja.Add(takmicenje);
            ctx.SaveChanges();

            return RedirectToAction("Index", "UpravljanjeTakmicenjimaTakmicarima", new { brojTaba = 1, aktivnost = 0 });

        }

        public ActionResult Uredi(int takmicenjeId)
        {
            Takmicenja takmicenje = ctx.Takmicenja.Where(x => x.Id == takmicenjeId).FirstOrDefault();
            TakmicenjaUrediVM model = new TakmicenjaUrediVM
            {
                Id = takmicenjeId,
                isDeleted = takmicenje.isDeleted,
                DatumOdrzavanjaTakmicenja = takmicenje.DatumOdrzavanjaTakmicenja.ToString("dd.MM.yyyy"),
                MjestoOdrzavanjaTakmicenja = takmicenje.MjestoOdrzavanjaTakmicenja,
                OrganizatorTakmicenja = takmicenje.OrganizatorTakmicenja,
                NazivTakmicenja = takmicenje.NazivTakmicenja,
                SavezId = takmicenje.SavezId,
                savezi = BindSaveze()
            };
            model.savezi.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite savez-" });

            return View("Uredi", model);
        }


        public ActionResult SpremiIzmjenuTakmicenja(TakmicenjaUrediVM model)
        {
            Takmicenja takmicenje = ctx.Takmicenja.Where(x => x.Id == model.Id).FirstOrDefault();

            takmicenje.MjestoOdrzavanjaTakmicenja = model.MjestoOdrzavanjaTakmicenja;
            takmicenje.NazivTakmicenja = model.NazivTakmicenja;
            takmicenje.OrganizatorTakmicenja = model.OrganizatorTakmicenja;
            takmicenje.SavezId = model.SavezId;
            if (model.DatumOdrzavanjaTakmicenja != null)
                takmicenje.DatumOdrzavanjaTakmicenja = KonvertujUDatum_dd_mm_yyyy(model.DatumOdrzavanjaTakmicenja);
            ctx.SaveChanges();

            return RedirectToAction("Index", "UpravljanjeTakmicenjimaTakmicarima", new { brojTaba = 1, aktivnost = 0 });

        }
        public JsonResult Obrisi(int takmicenjeId)
        {

            Takmicenja takmicenje = ctx.Takmicenja.Where(x => x.Id == takmicenjeId).FirstOrDefault();
            List<RezultatiTakmicenja> rezultatiTakmicenje = ctx.RezultatiTakmicenja.Where(x => x.isDeleted == false && x.TakmicenjeId == takmicenjeId).ToList();
            List<TroskoviTakmicenja> troskoviTakmicenja = ctx.TroskoviTakmicenja.Where(x => x.isDeleted == false && x.TakmicenjeId == takmicenjeId).ToList();
            for (int i = 0; i < rezultatiTakmicenje.Count(); i++)
            {
                rezultatiTakmicenje[i].isDeleted = true;
                ctx.SaveChanges();

            }
            for (int i = 0; i < troskoviTakmicenja.Count(); i++)
            {
                troskoviTakmicenja[i].isDeleted = true;
                ctx.SaveChanges();

            }
            takmicenje.isDeleted = true;
            ctx.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}