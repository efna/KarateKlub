using KarateKlub.Data;
using KarateKlub.Data.Models;
using KarateKlub.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
       
        MojContext ctx = new MojContext();

        public ActionResult Index()
        {
            KorisnickiNalozi korisnickiNalog = Autentifikacija.GetLogiraniKorisnik(HttpContext);
            if (korisnickiNalog == null)
            {
                return RedirectToAction("Index", "Autentifikacija", new { area = "" });



            }
            if (korisnickiNalog.isAktivanNalog)
            {
                if (korisnickiNalog.Osoba.isAdministrator == true)
                {
                    return RedirectToAction("Index", "Home", new { area = "ModulAdministrator" });
                }
                else if (korisnickiNalog.Osoba.isSekretar == true)
                {
                    return RedirectToAction("Index", "Home", new { area = "ModulSekretar" });
                }
                else if (korisnickiNalog.Osoba.isClanKluba == true)
                {
                    
                    return RedirectToAction("Index", "Home", new { area = "ModulClanKluba" });

                }
                else if (korisnickiNalog.Osoba.isTrener == true)
                {
                    return RedirectToAction("Index", "Home", new { area = "ModulTrener" });

                }
                else if(korisnickiNalog.Osoba.isBlagajnik==true)
                { 
                    return RedirectToAction("Index", "Home", new { area = "ModulBlagajnik" });

                }
            }

            return RedirectToAction("Logout", "Autentifikacija");


        }

    }
}