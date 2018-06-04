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

    public class OsobaController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulBlagajnik/Osoba
        public ActionResult Index(int funkcijaOsobe)
        {
            List<Osoba> osobe = new List<Osoba>();
            if (funkcijaOsobe == 0)
            {
                osobe = ctx.Osoba.Where(x => x.isDeleted == false && (x.isTrener == true || x.isBlagajnik == true || x.isSekretar == true || x.isKnjigovodja == true || x.isClanUpravnogOdbora == true)).ToList();
            }
            else if (funkcijaOsobe == 1)
            {
                osobe = ctx.Osoba.Where(x => x.isDeleted == false && x.isTrener == true).ToList();
            }
            else if (funkcijaOsobe == 2)
            {
                osobe = ctx.Osoba.Where(x => x.isDeleted == false && x.isSekretar == true).ToList();
            }
            else if (funkcijaOsobe == 3)
            {
                osobe = ctx.Osoba.Where(x => x.isDeleted == false && x.isBlagajnik == true).ToList();
            }
            else if (funkcijaOsobe == 4)
            {
                osobe = ctx.Osoba.Where(x => x.isDeleted == false && x.isKnjigovodja == true).ToList();
            }
            else if (funkcijaOsobe == 5)
            {
                osobe = ctx.Osoba.Where(x => x.isDeleted == false && x.isClanUpravnogOdbora == true).ToList();
            }
            OsobaIndexVM model = new OsobaIndexVM(osobe, funkcijaOsobe);
            ViewData["funkcijaOsobe"] = funkcijaOsobe;
            return View(model);
        }
        public ActionResult GoToUpravljanjeUplatama(int funkcijaOsobe)
        {
            return RedirectToAction("Index", "UpravljanjeUplatama", new { brojTaba = 1, funkcijaOsobe = funkcijaOsobe });
        }
    }
}