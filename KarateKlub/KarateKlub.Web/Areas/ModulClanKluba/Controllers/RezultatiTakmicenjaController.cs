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

    public class RezultatiTakmicenjaController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulClanKluba/RezultatiTakmicenja
        public ActionResult Index(int takmicarId)
        {
            RezultatiTakmicenjaIndexVM model = new RezultatiTakmicenjaIndexVM
            {
                rezultatiTakmicenja = ctx.RezultatiTakmicenja.Where(x => x.TakmicarId == takmicarId && x.isDeleted == false).Select(x => new RezultatTakmicenjaPodaci
                {
                    Id = x.Id,
                    isDeleted = x.isDeleted,
                    TakmicenjeId = x.TakmicenjeId,
                    Takmicenje = x.Takmicenje.NazivTakmicenja,
                    TakmicarId = x.TakmicarId,
                    Takmicar = x.Takmicar.ClanKluba.Osoba.Ime + " (" + x.Takmicar.ClanKluba.Osoba.ImeRoditelja + ") " + x.Takmicar.ClanKluba.Osoba.Prezime,
                    DisciplinaTakmicenjaId = x.DisciplinaTakmicenjaId,
                    DisciplinaTakmicenja = x.DisciplinaTakmicenja.Naziv,
                    Kategorija = x.Kategorija,
                    BrojTakmicaraUKategoriji = x.BrojTakmicaraUKategoriji.ToString(),
                    BrojPobjeda = x.BrojPobjeda.ToString(),
                    BrojPoraza = x.BrojPoraza.ToString(),
                    OsvojenoMjestoNaTakmicenjuId = x.OsvojenoMjestoNaTakmicenjuId,
                    OsvojenoMjestoNaTakmicenju = x.OsvojenoMjestoNaTakmicenju.Naziv,
                    Obrazlozenje = x.Obrazlozenje,
                    StarosnaDobId = x.StarosnaDobId,
                    StarosnaDob = x.StarosnaDob.Naziv,
                    Datum = x.Takmicenje.DatumOdrzavanjaTakmicenja
                }).ToList()

            };
            for (int i = 0; i < model.rezultatiTakmicenja.Count(); i++)
            {
                if (model.rezultatiTakmicenja[i].BrojPobjeda == "0")
                    model.rezultatiTakmicenja[i].BrojPobjeda = "/";
                if (model.rezultatiTakmicenja[i].BrojPoraza == "0")
                    model.rezultatiTakmicenja[i].BrojPoraza = "/";
                if (model.rezultatiTakmicenja[i].BrojTakmicaraUKategoriji == "0")
                    model.rezultatiTakmicenja[i].BrojTakmicaraUKategoriji = "/";
            }
            return View("Index", model);
        }
    }
}