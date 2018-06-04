using KarateKlub.Data;
using KarateKlub.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulBlagajnik.Controllers
{
    [Autorizacija(false, false, false, false, true)]

    public class UpravljanjePolaganjaUcenickaZvanjaController : Controller
    {
        MojContext ctx = new MojContext();

        // GET: ModulBlagajnik/UpravljanjePolaganjaUcenickaZvanja
        public ActionResult Index(int polaganjeId, int brojTaba = 1, int zvanje = 0, int brojTabaParticipacije = 1)
        {
            string datumPolaganje = ctx.PolaganjaUcenickaZvanja.Where(x => x.Id == polaganjeId).FirstOrDefault().DatumPolaganja.ToString("dd.MM.yyyy");
            ViewData["tab"] = brojTaba;
            ViewData["polaganjeId"] = polaganjeId;
            ViewData["datumPolaganja"] = datumPolaganje;
            ViewData["zvanje"] = zvanje;
            ViewData["brojTabaParticipacije"] = brojTabaParticipacije;

            return View();
        }
    }
}