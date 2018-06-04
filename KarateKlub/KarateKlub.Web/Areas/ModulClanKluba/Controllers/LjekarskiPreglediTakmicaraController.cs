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

    public class LjekarskiPreglediTakmicaraController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulClanKluba/LjekarskiPreglediTakmicara
        public ActionResult Index(int takmicarId)
        {
            Takmicari takmicar = ctx.Takmicari.Where(x => x.Id == takmicarId).FirstOrDefault();
            LjekarskiPreglediTakmicaraIndexVM model = new LjekarskiPreglediTakmicaraIndexVM
            {
                ljekarskiPregledi = ctx.LjekarskiPreglediTakmicara.Where(x => x.TakmicarId == takmicarId && x.isDeleted == false).Select(x => new LjekarskiPreglediTakmicaraPodaci
                {
                    Id = x.Id,
                    isDeleted = x.isDeleted,
                    TakmicarId = x.TakmicarId,
                    DatumLjekarskogPregleda = x.DatumLjekarskogPregleda,
                    Dijagnoza = x.Dijagnoza,
                    Doktor = x.Titula + " " + x.ImeDoktora + " " + x.PrezimeDoktora

                }).ToList(),
                Takmicar = takmicar.ClanKluba.Osoba.Ime + " (" + takmicar.ClanKluba.Osoba.ImeRoditelja + ") " + takmicar.ClanKluba.Osoba.Prezime,
                takmicarId = takmicarId


            };

            return View(model);
        }
    }
}