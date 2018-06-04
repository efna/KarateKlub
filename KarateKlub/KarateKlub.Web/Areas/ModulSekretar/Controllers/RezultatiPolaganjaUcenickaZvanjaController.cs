using KarateKlub.Data;
using KarateKlub.Data.Models;
using KarateKlub.Web.Areas.ModulSekretar.Models;
using KarateKlub.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulSekretar.Controllers
{
    [Autorizacija(false, true, false, false, false)]

    public class RezultatiPolaganjaUcenickaZvanjaController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulSekretar/RezultatiPolaganjaUcenickaZvanja
        public ActionResult Index(int polaganjeId)
        {
            RezultatiPolaganjaUcenickaZvanjaIndexVM model = new RezultatiPolaganjaUcenickaZvanjaIndexVM
            {
                rezultatiPolaganja = ctx.RezultatiPolaganjaUcenickaZvanja.Where(x => x.isDeleted == false && x.PolaganjeUcenickaZvanjaId == polaganjeId).Select(x => new RezultatPolaganjaZaUcenickaZvanjaPodaci
                {
                    Id = x.Id,
                    UcesnikPolaganjaZaUcenickaZvanjaId = x.UcesnikPolaganjaZaUcenickaZvanjaId,
                    PolaganjeUcenickaZvanjaId = x.PolaganjeUcenickaZvanjaId,
                    isPolozio = x.isPolozio,
                    isDeleted = x.isDeleted,
                    Ucesnik = x.UcesnikPolaganjaZaUcenickaZvanja.ClanKluba.Osoba.Ime + " (" + x.UcesnikPolaganjaZaUcenickaZvanja.ClanKluba.Osoba.ImeRoditelja + ") " + x.UcesnikPolaganjaZaUcenickaZvanja.ClanKluba.Osoba.Prezime,
                    Zvanje = x.UcesnikPolaganjaZaUcenickaZvanja.ZvanjeUKarateu.Naziv

                }).ToList(),
                polaganjeId=polaganjeId
            };
         
            return View(model);
        }

        public ActionResult Index2(int polaganjeId)
        {
            RezultatiPolaganjaUcenickaZvanjaIndexVM model = new RezultatiPolaganjaUcenickaZvanjaIndexVM
            {
                rezultatiPolaganja = ctx.RezultatiPolaganjaUcenickaZvanja.Where(x => x.isDeleted == false && x.PolaganjeUcenickaZvanjaId == polaganjeId).OrderByDescending(x=>x.UcesnikPolaganjaZaUcenickaZvanja.ZvanjeUKarateu.Naziv).Select(x => new RezultatPolaganjaZaUcenickaZvanjaPodaci
                {
                    Id = x.Id,
                    UcesnikPolaganjaZaUcenickaZvanjaId = x.UcesnikPolaganjaZaUcenickaZvanjaId,
                    PolaganjeUcenickaZvanjaId = x.PolaganjeUcenickaZvanjaId,
                    isPolozio = x.isPolozio,
                    isDeleted = x.isDeleted,
                    Ucesnik = x.UcesnikPolaganjaZaUcenickaZvanja.ClanKluba.Osoba.Ime + " (" + x.UcesnikPolaganjaZaUcenickaZvanja.ClanKluba.Osoba.ImeRoditelja + ") " + x.UcesnikPolaganjaZaUcenickaZvanja.ClanKluba.Osoba.Prezime,
                    Zvanje = x.UcesnikPolaganjaZaUcenickaZvanja.ZvanjeUKarateu.Naziv

                }).ToList(),
                polaganjeId = polaganjeId
            };
            return View("Index2",model);
        }
        [HttpPost]
        public ActionResult SpremiRezultat(RezultatiPolaganjaUcenickaZvanjaIndexVM model)
        {


            foreach (var rez in model.rezultatiPolaganja)
            { 
                RezultatiPolaganjaUcenickaZvanja rezultat = ctx.RezultatiPolaganjaUcenickaZvanja.Where(x => x.Id == rez.Id).FirstOrDefault();
                rezultat.isPolozio = rez.isPolozio;
                ctx.SaveChanges();

            }
            return RedirectToAction("Index", "UpravljanjePolaganjaUcenickaZvanja", new { polaganjeId = model.polaganjeId, brojTaba = 2, zvanje =0 });

        }

    }
}