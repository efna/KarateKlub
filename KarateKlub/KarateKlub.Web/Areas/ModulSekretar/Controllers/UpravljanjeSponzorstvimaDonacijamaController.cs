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

    public class UpravljanjeSponzorstvimaDonacijamaController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulSekretar/UpravljanjeSponzorstvimaDonacijama
        public ActionResult Index(int brojTaba=1,int aktivnost=0)
        {
            ViewData["tab"] = brojTaba;
            ViewData["aktivnost"] = aktivnost;
            return View();
        }
    }
}