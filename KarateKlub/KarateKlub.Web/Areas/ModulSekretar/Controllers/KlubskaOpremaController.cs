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

    public class KlubskaOpremaController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulSekretar/KlubskaOprema
        public ActionResult Index()
        {
            KlubskaOpremaIndexVM model = new KlubskaOpremaIndexVM {
                klubskaOprema=ctx.KlubskaOprema.Where(x=>x.isDeleted==false).Select(x=>new KlubskaOpremaPodaci {
                    Id=x.Id,
                    isDeleted=x.isDeleted,
                    Kolicina=x.Kolicina,
                    VrstaOpremeKlubaId=x.VrstaOpremeKlubaId,
                    JedinicaMjereId=x.JedinicaMjereId,
                    VrstaOpreme=x.VrstaOpremeKluba.Naziv,
                    JedinicaMjere=x.JedinicaMjere.Naziv
                }).ToList()
            };
            return View(model);
        }

        public ActionResult Dodaj()
        {
            KlubskaOpremaDodajVM model = new KlubskaOpremaDodajVM {
                vrsteOpremeKluba=BindVrsteOpreme(),
                jediniceMjere=BindJediniceMjere()
            };
            model.vrsteOpremeKluba.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite vrstu opreme-" });
            model.jediniceMjere.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite jedinicu mjere-" });
            return View("Dodaj",model);
        }

        private List<SelectListItem> BindJediniceMjere()
        {
            return ctx.JediniceMjere.OrderBy(x=>x.Naziv).Where(x => x.isDeleted == false).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv }).ToList();

        }

        private List<SelectListItem> BindVrsteOpreme()
        {
            return ctx.VrsteOpremeKluba.OrderBy(x=>x.Naziv).Where(x => x.isDeleted == false).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv }).ToList();

        }

        public ActionResult Uredi(int opremaId)
        {
            KlubskaOprema oprema = ctx.KlubskaOprema.Where(x => x.Id == opremaId).FirstOrDefault();

            KlubskaOpremaUrediVM model = new KlubskaOpremaUrediVM
            {
                vrsteOpremeKluba = BindVrsteOpreme(),
                jediniceMjere = BindJediniceMjere(),
                Id=opremaId,
                Kolicina=oprema.Kolicina.ToString(),
                JedinicaMjereId=oprema.JedinicaMjereId,
                VrstaOpremeKlubaId=oprema.VrstaOpremeKlubaId
            };
            
            model.vrsteOpremeKluba.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite vrstu opreme-" });
            model.jediniceMjere.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite jedinicu mjere-" });
            return View("Uredi",model);

        }
        public ActionResult SpremiNovuOpremu(KlubskaOpremaDodajVM model) {
            KlubskaOprema oprema = new KlubskaOprema();
            oprema.isDeleted = false;
            oprema.JedinicaMjereId = model.JedinicaMjereId;
            oprema.VrstaOpremeKlubaId = model.VrstaOpremeKlubaId;
            oprema.Kolicina =Convert.ToInt32(model.Kolicina);
            ctx.KlubskaOprema.Add(oprema);
            ctx.SaveChanges();

            return RedirectToAction("Index", "UpravljanjeOpremomKluba", new { brojTaba = 1,aktivnost=0 });

        }

        public ActionResult SpremiIzmjenuOpreme(KlubskaOpremaUrediVM model)
        {
            KlubskaOprema oprema = ctx.KlubskaOprema.Where(x => x.Id == model.Id).FirstOrDefault();
            oprema.JedinicaMjereId = model.JedinicaMjereId;
            oprema.VrstaOpremeKlubaId = model.VrstaOpremeKlubaId;
            oprema.Kolicina =Convert.ToInt32(model.Kolicina);
            ctx.SaveChanges();
            return RedirectToAction("Index", "UpravljanjeOpremomKluba", new { brojTaba = 1,aktivnost=0});


        }
        public JsonResult Obrisi(int opremaId)
        {

            KlubskaOprema oprema = ctx.KlubskaOprema.Where(x => x.Id == opremaId).FirstOrDefault();
            oprema.isDeleted = true;
            ctx.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

    }
}