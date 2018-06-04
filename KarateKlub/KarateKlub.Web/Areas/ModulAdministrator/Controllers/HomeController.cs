using KarateKlub.Data;
using KarateKlub.Data.Models;
using KarateKlub.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulAdministrator.Controllers
{
    [Autorizacija(true,false,false,false,false)]
    public class HomeController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulAdministrator/Home
        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult GetImage(int osobaId, int ulogaId)
        {
            KorisnickiNalozi nalog = ctx.KorisnickiNalozi.Where(x => x.OsobaId == osobaId && x.KorisnickaUlogaId == ulogaId).FirstOrDefault();
            return File(nalog.Osoba.Slika, nalog.Osoba.TipSlike);

        }
        public ActionResult MojiPodaci(int osobaId)
        {
            return RedirectToAction("Detalji", "Administratori", new { osobaId = osobaId });
        }
    }
}