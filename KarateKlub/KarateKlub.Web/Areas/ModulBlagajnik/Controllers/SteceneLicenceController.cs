using KarateKlub.Data;
using KarateKlub.Data.Models;
using KarateKlub.Web.Areas.ModulBlagajnik.Models;
using KarateKlub.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulBlagajnik.Controllers
{
    [Autorizacija(false, false, false, false, true)]

    public class SteceneLicenceController : Controller
    {
       
        MojContext ctx = new MojContext();
        // GET: ModulBlagajnik/SteceneLicence
        public ActionResult PregledStecenihLicenci(int osobaId, int aktivnostLicence, int aktivan)
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

            SteceneLicencePregledStecenihLicenciVM model = new SteceneLicencePregledStecenihLicenciVM(listaLicenci, aktivnostLicence, osobaId, aktivan, osoba);


            return View("PregledStecenihLicenci", model);
        }
    }
}