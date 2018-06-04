using KarateKlub.Data;
using KarateKlub.Data.Models;
using KarateKlub.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulBlagajnik.Controllers
{
    [Autorizacija(false, false, false, false, true)]

    public class UpravljanjePodacimaUpisaController : Controller
    {
        MojContext ctx = new MojContext();

        // GET: ModulBlagajnik/UpravljanjePodacimaUpisa
        public ActionResult Index(int upisId, int brojTaba = 1, int izmirena = 0)
        {
            Upisi upis = ctx.Upisi.Where(x => x.Id == upisId).FirstOrDefault();
            ViewData["upisId"] = upisId;
            ViewData["tab"] = brojTaba;
            ViewData["upis"] = upis;
            ViewData["izmirena"] = izmirena;

            return View();
        }
    }
}