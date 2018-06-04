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

    public class LjekarskiPreglediTakmicaraController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulSekretar/LjekarskiPreglediTakmicara
        public ActionResult Index(int takmicarId,int aktivnost)
        {
            Takmicari takmicar = ctx.Takmicari.Where(x => x.Id == takmicarId).FirstOrDefault();
            LjekarskiPreglediTakmicaraIndexVM model = new LjekarskiPreglediTakmicaraIndexVM {
                ljekarskiPregledi=ctx.LjekarskiPreglediTakmicara.Where(x=>x.TakmicarId==takmicarId && x.isDeleted==false).Select(x=>new LjekarskiPreglediTakmicaraPodaci {
                    Id=x.Id,
                    isDeleted=x.isDeleted,
                    TakmicarId=x.TakmicarId,
                    DatumLjekarskogPregleda=x.DatumLjekarskogPregleda,
                    Dijagnoza=x.Dijagnoza,
                    Doktor=x.Titula+" "+x.ImeDoktora+" "+x.PrezimeDoktora

                }).ToList(),
                Takmicar = takmicar.ClanKluba.Osoba.Ime + " (" + takmicar.ClanKluba.Osoba.ImeRoditelja + ") " + takmicar.ClanKluba.Osoba.Prezime,
                takmicarId=takmicarId


            };
            ViewData["aktivnost"] = aktivnost;
            return View(model);
        }

        public ActionResult Dodaj(int takmicarId,int aktivnost){
            LjekarskiPreglediTakmicaraDodajVM model = new LjekarskiPreglediTakmicaraDodajVM
            {
                TakmicarId=takmicarId,
                aktivnost=aktivnost
            };
            return View("Dodaj", model);
        }
        public DateTime KonvertujUDatum_dd_mm_yyyy(string datum)
        {

            int dan = Convert.ToInt32(datum.Substring(0, 2));
            int mjesec = Convert.ToInt32(datum.Substring(3, 2));
            int godina = Convert.ToInt32(datum.Substring(6, 4));
            DateTime uneseniDatum = new DateTime(godina, mjesec, dan);
            return uneseniDatum;


        }
        public ActionResult SpremiNoviLjekarskiPregled(LjekarskiPreglediTakmicaraDodajVM model)
        {
            LjekarskiPreglediTakmicara ljekarskiPregled = new LjekarskiPreglediTakmicara();
            ljekarskiPregled.isDeleted = false;
            ljekarskiPregled.TakmicarId = model.TakmicarId;
            ljekarskiPregled.Dijagnoza = model.Dijagnoza;
            ljekarskiPregled.ImeDoktora = model.ImeDoktora;
            ljekarskiPregled.PrezimeDoktora = model.PrezimeDoktora;
            ljekarskiPregled.Titula = model.Titula;
            if (model.DatumLjekarskogPregleda != null)
                ljekarskiPregled.DatumLjekarskogPregleda = KonvertujUDatum_dd_mm_yyyy(model.DatumLjekarskogPregleda);
            ctx.LjekarskiPreglediTakmicara.Add(ljekarskiPregled);
            ctx.SaveChanges();
            return RedirectToAction("Index", "UpravljanjeTakmicarskomKnjizicom", new { takmicarId = model.TakmicarId, aktivnost = model.aktivnost, brojTaba = 2 });


        }
        public ActionResult Uredi(int ljekarskiPregledId,int aktivnost)
        {
            LjekarskiPreglediTakmicara ljekarskiPregled = ctx.LjekarskiPreglediTakmicara.Where(x => x.Id == ljekarskiPregledId).FirstOrDefault();
            LjekarskiPreglediTakmicaraUrediVM model = new LjekarskiPreglediTakmicaraUrediVM {
                Id = ljekarskiPregledId,
                isDeleted = ljekarskiPregled.isDeleted,
                TakmicarId = ljekarskiPregled.TakmicarId,
                DatumLjekarskogPregleda = ljekarskiPregled.DatumLjekarskogPregleda.ToString("dd.MM.yyyy"),
            Dijagnoza=ljekarskiPregled.Dijagnoza,
            ImeDoktora=ljekarskiPregled.ImeDoktora,
            PrezimeDoktora=ljekarskiPregled.PrezimeDoktora,
            Titula=ljekarskiPregled.Titula,
            aktivnost=aktivnost
            };
     
            return View("Uredi", model);

        }
        public ActionResult SpremiIzmjenuLjekarskogPregled(LjekarskiPreglediTakmicaraUrediVM model)
        {
            LjekarskiPreglediTakmicara ljekarskiPregled = ctx.LjekarskiPreglediTakmicara.Where(x => x.Id == model.Id).FirstOrDefault();

     
            ljekarskiPregled.Dijagnoza = model.Dijagnoza;
            ljekarskiPregled.ImeDoktora = model.ImeDoktora;
            ljekarskiPregled.PrezimeDoktora = model.PrezimeDoktora;
            ljekarskiPregled.Titula = model.Titula;
            if (model.DatumLjekarskogPregleda != null)
                ljekarskiPregled.DatumLjekarskogPregleda = KonvertujUDatum_dd_mm_yyyy(model.DatumLjekarskogPregleda);
            ctx.SaveChanges();
            return RedirectToAction("Index", "UpravljanjeTakmicarskomKnjizicom", new { takmicarId = model.TakmicarId, aktivnost = model.aktivnost, brojTaba = 2 });


        }
        public JsonResult Obrisi(int ljekarskiPregledId)
        {

            LjekarskiPreglediTakmicara ljekarskiPregled = ctx.LjekarskiPreglediTakmicara.Where(x => x.Id == ljekarskiPregledId).FirstOrDefault();
            if (ljekarskiPregled != null)
                ljekarskiPregled.isDeleted = true;
            ctx.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}