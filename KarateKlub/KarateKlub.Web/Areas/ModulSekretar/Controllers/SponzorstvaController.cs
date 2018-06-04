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

    public class SponzorstvaController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulSekretar/Sponzorstva
        public ActionResult Index(int sponzorId, int aktivnost)
        {
            SponzorstvaIndexVM model = new SponzorstvaIndexVM(sponzorId);
            ViewData["aktivnost"] = aktivnost;
            return View(model);
        }
        public ActionResult PregledSvihSponzorstva(string DatumOd="",string DatumDo="") {
            decimal ukupanIznosUlazaOstvarenogOdSponzorstva = 0;

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
                List<Sponzorstva> listaSponzorstva = ctx.Sponzorstva.Where(x => x.isDeleted == false && x.DatumUplate >= datumOd && x.DatumUplate <= datumDo).ToList();
                SponzorstvaIndexVM model = new SponzorstvaIndexVM(listaSponzorstva);
                for (int i = 0; i < model.sponzorstva.Count(); i++)
                {
                    ukupanIznosUlazaOstvarenogOdSponzorstva += model.sponzorstva[i].IznosKMBrojevima;
                }
                ViewData["ukupanIznosUlazaOstvarenogOdSponzorstva"] = ukupanIznosUlazaOstvarenogOdSponzorstva;
                return View("PregledSvihSponzorstva",model);

            }
            else
            {
                ViewData["ukupanIznosUlazaOstvarenogOdSponzorstva"] = ukupanIznosUlazaOstvarenogOdSponzorstva;

                List<Sponzorstva> listaSponzorstva = ctx.Sponzorstva.Where(x => x.isDeleted == false).ToList();
                SponzorstvaIndexVM model = new SponzorstvaIndexVM(listaSponzorstva);
                return View("PregledSvihSponzorstva", model);

            }
        }

        public ActionResult Dodaj(int sponzorId,int aktivnost)
        {
            SponzorstvaDodajVM model = new SponzorstvaDodajVM
            {
                aktivnost = aktivnost,
                SponzorId=sponzorId

            };

            return View("Dodaj", model);


        }
        public ActionResult Uredi(int sponzorstvoId, int aktivnost)
        {
            Sponzorstva sponzorstvo = ctx.Sponzorstva.Where(x => x.Id == sponzorstvoId).FirstOrDefault();
            SponzorstvaUrediVM model = new SponzorstvaUrediVM {
                Id=sponzorstvoId,
                isDeleted=sponzorstvo.isDeleted,
                DatumUplate=sponzorstvo.DatumUplate.ToString("dd.MM.yyyy"),
                SponzorId=sponzorstvo.SponzorId,
                KorisnikId=sponzorstvo.OsobaId,
                IznosKMBrojevima=sponzorstvo.IznosKMBrojevima.ToString(),
                IznosKMSlovima=sponzorstvo.IznosKMSlovima,
                Obrazlozenje=sponzorstvo.Obrazlozenje,
                aktivnost=aktivnost

            };
            return View("Uredi", model);
        }

        public DateTime KonvertujUDatum_dd_mm_yyyy(string datum)
        {

            int dan = Convert.ToInt32(datum.Substring(0, 2));
            int mjesec = Convert.ToInt32(datum.Substring(3, 2));
            int godina = Convert.ToInt32(datum.Substring(6, 4));
            DateTime uneseniDatum = new DateTime(godina, mjesec, dan);
            return uneseniDatum;


        }

        public ActionResult SpremiNovoSponzorstvo(SponzorstvaDodajVM model)
        {
            int KorisnikId = Autentifikacija.GetLogiraniKorisnik(HttpContext).Osoba.Id;
            Sponzorstva sponzorstvo = new Sponzorstva();
            sponzorstvo.isDeleted = false;
            sponzorstvo.SponzorId = model.SponzorId;
            if(model.DatumUplate!=null)
            sponzorstvo.DatumUplate = KonvertujUDatum_dd_mm_yyyy(model.DatumUplate);
            sponzorstvo.IznosKMBrojevima = Convert.ToDecimal(model.IznosKMBrojevima);
            sponzorstvo.IznosKMSlovima = model.IznosKMSlovima;
            sponzorstvo.Obrazlozenje = model.Obrazlozenje;
            sponzorstvo.OsobaId = KorisnikId;
            ctx.Sponzorstva.Add(sponzorstvo);
            ctx.SaveChanges();
            return RedirectToAction("Index", new { sponzorId = model.SponzorId, aktivnost = model.aktivnost });
        }

        public ActionResult SpremiIzmjenuSponzorstva(SponzorstvaUrediVM model)
        {
            Sponzorstva sponzorstvo = ctx.Sponzorstva.Where(x => x.Id == model.Id).FirstOrDefault();
            if (model.DatumUplate != null)
                sponzorstvo.DatumUplate = KonvertujUDatum_dd_mm_yyyy(model.DatumUplate);
            sponzorstvo.IznosKMBrojevima = Convert.ToDecimal(model.IznosKMBrojevima);
            sponzorstvo.IznosKMSlovima = model.IznosKMSlovima;
            sponzorstvo.Obrazlozenje = model.Obrazlozenje;
            ctx.SaveChanges();
            return RedirectToAction("Index", new { sponzorId = model.SponzorId, aktivnost = model.aktivnost });
        }


        public JsonResult Obrisi(int sponzorstvoId)
        {

          Sponzorstva  sponzosrtvo = ctx.Sponzorstva.Where(x => x.Id==sponzorstvoId).FirstOrDefault();
      
            if (sponzosrtvo != null)
                sponzosrtvo.isDeleted = true;

            ctx.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}