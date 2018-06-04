using KarateKlub.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulBlagajnik.Controllers
{
    [Autorizacija(false, false, false, false, true)]

    public class UpravljanjeClanarinamaController : Controller
    {
        // GET: ModulBlagajnik/UpravljanjeClanarinama
        public ActionResult Index(int brojTaba = 1)
        {
            ViewData["tab"] = brojTaba;

            return View();
        }
    }
}