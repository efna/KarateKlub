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

    public class StavkeClanarineController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulClanKluba/StavkeClanarine
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PregledClanarinaClana(int osobaId, int izmirena)
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
            StavkeClanarinePregledClanarinaClanaVM model = new StavkeClanarinePregledClanarinaClanaVM(stavke, izmirena, osobaId);
            ViewData["osobaId"] = osobaId;
            ViewData["izmirena"] = izmirena;
            
            if (izmirena == 0)
                return View("PregledIzmirenihClanarinaClanaKluba", model);
            else
                return View("PregledNeizmirenihClanarinaClanaKluba", model);
        }
    }
}