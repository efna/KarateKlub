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

    public class NagradeTakmicariController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulTrener/NagradeTakmicari
        public ActionResult Index(int takmicarId, int aktivnost)
        {
            NagradeTakmicariIndexVM model = new NagradeTakmicariIndexVM
            {
                nagradeTakmicara = ctx.NagradeTakmicari.Where(x => x.TakmicarId == takmicarId && x.isDeleted == false).Select(x => new NagradaTakmicaraPodaci
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
            NagradeTakmicariDodajVM model = new NagradeTakmicariDodajVM
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
        public ActionResult SpremiNovuNagradu(NagradeTakmicariDodajVM model)
        {
            NagradeTakmicari nagrada = new NagradeTakmicari();
            nagrada.isDeleted = false;
            nagrada.TakmicarId = model.TakmicarId;
            nagrada.DodjeljenoOd = model.DodjeljenoOd;
            nagrada.DodjeljenoZbog = model.DodjeljenoZbog;
            nagrada.MjestoDodjele = model.MjestoDodjele;
            nagrada.Obrazlozenje = model.Obrazlozenje;
            if (model.DatumDodjele != null)
                nagrada.DatumDodjele = KonvertujUDatum_dd_mm_yyyy(model.DatumDodjele);
            ctx.NagradeTakmicari.Add(nagrada);
            ctx.SaveChanges();

            return RedirectToAction("Index", "UpravljanjeTakmicarskomKnjizicom", new { takmicarId = model.TakmicarId, aktivnost = model.aktivnost, brojTaba = 5 });
        }
        public ActionResult Uredi(int nagradaId, int aktivnost)
        {
            NagradeTakmicari nagrada = ctx.NagradeTakmicari.Where(x => x.Id == nagradaId).FirstOrDefault();
            NagradeTakmicariUrediVM model = new NagradeTakmicariUrediVM
            {
                Id = nagradaId,
                isDeleted = nagrada.isDeleted,
                TakmicarId = nagrada.TakmicarId,
                DodjeljenoOd = nagrada.DodjeljenoOd,
                DodjeljenoZbog = nagrada.DodjeljenoZbog,
                DatumDodjele = nagrada.DatumDodjele.ToString("dd.MM.yyyy"),
                MjestoDodjele = nagrada.MjestoDodjele,
                Obrazlozenje = nagrada.Obrazlozenje,
                aktivnost = aktivnost
            };

            return View("Uredi", model);
        }
        public ActionResult SpremiIzmjenuNagrade(NagradeTakmicariUrediVM model)
        {
            NagradeTakmicari nagrada = ctx.NagradeTakmicari.Where(x => x.Id == model.Id).FirstOrDefault();

            nagrada.DodjeljenoOd = model.DodjeljenoOd;
            nagrada.DodjeljenoZbog = model.DodjeljenoZbog;
            nagrada.MjestoDodjele = model.MjestoDodjele;
            nagrada.Obrazlozenje = model.Obrazlozenje;
            if (model.DatumDodjele != null)
                nagrada.DatumDodjele = KonvertujUDatum_dd_mm_yyyy(model.DatumDodjele);

            ctx.SaveChanges();
            return RedirectToAction("Index", "UpravljanjeTakmicarskomKnjizicom", new { takmicarId = model.TakmicarId, aktivnost = model.aktivnost, brojTaba = 5 });

        }
        public JsonResult Obrisi(int nagradaId)
        {
            NagradeTakmicari nagrada = ctx.NagradeTakmicari.Where(x => x.Id == nagradaId).FirstOrDefault();
            if (nagrada != null)
                nagrada.isDeleted = true;

            ctx.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}