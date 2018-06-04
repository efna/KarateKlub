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

    public class ProtokoliController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulSekretar/Protokoli
        public ActionResult Index()
        {
            ProtokoliIndexVM model = new ProtokoliIndexVM
            {
                listaProtokola = ctx.Protokol.Select(x => new ProtokolPodaci
                {
                    Id = x.Id,
                    OsobaId = x.OsobaId,
                    OsobaImePrezime = x.Osoba.Ime + " " + x.Osoba.Prezime,
                    isDeleted = x.isDeleted,
                    Naziv = x.Naziv,
                    Godina = x.Godina
                }).Where(x => x.isDeleted == false).ToList()
               
            };

            return View(model);
        }
      
        public ActionResult Dodaj(bool IzIzbornika = true)
        {
            ProtokoliDodajVM model = new ProtokoliDodajVM
            {
                OsobaId = Autentifikacija.GetLogiraniKorisnik(HttpContext).OsobaId
            };
            ViewData["opcija"] = IzIzbornika;
            return View("Dodaj", model);
        }

        public ActionResult Uredi(int protokolId) {

            Protokol protokol = ctx.Protokol.Where(x => x.Id == protokolId).FirstOrDefault();
            ProtokoliUrediVM model = new ProtokoliUrediVM
            {
                Id = protokol.Id,
                OsobaId=protokol.OsobaId,
                IsDeleted = protokol.isDeleted,
                Naziv = protokol.Naziv,
                Godina = protokol.Godina.ToString()

            }; 
           
            return View("Uredi",model);
        }

    
        public JsonResult Obrisi(int protokolId)
        {
            
           
              ctx.Protokol.Where(x => x.Id == protokolId).FirstOrDefault().isDeleted = true;
                ctx.SaveChanges();
                List<StavkeProtokola> stavkeProtokola = ctx.StavkeProtokola.Where(x => x.ProtokolId == protokolId).ToList();
                for (int i = 0; i < stavkeProtokola.Count(); i++)
            {
                stavkeProtokola[i].isDeleted = true;
                ctx.SaveChanges();
            }
            List<Dokumenti> dokumenti = ctx.Dokumenti.Where(x => x.StavkaProtokola.ProtokolId == protokolId).ToList();
            for (int i = 0; i < dokumenti.Count(); i++)
            {
                dokumenti[i].isDeleted = true;
                ctx.SaveChanges();
                
            }
           
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SpremiIzmjenu(ProtokoliUrediVM model)
        {
            Protokol protokol = ctx.Protokol.Where(x => x.Id == model.Id).FirstOrDefault();
            protokol.Naziv = model.Naziv;
            protokol.Godina = Convert.ToInt32(model.Godina);
            ctx.SaveChanges();
            return RedirectToAction("Index", "UpravljanjeProtokolima", new { brojTaba = 1 });

        }

        public ActionResult SpremiNoviProtokol(ProtokoliDodajVM model)
        {
            Protokol protokol = new Protokol();
            protokol.isDeleted = false;
            protokol.Naziv = model.Naziv;
            protokol.Godina =Convert.ToInt32(model.Godina);
            protokol.OsobaId = model.OsobaId;
            ctx.Protokol.Add(protokol);
            ctx.SaveChanges();


            return RedirectToAction("Index", "UpravljanjeProtokolima", new {brojTaba=1 });
            
        }


    }
}