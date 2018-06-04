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

    public class VrsteOpremeKlubaController : Controller
    {
        MojContext ctx = new MojContext();

        // GET: ModulSekretar/VrsteOpremeKluba
        public ActionResult Index()
        {
            VrsteOpremeKlubaIndexVM model = new VrsteOpremeKlubaIndexVM {
                vrsteOpreme=ctx.VrsteOpremeKluba.Where(x=>x.isDeleted==false).Select(x=>new VrstaOpremePodaci {
                   Id=x.Id,
                   Naziv=x.Naziv
                   
                }).ToList()

            };
            return View(model);
        }

        public ActionResult Dodaj()
        {
            VrsteOpremeKlubaDodajVM model = new VrsteOpremeKlubaDodajVM {

            };
            return View("Dodaj", model);
        }
        public ActionResult SpremiNovuVrstuOpreme(VrsteOpremeKlubaDodajVM model)
        {
            VrsteOpremeKluba vrstaOpreme = new VrsteOpremeKluba();
            vrstaOpreme.isDeleted = false;
            vrstaOpreme.Naziv = model.Naziv;
            ctx.VrsteOpremeKluba.Add(vrstaOpreme);
            ctx.SaveChanges();
            return RedirectToAction("Index");


        }
        public ActionResult Uredi(int vrstaId)
        {

            VrsteOpremeKluba vrsta = ctx.VrsteOpremeKluba.Where(x => x.isDeleted == false && x.Id == vrstaId).FirstOrDefault();
            VrsteOpremeKlubaUrediVM model = new VrsteOpremeKlubaUrediVM {
                Id=vrstaId,
                Naziv=vrsta.Naziv

            };
            return View("Uredi", model);
        }
        public ActionResult SpremiIzmjenuVrsteOpreme(VrsteOpremeKlubaUrediVM model)
        {
            VrsteOpremeKluba vrsta = ctx.VrsteOpremeKluba.Where(x => x.isDeleted == false && x.Id == model.Id).FirstOrDefault();
            vrsta.Naziv = model.Naziv;
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }
        public JsonResult Obrisi(int vrstaId)
        {
            VrsteOpremeKluba vrsta = ctx.VrsteOpremeKluba.Where(x => x.isDeleted == false && x.Id == vrstaId).FirstOrDefault();

            if (vrsta != null)
                vrsta.isDeleted = true;

            ctx.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}