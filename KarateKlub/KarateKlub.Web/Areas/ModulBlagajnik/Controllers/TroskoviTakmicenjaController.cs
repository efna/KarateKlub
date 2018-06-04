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

    public class TroskoviTakmicenjaController : Controller
    {
        MojContext ctx = new MojContext();

        // GET: ModulBlagajnik/TroskoviTakmicenja
        public ActionResult Index(int takmicenjeId)
        {
            TroskoviTakmicenjaIndexVM model = new TroskoviTakmicenjaIndexVM
            {
                troskoviTakmicenja = ctx.TroskoviTakmicenja.Where(x => x.isDeleted == false && x.TakmicenjeId == takmicenjeId).Select(x => new TroskoviTakmicenjaPodaci
                {
                    Id = x.Id,
                    isDeleted = x.isDeleted,
                    Naziv = x.Naziv,
                    TakmicenjeId = x.TakmicenjeId,
                    Takmicenje = x.Takmicenje.NazivTakmicenja,
                    IznosKMBrojevima = x.IznosKMBrojevima,
                    IznosKMSlovima = x.IznosKMSlovima,
                    DatumUplate = x.DatumUplate,
                    Obrazlozenje = x.Obrazlozenje,
                    DatumOdravanjaTakmicenja = x.Takmicenje.DatumOdrzavanjaTakmicenja,
                    MjestOdrzavanjaTakmicenja = x.Takmicenje.MjestoOdrzavanjaTakmicenja

                }).ToList(),
                takmicenjeId = takmicenjeId,
                NazivTakmicenja = ctx.Takmicenja.Where(x => x.Id == takmicenjeId).FirstOrDefault().NazivTakmicenja
            };
            decimal ukupanTrosakTakmicenja = 0;
            List<TroskoviTakmicenja> troskoviTakmicenja = ctx.TroskoviTakmicenja.Where(x => x.isDeleted == false && x.TakmicenjeId == takmicenjeId).ToList();
            for (int i = 0; i < troskoviTakmicenja.Count(); i++)
            {
                ukupanTrosakTakmicenja += troskoviTakmicenja[i].IznosKMBrojevima;
            }
            ViewData["ukupanTrosakTakmicenja"] = ukupanTrosakTakmicenja;

            return View(model);

        }
        public ActionResult PregledSvihTroskova(string DatumOd, string DatumDo)
        {
            decimal ukupanIznosIzlazaZaTroskoveTakmicenja = 0;

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

                TroskoviTakmicenjaIndexVM model = new TroskoviTakmicenjaIndexVM
                {
                    troskoviTakmicenja = ctx.TroskoviTakmicenja.Where(x => x.isDeleted == false && x.DatumUplate >= datumOd && x.DatumUplate <= datumDo).Select(x => new TroskoviTakmicenjaPodaci
                    {
                        Id = x.Id,
                        isDeleted = x.isDeleted,
                        Naziv = x.Naziv,
                        TakmicenjeId = x.TakmicenjeId,
                        Takmicenje = x.Takmicenje.NazivTakmicenja,
                        IznosKMBrojevima = x.IznosKMBrojevima,
                        IznosKMSlovima = x.IznosKMSlovima,
                        DatumUplate = x.DatumUplate,
                        Obrazlozenje = x.Obrazlozenje,
                        DatumOdravanjaTakmicenja = x.Takmicenje.DatumOdrzavanjaTakmicenja,
                        MjestOdrzavanjaTakmicenja = x.Takmicenje.MjestoOdrzavanjaTakmicenja
                    }).ToList()
                };
                for (int i = 0; i < model.troskoviTakmicenja.Count(); i++)
                {
                    ukupanIznosIzlazaZaTroskoveTakmicenja += model.troskoviTakmicenja[i].IznosKMBrojevima;
                }
                ViewData["ukupanIznosIzlazaZaTroskoveTakmicenja"] = ukupanIznosIzlazaZaTroskoveTakmicenja;
                return View("PregledSvihTroskova", model);

            }
            else
            {
                ViewData["ukupanIznosIzlazaZaTroskoveTakmicenja"] = ukupanIznosIzlazaZaTroskoveTakmicenja;

                TroskoviTakmicenjaIndexVM model = new TroskoviTakmicenjaIndexVM
                {
                    troskoviTakmicenja = ctx.TroskoviTakmicenja.Where(x => x.isDeleted == false).Select(x => new TroskoviTakmicenjaPodaci
                    {
                        Id = x.Id,
                        isDeleted = x.isDeleted,
                        Naziv = x.Naziv,
                        TakmicenjeId = x.TakmicenjeId,
                        Takmicenje = x.Takmicenje.NazivTakmicenja,
                        IznosKMBrojevima = x.IznosKMBrojevima,
                        IznosKMSlovima = x.IznosKMSlovima,
                        DatumUplate = x.DatumUplate,
                        Obrazlozenje = x.Obrazlozenje,
                        DatumOdravanjaTakmicenja = x.Takmicenje.DatumOdrzavanjaTakmicenja,
                        MjestOdrzavanjaTakmicenja = x.Takmicenje.MjestoOdrzavanjaTakmicenja
                    }).ToList()
                };
                return View("PregledSvihTroskova", model);
            }
        }
        public ActionResult Dodaj(int takmicenjeId)
        {
            TroskoviTakmicenjaDodajVM model = new TroskoviTakmicenjaDodajVM
            {
                TakmicenjeId = takmicenjeId
            };

            return View("Dodaj", model);
        }
        public DateTime KonvertujUDatum_dd_mm_yyyy(string datum)
        {

            int dan = Convert.ToInt32(datum.Substring(0, 2));
            int mjesec = Convert.ToInt32(datum.Substring(3, 2));
            int godina = Convert.ToInt32(datum.Substring(6, 4));
            DateTime uneseniDatum = new DateTime(godina, mjesec, dan);
            return uneseniDatum;


        }
        public ActionResult SpremiNoviTrosakTamicenja(TroskoviTakmicenjaDodajVM model)
        {
            TroskoviTakmicenja trosak = new TroskoviTakmicenja();
            trosak.isDeleted = false;
            trosak.TakmicenjeId = model.TakmicenjeId;
            if (model.DatumUplate != null)
                trosak.DatumUplate = KonvertujUDatum_dd_mm_yyyy(model.DatumUplate);
            trosak.IznosKMBrojevima = Convert.ToDecimal(model.IznosKMBrojevima);
            trosak.IznosKMSlovima = model.IznosKMSlovima;
            trosak.Obrazlozenje = model.Obrazlozenje;
            trosak.Naziv = model.Naziv;
            ctx.TroskoviTakmicenja.Add(trosak);
            ctx.SaveChanges();

            return RedirectToAction("Index", new { takmicenjeId = model.TakmicenjeId});

        }

        public ActionResult Uredi(int trosakId)
        {
            TroskoviTakmicenja trosak = ctx.TroskoviTakmicenja.Where(x => x.Id == trosakId).FirstOrDefault();
            TroskoviTakmicenjaUrediVM model = new TroskoviTakmicenjaUrediVM
            {
                Id = trosakId,
                isDeleted = trosak.isDeleted,
                TakmicenjeId = trosak.TakmicenjeId,
                DatumUplate = trosak.DatumUplate.ToString("dd.MM.yyyy"),
                IznosKMBrojevima = trosak.IznosKMBrojevima.ToString(),
                IznosKMSlovima = trosak.IznosKMSlovima,
                Obrazlozenje = trosak.Obrazlozenje,
                Naziv = trosak.Naziv
            };
            return View("Uredi", model);
        }
        public ActionResult SpremiIzmjenuTroskaTamicenja(TroskoviTakmicenjaUrediVM model)
        {
            TroskoviTakmicenja trosak = ctx.TroskoviTakmicenja.Where(x => x.Id == model.Id).FirstOrDefault();

            trosak.TakmicenjeId = model.TakmicenjeId;
            if (model.DatumUplate != null)
                trosak.DatumUplate = KonvertujUDatum_dd_mm_yyyy(model.DatumUplate);
            trosak.IznosKMBrojevima = Convert.ToDecimal(model.IznosKMBrojevima);
            trosak.IznosKMSlovima = model.IznosKMSlovima;
            trosak.Obrazlozenje = model.Obrazlozenje;
            trosak.Naziv = model.Naziv;
            ctx.SaveChanges();

            return RedirectToAction("Index", new { takmicenjeId = model.TakmicenjeId });


        }
        public JsonResult Obrisi(int trosakId)
        {

            TroskoviTakmicenja trosak = ctx.TroskoviTakmicenja.Where(x => x.Id == trosakId).FirstOrDefault();
            if (trosak != null)
                trosak.isDeleted = true;
            ctx.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}