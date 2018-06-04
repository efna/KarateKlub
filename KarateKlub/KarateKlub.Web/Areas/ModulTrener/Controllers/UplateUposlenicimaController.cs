using KarateKlub.Data;
using KarateKlub.Web.Areas.ModulTrener.Models;
using KarateKlub.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulTrener.Controllers
{
    [Autorizacija(false, false, false, true, false)]

    public class UplateUposlenicimaController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulTrener/UplateUposlenicima
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PregledUplataUposlenika(int osobaId)
        {
            UplateUposlenicimaIndexVM model = new UplateUposlenicimaIndexVM
            {
                uplateUposlenicima = ctx.UplateUposlenicima.Where(x => x.isDeleted == false && x.OsobaId == osobaId).Select(x => new UplataUposlenikaPodaci
                {
                    Id = x.Id,
                    osoba = x.Osoba,
                    Osoba = x.Osoba.Ime + " (" + x.Osoba.ImeRoditelja + ") " + x.Osoba.Prezime,
                    JMBG = x.Osoba.JMBG,
                    DatumUplate = x.DatumUplate,
                    DatumOd = x.DatumOd,
                    DatumDo = x.DatumDo,
                    IznosKMBrojevima = x.IznosKMBrojevima,
                    IznosKMSLovima = x.IznosKMSLovima,
                    SvrhaUplate = x.SvrhaUplate,
                    Obrazlozenje = x.Obrazlozenje
                }).ToList(),
                uposlenik = ctx.Osoba.Where(x => x.Id == osobaId).FirstOrDefault()
            };
            ViewData["osobaId"] = osobaId;

            return View("PregledUplataUposlenika", model);
        }
    }
}