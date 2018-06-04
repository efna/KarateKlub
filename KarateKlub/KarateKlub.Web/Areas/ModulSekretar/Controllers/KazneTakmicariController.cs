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

    public class KazneTakmicariController : Controller
    {
        MojContext ctx = new MojContext();
        //GET: ModulSekretar/KazneTakmicari
        public ActionResult Index(int takmicarId, int aktivnost)
        {
            KazneTakmicariIndexVM model = new KazneTakmicariIndexVM
            {
                kazneTakmicara = ctx.KazneTakmicari.Where(x => x.TakmicarId == takmicarId && x.isDeleted == false).Select(x => new KaznaTakmicaraPodaci
                {
                    Id = x.Id,
                    TakmicarId = x.TakmicarId,
                    isDeleted = x.isDeleted,
                    DodjeljenoOd = x.DodjeljenoOd,
                    DodjeljenoZbog = x.DodjeljenoZbog,
                    DatumSticanja = x.DatumSticanja,
                    DatumIsteka = x.DatumIsteka,
                    Obrazlozenje = x.Obrazlozenje,
                    MjestoSticanja=x.Mjesto

                }).ToList()
            };
            ViewData["takmicarId"] = takmicarId;
            ViewData["aktivnost"] = aktivnost;

            return View(model);
        }
        public ActionResult Dodaj(int takmicarId, int aktivnost)
        {
            KazneTakmicariDodajVM model = new KazneTakmicariDodajVM
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

        public ActionResult SpremiNovuKaznu(KazneTakmicariDodajVM model)
        {
            KazneTakmicari kazna = new KazneTakmicari();
            kazna.TakmicarId = model.TakmicarId;
            kazna.DodjeljenoOd = model.DodjeljenoOd;
            kazna.DodjeljenoZbog = model.DodjeljenoZbog;
            kazna.Mjesto = model.MjestoSticanja;
            kazna.Obrazlozenje = model.Obrazlozenje;
            if (model.DatumSticanja != null)
                kazna.DatumSticanja = KonvertujUDatum_dd_mm_yyyy(model.DatumSticanja);
            if (model.DatumIsteka != null)
                kazna.DatumIsteka = KonvertujUDatum_dd_mm_yyyy(model.DatumIsteka);
            ctx.KazneTakmicari.Add(kazna);
            ctx.SaveChanges();

            return RedirectToAction("Index", "UpravljanjeTakmicarskomKnjizicom", new { takmicarId = model.TakmicarId, aktivnost = model.aktivnost, brojTaba = 6 });
        }
        public ActionResult Uredi(int kaznaId, int aktivnost)
        {
            KazneTakmicari kazna = ctx.KazneTakmicari.Where(x => x.Id == kaznaId).FirstOrDefault();
            KazneTakmicariUrediVM model = new KazneTakmicariUrediVM
            {
                Id = kaznaId,
                isDeleted = kazna.isDeleted,
                TakmicarId = kazna.TakmicarId,
                DodjeljenoOd = kazna.DodjeljenoOd,
                DodjeljenoZbog = kazna.DodjeljenoZbog,
                DatumSticanja = kazna.DatumSticanja.ToString("dd.MM.yyyy"),
                DatumIsteka=kazna.DatumIsteka.ToString("dd.MM.yyyy"),
                MjestoSticanja = kazna.Mjesto,
                Obrazlozenje = kazna.Obrazlozenje,
                aktivnost = aktivnost
            };

            return View("Uredi", model);
        }
        public ActionResult SpremiIzmjenuKazne(KazneTakmicariUrediVM model)
        {
            KazneTakmicari kazna = ctx.KazneTakmicari.Where(x => x.Id == model.Id).FirstOrDefault();


            kazna.DodjeljenoOd = model.DodjeljenoOd;
            kazna.DodjeljenoZbog = model.DodjeljenoZbog;
            kazna.Mjesto = model.MjestoSticanja;
            kazna.Obrazlozenje = model.Obrazlozenje;
            if (model.DatumSticanja != null)
                kazna.DatumSticanja = KonvertujUDatum_dd_mm_yyyy(model.DatumSticanja);
            if (model.DatumIsteka != null)
                kazna.DatumIsteka = KonvertujUDatum_dd_mm_yyyy(model.DatumIsteka);


            ctx.SaveChanges();

            return RedirectToAction("Index", "UpravljanjeTakmicarskomKnjizicom", new { takmicarId = model.TakmicarId, aktivnost = model.aktivnost, brojTaba = 6 });

        }
        public JsonResult Obrisi(int kaznaId)
        {
            KazneTakmicari kazna = ctx.KazneTakmicari.Where(x => x.Id == kaznaId).FirstOrDefault();

            if (kazna != null)
                kazna.isDeleted = true;

            ctx.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}