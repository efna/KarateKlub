using KarateKlub.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulTrener.Controllers
{
    [Autorizacija(false, false, false, true, false)]

    public class UpravljanjeTakmicenjimaTakmicarimaController : Controller
    {
        // GET: ModulTrener/UpravljanjeTakmicenjimaTakmicarima
        public ActionResult Index(int brojTaba = 1, int aktivnost = 0)
        {
            ViewData["tab"] = brojTaba;
            ViewData["aktivnost"] = aktivnost;

            return View();
        }
    }
}