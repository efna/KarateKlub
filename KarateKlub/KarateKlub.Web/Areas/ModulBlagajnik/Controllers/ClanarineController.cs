using KarateKlub.Data;
using KarateKlub.Data.Models;
using KarateKlub.Web.Areas.ModulBlagajnik.Models;
using KarateKlub.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulBlagajnik.Controllers
{
    [Autorizacija(false, false, false, false, true)]

    public class ClanarineController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulBlagajnik/Clanarine
        public ActionResult Index()
        {
            ClanarineIndexVM model = new ClanarineIndexVM
            {
                clanarine = ctx.Clanarine.Where(x => x.isDeleted == false).Select(x => new ClanarinaPodaci
                {
                    Id = x.Id,
                    isDeleted = x.isDeleted,
                    DatumDo = x.DatumDo,
                    DatumOd = x.DatumOd,
                    Naziv = x.Naziv
                }).ToList()
            };
            return View(model);
        }

        public ActionResult Dodaj()
        {
            ClanarineDodajVM model = new ClanarineDodajVM
            {
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
        public ActionResult SpremiNovuClanarinu(ClanarineDodajVM model)
        {
            Clanarine clanarina = new Clanarine();
            clanarina.isDeleted = false;
            clanarina.Naziv = model.Naziv;
            clanarina.DatumOd = KonvertujUDatum_dd_mm_yyyy(model.DatumOd);
            clanarina.DatumDo = KonvertujUDatum_dd_mm_yyyy(model.DatumDo);
            ctx.Clanarine.Add(clanarina);
            ctx.SaveChanges();
            int clanarinaId = ctx.Clanarine.ToList().OrderByDescending(x => x.Id).FirstOrDefault().Id;

            List<ClanoviKluba> clanovi = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true).ToList();
            for (int i = 0; i < clanovi.Count(); i++)
            {
                StavkeClanarine stavka = new StavkeClanarine();

                stavka.isDeleted = false;
                stavka.isIzmirenaClanarina = false;
                stavka.ClanKlubaId = clanovi[i].Id;
                stavka.ClanarinaId = clanarinaId;
                stavka.DatumUplate = null;
                ctx.StavkeClanarine.Add(stavka);
                ctx.SaveChanges();
            }
            return RedirectToAction("Index", "UpravljanjeClanarinama", new { brojTaba = 1 });
        }
        public ActionResult Uredi(int clanarinaId)
        {
            Clanarine clanarina = ctx.Clanarine.Where(x => x.Id == clanarinaId).FirstOrDefault();
            ClanarineUrediVM model = new ClanarineUrediVM
            {
                Id = clanarinaId,
                isDeleted = clanarina.isDeleted,
                Naziv = clanarina.Naziv,
                DatumOd = clanarina.DatumOd.ToString("dd.MM.yyyy"),
                DatumDo = clanarina.DatumDo.ToString("dd.MM.yyyy")
            };
            return View("Uredi", model);
        }
        public ActionResult SpremiIzmjenuClanarine(ClanarineUrediVM model)
        {
            Clanarine clanarina = ctx.Clanarine.Where(x => x.Id == model.Id).FirstOrDefault();

            clanarina.Naziv = model.Naziv;
            clanarina.DatumOd = KonvertujUDatum_dd_mm_yyyy(model.DatumOd);
            clanarina.DatumDo = KonvertujUDatum_dd_mm_yyyy(model.DatumDo);
            ctx.SaveChanges();
            return RedirectToAction("Index", "UpravljanjeClanarinama", new { brojTaba = 1 });


        }
        public JsonResult Obrisi(int clanarinaId)
        {
            Clanarine clanarina = ctx.Clanarine.Where(x => x.Id == clanarinaId).FirstOrDefault();
            List<StavkeClanarine> stavke = ctx.StavkeClanarine.Where(x => x.isDeleted == false && x.ClanarinaId == clanarinaId).ToList();
            for (int i = 0; i < stavke.Count(); i++)
            {
                stavke[i].isDeleted = true;
                ctx.SaveChanges();
            }
            clanarina.isDeleted = true;
            ctx.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}