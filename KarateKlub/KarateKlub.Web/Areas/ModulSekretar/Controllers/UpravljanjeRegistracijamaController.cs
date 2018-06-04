using KarateKlub.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulSekretar.Controllers
{
    [Autorizacija(false, true, false, false, false)]

    public class UpravljanjeRegistracijamaController : Controller
    {
        // GET: ModulSekretar/UpravljanjeRegistracijama
        public ActionResult Index(int brojaTaba=1,int savez=0)
        {
            ViewData["tab"] = brojaTaba;
            ViewData["savez"] = savez;

            return View();
        }

        public ActionResult PozivIndexStranice(int brTaba,int savez)
        {

            ViewData["tab"] = brTaba;
            ViewData["savez"] = savez;
            return View("Index");
        }
    }
}