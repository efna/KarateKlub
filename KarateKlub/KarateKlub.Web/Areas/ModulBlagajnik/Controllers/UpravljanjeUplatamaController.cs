using KarateKlub.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulBlagajnik.Controllers
{
    [Autorizacija(false, false, false, false, true)]

    public class UpravljanjeUplatamaController : Controller
    {
        // GET: ModulBlagajnik/UpravljanjeUplatama
        public ActionResult Index(int brojTaba = 1, int funkcijaOsobe = 0)
        {
            ViewData["tab"] = brojTaba;
            ViewData["funkcijaOsobe"] = funkcijaOsobe;

            return View();
        }
    }
}