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

    public class TroskoviPolaganjaUcenickaZvanjaController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulBlagajnik/TroskoviPolaganjaUcenickaZvanja
        public ActionResult Index(int polaganjeId)
        {
            TroskoviPolaganjaUcenickaZvanjaIndexVM model = new TroskoviPolaganjaUcenickaZvanjaIndexVM
            {
                troskoviPolaganja = ctx.TroskoviPolaganjaUcenickaZvanja.Where(x => x.isDeleted == false && x.PolaganjeUcenickaZvanjaId == polaganjeId).Select(x => new TrosakPolaganjaPodaci
                {
                    Id = x.Id,
                    isDeleted = x.isDeleted,
                    PolaganjeUcenickaZvanjaId = x.PolaganjeUcenickaZvanjaId,
                    Naziv = x.Naziv,
                    IznosKMBrojevima = x.IznosKMBrojevima,
                    IznosKMSlovima = x.IznosKMSlovima,
                    DatumUplate = x.DatumUplate,
                    Obrazlozenje = x.Obrazlozenje
                }).ToList()
            };
            ViewData["polaganjeId"] = polaganjeId;
            decimal ukupanTrosakPolaganja = 0;
            List<TroskoviPolaganjaUcenickaZvanja> troskoviPolaganja = ctx.TroskoviPolaganjaUcenickaZvanja.Where(x => x.isDeleted == false && x.PolaganjeUcenickaZvanjaId == polaganjeId).ToList();
            for (int i = 0; i < troskoviPolaganja.Count(); i++)
            {
                ukupanTrosakPolaganja += troskoviPolaganja[i].IznosKMBrojevima;
            }
            ViewData["ukupanTrosakPolaganja"] = ukupanTrosakPolaganja;

            return View(model);
        }
        public ActionResult PregledSvihTroskova(string DatumOd, string DatumDo)
        {
            decimal ukupanIznosIzlazaZaTroskovePolaganja = 0;

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
                TroskoviPolaganjaUcenickaZvanjaIndexVM model = new TroskoviPolaganjaUcenickaZvanjaIndexVM
                {
                    troskoviPolaganja = ctx.TroskoviPolaganjaUcenickaZvanja.Where(x => x.isDeleted == false && x.DatumUplate >= datumOd && x.DatumUplate <= datumDo).Select(x => new TrosakPolaganjaPodaci
                    {
                        Id = x.Id,
                        isDeleted = x.isDeleted,
                        PolaganjeUcenickaZvanjaId = x.PolaganjeUcenickaZvanjaId,
                        Naziv = x.Naziv,
                        IznosKMBrojevima = x.IznosKMBrojevima,
                        IznosKMSlovima = x.IznosKMSlovima,
                        DatumUplate = x.DatumUplate,
                        Obrazlozenje = x.Obrazlozenje,
                        DatumOdrzavanja = x.PolaganjeUcenickaZvanja.DatumPolaganja,
                        MjestoOdrzavanja = x.PolaganjeUcenickaZvanja.MjestoPolaganja
                    }).ToList()
                };
                for (int i = 0; i < model.troskoviPolaganja.Count(); i++)
                {
                    ukupanIznosIzlazaZaTroskovePolaganja += model.troskoviPolaganja[i].IznosKMBrojevima;
                }
                ViewData["ukupanIznosIzlazaZaTroskovePolaganja"] = ukupanIznosIzlazaZaTroskovePolaganja;
                return View("PregledSvihTroskova", model);
            }
            else
            {
                ViewData["ukupanIznosIzlazaZaTroskovePolaganja"] = ukupanIznosIzlazaZaTroskovePolaganja;

                TroskoviPolaganjaUcenickaZvanjaIndexVM model = new TroskoviPolaganjaUcenickaZvanjaIndexVM
                {
                    troskoviPolaganja = ctx.TroskoviPolaganjaUcenickaZvanja.Where(x => x.isDeleted == false).Select(x => new TrosakPolaganjaPodaci
                    {
                        Id = x.Id,
                        isDeleted = x.isDeleted,
                        PolaganjeUcenickaZvanjaId = x.PolaganjeUcenickaZvanjaId,
                        Naziv = x.Naziv,
                        IznosKMBrojevima = x.IznosKMBrojevima,
                        IznosKMSlovima = x.IznosKMSlovima,
                        DatumUplate = x.DatumUplate,
                        Obrazlozenje = x.Obrazlozenje,
                        DatumOdrzavanja = x.PolaganjeUcenickaZvanja.DatumPolaganja,
                        MjestoOdrzavanja = x.PolaganjeUcenickaZvanja.MjestoPolaganja
                    }).ToList()
                };
                return View("PregledSvihTroskova", model);
            }
        }

        public DateTime KonvertujUDatum_dd_mm_yyyy(string datum)
        {

            int dan = Convert.ToInt32(datum.Substring(0, 2));
            int mjesec = Convert.ToInt32(datum.Substring(3, 2));
            int godina = Convert.ToInt32(datum.Substring(6, 4));
            DateTime uneseniDatum = new DateTime(godina, mjesec, dan);
            return uneseniDatum;


        }
        public ActionResult Dodaj(int polaganjeId)
        {
            TroskoviPolaganjaUcenickaZvanjaDodajVM model = new TroskoviPolaganjaUcenickaZvanjaDodajVM
            {
                PolaganjeUcenickaZvanjaId = polaganjeId
            };

            return View("Dodaj", model);
        }

        public ActionResult SpremiNoviTrosakPolaganjaUcenickaZvanja(TroskoviPolaganjaUcenickaZvanjaDodajVM model)
        {
            TroskoviPolaganjaUcenickaZvanja trosak = new TroskoviPolaganjaUcenickaZvanja();
            trosak.isDeleted = false;
            trosak.IznosKMBrojevima = Convert.ToDecimal(model.IznosKMBrojevima);
            trosak.IznosKMSlovima = model.IznosKMSlovima;
            if (model.DatumUplate != null)
                trosak.DatumUplate = KonvertujUDatum_dd_mm_yyyy(model.DatumUplate);
            trosak.Obrazlozenje = model.Obrazlozenje;
            trosak.Naziv = model.Naziv;
            trosak.PolaganjeUcenickaZvanjaId = model.PolaganjeUcenickaZvanjaId;
            ctx.TroskoviPolaganjaUcenickaZvanja.Add(trosak);
            ctx.SaveChanges();
            return RedirectToAction("Index", "UpravljanjePolaganjaUcenickaZvanja", new { polaganjeId = model.PolaganjeUcenickaZvanjaId, brojTaba = 3, zvanje = 0 });

        }

        public ActionResult Uredi(int trosakPolaganjaId)
        {

            TroskoviPolaganjaUcenickaZvanja trosak = ctx.TroskoviPolaganjaUcenickaZvanja.Where(x => x.Id == trosakPolaganjaId).FirstOrDefault();
            TroskoviPolaganjaUcenickaZvanjaUrediVM model = new TroskoviPolaganjaUcenickaZvanjaUrediVM
            {
                Id = trosakPolaganjaId,
                isDeleted = trosak.isDeleted,
                Naziv = trosak.Naziv,
                IznosKMBrojevima = trosak.IznosKMBrojevima.ToString(),
                IznosKMSlovima = trosak.IznosKMSlovima,
                DatumUplate = trosak.DatumUplate.ToString("dd.MM.yyyy"),
                Obrazlozenje = trosak.Obrazlozenje,
                PolaganjeUcenickaZvanjaId = trosak.PolaganjeUcenickaZvanjaId

            };
            return View("Uredi", model);
        }
        public ActionResult SpremiIzmjenuTroskaPolaganjaUcenickaZvanja(TroskoviPolaganjaUcenickaZvanjaUrediVM model)
        {
            TroskoviPolaganjaUcenickaZvanja trosak = ctx.TroskoviPolaganjaUcenickaZvanja.Where(x => x.Id == model.Id).FirstOrDefault();

            trosak.IznosKMBrojevima = Convert.ToDecimal(model.IznosKMBrojevima);
            trosak.IznosKMSlovima = model.IznosKMSlovima;
            if (model.DatumUplate != null)
                trosak.DatumUplate = KonvertujUDatum_dd_mm_yyyy(model.DatumUplate);
            trosak.Obrazlozenje = model.Obrazlozenje;
            trosak.Naziv = model.Naziv;
            ctx.SaveChanges();
            return RedirectToAction("Index", "UpravljanjePolaganjaUcenickaZvanja", new { polaganjeId = model.PolaganjeUcenickaZvanjaId, brojTaba = 3, zvanje = 0 });


        }
        public JsonResult Obrisi(int trosakPolaganjaId)
        {

            TroskoviPolaganjaUcenickaZvanja trosak = ctx.TroskoviPolaganjaUcenickaZvanja.Where(x => x.Id == trosakPolaganjaId).FirstOrDefault();

            if (trosak != null)
                trosak.isDeleted = true;
            ctx.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}