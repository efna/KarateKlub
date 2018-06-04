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

    public class OtpisanaOpremaKlubaController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulSekretar/OtpisanaOpremaKluba
        public ActionResult Index()
        {
            OtpisanaOpremaKlubaIndexVM model = new OtpisanaOpremaKlubaIndexVM
            {
                otpisanaOpremaKluba = ctx.OtpisanaOpremaKluba.Where(x => x.isDeleted == false).Select(x => new OtpisanaOpremaKlubaPodaci
                {
                    Id = x.Id,
                    isDeleted = x.isDeleted,
                    Kolicina = x.OtpisanaKolicina,
                    VrstaOpremeKlubaId = x.VrstaOpremeKlubaId,
                    JedinicaMjereId = x.JedinicaMjereId,
                    VrstaOpreme = x.VrstaOpremeKluba.Naziv,
                    JedinicaMjere = x.JedinicaMjere.Naziv,
                    DatumOtpisa=x.DatumOtpisaOpreme,
                    Obrazlozenje=x.Obrazlozenje

                }).ToList()
            };
            return View(model);
        }

        public ActionResult Dodaj()
        {
            OtpisanaOpremaKlubaDodajVM model = new OtpisanaOpremaKlubaDodajVM
            {
                vrsteOpremeKluba = BindVrsteOpreme(),
                jediniceMjere = BindJediniceMjere()
            };
            model.vrsteOpremeKluba.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite vrstu opreme-" });
            model.jediniceMjere.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite jedinicu mjere-" });
            return View("Dodaj", model);
        }

        private List<SelectListItem> BindJediniceMjere()
        {
            return ctx.JediniceMjere.Where(x => x.isDeleted == false).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv }).ToList();

        }

        private List<SelectListItem> BindVrsteOpreme()
        {
            return ctx.VrsteOpremeKluba.Where(x => x.isDeleted == false).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv }).ToList();

        }

        public ActionResult Uredi(int opremaId)
        {
            OtpisanaOpremaKluba oprema = ctx.OtpisanaOpremaKluba.Where(x => x.Id == opremaId).FirstOrDefault();

            OtpisanaOpremaKlubaUrediVM model = new OtpisanaOpremaKlubaUrediVM
            {
                vrsteOpremeKluba = BindVrsteOpreme(),
                jediniceMjere = BindJediniceMjere(),
                Id = opremaId,
                OtpisanaKolicina = oprema.OtpisanaKolicina.ToString(),
                JedinicaMjereId = oprema.JedinicaMjereId,
                VrstaOpremeKlubaId = oprema.VrstaOpremeKlubaId,
                DatumOtpisaOpreme=oprema.DatumOtpisaOpreme.ToString("dd.MM.yyyy"),
                Obrazlozenje=oprema.Obrazlozenje
                
            };

            model.vrsteOpremeKluba.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite vrstu opreme-" });
            model.jediniceMjere.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite jedinicu mjere-" });
            return View("Uredi", model);

        }
        public ActionResult SpremiNovuOtpisanuOpremu(OtpisanaOpremaKlubaDodajVM model)
        {
            OtpisanaOpremaKluba oprema = new OtpisanaOpremaKluba();
            oprema.isDeleted = false;
            oprema.JedinicaMjereId = model.JedinicaMjereId;
            oprema.VrstaOpremeKlubaId = model.VrstaOpremeKlubaId;
            oprema.OtpisanaKolicina = Convert.ToInt32(model.OtpisanaKolicina);
            oprema.DatumOtpisaOpreme = KonvertujUDatum_dd_mm_yyyy(model.DatumOtpisaOpreme);
            oprema.Obrazlozenje = model.Obrazlozenje;
            ctx.OtpisanaOpremaKluba.Add(oprema);
            ctx.SaveChanges();

            return RedirectToAction("Index", "UpravljanjeOpremomKluba", new { brojTaba = 3,aktivnost=0});

        }
        public DateTime KonvertujUDatum_dd_mm_yyyy(string datum)
        {

            int dan = Convert.ToInt32(datum.Substring(0, 2));
            int mjesec = Convert.ToInt32(datum.Substring(3, 2));
            int godina = Convert.ToInt32(datum.Substring(6, 4));
            DateTime uneseniDatum = new DateTime(godina, mjesec, dan);
            return uneseniDatum;


        }
        public ActionResult SpremiIzmjenuOtpisaneOpreme(OtpisanaOpremaKlubaUrediVM model)
        {
            OtpisanaOpremaKluba oprema = ctx.OtpisanaOpremaKluba.Where(x => x.Id == model.Id).FirstOrDefault();

            oprema.JedinicaMjereId = model.JedinicaMjereId;
            oprema.VrstaOpremeKlubaId = model.VrstaOpremeKlubaId;
            oprema.OtpisanaKolicina = Convert.ToInt32(model.OtpisanaKolicina);
            oprema.DatumOtpisaOpreme = KonvertujUDatum_dd_mm_yyyy(model.DatumOtpisaOpreme);
            oprema.Obrazlozenje = model.Obrazlozenje;
            ctx.SaveChanges();
            return RedirectToAction("Index", "UpravljanjeOpremomKluba", new { brojTaba = 3,aktivnost=0 });


        }
        public JsonResult Obrisi(int otpisanaOpremaId)
        {
            OtpisanaOpremaKluba oprema = ctx.OtpisanaOpremaKluba.Where(x => x.Id == otpisanaOpremaId).FirstOrDefault();
            oprema.isDeleted = true;
            ctx.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}