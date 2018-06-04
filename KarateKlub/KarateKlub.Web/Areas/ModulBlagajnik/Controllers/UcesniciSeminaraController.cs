using KarateKlub.Data;
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

    public class UcesniciSeminaraController : Controller
    {
        MojContext ctx = new MojContext();

        // GET: ModulBlagajnik/UcesniciSeminara
        public ActionResult Index(int seminarId)
        {
            UcesniciSeminaraIndexVM model = new UcesniciSeminaraIndexVM
            {
                ucesniciSeminara = ctx.UcesniciSeminara.Where(x => x.isDeleted == false && x.SeminariId == seminarId).Select(x => new UcesnikSeminaraPodaci
                {
                    Id = x.Id,
                    isDeleted = x.isDeleted,
                    SeminariId = x.SeminariId,
                    OsobaId = x.OsobaId,
                    Osoba = x.Osoba.Ime + " (" + x.Osoba.ImeRoditelja + ") " + x.Osoba.Prezime,
                    Spol = x.Osoba.Spol,
                    DatumRodjenja = x.Osoba.DatumRodjenja,
                    MjestoRodjenja = x.Osoba.MjestoRodjenja,
                    KontaktTelefon = x.Osoba.KontaktTelefon,
                    Email = x.Osoba.Email

                }).ToList()
            };
            ViewData["seminarId"] = seminarId;
            return View(model);
        }
    }
}