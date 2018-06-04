﻿using KarateKlub.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulBlagajnik.Controllers
{
    [Autorizacija(false, false, false, false, true)]

    public class UpravljanjeClanovimaKlubaController : Controller
    {
        // GET: ModulBlagajnik/UpravljanjeClanovimaKluba
        public ActionResult Index(int brojTaba = 1, int aktivan = 0, int zvanje = 0)
        {
            ViewData["tab"] = brojTaba;
            ViewData["aktivan"] = aktivan;
            ViewData["zvanje"] = zvanje;

            return View();
        }
    }
}