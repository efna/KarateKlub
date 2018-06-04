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

    public class RazduzenaOpremaKlubaClanoviController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulSekretar/RazduzenaOpremaKlubaClanovi
        public ActionResult Index()
        {
            return View();
        }
        public DateTime KonvertujUDatum_dd_mm_yyyy(string datum)
        {

            int dan = Convert.ToInt32(datum.Substring(0, 2));
            int mjesec = Convert.ToInt32(datum.Substring(3, 2));
            int godina = Convert.ToInt32(datum.Substring(6, 4));
            DateTime uneseniDatum = new DateTime(godina, mjesec, dan);
            return uneseniDatum;


        }
        public ActionResult Dodaj(int zaduzenjeId,int aktivnost)
        {
            RazduzenaOpremaKlubaClanoviDodajVM model = new RazduzenaOpremaKlubaClanoviDodajVM {
                ZaduzenjeOpremeKlubaClanovimaId = zaduzenjeId,
                aktivnost=aktivnost
                
            };
            
            return View("Dodaj", model);
        }

        public ActionResult Uredi(int razduzenjeId, int aktivnost)
        {
            RazduzenaOpremaKlubaClanovi razduzenje = ctx.RazduzenaOpremaKlubaClanovi.Where(x => x.Id == razduzenjeId).FirstOrDefault();
            RazduzenaOpremaKlubaClanoviUrediVM model = new RazduzenaOpremaKlubaClanoviUrediVM
            {
                Id= razduzenjeId,
                isDeleted=razduzenje.isDeleted,
                DatumRazduzenjaOpreme=razduzenje.DatumRazduzenjaOpreme.ToString("dd.MM.yyyy"),
                isRazduzeno=razduzenje.isRazduzeno,
                ZaduzenjeOpremeKlubaClanovimaId=razduzenje.ZaduzenjeOpremeKlubaClanovimaId

            };

            return View("Uredi", model);
        }

        public ActionResult SpremiNovoRazduzenjeOpreme(RazduzenaOpremaKlubaClanoviDodajVM model)
        {
            RazduzenaOpremaKlubaClanovi razduzenje = new RazduzenaOpremaKlubaClanovi();
            razduzenje.isDeleted = false;
            razduzenje.isRazduzeno = true;
            razduzenje.DatumRazduzenjaOpreme = KonvertujUDatum_dd_mm_yyyy(model.DatumRazduzenjaOpreme);
            razduzenje.ZaduzenjeOpremeKlubaClanovimaId = model.ZaduzenjeOpremeKlubaClanovimaId;
            ctx.RazduzenaOpremaKlubaClanovi.Add(razduzenje);
            ctx.SaveChanges();
            ZaduzenjeOpremeKlubaClanovima zaduzenje = ctx.ZaduzenjeOpremeKlubaClanovima.Where(x => x.Id == model.ZaduzenjeOpremeKlubaClanovimaId).FirstOrDefault();
            zaduzenje.isAktivnoZaduzenje = false;
            ctx.SaveChanges();
            return RedirectToAction("Index", "UpravljanjeOpremomKluba", new { brojTaba = 2, aktivnost = model.aktivnost });
            
        }
        public ActionResult SpremiIzmjenuRazduzenjaOpreme(RazduzenaOpremaKlubaClanoviUrediVM model)
        {
            RazduzenaOpremaKlubaClanovi razduzenje = ctx.RazduzenaOpremaKlubaClanovi.Where(x => x.Id == model.Id).FirstOrDefault();
     
            if(model.DatumRazduzenjaOpreme!=null)
            razduzenje.DatumRazduzenjaOpreme = KonvertujUDatum_dd_mm_yyyy(model.DatumRazduzenjaOpreme);
            ctx.SaveChanges();
            return RedirectToAction("Index", "UpravljanjeOpremomKluba", new { brojTaba = 2, aktivnost = model.aktivnost });

        }

    }
}