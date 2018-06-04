using KarateKlub.Data;
using KarateKlub.Data.Models;
using KarateKlub.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulClanKluba.Controllers
{
    [Autorizacija(false, false, true, false, false)]

    public class UpravljanjeUplatamaClanaKlubaController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulClanKluba/UpravljanjeUplatamaClanaKluba
        public ActionResult Index(int osobaId, int brojTaba = 1, int izmirena = 0)
        {
            Osoba osoba = ctx.Osoba.Where(x => x.Id == osobaId).FirstOrDefault();
            ViewData["osobaId"] = osobaId;
            ViewData["tab"] = brojTaba;
            ViewData["osoba"] = osoba;
            ViewData["izmirena"] = izmirena;

            return View();
        }
    }
}