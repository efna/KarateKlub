using KarateKlub.Data;
using KarateKlub.Data.Models;
using KarateKlub.Web.Areas.ModulBlagajnik.Models;
using KarateKlub.Web.Helper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulBlagajnik.Controllers
{
    [Autorizacija(false, false, false, false, true)]

    public class RegistracijeTakmicaraController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulBlagajnik/RegistracijeTakmicara
        public ActionResult Index(int savez)
        {
            List<RegistracijeTakmicara> registracijeTakmicara = new List<RegistracijeTakmicara>();
            if (savez == 0)
            {
                registracijeTakmicara = ctx.RegistracijeTakmicara.Where(x => x.isDeleted == false).ToList();
            }
            else
            {
                registracijeTakmicara = ctx.RegistracijeTakmicara.Where(x => x.isDeleted == false && x.SavezId == savez).ToList();
            }

            RegistracijeTakmicaraIndexVM model = new RegistracijeTakmicaraIndexVM(registracijeTakmicara, savez);
            return View(model);
        }
        public ActionResult PregledSvihRegistracija(string DatumOd, string DatumDo)
        {
            List<TroskoviRegistracijeTakmicara> troskoviRegistracijeTakmicara = new List<TroskoviRegistracijeTakmicara>();

            RegistracijeTakmicaraIndexVM model;
            decimal ukupanIznosIzlazaZaTrosakRegistracijeKTakmicara = 0;
            if (DatumOd != "" && DatumDo != "")
            {
                string danDatumOd = DatumOd.Substring(0, 2);
                string mjesecDatumOd = DatumOd.Substring(3, 2);
                string godinaDatumOd = DatumOd.Substring(6, 4);

                string danDatumDo = DatumDo.Substring(0, 2);
                string mjesecDatumDo = DatumDo.Substring(3, 2);
                string godinaDatumDo = DatumDo.Substring(6, 4);


                string dOd = mjesecDatumOd + "/" + danDatumOd + "/" + godinaDatumOd;
                string dDo = mjesecDatumDo + "/" + danDatumDo + "/" + godinaDatumDo;

                CultureInfo provider = new CultureInfo("en-US");

                DateTime datumOd = DateTime.ParseExact(dOd, "MM/dd/yyyy",
                                  provider);

                DateTime datumDo = DateTime.ParseExact(dDo, "MM/dd/yyyy",
                                  provider);

                troskoviRegistracijeTakmicara = ctx.TroskoviRegistracijeTakmicara.Where(x => x.isDeleted == false && x.DatumUplate >= datumOd && x.DatumUplate <= datumDo).ToList();

                model = new RegistracijeTakmicaraIndexVM(troskoviRegistracijeTakmicara);

                for (int i = 0; i < model.troskoviRegistracije.Count(); i++)
                {
                    ukupanIznosIzlazaZaTrosakRegistracijeKTakmicara += model.troskoviRegistracije[i].trosakRegistracijeTakmicara.IznosKMBrojevima;
                }
                ViewData["ukupanIznosIzlazaZaTrosakRegistracijeKTakmicara"] = ukupanIznosIzlazaZaTrosakRegistracijeKTakmicara;
                return View("PregledSvihRegistracija", model);
            }
            else
            {
                ViewData["ukupanIznosIzlazaZaTrosakRegistracijeKTakmicara"] = ukupanIznosIzlazaZaTrosakRegistracijeKTakmicara;

                troskoviRegistracijeTakmicara = ctx.TroskoviRegistracijeTakmicara.Where(x => x.isDeleted == false).ToList();

                model = new RegistracijeTakmicaraIndexVM(troskoviRegistracijeTakmicara);
                return View("PregledSvihRegistracija", model);
            }
        }
        public ActionResult GoToUpravljanjeRegistracijamaTabTakmicari(int savez)
        {

            return RedirectToAction("PozivIndexStranice", "UpravljanjeRegistracijama", new { brTaba = 2, savez = savez });
        }
        public DateTime KonvertujUDatum_dd_mm_yyyy(string datum)
        {

            int dan = Convert.ToInt32(datum.Substring(0, 2));
            int mjesec = Convert.ToInt32(datum.Substring(3, 2));
            int godina = Convert.ToInt32(datum.Substring(6, 4));
            DateTime uneseniDatum = new DateTime(godina, mjesec, dan);
            return uneseniDatum;


        }
        public ActionResult Dodaj(int savez)
        {
            RegistracijeTakmicaraDodajVM model = new RegistracijeTakmicaraDodajVM
            {
                savez = savez,
                savezi = BindSavezi(),
                takmicariKluba = BindTakmicare(),

            };
            model.savezi.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite savez-" });
            model.takmicariKluba.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite takmičare-" });

            return View("Dodaj", model);
        }

        private List<SelectListItem> BindTakmicare()
        {
            return ctx.Takmicari.Where(x => x.isDeleted == false && x.ClanKluba.Osoba.isAktivnaOsoba == true && x.isAktivanTakmicar == true).Select(x => new SelectListItem { Value = x.ClanKlubaId.ToString(), Text = x.ClanKluba.Osoba.Ime + " (" + x.ClanKluba.Osoba.ImeRoditelja + ") " + x.ClanKluba.Osoba.Prezime }).ToList();

        }

        private List<SelectListItem> BindSavezi()
        {
            return ctx.Savezi.Where(x => x.isDeleted == false).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv }).ToList();

        }

        public ActionResult Uredi(int registracijaId, int savez)
        {
            RegistracijeTakmicara registracija = ctx.RegistracijeTakmicara.Where(x => x.Id == registracijaId).FirstOrDefault();
            TroskoviRegistracijeTakmicara trosak = ctx.TroskoviRegistracijeTakmicara.Where(x => x.RegistracijaTakmicaraId == registracijaId && x.isDeleted == false).FirstOrDefault();
            RegistracijeTakmicaraUrediVM model = new RegistracijeTakmicaraUrediVM
            {
                Id = registracija.Id,
                isDeleted = registracija.isDeleted,
                Naziv = registracija.Naziv,
                DatumIstekaRegistracijeTakmicara = registracija.DatumIstekaRegistracijeTakmicara.ToString("dd.MM.yyyy"),
                DatumRegistracijeTakmicara = registracija.DatumRegistracijeTakmicara.ToString("dd.MM.yyyy"),
                SavezId = registracija.SavezId,
                OsobaId = registracija.OsobaId,
                DatumUplate = trosak.DatumUplate.ToString("dd.MM.yyyy"),
                IznosKMBrojevima = trosak.IznosKMBrojevima.ToString(),
                IznosKMSlovima = trosak.IznosKMSlovima,
                Obrazlozenje = trosak.Obrazlozenje,
                savez = savez,
                savezi = BindSavezi()
            };
            model.savezi.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite savez-" });

            return View("Uredi", model);
        }

        public ActionResult SpremiNovuRegistracijuTakmicara(RegistracijeTakmicaraDodajVM model)
        {
            RegistracijeTakmicara registracija = new RegistracijeTakmicara();
            TroskoviRegistracijeTakmicara trosak = new TroskoviRegistracijeTakmicara();
            int korisnikId = Autentifikacija.GetLogiraniKorisnik(HttpContext).Osoba.Id;
            registracija.isDeleted = false;
            registracija.Naziv = model.Naziv;
            if (model.DatumRegistracijeTakmicara != null)
                registracija.DatumRegistracijeTakmicara = KonvertujUDatum_dd_mm_yyyy(model.DatumRegistracijeTakmicara);
            if (model.DatumIstekaRegistracijeTakmicara != null)
                registracija.DatumIstekaRegistracijeTakmicara = KonvertujUDatum_dd_mm_yyyy(model.DatumIstekaRegistracijeTakmicara);
            registracija.SavezId = model.SavezId;
            registracija.OsobaId = korisnikId;
            ctx.RegistracijeTakmicara.Add(registracija);
            ctx.SaveChanges();
            int registracijaId = ctx.RegistracijeTakmicara.ToList().OrderByDescending(x => x.Id).FirstOrDefault().Id;
            trosak.isDeleted = false;
            trosak.RegistracijaTakmicaraId = registracijaId;
            trosak.IznosKMBrojevima = Convert.ToDecimal(model.IznosKMBrojevima);
            trosak.IznosKMSlovima = model.IznosKMSlovima;
            trosak.DatumUplate = KonvertujUDatum_dd_mm_yyyy(model.DatumUplate);
            trosak.Obrazlozenje = model.Obrazlozenje;
            ctx.TroskoviRegistracijeTakmicara.Add(trosak);
            ctx.SaveChanges();
            RegistrovaniTakmicari registrovaniTakmicar;

            for (int i = 0; i < model.ClanKlubaId.Count(); i++)
            {
                registrovaniTakmicar = new RegistrovaniTakmicari();
                registrovaniTakmicar.isDeleted = false;
                registrovaniTakmicar.RegistracijaTakmicaraId = registracijaId;
                registrovaniTakmicar.ClanKlubaId = model.ClanKlubaId[i];
                ctx.RegistrovaniTakmicari.Add(registrovaniTakmicar);
                ctx.SaveChanges();
            }

            return RedirectToAction("PozivIndexStranice", "UpravljanjeRegistracijama", new { brTaba = 2, savez = model.savez });

        }
        public ActionResult SpremiIzmjenuRegistracijeTakmicara(RegistracijeTakmicaraUrediVM model)
        {
            RegistracijeTakmicara registracija = ctx.RegistracijeTakmicara.Where(x => x.Id == model.Id).FirstOrDefault();
            TroskoviRegistracijeTakmicara trosak = ctx.TroskoviRegistracijeTakmicara.Where(x => x.RegistracijaTakmicaraId == model.Id && x.isDeleted == false).FirstOrDefault();
            registracija.Naziv = model.Naziv;
            registracija.DatumIstekaRegistracijeTakmicara = KonvertujUDatum_dd_mm_yyyy(model.DatumIstekaRegistracijeTakmicara);
            registracija.DatumRegistracijeTakmicara = KonvertujUDatum_dd_mm_yyyy(model.DatumRegistracijeTakmicara);
            registracija.SavezId = model.SavezId;
            trosak.DatumUplate = KonvertujUDatum_dd_mm_yyyy(model.DatumUplate);
            trosak.Obrazlozenje = model.Obrazlozenje;
            trosak.IznosKMBrojevima = Convert.ToDecimal(model.IznosKMBrojevima);
            trosak.IznosKMSlovima = model.IznosKMSlovima;
            ctx.SaveChanges();
            return RedirectToAction("PozivIndexStranice", "UpravljanjeRegistracijama", new { brTaba = 2, savez = model.savez });

        }
        public JsonResult Obrisi(int registracijaId)
        {

            RegistracijeTakmicara registracija = ctx.RegistracijeTakmicara.Where(x => x.Id == registracijaId).FirstOrDefault();
            TroskoviRegistracijeTakmicara trosak = ctx.TroskoviRegistracijeTakmicara.Where(x => x.RegistracijaTakmicaraId == registracijaId && x.isDeleted == false).FirstOrDefault();
            if (trosak != null)
                trosak.isDeleted = true;
            registracija.isDeleted = true;
            ctx.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}