using KarateKlub.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulSekretar.Controllers
{
    [Autorizacija(false, true, false, false, false)]

    public class UpravljanjeTakmicarskomKnjizicomController : Controller
    {
        // GET: ModulSekretar/UpravljanjeTakmicarskomKnjizicom
        public ActionResult Index(int takmicarId,int aktivnost=0,int brojTaba=1)
        {
            ViewData["takmicarId"] = takmicarId;
            ViewData["aktivnost"] = aktivnost;
            ViewData["tab"] = brojTaba;

            return View();
        }
    }
}