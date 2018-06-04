using KarateKlub.Data;
using KarateKlub.Data.Models;
using KarateKlub.Web.Areas.ModulClanKluba.Models;
using KarateKlub.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulClanKluba.Controllers
{
    [Autorizacija(false, false, true, false, false)]

    public class SteceneLicenceController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulClanKluba/SteceneLicence
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PregledStecenihLicenci(int osobaId, int aktivnostLicence)
        {
            Osoba osoba = ctx.Osoba.Where(x => x.Id == osobaId).FirstOrDefault();
            List<SteceneLicence> listaLicenci = new List<SteceneLicence>();

            if (aktivnostLicence == 0)
            {
                listaLicenci = ctx.SteceneLicence.Where(x => x.isDeleted == false && x.OsobaId == osobaId).ToList();
            }
            else if (aktivnostLicence == 1)
            {
                listaLicenci = ctx.SteceneLicence.Where(x => x.isDeleted == false && x.OsobaId == osobaId && x.isAktivnaLicenca == true).ToList();

            }
            else
            {
                listaLicenci = ctx.SteceneLicence.Where(x => x.isDeleted == false && x.OsobaId == osobaId && x.isAktivnaLicenca == false).ToList();

            }

            SteceneLicencePregledStecenihLicenciVM model = new SteceneLicencePregledStecenihLicenciVM(listaLicenci, aktivnostLicence, osobaId, osoba);


            return View("PregledStecenihLicenci", model);
        }
    }
}