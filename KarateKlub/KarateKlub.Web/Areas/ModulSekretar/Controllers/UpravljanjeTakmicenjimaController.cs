using KarateKlub.Data;
using KarateKlub.Data.Models;
using KarateKlub.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulSekretar.Controllers
{
    [Autorizacija(false, true, false, false, false)]

    public class UpravljanjeTakmicenjimaController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulSekretar/UpravljanjeTakmicenjima
        public ActionResult Index(int takmicenjeId,int brojTaba=1,int brojTabaRezultati=1)
        {
            Takmicenja takmicenje = ctx.Takmicenja.Where(x => x.isDeleted == false && x.Id == takmicenjeId).FirstOrDefault();

            ViewData["takmicenjeId"] = takmicenjeId;
            ViewData["tab"] = brojTaba;
            ViewData["takmicenje"] = takmicenje;
            ViewData["brojTabaRezultati"] = brojTabaRezultati;

            return View();
        }
    }
}