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

    public class UcesniciPolaganjaZaUcenickaZvanjaController : Controller
    {

        MojContext ctx = new MojContext();
        // GET: ModulSekretar/UcesniciPolaganjaZaUcenickaZvanja
        public ActionResult Index(int polaganjeId, int zvanje)
        {
            List<UcesniciPolaganjaZaUcenickaZvanja> ucesnici = new List<UcesniciPolaganjaZaUcenickaZvanja>();
            if (zvanje == 0)
            {
                ucesnici = ctx.UcesniciPolaganjaZaUcenickaZvanja.Where(x => x.isDeleted == false && x.PolaganjeUcenickaZvanjaId == polaganjeId).ToList();
            }
            else
            {
                ucesnici = ctx.UcesniciPolaganjaZaUcenickaZvanja.Where(x => x.isDeleted == false && x.PolaganjeUcenickaZvanjaId == polaganjeId && x.ZvanjeUKarateuId == zvanje).ToList();
            }

            UcesniciPolaganjaZaUcenickaZvanjeIndexVM model = new UcesniciPolaganjaZaUcenickaZvanjeIndexVM(ucesnici, zvanje);
            ViewData["polaganjeId"] = polaganjeId;
            return View(model);
        }

        public ActionResult GoToUpravljanjePolaganjaUcenickaZvanja(int zvanje, int polaganjeId)
        {
            return RedirectToAction("Index", "UpravljanjePolaganjaUcenickaZvanja", new { polaganjeId = polaganjeId, brojTaba = 1, zvanje = zvanje });
        }
        public ActionResult Dodaj(int polaganjeId)
        {
            UcesniciPolaganjaZaUcenickaZvanjaDodajVM model = new UcesniciPolaganjaZaUcenickaZvanjaDodajVM
            {
                clanoviKluba = BindClanoveKluba(),
                zvanjaUKarateu = BindZvanjaUKarateu(),
                PolaganjeUcenickaZvanjaId = polaganjeId
            };
            model.zvanjaUKarateu.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite zvanje-" });
            model.clanoviKluba.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite člana-" });

            return View("Dodaj", model);
        }

        private List<SelectListItem> BindZvanjaUKarateu()
        {
            return ctx.ZvanjaUKarateu.Where(x => x.isDeleted == false).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv }).ToList();

        }

        private List<SelectListItem> BindClanoveKluba()
        {
            return ctx.ClanoviKluba.OrderBy(x=>x.Osoba.Ime).Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Osoba.Ime + " (" + x.Osoba.ImeRoditelja + ") " + x.Osoba.Prezime }).ToList();

        }
        public ActionResult SpremiNovogUcesnikaPolaganjaZaUcenickaZvanja(UcesniciPolaganjaZaUcenickaZvanjaDodajVM model)
        {
            UcesniciPolaganjaZaUcenickaZvanja ucesnik = new UcesniciPolaganjaZaUcenickaZvanja();
            RezultatiPolaganjaUcenickaZvanja rezultat = new RezultatiPolaganjaUcenickaZvanja();

            ucesnik.isDeleted = false;
            ucesnik.PolaganjeUcenickaZvanjaId = model.PolaganjeUcenickaZvanjaId;
            ucesnik.ClanKlubaId = model.ClanKlubaId;
            ucesnik.ZvanjeUKarateuId = model.ZvanjeUKarateuId;
            ucesnik.PolaganjeUcenickaZvanjaId = model.PolaganjeUcenickaZvanjaId;
            ctx.UcesniciPolaganjaZaUcenickaZvanja.Add(ucesnik);
            ctx.SaveChanges();
            int ucesnikId = ctx.UcesniciPolaganjaZaUcenickaZvanja.ToList().OrderByDescending(x => x.Id).FirstOrDefault().Id;
            rezultat.isDeleted = false;
            rezultat.UcesnikPolaganjaZaUcenickaZvanjaId = ucesnikId;
            rezultat.PolaganjeUcenickaZvanjaId = model.PolaganjeUcenickaZvanjaId;
            rezultat.isPolozio = false;
            ctx.RezultatiPolaganjaUcenickaZvanja.Add(rezultat);
            ctx.SaveChanges();
            return RedirectToAction("Index", "UpravljanjePolaganjaUcenickaZvanja", new { polaganjeId = model.PolaganjeUcenickaZvanjaId, brojTaba = 1, zvanje = model.ZvanjeUKarateuId });
        }

        public ActionResult Uredi(int ucesnikId)
        {
            UcesniciPolaganjaZaUcenickaZvanja ucesnik = ctx.UcesniciPolaganjaZaUcenickaZvanja.Where(x => x.Id == ucesnikId).FirstOrDefault();
            UcesniciPolaganjaZaUcenickaZvanjaUrediVM model = new UcesniciPolaganjaZaUcenickaZvanjaUrediVM
            {
                Id = ucesnikId,
                isDeleted = ucesnik.isDeleted,
                PolaganjeUcenickaZvanjaId = ucesnik.PolaganjeUcenickaZvanjaId,
                ZvanjeUKarateuId = ucesnik.ZvanjeUKarateuId,
                ClanKlubaId = ucesnik.ClanKlubaId,
                zvanjaUKarateu = BindZvanjaUKarateu(),
                clanoviKluba = BindClanoveKluba()
            };
            model.zvanjaUKarateu.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite zvanje-" });
            model.clanoviKluba.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite člana-" });

            return View("Uredi", model);
        }
        public ActionResult SpremiIzmjenuUcesnikaPolaganjaZaUcenickaZvanja(UcesniciPolaganjaZaUcenickaZvanjaUrediVM model)
        {
            UcesniciPolaganjaZaUcenickaZvanja ucesnik = ctx.UcesniciPolaganjaZaUcenickaZvanja.Where(x => x.Id == model.Id).FirstOrDefault();

            ucesnik.ClanKlubaId = model.ClanKlubaId;
            ucesnik.ZvanjeUKarateuId = model.ZvanjeUKarateuId;
            ctx.SaveChanges();
            return RedirectToAction("Index", "UpravljanjePolaganjaUcenickaZvanja", new { polaganjeId = model.PolaganjeUcenickaZvanjaId, brojTaba = 1, zvanje = model.ZvanjeUKarateuId });

        }
        public JsonResult Obrisi(int ucesnikId)
        {
            UcesniciPolaganjaZaUcenickaZvanja ucesnik = ctx.UcesniciPolaganjaZaUcenickaZvanja.Where(x => x.Id == ucesnikId).FirstOrDefault();
            RezultatiPolaganjaUcenickaZvanja rezultat = ctx.RezultatiPolaganjaUcenickaZvanja.Where(x => x.UcesnikPolaganjaZaUcenickaZvanjaId == ucesnikId && x.isDeleted == false).FirstOrDefault();
            ParticipacijeZaPolaganjeUcenickaZvanja participacija = ctx.ParticipacijeZaPolaganjeUcenickaZvanja.Where(x => x.UcesnikPolaganjaZaUcenickaZvanjaId == ucesnikId && x.isDeleted == false).FirstOrDefault();

            if (rezultat != null)
                rezultat.isDeleted = true;
            if (ucesnik != null)
                ucesnik.isDeleted = true;
            if (participacija != null)
                participacija.isDeleted = true;
            ctx.SaveChanges();

            return Json(true, JsonRequestBehavior.AllowGet);


        }
    }
}