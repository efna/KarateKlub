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

    public class StavkeNarudzbeOpremeKlubaController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulSekretar/StavkeNarudzbeOpremeKluba
        public ActionResult Index(int narudzbaId)
        {
            StavkeNarudzbeOpremeKlubaIndexVM model = new StavkeNarudzbeOpremeKlubaIndexVM {
                stavkeNarudzbeOpremeKluba=ctx.StavkeNarudzbeOpremeKluba.Where(x=>x.NarudzbaOpremeKlubaId==narudzbaId && x.isDeleted==false).Select(x=>new StavkeNarudzbeOpremeKlubaPodaci {
                    Id=x.Id,
                    isDeleted=x.isDeleted,
                    VrstaOpremeKlubaId=x.VrstaOpremeKlubaId,
                    NarudzbaOpremeKlubaId=x.NarudzbaOpremeKlubaId,
                    KolicinaNabavljeneOpreme=x.KolicinaNabavljeneOpreme,
                    JedinicaMjereId=x.JedinicaMjereId,
                    MarkaNabavljeneOpreme=x.MarkaNabavljeneOpreme,
                    IsWkfEkfApproved=x.IsWkfEkfApproved,
                    VrstaOpreme=x.VrstaOpremeKluba.Naziv,
                    JedinicaMjere=x.JedinicaMjere.Naziv
                }).ToList(),
                narudzbaId=narudzbaId
            };
            return View(model);
        }

        public ActionResult Dodaj(int narudzbaId)
        {
            StavkeNarudzbeOpremeKlubaDodajVM model = new StavkeNarudzbeOpremeKlubaDodajVM
            {
                NarudzbaOpremeKlubaId = narudzbaId,
                vrsteOpremeKluba = BindVrsteOpreme(),
                jediniceMjere = BindJediniceMjere(),
                IsWkfEkfApproved = false
            };
            model.vrsteOpremeKluba.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite vrstu opreme-" });
            model.jediniceMjere.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite jedinicu mjere-" });

            return View("Dodaj", model);
        }

        private List<SelectListItem> BindJediniceMjere()
        {
            return ctx.JediniceMjere.Where(x=>x.isDeleted==false).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv }).ToList();

        }

        private List<SelectListItem> BindVrsteOpreme()
        {
            return ctx.VrsteOpremeKluba.Where(x=>x.isDeleted==false).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv }).ToList();

        }

        public ActionResult Uredi(int stavkaId)
        {
            StavkeNarudzbeOpremeKluba stavka = ctx.StavkeNarudzbeOpremeKluba.Where(x => x.Id == stavkaId).FirstOrDefault();

            StavkeNarudzbeOpremeKlubaUrediVM model = new StavkeNarudzbeOpremeKlubaUrediVM
            {
                Id = stavkaId,
                NarudzbaOpremeKlubaId=stavka.NarudzbaOpremeKlubaId,
                isDeleted=stavka.isDeleted,
                JedinicaMjereId=stavka.JedinicaMjereId,
                VrstaOpremeKlubaId=stavka.VrstaOpremeKlubaId,
                IsWkfEkfApproved=stavka.IsWkfEkfApproved,
                KolicinaNabavljeneOpreme=stavka.KolicinaNabavljeneOpreme.ToString(),
                MarkaNabavljeneOpreme=stavka.MarkaNabavljeneOpreme,
                jediniceMjere=BindJediniceMjere(),
                vrsteOpremeKluba=BindVrsteOpreme()

            };
            model.vrsteOpremeKluba.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite vrstu opreme-" });
            model.jediniceMjere.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite jedinicu mjere-" });

            return View("Uredi", model);
        }
        public ActionResult SpremiNovuStavkuNarudzbe(StavkeNarudzbeOpremeKlubaDodajVM model)
        {
            StavkeNarudzbeOpremeKluba stavka = new StavkeNarudzbeOpremeKluba();
            stavka.isDeleted = false;
            stavka.NarudzbaOpremeKlubaId = model.NarudzbaOpremeKlubaId;
            stavka.JedinicaMjereId = model.JedinicaMjereId;
            stavka.KolicinaNabavljeneOpreme =Convert.ToInt32(model.KolicinaNabavljeneOpreme);
            stavka.VrstaOpremeKlubaId = model.VrstaOpremeKlubaId;
            stavka.MarkaNabavljeneOpreme = model.MarkaNabavljeneOpreme;
            stavka.IsWkfEkfApproved = model.IsWkfEkfApproved;
            ctx.StavkeNarudzbeOpremeKluba.Add(stavka);
            ctx.SaveChanges();
            return RedirectToAction("Index", "StavkeNarudzbeOpremeKluba", new { narudzbaId = model.NarudzbaOpremeKlubaId });
        }
        public ActionResult SpremiIzmjenuNarudzbe(StavkeNarudzbeOpremeKlubaUrediVM model)
        {
            StavkeNarudzbeOpremeKluba stavka = ctx.StavkeNarudzbeOpremeKluba.Where(x => x.Id == model.Id).FirstOrDefault();

            if (stavka != null)
            {
                stavka.JedinicaMjereId = model.JedinicaMjereId;
                stavka.KolicinaNabavljeneOpreme =Convert.ToInt32(model.KolicinaNabavljeneOpreme);
                stavka.VrstaOpremeKlubaId = model.VrstaOpremeKlubaId;
                stavka.MarkaNabavljeneOpreme = model.MarkaNabavljeneOpreme;
                stavka.IsWkfEkfApproved = model.IsWkfEkfApproved;
                ctx.SaveChanges();
            }
            return RedirectToAction("Index", "StavkeNarudzbeOpremeKluba", new { narudzbaId = model.NarudzbaOpremeKlubaId });

        }
        public JsonResult Obrisi(int stavkaId)
        {

            StavkeNarudzbeOpremeKluba stavka = ctx.StavkeNarudzbeOpremeKluba.Where(x => x.Id == stavkaId).FirstOrDefault();
            stavka.isDeleted = true;
            ctx.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

    }
}
