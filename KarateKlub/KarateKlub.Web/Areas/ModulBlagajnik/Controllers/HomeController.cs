using KarateKlub.Data;
using KarateKlub.Data.Models;
using KarateKlub.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulBlagajnik.Controllers
{
    [Autorizacija(false, false, false, false, true)]

    public class HomeController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulBlagajnik/Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetImage(int osobaId, int ulogaId)
        {
            KorisnickiNalozi nalog = ctx.KorisnickiNalozi.Where(x => x.OsobaId == osobaId && x.KorisnickaUlogaId == ulogaId).FirstOrDefault();
            return File(nalog.Osoba.Slika, nalog.Osoba.TipSlike);

        }
        public ActionResult MojiPodaci(int osobaId)
        {
            return RedirectToAction("UpravljanjeDetaljimaBlagajnika", "Blagajnici", new { osobaId = osobaId });
        }
    }
}