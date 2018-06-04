using KarateKlub.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulSekretar.Controllers
{
    [Autorizacija(false, true, false, false, false)]

    public class UpravljanjeProtokolimaController : Controller
    {
        // GET: ModulSekretar/UpravljanjeProtokolima
        public ActionResult Index(int brojTaba = 1)
        {
            ViewData["tab"] = brojTaba;
            return View();
        }
    }
}