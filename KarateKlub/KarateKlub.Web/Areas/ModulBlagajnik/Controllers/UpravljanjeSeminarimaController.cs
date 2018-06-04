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

    public class UpravljanjeSeminarimaController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulBlagajnik/UpravljanjeSeminarima
        public ActionResult Index(int seminarId, int brojTaba = 1)
        {
            Seminari seminar = ctx.Seminari.Where(x => x.Id == seminarId).FirstOrDefault();
            ViewData["tab"] = brojTaba;
            ViewData["seminarId"] = seminarId;
            ViewData["seminar"] = seminar;

            return View();
        }
    }
}