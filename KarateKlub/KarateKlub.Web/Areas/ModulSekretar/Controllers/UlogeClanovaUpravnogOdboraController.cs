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

    public class UlogeClanovaUpravnogOdboraController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulSekretar/UlogeClanovaUpravnogOdbora
        public ActionResult Index()
        {
            UlogeClanovaUpravnogOdboraIndexVM model = new UlogeClanovaUpravnogOdboraIndexVM
            {
                uloge = ctx.UlogeClanovaUpravnogOdbora.Where(x => x.isDeleted == false).Select(x => new UlogaPodaci
                {
                    Id = x.Id,
                    Naziv = x.Naziv
                }).ToList()
            };
            return View(model);
        }
        public ActionResult Dodaj()
        {
            UlogeClanovaUpravnogOdboraDodajVM model = new UlogeClanovaUpravnogOdboraDodajVM
            {


            };
            return View("Dodaj", model);

        }
        public ActionResult SpremiNovuUlogu(UlogeClanovaUpravnogOdboraDodajVM model)
        {
            UlogeClanovaUpravnogOdbora uloga = new UlogeClanovaUpravnogOdbora();
            uloga.isDeleted = false;
            uloga.Naziv = model.Naziv;
            ctx.UlogeClanovaUpravnogOdbora.Add(uloga);
            ctx.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult Uredi(int ulogaId)
        {
            UlogeClanovaUpravnogOdbora  uloga = ctx.UlogeClanovaUpravnogOdbora.Where(x => x.isDeleted == false && x.Id == ulogaId).FirstOrDefault();
            UlogeClanovaUpravnogOdboraUrediVM model = new UlogeClanovaUpravnogOdboraUrediVM
            {
                Id = ulogaId,
                Naziv = uloga.Naziv

            };
            return View("Uredi", model);

        }
        public ActionResult SpremiIzmjenuUloge(UlogeClanovaUpravnogOdboraUrediVM model)
        {
            UlogeClanovaUpravnogOdbora uloga = ctx.UlogeClanovaUpravnogOdbora.Where(x => x.isDeleted == false && x.Id == model.Id).FirstOrDefault();

            uloga.Naziv = model.Naziv;
            ctx.SaveChanges();
            return RedirectToAction("Index");

        }
        public JsonResult Obrisi(int ulogaId)
        {
            UlogeClanovaUpravnogOdbora uloga = ctx.UlogeClanovaUpravnogOdbora.Where(x => x.isDeleted == false && x.Id == ulogaId).FirstOrDefault();
            if (uloga != null)
                uloga.isDeleted = true;

            ctx.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}