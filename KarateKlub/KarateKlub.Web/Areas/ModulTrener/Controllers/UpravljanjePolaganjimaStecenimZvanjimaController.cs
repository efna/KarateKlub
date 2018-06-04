using KarateKlub.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulTrener.Controllers
{
    [Autorizacija(false, false, false, true, false)]

    public class UpravljanjePolaganjimaStecenimZvanjimaController : Controller
    {
        // GET: ModulTrener/UpravljanjePolaganjimaStecenimZvanjima
        public ActionResult Index(int brojTaba = 1)
        {
            ViewData["tab"] = brojTaba;
            return View();
        }
    }
}