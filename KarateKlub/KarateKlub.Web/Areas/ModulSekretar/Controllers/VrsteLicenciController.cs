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

    public class VrsteLicenciController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulSekretar/VrsteLicenci
        public ActionResult Index(int seminarId)
        {
            VrsteLicenciIndexVM model = new VrsteLicenciIndexVM
            {
                vrsteLicenci = ctx.VrsteLicenci.Where(x => x.isDeleted == false).Select(x => new VrstaLicencePodaci
                {
                    Id = x.Id,
                    Naziv = x.Naziv
                }).ToList()

            };
            ViewData["seminarId"] = seminarId;
            return View(model);
        }

        public ActionResult Dodaj(int seminarId)
        {
            VrsteLicenciDodajVM model = new VrsteLicenciDodajVM
            {
                seminarId=seminarId
            };
            return View("Dodaj", model);
        }
        public ActionResult SpremiNovuVrstuLicence(VrsteLicenciDodajVM model)
        {
            VrsteLicenci vrsta = new VrsteLicenci();
            vrsta.isDeleted = false;
            vrsta.Naziv = model.Naziv;
            ctx.VrsteLicenci.Add(vrsta);
            ctx.SaveChanges();
            return RedirectToAction("Index",new {seminarId=model.seminarId });
        }
        public ActionResult Uredi(int vrstaId,int seminarId)
        {
            VrsteLicenci vrsta = ctx.VrsteLicenci.Where(x => x.isDeleted == false && x.Id == vrstaId).FirstOrDefault();
            VrsteLicenciUrediVM model = new VrsteLicenciUrediVM
            {
                Id = vrstaId,
                Naziv = vrsta.Naziv,
                seminarId=seminarId
            };
            return View("Uredi", model);
        }
        public ActionResult SpremiIzmjenuVrsteLicence(VrsteLicenciUrediVM model)
        {
            VrsteLicenci vrsta = ctx.VrsteLicenci.Where(x => x.isDeleted == false && x.Id == model.Id).FirstOrDefault();
            vrsta.Naziv = model.Naziv;
            ctx.SaveChanges();
            return RedirectToAction("Index",new {seminarId=model.seminarId });
        }
        public JsonResult Obrisi(int vrstaId)
        {
            VrsteLicenci vrsta = ctx.VrsteLicenci.Where(x => x.isDeleted == false && x.Id ==vrstaId).FirstOrDefault();

            if (vrsta != null)
                vrsta.isDeleted = true;

            ctx.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}