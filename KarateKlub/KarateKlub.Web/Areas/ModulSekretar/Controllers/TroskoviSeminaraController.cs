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

    public class TroskoviSeminaraController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulSekretar/TroskoviSeminara
        public ActionResult Index(int seminarId)
        {
            TroskoviSeminaraIndexVM model = new TroskoviSeminaraIndexVM
            {
                troskoviSeminara = ctx.TroskoviSeminara.Where(x => x.isDeleted == false && x.SeminarId == seminarId).Select(x => new TrosakSeminaraPodaci
                {
                    Id = x.Id,
                    isDeleted = x.isDeleted,
                    SeminarId = x.SeminarId,
                    IznosKMBrojevima = x.IznosKMBrojevima,
                    IznosKMSlovima = x.IznosKMSlovima,
                    DatumUplate = x.DatumUplate,
                    Obrazlozenje = x.Obrazlozenje,
                    Naziv=x.Naziv,
                    NazivSeminara=x.Seminar.NazivSeminara,
                    MjestoOdrzavanja=x.Seminar.MjestoOdrzavanjaSeminara,
                    DatumOd=x.Seminar.DatumOdrzavanjaSeminaraOd,
                    DatumDo=x.Seminar.DatumOdrzavanjaSeminaraDo
                }).ToList()
            };
            ViewData["seminarId"] = seminarId;
            decimal ukupanTrosakSeminara = 0;
            List<TroskoviSeminara> troskoviSeminara = ctx.TroskoviSeminara.Where(x => x.isDeleted == false && x.SeminarId == seminarId).ToList();
            for (int i = 0; i < troskoviSeminara.Count(); i++)
            {
                ukupanTrosakSeminara += troskoviSeminara[i].IznosKMBrojevima;
            }
            ViewData["ukupanTrosakSeminara"] = ukupanTrosakSeminara;

            return View(model);
        }

        public ActionResult PregledSvihTroskova(string DatumOd,string DatumDo)
        {
            decimal ukupanIznosIzlazaZaTroskoveSeminara = 0;

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
                TroskoviSeminaraIndexVM model = new TroskoviSeminaraIndexVM
                {
                    troskoviSeminara = ctx.TroskoviSeminara.Where(x => x.isDeleted == false && x.DatumUplate>=datumOd && x.DatumUplate<=datumDo).Select(x => new TrosakSeminaraPodaci
                    {
                        Id = x.Id,
                        isDeleted = x.isDeleted,
                        SeminarId = x.SeminarId,
                        IznosKMBrojevima = x.IznosKMBrojevima,
                        IznosKMSlovima = x.IznosKMSlovima,
                        DatumUplate = x.DatumUplate,
                        Obrazlozenje = x.Obrazlozenje,
                        Naziv = x.Naziv,
                        NazivSeminara = x.Seminar.NazivSeminara,
                        MjestoOdrzavanja = x.Seminar.MjestoOdrzavanjaSeminara,
                        DatumOd = x.Seminar.DatumOdrzavanjaSeminaraOd,
                        DatumDo = x.Seminar.DatumOdrzavanjaSeminaraDo
                    }).ToList()
                };
                for (int i = 0; i < model.troskoviSeminara.Count(); i++)
                {
                    ukupanIznosIzlazaZaTroskoveSeminara += model.troskoviSeminara[i].IznosKMBrojevima;
                }
                ViewData["ukupanIznosIzlazaZaTroskoveSeminara"] = ukupanIznosIzlazaZaTroskoveSeminara;
                return View("PregledSvihTroskova", model);
            }
            else
            {
                ViewData["ukupanIznosIzlazaZaTroskoveSeminara"] = ukupanIznosIzlazaZaTroskoveSeminara;

                TroskoviSeminaraIndexVM model = new TroskoviSeminaraIndexVM
                {
                    troskoviSeminara = ctx.TroskoviSeminara.Where(x => x.isDeleted == false).Select(x => new TrosakSeminaraPodaci
                    {
                        Id = x.Id,
                        isDeleted = x.isDeleted,
                        SeminarId = x.SeminarId,
                        IznosKMBrojevima = x.IznosKMBrojevima,
                        IznosKMSlovima = x.IznosKMSlovima,
                        DatumUplate = x.DatumUplate,
                        Obrazlozenje = x.Obrazlozenje,
                        Naziv = x.Naziv,
                        NazivSeminara = x.Seminar.NazivSeminara,
                        MjestoOdrzavanja = x.Seminar.MjestoOdrzavanjaSeminara,
                        DatumOd = x.Seminar.DatumOdrzavanjaSeminaraOd,
                        DatumDo = x.Seminar.DatumOdrzavanjaSeminaraDo
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
        public ActionResult Dodaj(int seminarId)
        {
            TroskoviSeminaraDodajVM model = new TroskoviSeminaraDodajVM
            {
                SeminarId = seminarId
            };

            return View("Dodaj", model);
        }

        public ActionResult SpremiNoviTrosakSeminara(TroskoviSeminaraDodajVM model)
        {
            TroskoviSeminara trosak = new TroskoviSeminara();
            trosak.isDeleted = false;
            trosak.Naziv = model.Naziv;
            trosak.IznosKMBrojevima = Convert.ToDecimal(model.IznosKMBrojevima);
            trosak.IznosKMSlovima = model.IznosKMSlovima;
            if (model.DatumUplate != null)
                trosak.DatumUplate = KonvertujUDatum_dd_mm_yyyy(model.DatumUplate);
            trosak.Obrazlozenje = model.Obrazlozenje;
            trosak.SeminarId = model.SeminarId;
            ctx.TroskoviSeminara.Add(trosak);
            ctx.SaveChanges();
            return RedirectToAction("Index", "UpravljanjeSeminarima", new { seminarId = model.SeminarId, brojTaba = 4 });

        }

        public ActionResult Uredi(int trosakSeminaraId)
        {

            TroskoviSeminara trosak = ctx.TroskoviSeminara.Where(x => x.Id == trosakSeminaraId).FirstOrDefault();
            TroskoviSeminaraUrediVM model = new TroskoviSeminaraUrediVM
            {
                Id = trosakSeminaraId,
                isDeleted = trosak.isDeleted,
                Naziv=trosak.Naziv,
                IznosKMBrojevima = trosak.IznosKMBrojevima.ToString(),
                IznosKMSlovima = trosak.IznosKMSlovima,
                DatumUplate = trosak.DatumUplate.ToString("dd.MM.yyyy"),
                Obrazlozenje = trosak.Obrazlozenje,
                SeminarId = trosak.SeminarId

            };
            return View("Uredi", model);
        }
        public ActionResult SpremiIzmjenuTroskaSeminara(TroskoviSeminaraUrediVM model)
        {
            TroskoviSeminara trosak = ctx.TroskoviSeminara.Where(x => x.Id == model.Id).FirstOrDefault();
            trosak.Naziv = model.Naziv;
            trosak.IznosKMBrojevima = Convert.ToDecimal(model.IznosKMBrojevima);
            trosak.IznosKMSlovima = model.IznosKMSlovima;
            if (model.DatumUplate != null)
                trosak.DatumUplate = KonvertujUDatum_dd_mm_yyyy(model.DatumUplate);
            trosak.Obrazlozenje = model.Obrazlozenje;
            ctx.SaveChanges();
            return RedirectToAction("Index", "UpravljanjeSeminarima", new { seminarId = model.SeminarId, brojTaba = 4});


        }
        public JsonResult Obrisi(int trosakSeminaraId)
        {

            TroskoviSeminara trosak = ctx.TroskoviSeminara.Where(x => x.Id == trosakSeminaraId).FirstOrDefault();

            if (trosak != null)
                trosak.isDeleted = true;
            ctx.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}