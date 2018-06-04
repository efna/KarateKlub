using KarateKlub.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulSekretar.Controllers
{
    [Autorizacija(false, true, false, false, false)]

    public class UpravljanjeUpravnimOdboromController : Controller
    {
        // GET: ModulSekretar/UpravljanjeUpravnimOdborom
        public ActionResult Index(int aktivnost=0,int brojTaba=1)
        {
            ViewData["tab"] = brojTaba;

            ViewData["aktivnost"] = aktivnost;

            return View();
        }
    }
}