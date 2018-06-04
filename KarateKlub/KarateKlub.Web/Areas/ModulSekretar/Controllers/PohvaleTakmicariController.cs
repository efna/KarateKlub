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

    public class PohvaleTakmicariController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulSekretar/PohvaleTakmicari
        public ActionResult Index(int takmicarId, int aktivnost)
        {
            PohvaleTakmicariIndexVM model = new PohvaleTakmicariIndexVM
            {
                pohvaleTakmicara = ctx.PohvaleTakmicari.Where(x=>x.TakmicarId==takmicarId && x.isDeleted==false).Select(x => new PohvalaTakmicaraPodaci
                {
                    Id = x.Id,
                    TakmicarId = x.TakmicarId,
                    isDeleted = x.isDeleted,
                    DodjeljenoOd = x.DodjeljenoOd,
                    DodjeljenoZbog = x.DodjeljenoZbog,
                    DatumDodjele = x.DatumDodjele,
                    MjestoDodjele = x.MjestoDodjele,
                    Obrazlozenje = x.Obrazlozenje

                }).ToList()
            };
            ViewData["takmicarId"] = takmicarId;
            ViewData["aktivnost"] = aktivnost;

            return View(model);
        }
        public ActionResult Dodaj(int takmicarId, int aktivnost)
        {
            PohvaleTakmicariDodajVM model = new PohvaleTakmicariDodajVM
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
        public ActionResult SpremiNovuPohvalu(PohvaleTakmicariDodajVM model)
        {
            PohvaleTakmicari pohvala = new PohvaleTakmicari();
            pohvala.isDeleted = false;
            pohvala.TakmicarId = model.TakmicarId;
            pohvala.DodjeljenoOd = model.DodjeljenoOd;
            pohvala.DodjeljenoZbog = model.DodjeljenoZbog;
            pohvala.MjestoDodjele = model.MjestoDodjele;
            pohvala.Obrazlozenje = model.Obrazlozenje;
            if (model.DatumDodjele != null)
                pohvala.DatumDodjele = KonvertujUDatum_dd_mm_yyyy(model.DatumDodjele);
                ctx.PohvaleTakmicari.Add(pohvala);
            ctx.SaveChanges();
            return RedirectToAction("Index", "UpravljanjeTakmicarskomKnjizicom", new { takmicarId = model.TakmicarId, aktivnost = model.aktivnost, brojTaba = 4 });

        }
        public ActionResult Uredi(int pohvalaId, int aktivnost)
        {
            PohvaleTakmicari pohvala = ctx.PohvaleTakmicari.Where(x => x.Id == pohvalaId).FirstOrDefault();
            PohvaleTakmicariUrediVM model = new PohvaleTakmicariUrediVM
            {
                Id = pohvalaId,
                isDeleted = pohvala.isDeleted,
                TakmicarId = pohvala.TakmicarId,
                DodjeljenoOd = pohvala.DodjeljenoOd,
                DodjeljenoZbog = pohvala.DodjeljenoZbog,
                DatumDodjele = pohvala.DatumDodjele.ToString("dd.MM.yyyy"),
                MjestoDodjele = pohvala.MjestoDodjele,
                Obrazlozenje = pohvala.Obrazlozenje,
                aktivnost=aktivnost
            };

            return View("Uredi", model);
        }
        public ActionResult SpremiIzmjenuPohvale(PohvaleTakmicariUrediVM model)
        {
            PohvaleTakmicari pohvala = ctx.PohvaleTakmicari.Where(x => x.Id == model.Id).FirstOrDefault();
            
            pohvala.DodjeljenoOd = model.DodjeljenoOd;
            pohvala.DodjeljenoZbog = model.DodjeljenoZbog;
            pohvala.MjestoDodjele = model.MjestoDodjele;
            pohvala.Obrazlozenje = model.Obrazlozenje;
            if (model.DatumDodjele != null)
                pohvala.DatumDodjele = KonvertujUDatum_dd_mm_yyyy(model.DatumDodjele);

            ctx.SaveChanges();
            return RedirectToAction("Index", "UpravljanjeTakmicarskomKnjizicom", new { takmicarId = model.TakmicarId, aktivnost = model.aktivnost, brojTaba = 4 });

        }
        public JsonResult Obrisi(int pohvalaId)
        {
            PohvaleTakmicari pohvala = ctx.PohvaleTakmicari.Where(x => x.Id == pohvalaId).FirstOrDefault();
            if(pohvala!=null)
            pohvala.isDeleted = true;
          
            ctx.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}