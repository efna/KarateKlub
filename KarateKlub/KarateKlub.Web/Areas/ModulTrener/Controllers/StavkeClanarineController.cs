using KarateKlub.Data;
using KarateKlub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KarateKlub.Web.Areas.ModulTrener.Models;
using KarateKlub.Web.Helper;

namespace KarateKlub.Web.Areas.ModulTrener.Controllers
{
    [Autorizacija(false, false, false, true, false)]

    public class StavkeClanarineController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulTrener/StavkeClanarine
        public ActionResult PregledClanarinaClana(int osobaId, int aktivan, int izmirena)
        {

            List<StavkeClanarine> stavke = new List<StavkeClanarine>();
            if (izmirena == 0)
            {
                stavke = ctx.StavkeClanarine.Where(x => x.ClanKluba.OsobaId == osobaId && x.isDeleted == false && x.isIzmirenaClanarina == true).ToList();
            }
            else
            {
                stavke = ctx.StavkeClanarine.Where(x => x.ClanKluba.OsobaId == osobaId && x.isDeleted == false && x.isIzmirenaClanarina == false).ToList();

            }
            StavkeClanarinePregledClanarinaClanaVM model = new StavkeClanarinePregledClanarinaClanaVM(stavke, izmirena, aktivan, osobaId);
            ViewData["osobaId"] = osobaId;
            ViewData["izmirena"] = izmirena;
            ViewData["aktivan"] = aktivan;

            if (izmirena == 0)
                return View("PregledIzmirenihClanarinaClanaKluba", model);
            else
                return View("PregledNeizmirenihClanarinaClanaKluba", model);
        }
    }
}