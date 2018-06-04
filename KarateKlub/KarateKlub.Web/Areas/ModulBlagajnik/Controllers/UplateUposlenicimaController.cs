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

    public class UplateUposlenicimaController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulBlagajnik/UplateUposlenicima
        public ActionResult Index()
        {
            UplateUposlenicimaIndexVM model = new UplateUposlenicimaIndexVM
            {
                uplateUposlenicima = ctx.UplateUposlenicima.Where(x => x.isDeleted == false).Select(x => new UplataUposlenikaPodaci
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
                }).ToList()
            };

            return View(model);
        }
        public ActionResult PregledUplataSvihUposlenikaZaIzlaz(string DatumOd, string DatumDo)
        {


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

                UplateUposlenicimaIndexVM model = new UplateUposlenicimaIndexVM
                {
                    uplateUposlenicima = ctx.UplateUposlenicima.Where(x => x.isDeleted == false && x.DatumUplate >= datumOd && x.DatumUplate <= datumDo).Select(x => new UplataUposlenikaPodaci
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
                    }).ToList()
                };
                decimal ukupanIznosIzlazaZaUplate = 0;
                for (int i = 0; i < model.uplateUposlenicima.Count(); i++)
                {
                    ukupanIznosIzlazaZaUplate += model.uplateUposlenicima[i].IznosKMBrojevima;
                }
                ViewData["ukupanIznosIzlazaZaUplate"] = ukupanIznosIzlazaZaUplate;
                return View("PregledUplataSvihUposlenikaZaIzlaz", model);

            }

            else
            {
                UplateUposlenicimaIndexVM model = new UplateUposlenicimaIndexVM
                {
                    uplateUposlenicima = ctx.UplateUposlenicima.Where(x => x.isDeleted == false).Select(x => new UplataUposlenikaPodaci
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
                    }).ToList()
                };
                return View("Index", model);
            }

        }
        public ActionResult PregledUplataUposlenika(int osobaId, int funkcijaOsobe)
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
            ViewData["funkcijaOsobe"] = funkcijaOsobe;
            return View("PregledUplataUposlenika", model);
        }
        public ActionResult PregledUplataUposlenika2(int osobaId, int aktivan)
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
            ViewData["aktivan"] = aktivan;
            ViewData["osobaId"] = osobaId;

            return View("PregledUplataUposlenika2", model);
        }
        public DateTime KonvertujUDatum_dd_mm_yyyy(string datum)
        {

            int dan = Convert.ToInt32(datum.Substring(0, 2));
            int mjesec = Convert.ToInt32(datum.Substring(3, 2));
            int godina = Convert.ToInt32(datum.Substring(6, 4));
            DateTime uneseniDatum = new DateTime(godina, mjesec, dan);
            return uneseniDatum;


        }
        public ActionResult Dodaj(int osobaId, int funkcijaOsobe)
        {
            UplateUposlenicimaDodajVM model = new UplateUposlenicimaDodajVM
            {
                OsobaId = osobaId,
                funkcijaOsobe = funkcijaOsobe
            };
            return View("Dodaj", model);
        }
        public ActionResult SpremiNovuUplatu(UplateUposlenicimaDodajVM model)
        {
            UplateUposlenicima uplata = new UplateUposlenicima();
            uplata.isDeleted = false;
            uplata.OsobaId = model.OsobaId;
            if (model.DatumUplate != null)
                uplata.DatumUplate = KonvertujUDatum_dd_mm_yyyy(model.DatumUplate);
            if (model.DatumOd != null)
                uplata.DatumOd = KonvertujUDatum_dd_mm_yyyy(model.DatumOd);
            if (model.DatumDo != null)
                uplata.DatumDo = KonvertujUDatum_dd_mm_yyyy(model.DatumDo);
            uplata.SvrhaUplate = model.SvrhaUplate;
            uplata.IznosKMBrojevima = Convert.ToDecimal(model.IznosKMBrojevima);
            uplata.IznosKMSLovima = model.IznosKMSLovima;
            uplata.Obrazlozenje = model.Obrazlozenje;
            ctx.UplateUposlenicima.Add(uplata);
            ctx.SaveChanges();
            return RedirectToAction("PregledUplataUposlenika", new { osobaId = model.OsobaId, funkcijaOsobe = model.funkcijaOsobe });
        }
        public ActionResult Uredi(int uplataId, int funkcijaOsobe)
        {
            UplateUposlenicima uplata = ctx.UplateUposlenicima.Where(x => x.Id == uplataId).FirstOrDefault();
            UplateUposlenicimaUrediVM model = new UplateUposlenicimaUrediVM
            {
                Id = uplataId,
                isDeleted = uplata.isDeleted,
                DatumUplate = uplata.DatumUplate.ToString("dd.MM.yyyy"),
                DatumOd = uplata.DatumOd.ToString("dd.MM.yyyy"),
                DatumDo = uplata.DatumDo.ToString("dd.MM.yyyy"),
                SvrhaUplate = uplata.SvrhaUplate,
                Obrazlozenje = uplata.Obrazlozenje,
                OsobaId = uplata.OsobaId,
                IznosKMBrojevima = uplata.IznosKMBrojevima.ToString(),
                IznosKMSLovima = uplata.IznosKMSLovima,
                funkcijaOsobe = funkcijaOsobe
            };
            return View("Uredi", model);
        }
        public ActionResult SpremiIzmjenuUplate(UplateUposlenicimaUrediVM model)
        {
            UplateUposlenicima uplata = ctx.UplateUposlenicima.Where(x => x.Id == model.Id).FirstOrDefault();

            if (model.DatumUplate != null)
                uplata.DatumUplate = KonvertujUDatum_dd_mm_yyyy(model.DatumUplate);
            if (model.DatumOd != null)
                uplata.DatumOd = KonvertujUDatum_dd_mm_yyyy(model.DatumOd);
            if (model.DatumDo != null)
                uplata.DatumDo = KonvertujUDatum_dd_mm_yyyy(model.DatumDo);
            uplata.SvrhaUplate = model.SvrhaUplate;
            uplata.IznosKMBrojevima = Convert.ToDecimal(model.IznosKMBrojevima);
            uplata.IznosKMSLovima = model.IznosKMSLovima;
            uplata.Obrazlozenje = model.Obrazlozenje;
            ctx.SaveChanges();
            return RedirectToAction("PregledUplataUposlenika", new { osobaId = model.OsobaId, funkcijaOsobe = model.funkcijaOsobe });

        }
        public JsonResult Obrisi(int uplataId)
        {
            UplateUposlenicima uplata = ctx.UplateUposlenicima.Where(x => x.Id == uplataId).FirstOrDefault();
            if (uplata != null)
                uplata.isDeleted = true;
            ctx.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}