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

    public class ZaduzenjeOpremeKlubaClanovimaController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulSekretar/ZaduzenjeOpremeKlubaClanovima
        public ActionResult Index(int aktivnost=0)
        {
            List<ZaduzenjeOpremeKlubaClanovima> zaduzenja = new List<ZaduzenjeOpremeKlubaClanovima>();
            
            if (aktivnost == 0) {
                zaduzenja = ctx.ZaduzenjeOpremeKlubaClanovima.Where(x => x.isAktivnoZaduzenje == true && x.isDeleted == false).ToList();
                   }
            else if (aktivnost == 1)
            {
                zaduzenja = ctx.ZaduzenjeOpremeKlubaClanovima.Where(x => x.isAktivnoZaduzenje == false && x.isDeleted == false).ToList();

            }
            else
            {
                zaduzenja = ctx.ZaduzenjeOpremeKlubaClanovima.Where(x=>x.isDeleted == false).ToList();

            }
            ZaduzenjeOpremeKlubaClanovimaIndexVM model = new ZaduzenjeOpremeKlubaClanovimaIndexVM(zaduzenja, aktivnost);
            ViewData["aktivnost"] = aktivnost;

            return View(model);
        }
        public ActionResult GoToUpravljanjeOpremomKlubaByAktivnost(int aktivnost = 0)
        {
            return RedirectToAction("Index", "UpravljanjeOpremomKluba", new { brojTaba = 2, aktivnost = aktivnost });
        }
        public ActionResult Dodaj(int aktivnost)
        {
            ZaduzenjeOpremeKlubaClanovimaDodajVM model = new ZaduzenjeOpremeKlubaClanovimaDodajVM
            {
                vrsteOpremeKluba = BindVrsteOpreme(),
                jediniceMjere = BindJediniceMjere(),
                clanoviKluba=BindClanoviKluba(),
                DatumZaduzenjaOpreme="",
                aktivnost=aktivnost
            };
            model.vrsteOpremeKluba.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite vrstu opreme-" });
            model.jediniceMjere.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite jedinicu mjere-" });
            model.clanoviKluba.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite člana-" });

            return View("Dodaj", model);
        }

        private List<SelectListItem> BindClanoviKluba()
        {
            return ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba==true).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Osoba.Ime+" ("+x.Osoba.ImeRoditelja+") "+x.Osoba.Prezime }).ToList();

        }

        private List<SelectListItem> BindJediniceMjere()
        {
            return ctx.JediniceMjere.Where(x => x.isDeleted == false).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv }).ToList();

        }

        private List<SelectListItem> BindVrsteOpreme()
        {
            return ctx.VrsteOpremeKluba.Where(x => x.isDeleted == false).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv }).ToList();

        }

        public ActionResult Uredi(int zaduzenjeId,int aktivnost)
        {
            ZaduzenjeOpremeKlubaClanovima zaduzenje = ctx.ZaduzenjeOpremeKlubaClanovima.Where(x => x.Id == zaduzenjeId).FirstOrDefault();

            ZaduzenjeOpremeKlubaClanovimaUrediVM model = new ZaduzenjeOpremeKlubaClanovimaUrediVM
            {
                vrsteOpremeKluba = BindVrsteOpreme(),
                jediniceMjere = BindJediniceMjere(),
                clanoviKluba=BindClanoviKluba(),
                Id = zaduzenjeId,
                isDeleted=zaduzenje.isDeleted,
                JedinicaMjereId = zaduzenje.JedinicaMjereId,
                VrstaOpremeKlubaId = zaduzenje.VrstaOpremeKlubaId,
                ClanKlubaId=zaduzenje.ClanKlubaId,
                DatumZaduzenjaOpreme = zaduzenje.DatumZaduzenjaOpreme.ToString("dd.MM.yyyy"),
                Obrazlozenje = zaduzenje.Obrazlozenje,
                isAktivnoZaduzenje=zaduzenje.isAktivnoZaduzenje,
                aktivnost=aktivnost

            };

            model.vrsteOpremeKluba.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite vrstu opreme-" });
            model.jediniceMjere.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite jedinicu mjere-" });
            model.clanoviKluba.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite člana-" });

            return View("Uredi", model);

        }
        public ActionResult SpremiNovuZaduzenuOpremu(ZaduzenjeOpremeKlubaClanovimaDodajVM model)
        {
            ZaduzenjeOpremeKlubaClanovima zaduzenje = new ZaduzenjeOpremeKlubaClanovima();
            zaduzenje.isDeleted = false;
            zaduzenje.isAktivnoZaduzenje = true;
            zaduzenje.JedinicaMjereId = model.JedinicaMjereId;
            zaduzenje.VrstaOpremeKlubaId = model.VrstaOpremeKlubaId;
            zaduzenje.ClanKlubaId = model.ClanKlubaId;
            zaduzenje.DatumZaduzenjaOpreme = KonvertujUDatum_dd_mm_yyyy(model.DatumZaduzenjaOpreme);
            zaduzenje.Obrazlozenje = model.Obrazlozenje;
            ctx.ZaduzenjeOpremeKlubaClanovima.Add(zaduzenje);
            ctx.SaveChanges();

            return RedirectToAction("Index", "UpravljanjeOpremomKluba", new { brojTaba = 2,aktivnost=model.aktivnost});

        }
        public DateTime KonvertujUDatum_dd_mm_yyyy(string datum)
        {

            int dan = Convert.ToInt32(datum.Substring(0, 2));
            int mjesec = Convert.ToInt32(datum.Substring(3, 2));
            int godina = Convert.ToInt32(datum.Substring(6, 4));
            DateTime uneseniDatum = new DateTime(godina, mjesec, dan);
            return uneseniDatum;


        }
        public ActionResult SpremiIzmjenuZaduzeneOpreme(ZaduzenjeOpremeKlubaClanovimaUrediVM model)
        {
            ZaduzenjeOpremeKlubaClanovima zaduzenje = ctx.ZaduzenjeOpremeKlubaClanovima.Where(x => x.Id == model.Id).FirstOrDefault();
            zaduzenje.isAktivnoZaduzenje = model.isAktivnoZaduzenje;
            zaduzenje.JedinicaMjereId = model.JedinicaMjereId;
            zaduzenje.VrstaOpremeKlubaId = model.VrstaOpremeKlubaId;
            zaduzenje.ClanKlubaId = model.ClanKlubaId;
            zaduzenje.DatumZaduzenjaOpreme = KonvertujUDatum_dd_mm_yyyy(model.DatumZaduzenjaOpreme);
            zaduzenje.Obrazlozenje = model.Obrazlozenje;
            ctx.SaveChanges();
            return RedirectToAction("Index", "UpravljanjeOpremomKluba", new { brojTaba = 2,aktivnost=model.aktivnost});


        }
        public JsonResult Obrisi(int zaduzenaOpremaId)
        {
            ZaduzenjeOpremeKlubaClanovima zaduzenje = ctx.ZaduzenjeOpremeKlubaClanovima.Where(x => x.Id == zaduzenaOpremaId).FirstOrDefault();
            zaduzenje.isDeleted = true;
            zaduzenje.isAktivnoZaduzenje = false;
            RazduzenaOpremaKlubaClanovi razduzenje = ctx.RazduzenaOpremaKlubaClanovi.Where(x => x.ZaduzenjeOpremeKlubaClanovimaId == zaduzenaOpremaId).FirstOrDefault();
            if (razduzenje != null)
                razduzenje.isDeleted = true;
            ctx.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}