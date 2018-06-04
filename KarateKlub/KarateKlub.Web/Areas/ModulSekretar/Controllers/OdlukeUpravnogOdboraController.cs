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

    public class OdlukeUpravnogOdboraController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulSekretar/OdlukeUpravnogOdbora
        public ActionResult Index(int sjednicaId)
        {
            OdlukeUpravnogOdboraIndexVM model = new OdlukeUpravnogOdboraIndexVM {
               odlukeUpravnogOdbora=ctx.OdlukeUpravnogOdbora.Where(x=>x.SjednicaUpravnogOdboraId==sjednicaId && x.isDeleted==false).Select(x=>new OdlukeUpravnogOdboraPodaci {
                   Id=x.Id,
                   isDeleted=x.isDeleted,
                   SjednicaUpravnogOdboraId=x.SjednicaUpravnogOdboraId,
                   DonesenaOdluka=x.DonesenaOdluka,
                   Obrazlozenje=x.Obrazlozenje


               }).ToList(),
               SjednicaId=sjednicaId
            };
            return View(model);
        }

        public JsonResult Obrisi(int odlukaId)
        {

            OdlukeUpravnogOdbora odluka = ctx.OdlukeUpravnogOdbora.Where(x => x.Id == odlukaId).FirstOrDefault();
            odluka.isDeleted = true;
            ctx.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Dodaj(int sjednicaId)
        {
            OdlukeUpravnogOdboraDodajVM model = new OdlukeUpravnogOdboraDodajVM {
                SjednicaUpravnogOdboraId=sjednicaId
            };
            return View("Dodaj",model);
        }
       
        public ActionResult SpremiNovuOdluku(OdlukeUpravnogOdboraDodajVM model)
        {
            OdlukeUpravnogOdbora odluka = new OdlukeUpravnogOdbora();
            odluka.isDeleted = false;
            odluka.DonesenaOdluka = model.DonesenaOdluka;
            odluka.Obrazlozenje = model.Obrazlozenje;
            odluka.SjednicaUpravnogOdboraId = model.SjednicaUpravnogOdboraId;
            ctx.OdlukeUpravnogOdbora.Add(odluka);
            ctx.SaveChanges();
            return RedirectToAction("Detalji", "SjedniceUpravnogOdbora", new {sjednicaId=model.SjednicaUpravnogOdboraId,brojTaba=2});
        }
        public ActionResult Uredi(int odlukaId)
        {
            OdlukeUpravnogOdbora odluka = ctx.OdlukeUpravnogOdbora.Where(x => x.Id == odlukaId).FirstOrDefault();

            OdlukeUpravnogOdboraUrediVM model = new OdlukeUpravnogOdboraUrediVM {
                Id=odluka.Id,
                DonesenaOdluka=odluka.DonesenaOdluka,
                Obrazlozenje=odluka.Obrazlozenje,
                isDeleted=odluka.isDeleted,
                SjednicaUpravnogOdboraId=odluka.SjednicaUpravnogOdboraId
            };
            return View("Uredi", model);
        }

        public ActionResult SpremiIzmjenuOdluke(OdlukeUpravnogOdboraUrediVM model)
        {
            OdlukeUpravnogOdbora odluka = ctx.OdlukeUpravnogOdbora.Where(x => x.Id == model.Id).FirstOrDefault();

            odluka.isDeleted = model.isDeleted;
            odluka.DonesenaOdluka = model.DonesenaOdluka;
            odluka.Obrazlozenje = model.Obrazlozenje;
            odluka.SjednicaUpravnogOdboraId = model.SjednicaUpravnogOdboraId;
            ctx.SaveChanges();
            return RedirectToAction("Detalji", "SjedniceUpravnogOdbora", new { sjednicaId = model.SjednicaUpravnogOdboraId,brojTaba=2 });
        }

    }
}