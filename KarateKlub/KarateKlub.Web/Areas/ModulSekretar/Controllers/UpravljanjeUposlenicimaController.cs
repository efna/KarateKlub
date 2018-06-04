using KarateKlub.Data;
using KarateKlub.Data.Models;
using KarateKlub.Web.Areas.ModulSekretar.Models;
using KarateKlub.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulSekretar.Controllers
{
    [Autorizacija(false, true, false, false, false)]

    public class UpravljanjeUposlenicimaController : Controller
    {
        MojContext ctx = new MojContext();

        // GET: ModulSekretar/UpravljanjeUposlenicima
        public ActionResult Index(int brojTaba=1,int aktivan=0,int uloga=1)
        {
            ViewData["tab"] = brojTaba;
            ViewData["aktivan"] = aktivan;

            List<Osoba> listaOsoba = new List<Osoba>();
            if (uloga == 1)
            {
                if (aktivan == 0)
                {
                    listaOsoba = ctx.Osoba.Where(x => x.isDeleted == false && x.isTrener == true).ToList();
                }
                else if (aktivan == 1)
                {
                    listaOsoba = ctx.Osoba.Where(x => x.isDeleted == false && x.isTrener == true && x.isAktivnaOsoba == true).ToList();


                }
                else
                {
                    listaOsoba = ctx.Osoba.Where(x => x.isDeleted == false && x.isTrener == true && x.isAktivnaOsoba == false).ToList();

                }

            }
            else if (uloga == 2)
            {
                if (aktivan == 0)
                {
                    listaOsoba = ctx.Osoba.Where(x => x.isDeleted == false && x.isSekretar == true).ToList();
                }
                else if (aktivan == 1)
                {
                    listaOsoba = ctx.Osoba.Where(x => x.isDeleted == false && x.isSekretar == true && x.isAktivnaOsoba == true).ToList();

                }
                else
                {
                    listaOsoba = ctx.Osoba.Where(x => x.isDeleted == false && x.isSekretar == true && x.isAktivnaOsoba == false).ToList();

                }

            }
            else if (uloga == 3)
            {
                if (aktivan == 0)
                {
                    listaOsoba = ctx.Osoba.Where(x => x.isDeleted == false && x.isBlagajnik == true).ToList();
                }
                else if (aktivan == 1)
                {
                    listaOsoba = ctx.Osoba.Where(x => x.isDeleted == false && x.isBlagajnik == true && x.isAktivnaOsoba == true).ToList();

                }
                else
                {
                    listaOsoba = ctx.Osoba.Where(x => x.isDeleted == false && x.isBlagajnik == true && x.isAktivnaOsoba == false).ToList();

                }
            }
            else
            {
                if (aktivan == 0)
                {
                    listaOsoba = ctx.Osoba.Where(x => x.isDeleted == false && x.isKnjigovodja == true).ToList();
                }
                else if (aktivan == 1)
                {
                    listaOsoba = ctx.Osoba.Where(x => x.isDeleted == false && x.isKnjigovodja == true && x.isAktivnaOsoba == true).ToList();

                }
                else
                {
                    listaOsoba = ctx.Osoba.Where(x => x.isDeleted == false && x.isKnjigovodja == true && x.isAktivnaOsoba == false).ToList();

                }
            }

            UpravljanjeUposlenicimaPrikazUposlenikaVM model = new UpravljanjeUposlenicimaPrikazUposlenikaVM(listaOsoba, aktivan, uloga);


            return View(model);
        }
        public ActionResult GoToPrikazTrenera(int aktivan)
        {

            return RedirectToAction("Index", new { brojTaba = 1, aktivan, uloga = 1 });
        }
        public ActionResult GoToPrikazSekretara(int aktivan)
        {

            return RedirectToAction("Index", new { brojTaba = 2, aktivan, uloga = 2 });
        }

        public ActionResult GoToPrikazBlagajnika(int aktivan)
        {
           
            return RedirectToAction("Index", new { brojTaba=3, aktivan,uloga=3 });
        }
      
        public ActionResult GoToPrikazKnjigovodja(int aktivan)
        {

            return RedirectToAction("Index", new { brojTaba = 4, aktivan, uloga = 4 });
        }
        public ActionResult PrikazUposlenika(int aktivan,int uloga)
        {
            List<Osoba> listaOsoba = new List<Osoba>();
            if (uloga == 1)
            {
                if (aktivan == 0)
                {
                    listaOsoba = ctx.Osoba.Where(x => x.isDeleted == false && x.isTrener == true).ToList();
                }
                else if(aktivan==1)
                {
                    listaOsoba = ctx.Osoba.Where(x => x.isDeleted == false && x.isTrener == true && x.isAktivnaOsoba==true).ToList();


                }
                else
                {
                    listaOsoba = ctx.Osoba.Where(x => x.isDeleted == false && x.isTrener == true && x.isAktivnaOsoba == false).ToList();

                }

            }
            else if (uloga == 2)
            {
                if (aktivan == 0)
                {
                    listaOsoba = ctx.Osoba.Where(x => x.isDeleted==false && x.isSekretar==true).ToList();
                }
                else if (aktivan == 1)
                {
                    listaOsoba = ctx.Osoba.Where(x => x.isDeleted == false && x.isSekretar == true && x.isAktivnaOsoba == true).ToList();
                    
                }
                else
                {
                    listaOsoba = ctx.Osoba.Where(x => x.isDeleted == false && x.isSekretar == true && x.isAktivnaOsoba == false).ToList();

                }

            }
            else if (uloga == 3) {
                if (aktivan == 0)
                {
                    listaOsoba = ctx.Osoba.Where(x => x.isDeleted == false && x.isBlagajnik == true).ToList();
                }
                else if (aktivan == 1)
                {
                    listaOsoba = ctx.Osoba.Where(x => x.isDeleted == false && x.isBlagajnik == true && x.isAktivnaOsoba == true).ToList();

                }
                else
                {
                    listaOsoba = ctx.Osoba.Where(x => x.isDeleted == false && x.isBlagajnik == true && x.isAktivnaOsoba == false).ToList();

                }
            }
            else
            {
                if (aktivan == 0)
                {
                    listaOsoba = ctx.Osoba.Where(x => x.isDeleted == false && x.isKnjigovodja == true).ToList();
                }
                else if (aktivan == 1)
                {
                    listaOsoba = ctx.Osoba.Where(x => x.isDeleted == false && x.isKnjigovodja == true && x.isAktivnaOsoba == true).ToList();

                }
                else
                {
                    listaOsoba = ctx.Osoba.Where(x => x.isDeleted == false && x.isKnjigovodja == true && x.isAktivnaOsoba == false).ToList();

                }
            }

            UpravljanjeUposlenicimaPrikazUposlenikaVM model = new UpravljanjeUposlenicimaPrikazUposlenikaVM(listaOsoba, aktivan, uloga);
            ViewData["aktivan"] = aktivan;
            ViewData["uloga"] = uloga;
            if (uloga == 1)
                return View("PrikazTrenera", model);
            else if (uloga == 2)
                return View("PrikazSekretara",model);
            else if (uloga == 3)
                return View("PrikazBlagajnika", model);
            else
            return View("PrikazKnjigovodja", model);
        }
    }
}