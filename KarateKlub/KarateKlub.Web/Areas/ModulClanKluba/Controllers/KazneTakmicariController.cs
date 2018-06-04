using KarateKlub.Data;
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

    public class KazneTakmicariController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulClanKluba/KazneTakmicari
        public ActionResult Index(int takmicarId)
        {
            KazneTakmicariIndexVM model = new KazneTakmicariIndexVM
            {
                kazneTakmicara = ctx.KazneTakmicari.Where(x => x.TakmicarId == takmicarId && x.isDeleted == false).Select(x => new KaznaTakmicaraPodaci
                {
                    Id = x.Id,
                    TakmicarId = x.TakmicarId,
                    isDeleted = x.isDeleted,
                    DodjeljenoOd = x.DodjeljenoOd,
                    DodjeljenoZbog = x.DodjeljenoZbog,
                    DatumSticanja = x.DatumSticanja,
                    DatumIsteka = x.DatumIsteka,
                    Obrazlozenje = x.Obrazlozenje,
                    MjestoSticanja = x.Mjesto

                }).ToList()
            };
            ViewData["takmicarId"] = takmicarId;
         
            return View(model);
        }
    }
}