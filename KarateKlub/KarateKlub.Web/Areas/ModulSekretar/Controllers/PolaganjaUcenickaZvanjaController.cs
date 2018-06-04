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

    public class PolaganjaUcenickaZvanjaController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulSekretar/PolaganjaUcenickaZvanja
        public ActionResult Index()
        {
            PolaganjaUcenickaZvanjaIndexVM model = new PolaganjaUcenickaZvanjaIndexVM {
                polaganjaUcenickaZvanja=ctx.PolaganjaUcenickaZvanja.Where(x=>x.isDeleted==false).Select(x=>new PolaganjeUcenickaZvanjaPodaci {
                    Id=x.Id,
                    isDeleted=x.isDeleted,
                    DatumPolaganja=x.DatumPolaganja,
                    MjestoPolaganja=x.MjestoPolaganja,
                    OrganizatorPolaganja=x.OrganizatorPolaganja,
                    Obrazlozenje=x.Obrazlozenje
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
            PolaganjaUcenickaZvanjaDodajVM model = new PolaganjaUcenickaZvanjaDodajVM {
               
            };

            return View("Dodaj", model);
        }

     

        public ActionResult SpremiNovoPolaganjeUcenickaZvanja(PolaganjaUcenickaZvanjaDodajVM model)
        {
            PolaganjaUcenickaZvanja polaganje = new PolaganjaUcenickaZvanja();
            polaganje.isDeleted = false;
            polaganje.DatumPolaganja = KonvertujUDatum_dd_mm_yyyy(model.DatumPolaganja);
            polaganje.OrganizatorPolaganja = model.OrganizatorPolaganja;
            polaganje.MjestoPolaganja = model.MjestoPolaganja;
            polaganje.Obrazlozenje = model.Obrazlozenje;
            ctx.PolaganjaUcenickaZvanja.Add(polaganje);
            ctx.SaveChanges();

            return RedirectToAction("Index", "UpravljanjePolaganjimaStecenimZvanjima",new {brojTaba=1 });
 
        }

        public ActionResult Uredi(int polaganjeId)
        {
            PolaganjaUcenickaZvanja polaganje = ctx.PolaganjaUcenickaZvanja.Where(x => x.Id == polaganjeId).FirstOrDefault();
            PolaganjaUcenickaZvanjaUrediVM model = new PolaganjaUcenickaZvanjaUrediVM {
                Id = polaganjeId,
                isDeleted = polaganje.isDeleted,
                DatumPolaganja = polaganje.DatumPolaganja.ToString("dd.MM.yyyy"),
                MjestoPolaganja=polaganje.MjestoPolaganja,
                Obrazlozenje=polaganje.Obrazlozenje,
                OrganizatorPolaganja=polaganje.OrganizatorPolaganja

            };
            return View("Uredi",model);
    }
        public ActionResult SpremiIzmjenPolaganjeUcenickaZvanja(PolaganjaUcenickaZvanjaUrediVM model)
        {
            PolaganjaUcenickaZvanja polaganje = ctx.PolaganjaUcenickaZvanja.Where(x=>x.Id==model.Id).FirstOrDefault();
            polaganje.DatumPolaganja = KonvertujUDatum_dd_mm_yyyy(model.DatumPolaganja);
            polaganje.OrganizatorPolaganja = model.OrganizatorPolaganja;
            polaganje.MjestoPolaganja = model.MjestoPolaganja;
            polaganje.Obrazlozenje = model.Obrazlozenje;
            ctx.SaveChanges();

            return RedirectToAction("Index", "UpravljanjePolaganjimaStecenimZvanjima", new { brojTaba = 1 });


        }
        public JsonResult Obrisi(int polaganjeId)
        {
            PolaganjaUcenickaZvanja polaganje = ctx.PolaganjaUcenickaZvanja.Where(x => x.Id == polaganjeId).FirstOrDefault();
            List<UcesniciPolaganjaZaUcenickaZvanja> ucesniciPolaganja = ctx.UcesniciPolaganjaZaUcenickaZvanja.Where(x => x.PolaganjeUcenickaZvanjaId == polaganjeId && x.isDeleted == false).ToList();
            List<RezultatiPolaganjaUcenickaZvanja> rezultatiPolaganja = ctx.RezultatiPolaganjaUcenickaZvanja.Where(x => x.PolaganjeUcenickaZvanjaId == polaganjeId && x.isDeleted == false).ToList();
            List<TroskoviPolaganjaUcenickaZvanja> troskoviPolaganja = ctx.TroskoviPolaganjaUcenickaZvanja.Where(x => x.PolaganjeUcenickaZvanjaId == polaganjeId && x.isDeleted == false).ToList();

            for (int i = 0; i < rezultatiPolaganja.Count(); i++)
            {
                rezultatiPolaganja[i].isDeleted = true;
                ctx.SaveChanges();
            }
            for (int i = 0; i < ucesniciPolaganja.Count(); i++)
            {
                ucesniciPolaganja[i].isDeleted = true;
                ctx.SaveChanges();
            }
            for (int i = 0; i < troskoviPolaganja.Count(); i++)
            {
                troskoviPolaganja[i].isDeleted = true;
                ctx.SaveChanges();
            }
            polaganje.isDeleted = true;
            ctx.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }


}