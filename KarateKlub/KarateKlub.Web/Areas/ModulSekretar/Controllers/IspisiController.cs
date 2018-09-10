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

    public class IspisiController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulSekretar/Ispisi
        public ActionResult Index()
        {
            IspisiIndexVM model = new IspisiIndexVM {
                ispisi=ctx.Ispisi.Where(x=>x.isDeleted==false).Select(x=>new IspisPodaci {
                    Id=x.Id,
                    isDeleted=x.isDeleted,
                    ClanKlubaId=x.ClanKlubaId,
                    DatumIspisa=x.DatumIspisa,
                    RazlogIspisa=x.RazlogIspisa,
                    ClanKluba=x.ClanKluba.Osoba.Ime+" ("+x.ClanKluba.Osoba.ImeRoditelja+") "+x.ClanKluba.Osoba.Prezime,
                    JMBG=x.ClanKluba.Osoba.JMBG
                }).ToList()
            };

            return View(model);
        }

        public ActionResult Dodaj()
        {
            IspisiDodajVM model = new IspisiDodajVM {
                ClanoviKluba=BindClanoveKluba()

            };
            model.ClanoviKluba.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite člana-" });

            return View("Dodaj", model);
        }

        private List<SelectListItem> BindClanoveKluba()
        {
            return ctx.ClanoviKluba.OrderBy(x=>x.Osoba.Ime).Where(x => x.isDeleted == false).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Osoba.Ime + " (" + x.Osoba.ImeRoditelja + ") " + x.Osoba.Prezime }).ToList();

        }
        public DateTime KonvertujUDatum_dd_mm_yyyy(string datum)
        {

            int dan = Convert.ToInt32(datum.Substring(0, 2));
            int mjesec = Convert.ToInt32(datum.Substring(3, 2));
            int godina = Convert.ToInt32(datum.Substring(6, 4));
            DateTime uneseniDatum = new DateTime(godina, mjesec, dan);
            return uneseniDatum;


        }
        public ActionResult SpremiNoviIspis(IspisiDodajVM model)
        {
            Ispisi ispis = new Ispisi();
            ispis.isDeleted = false;
            ispis.ClanKlubaId = model.ClanKlubaId;
            if (model.DatumIspisa != null)
                ispis.DatumIspisa = KonvertujUDatum_dd_mm_yyyy(model.DatumIspisa);
            ispis.RazlogIspisa = model.RazlogIspisa;
            ctx.Ispisi.Add(ispis);
            ctx.SaveChanges();
            return RedirectToAction("Index", "Ispisi");
        }
        public ActionResult Uredi(int ispisId)
        {
            Ispisi ispis = ctx.Ispisi.Where(x => x.Id == ispisId).FirstOrDefault();
            IspisiUrediVM model = new IspisiUrediVM {
                Id=ispisId,
                isDeleted=ispis.isDeleted,
                DatumIspisa=ispis.DatumIspisa.ToString("dd.MM.yyyy"),
                ClanKlubaId=ispis.ClanKlubaId,
                ClanoviKluba=BindClanoveKluba(),
                RazlogIspisa=ispis.RazlogIspisa
            };
            model.ClanoviKluba.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite člana-" });

            return View("Uredi", model);
        }
        public ActionResult SpremiIzmjenuIspisa(IspisiUrediVM model)
        {
            Ispisi ispis = ctx.Ispisi.Where(x => x.Id == model.Id).FirstOrDefault();


            ispis.ClanKlubaId = model.ClanKlubaId;
            if (model.DatumIspisa != null)
                ispis.DatumIspisa = KonvertujUDatum_dd_mm_yyyy(model.DatumIspisa);
            ispis.RazlogIspisa = model.RazlogIspisa;
           
            ctx.SaveChanges();
            return RedirectToAction("Index", "Ispisi");
        }
        public JsonResult Obrisi(int ispisId)
        {
            Ispisi ispis = ctx.Ispisi.Where(x => x.Id == ispisId).FirstOrDefault();

            if (ispis != null)
                ispis.isDeleted = true;
            ctx.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}