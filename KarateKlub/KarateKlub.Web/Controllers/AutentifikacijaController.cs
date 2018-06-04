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
    public class AutentifikacijaController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: Autentifikacija

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string username, string password)
        {

            List<KorisnickiNalozi> listaKorisnickihNaloga = ctx.KorisnickiNalozi.ToList();
            KorisnickiNalozi korisnickiNalog = null;
            foreach (KorisnickiNalozi x in listaKorisnickihNaloga)
            {
                if (x.KorisnickoIme == username && x.Lozinka == password)
                {
                    korisnickiNalog = x;
                }
            }

            if (korisnickiNalog == null)
            {
                ViewData["PorukaCrvena"] = "Unijeli ste pogrešne podatke za prijavu. Neispravno korisničko ime i/ili lozinka.";
                return View();
            }

            Autentifikacija.PokreniNovuSesiju(korisnickiNalog, HttpContext);

            return RedirectToAction("Index", "Home");

        }

        public ActionResult Logout()
        {
            Autentifikacija.PokreniNovuSesiju(null, HttpContext);

            return RedirectToAction("Index", "Autentifikacija");
        }
    }
    
}