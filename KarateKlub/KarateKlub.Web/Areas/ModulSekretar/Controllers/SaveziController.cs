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

    public class SaveziController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulSekretar/Savezi
        public ActionResult Index()
        {
            SaveziIndexVM model = new SaveziIndexVM {
                savezi=ctx.Savezi.Where(x=>x.isDeleted==false).Select(x=>new SaveziPodaci {
                    Id=x.Id,
                    Naziv=x.Naziv
                }).ToList()
            };
            return View(model);
        }
        public ActionResult Dodaj()
        {
            SaveziDodajVM model = new SaveziDodajVM {

                
            };
            return View("Dodaj", model);
            
        }
        public ActionResult SpremiNoviSavez(SaveziDodajVM model) {
            Savezi savez = new Savezi();
            savez.isDeleted = false;
            savez.Naziv = model.Naziv;
            ctx.Savezi.Add(savez);
            ctx.SaveChanges();
            return RedirectToAction("Index");
          
        }
        public ActionResult Uredi(int savezId)
        {
            Savezi savez = ctx.Savezi.Where(x => x.isDeleted == false && x.Id == savezId).FirstOrDefault();
            SaveziUrediVM model = new SaveziUrediVM
            {
               Id=savezId,
               Naziv=savez.Naziv

            };
            return View("Uredi", model);

        }
        public ActionResult SpremiIzmjenuSaveza(SaveziUrediVM model)
        {
            Savezi savez = ctx.Savezi.Where(x => x.isDeleted == false && x.Id == model.Id).FirstOrDefault();
            
            savez.Naziv = model.Naziv;
            ctx.SaveChanges();
            return RedirectToAction("Index");

        }
        public JsonResult Obrisi(int savezId)
        {
            Savezi savez = ctx.Savezi.Where(x => x.isDeleted == false && x.Id == savezId).FirstOrDefault();
            if (savez != null)
                savez.isDeleted = true;

            ctx.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

    }
}