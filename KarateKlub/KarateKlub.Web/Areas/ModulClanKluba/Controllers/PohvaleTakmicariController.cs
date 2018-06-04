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

    public class PohvaleTakmicariController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulClanKluba/PohvaleTakmicari
        public ActionResult Index(int takmicarId)
        {
            PohvaleTakmicariIndexVM model = new PohvaleTakmicariIndexVM
            {
                pohvaleTakmicara = ctx.PohvaleTakmicari.Where(x => x.TakmicarId == takmicarId && x.isDeleted == false).Select(x => new PohvalaTakmicaraPodaci
                {
                    Id = x.Id,
                    TakmicarId = x.TakmicarId,
                    isDeleted = x.isDeleted,
                    DodjeljenoOd = x.DodjeljenoOd,
                    DodjeljenoZbog = x.DodjeljenoZbog,
                    DatumDodjele = x.DatumDodjele,
                    MjestoDodjele = x.MjestoDodjele,
                    Obrazlozenje = x.Obrazlozenje

                }).ToList()
            };
            ViewData["takmicarId"] = takmicarId;
            
            return View(model);
        }
    }
}