using KarateKlub.Data;
using KarateKlub.Data.Models;
using KarateKlub.Web.Areas.ModulSekretar.Models;
using KarateKlub.Web.Helper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulSekretar.Controllers
{
    [Autorizacija(false, true, false, false, false)]

    public class RegistracijeKlubaController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulSekretar/RegistracijeKluba
        public ActionResult Index(int savez)
        {
           List<RegistracijeKluba> registracijeKluba = new List<RegistracijeKluba>();
            if (savez == 0)
            {
                registracijeKluba = ctx.RegistracijeKluba.Where(x => x.isDeleted == false).ToList();
            }
            else
            {
                registracijeKluba = ctx.RegistracijeKluba.Where(x => x.isDeleted == false && x.SavezId == savez).ToList();
            }

            RegistracijeKlubaIndexVM model = new RegistracijeKlubaIndexVM(registracijeKluba, savez);
            return View(model);
        }
        public ActionResult PregledSvihRegistracija(string DatumOd,string DatumDo)
        {
            List<TroskoviRegracijeKluba> troskoviRegistracijeKluba = new List<TroskoviRegracijeKluba>();

            RegistracijeKlubaIndexVM model;
            decimal ukupanIznosIzlazaZaTrosakRegistracijeKluba = 0;
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

                troskoviRegistracijeKluba = ctx.TroskoviRegracijeKluba.Where(x => x.isDeleted == false && x.DatumUplate >= datumOd && x.DatumUplate <= datumDo).ToList();

                model = new RegistracijeKlubaIndexVM(troskoviRegistracijeKluba);
             
                for (int i = 0; i < model.troskoviRegistracije.Count(); i++)
                {
                    ukupanIznosIzlazaZaTrosakRegistracijeKluba += model.troskoviRegistracije[i].trosakRegistracije.IznosKMBrojevima;
                }
                ViewData["ukupanIznosIzlazaZaTrosakRegistracijeKluba"] = ukupanIznosIzlazaZaTrosakRegistracijeKluba;
                return View("PregledSvihRegistracija", model);
            }
            else
            {
                ViewData["ukupanIznosIzlazaZaTrosakRegistracijeKluba"] =ukupanIznosIzlazaZaTrosakRegistracijeKluba;

                troskoviRegistracijeKluba = ctx.TroskoviRegracijeKluba.Where(x => x.isDeleted == false).ToList();

                model = new RegistracijeKlubaIndexVM(troskoviRegistracijeKluba);
                return View("PregledSvihRegistracija", model);
            }

            
        }
        public ActionResult GoToUpravljanjeRegistracijama(int savez)
        {
            return RedirectToAction("Index", "UpravljanjeRegistracijama", new { brojTaba = 1, savez = savez });
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
            RegistracijeKlubaDodajVM model = new RegistracijeKlubaDodajVM
            {
            savez=savez,
            savezi=BindSavezi()
            };
            model.savezi.Insert(0, new SelectListItem { Value =null, Text = "-Odaberite savez-" });

            return View("Dodaj", model);
        }

        private List<SelectListItem> BindSavezi()
        {
            return ctx.Savezi.Where(x => x.isDeleted == false).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv }).ToList();

        }

        public ActionResult Uredi(int registracijaId,int savez)
        {
            RegistracijeKluba registracija = ctx.RegistracijeKluba.Where(x => x.Id == registracijaId).FirstOrDefault();
            TroskoviRegracijeKluba trosak = ctx.TroskoviRegracijeKluba.Where(x => x.RegistracijaKlubaId == registracijaId && x.isDeleted==false).FirstOrDefault();
            RegistracijeKlubaUrediVM model = new RegistracijeKlubaUrediVM
            {
                Id = registracija.Id,
                isDeleted = registracija.isDeleted,
                Naziv=registracija.Naziv,
                DatumRegistracijeKluba = registracija.DatumRegistracijeKluba.ToString("dd.MM.yyyy"),
                DatumIstekaRegistracijeKluba = registracija.DatumIstekaRegistracijeKluba.ToString("dd.MM.yyyy"),
                SavezId = registracija.SavezId,
                OsobaId=registracija.OsobaId,
                DatumUplate=trosak.DatumUplate.ToString("dd.MM.yyyy"),
                IznosKMBrojevima=trosak.IznosKMBrojevima.ToString(),
                IznosKMSlovima=trosak.IznosKMSlovima,
                Obrazlozenje=trosak.Obrazlozenje,
                savez=savez,
                savezi=BindSavezi()
            };
            model.savezi.Insert(0, new SelectListItem { Value = null, Text = "Odaberite savez" });

            return View("Uredi", model);
        }

        public ActionResult SpremiNovuRegistracijuKluba(RegistracijeKlubaDodajVM model)
        {
            RegistracijeKluba registracija = new RegistracijeKluba();
            TroskoviRegracijeKluba trosak = new TroskoviRegracijeKluba();
            int korisnikId = Autentifikacija.GetLogiraniKorisnik(HttpContext).Osoba.Id;
            registracija.isDeleted = false;
            registracija.Naziv = model.Naziv;
            if(model.DatumRegistracijeKluba!=null)
            registracija.DatumRegistracijeKluba = KonvertujUDatum_dd_mm_yyyy(model.DatumRegistracijeKluba);
            if (model.DatumIstekaRegistracijeKluba != null)
                registracija.DatumIstekaRegistracijeKluba = KonvertujUDatum_dd_mm_yyyy(model.DatumIstekaRegistracijeKluba);
            registracija.SavezId = model.SavezId;
            registracija.OsobaId = korisnikId;
            ctx.RegistracijeKluba.Add(registracija);
            ctx.SaveChanges();
            int registracijaId = ctx.RegistracijeKluba.ToList().OrderByDescending(x => x.Id).FirstOrDefault().Id;
            trosak.isDeleted = false;
            trosak.RegistracijaKlubaId = registracijaId;
            trosak.IznosKMBrojevima = Convert.ToDecimal(model.IznosKMBrojevima);
            trosak.IznosKMSlovima = model.IznosKMSlovima;
            trosak.DatumUplate = KonvertujUDatum_dd_mm_yyyy(model.DatumUplate);
            trosak.Obrazlozenje = model.Obrazlozenje;
            ctx.TroskoviRegracijeKluba.Add(trosak);
            ctx.SaveChanges();
            return RedirectToAction("Index", "UpravljanjeRegistracijama", new { brojTaba = 1, savez = model.savez });

        }
        public ActionResult SpremiIzmjenuRegistracije(RegistracijeKlubaUrediVM model)
        {
            RegistracijeKluba registracija = ctx.RegistracijeKluba.Where(x => x.Id == model.Id).FirstOrDefault();
            TroskoviRegracijeKluba trosak = ctx.TroskoviRegracijeKluba.Where(x => x.RegistracijaKlubaId == model.Id && x.isDeleted == false).FirstOrDefault();
            registracija.Naziv = model.Naziv;
            registracija.DatumRegistracijeKluba = KonvertujUDatum_dd_mm_yyyy(model.DatumRegistracijeKluba);
            registracija.DatumIstekaRegistracijeKluba = KonvertujUDatum_dd_mm_yyyy(model.DatumIstekaRegistracijeKluba);
            registracija.SavezId = model.SavezId;
            trosak.DatumUplate = KonvertujUDatum_dd_mm_yyyy(model.DatumUplate);
            trosak.Obrazlozenje = model.Obrazlozenje;
            trosak.IznosKMBrojevima = Convert.ToDecimal(model.IznosKMBrojevima);
            trosak.IznosKMSlovima = model.IznosKMSlovima;
            ctx.SaveChanges();
            return RedirectToAction("Index", "UpravljanjeRegistracijama", new { brojTaba = 1, savez = model.savez });

        }
        public JsonResult Obrisi(int registracijaId)
        {

            RegistracijeKluba registracija = ctx.RegistracijeKluba.Where(x => x.Id ==registracijaId).FirstOrDefault();
            TroskoviRegracijeKluba trosak = ctx.TroskoviRegracijeKluba.Where(x => x.RegistracijaKlubaId == registracijaId && x.isDeleted == false).FirstOrDefault();
            if (trosak != null)
                trosak.isDeleted = true;
            registracija.isDeleted = true;
            ctx.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}