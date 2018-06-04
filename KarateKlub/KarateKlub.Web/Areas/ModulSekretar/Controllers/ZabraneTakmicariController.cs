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

    public class ZabraneTakmicariController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulSekretar/ZabraneTakmicari
        public ActionResult Index(int takmicarId, int aktivnost)
        {
            ZabraneTakmicariIndexVM model = new ZabraneTakmicariIndexVM
            {
                zabraneTakmicara = ctx.ZabraneTakmicari.Where(x => x.TakmicarId == takmicarId && x.isDeleted == false).Select(x => new ZabranaTakmicaraPodaci
                {
                    Id = x.Id,
                    TakmicarId = x.TakmicarId,
                    isDeleted = x.isDeleted,
                    DodjeljenoOd = x.DodjeljenoOd,
                    DodjeljenoZbog = x.DodjeljenoZbog,
                    DatumSticanja = x.DatumSticanja,
                    DatumIsteka = x.DatumIsteka,
                    Obrazlozenje = x.Obrazlozenje,
                    MjestoSticanja = x.MjestoSticanja

                }).ToList()
            };
            ViewData["takmicarId"] = takmicarId;
            ViewData["aktivnost"] = aktivnost;

            return View(model);
        }
        public ActionResult Dodaj(int takmicarId, int aktivnost)
        {
            ZabraneTakmicariDodajVM model = new ZabraneTakmicariDodajVM
            {
                TakmicarId = takmicarId,
                aktivnost = aktivnost
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

        public ActionResult SpremiNovuZabranu(ZabraneTakmicariDodajVM model)
        {
            ZabraneTakmicari zabrana = new ZabraneTakmicari();
            zabrana.TakmicarId = model.TakmicarId;
            zabrana.DodjeljenoOd = model.DodjeljenoOd;
            zabrana.DodjeljenoZbog = model.DodjeljenoZbog;
            zabrana.MjestoSticanja = model.MjestoSticanja;
            zabrana.Obrazlozenje = model.Obrazlozenje;
            if (model.DatumSticanja != null)
                zabrana.DatumSticanja = KonvertujUDatum_dd_mm_yyyy(model.DatumSticanja);
            if (model.DatumIsteka != null)
                zabrana.DatumIsteka = KonvertujUDatum_dd_mm_yyyy(model.DatumIsteka);
            ctx.ZabraneTakmicari.Add(zabrana);
            ctx.SaveChanges();

            return RedirectToAction("Index", "UpravljanjeTakmicarskomKnjizicom", new { takmicarId = model.TakmicarId, aktivnost = model.aktivnost, brojTaba = 7 });
        }
        public ActionResult Uredi(int zabranaId, int aktivnost)
        {
            ZabraneTakmicari zabrana = ctx.ZabraneTakmicari.Where(x => x.Id == zabranaId).FirstOrDefault();
            ZabraneTakmicariUrediVM model = new ZabraneTakmicariUrediVM
            {
                Id = zabranaId,
                isDeleted = zabrana.isDeleted,
                TakmicarId = zabrana.TakmicarId,
                DodjeljenoOd = zabrana.DodjeljenoOd,
                DodjeljenoZbog = zabrana.DodjeljenoZbog,
                DatumSticanja = zabrana.DatumSticanja.ToString("dd.MM.yyyy"),
                DatumIsteka = zabrana.DatumIsteka.ToString("dd.MM.yyyy"),
                MjestoSticanja = zabrana.MjestoSticanja,
                Obrazlozenje = zabrana.Obrazlozenje,
                aktivnost = aktivnost
            };

            return View("Uredi", model);
        }
        public ActionResult SpremiIzmjenuZabrane(ZabraneTakmicariUrediVM model)
        {
            ZabraneTakmicari zabrana = ctx.ZabraneTakmicari.Where(x => x.Id == model.Id).FirstOrDefault();


            zabrana.DodjeljenoOd = model.DodjeljenoOd;
            zabrana.DodjeljenoZbog = model.DodjeljenoZbog;
            zabrana.MjestoSticanja = model.MjestoSticanja;
            zabrana.Obrazlozenje = model.Obrazlozenje;
            if (model.DatumSticanja != null)
                zabrana.DatumSticanja = KonvertujUDatum_dd_mm_yyyy(model.DatumSticanja);
            if (model.DatumIsteka != null)
                zabrana.DatumIsteka = KonvertujUDatum_dd_mm_yyyy(model.DatumIsteka);


            ctx.SaveChanges();

            return RedirectToAction("Index", "UpravljanjeTakmicarskomKnjizicom", new { takmicarId = model.TakmicarId, aktivnost = model.aktivnost, brojTaba = 7 });

        }
        public JsonResult Obrisi(int zabranaId)
        {
            ZabraneTakmicari zabrana = ctx.ZabraneTakmicari.Where(x => x.Id == zabranaId).FirstOrDefault();
            
            if (zabrana != null)
                zabrana.isDeleted = true;

            ctx.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}