using KarateKlub.Data;
using KarateKlub.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulSekretar.Controllers
{
    [Autorizacija(false, true, false, false, false)]

    public class UpravljanjeUplatamaController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulSekretar/UpravljanjeUplatama
        public ActionResult Index(int brojTaba=1,int funkcijaOsobe=0)
        {
            ViewData["tab"] = brojTaba;
            ViewData["funkcijaOsobe"] = funkcijaOsobe;

            return View();
        }
    }
}