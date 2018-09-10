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

    public class StecenaZvanjaController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulSekretar/StecenaZvanja
        public ActionResult Index()
        {
            StecenaZvanjaIndexVM model = new StecenaZvanjaIndexVM {
                stecenaZvanja=ctx.StecenaZvanja.Where(x=>x.isDeleted==false).Select(x=>new StecenoZvanjePodaci {
                    Id=x.Id,
                    isDeleted=x.isDeleted,
                    OsobaId=x.OsobaId,
                    Osoba=x.Osoba.Ime+" ("+x.Osoba.ImeRoditelja+") "+x.Osoba.Prezime,
                    ZvanjeUKarateuId=x.ZvanjeUKarateuId,
                    ZvanjeUKarateu=x.ZvanjeUKarateu.Naziv,
                    DatumSticanja=x.DatumSticanja,
                    Mjesto=x.Mjesto,
                    Organizator=x.Organizator

                }).ToList()
            };
            return View(model);
    
    }

        public DateTime KonvertujUDatum_dd_mm_yyyy(string datum)
        {

            int dan = Convert.ToInt32(datum.Substring(0, 2));
            int mjesec = Convert.ToInt32(datum.Substring(3, 2));
            int godina = Convert.ToInt32(datum.Substring(6, 4));
            DateTime uneseniDatum = new DateTime(godina, mjesec, dan);
            return uneseniDatum;


        }

        public ActionResult Dodaj()
        {
            StecenaZvanjaDodajVM model = new StecenaZvanjaDodajVM {
                ZvanjaUKarateu=BindZvanjaUKarateu(),
                Osobe=BindOsobe()
            };
            model.Osobe.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite osobu-" });

            model.ZvanjaUKarateu.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite zvanje-" });

            return View("Dodaj", model);
        }

        private List<SelectListItem> BindOsobe()
        {
            return ctx.Osoba.OrderBy(x=>x.Ime).Where(x => x.isDeleted == false && x.isAktivnaOsoba == true).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Ime + " (" + x.ImeRoditelja + ") " + x.Prezime }).ToList();

        }

        private List<SelectListItem> BindZvanjaUKarateu()
        {
            return ctx.ZvanjaUKarateu.Where(x=>x.isDeleted==false).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv }).ToList();

        }

        public ActionResult SpremiNovoStecenoZvanje(StecenaZvanjaDodajVM model)
        {
            StecenaZvanja zvanje = new StecenaZvanja();
            zvanje.isDeleted = false;
            zvanje.DatumSticanja = KonvertujUDatum_dd_mm_yyyy(model.DatumSticanja);
            zvanje.ZvanjeUKarateuId = model.ZvanjeUKarateuId;
            zvanje.Mjesto = model.Mjesto;
            zvanje.Organizator = model.Organizator;
            zvanje.OsobaId = model.OsobaId;
            ctx.StecenaZvanja.Add(zvanje);
            ctx.SaveChanges();

            return RedirectToAction("Index", "UpravljanjePolaganjimaStecenimZvanjima", new { brojTaba = 2});

        }

        public ActionResult Uredi(int stecenoZvanjeId)
        {
            StecenaZvanja zvanje = ctx.StecenaZvanja.Where(x => x.Id == stecenoZvanjeId).FirstOrDefault();
            StecenaZvanjaUrediVM model = new StecenaZvanjaUrediVM {
               Id=stecenoZvanjeId,
               isDeleted=zvanje.isDeleted,
               DatumSticanja=zvanje.DatumSticanja.ToString("dd.MM.yyyy"),
               OsobaId=zvanje.OsobaId,
               Mjesto=zvanje.Mjesto,
               Organizator=zvanje.Organizator,
               ZvanjeUKarateuId=zvanje.ZvanjeUKarateuId,
               ZvanjaUKarateu=BindZvanjaUKarateu(),
               Osobe=BindOsobe()

            };
            model.Osobe.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite osobu-" });

            model.ZvanjaUKarateu.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite zvanje-" });

            return View("Uredi", model);
        }
        public ActionResult SpremiIzmjenuStecenogZvanja(StecenaZvanjaUrediVM model)
        {
            StecenaZvanja zvanje = ctx.StecenaZvanja.Where(x => x.Id == model.Id).FirstOrDefault();

            zvanje.DatumSticanja = KonvertujUDatum_dd_mm_yyyy(model.DatumSticanja);
            zvanje.ZvanjeUKarateuId = model.ZvanjeUKarateuId;
            zvanje.Mjesto = model.Mjesto;
            zvanje.Organizator = model.Organizator;
            zvanje.OsobaId = model.OsobaId;
            ctx.SaveChanges();

            return RedirectToAction("Index", "UpravljanjePolaganjimaStecenimZvanjima", new { brojTaba = 2 });

        }
        public JsonResult Obrisi(int stecenoZvanjeId)
        {

            StecenaZvanja zvanje = ctx.StecenaZvanja.Where(x => x.Id == stecenoZvanjeId).FirstOrDefault();
            if (zvanje != null)
                zvanje.isDeleted = true;
            ctx.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }


        public ActionResult PregledStecenihZvanjaClana(int osobaId)
        {
            StecenaZvanjaPregledStecenihZvanjaClanaVM model = new StecenaZvanjaPregledStecenihZvanjaClanaVM {
                stecenaZvanja=ctx.StecenaZvanja.Where(x=>x.isDeleted==false && x.OsobaId==osobaId).Select(x=>new StecenoZvanjePodaci
                {
                    ZvanjeUKarateuId=x.ZvanjeUKarateuId,
                    OsobaId=x.OsobaId,
                    ZvanjeUKarateu=x.ZvanjeUKarateu.Naziv,
                    Osoba=x.Osoba.Ime+" ("+x.Osoba.ImeRoditelja+") "+x.Osoba.Prezime,
                    DatumSticanja=x.DatumSticanja,
                    Mjesto=x.Mjesto,
                    Organizator=x.Organizator
                }).ToList()
            };
            return View("PregledStecenihZvanjaClana",model);
        }
    }
}