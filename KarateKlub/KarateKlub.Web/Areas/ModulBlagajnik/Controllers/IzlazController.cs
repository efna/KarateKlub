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

    public class IzlazController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulBlagajnik/Izlaz
        public ActionResult Index(string DatumOd = "", string DatumDo = "")
        {
            decimal ukupanIznosIzlazaZaOstaleTroskove = 0;

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
                IzlazIndexVM model = new IzlazIndexVM
                {
                    izlazi = ctx.Izlaz.Where(x => x.isDeleted == false && x.Datum >= datumOd && x.Datum <= datumDo).Select(x => new IzlazPodaci
                    {
                        Id = x.Id,
                        isDeleted = x.isDeleted,
                        OsobaId = x.OsobaId,
                        Osoba = x.Osoba.Ime + " " + x.Osoba.Prezime,
                        Naziv = x.Naziv,
                        IznosKMSBrojevima = x.IznosKMSBrojevima,
                        IznosKMSlovima = x.IznosKMSlovima,
                        Datum = x.Datum,
                        Obrazlozenje = x.Obrazlozenje
                    }).ToList()
                };
                for (int i = 0; i < model.izlazi.Count(); i++)
                {
                    ukupanIznosIzlazaZaOstaleTroskove += model.izlazi[i].IznosKMSBrojevima;
                }
                ViewData["ukupanIznosIzlazaZaOstaleTroskove"] = ukupanIznosIzlazaZaOstaleTroskove;

                return View(model);
            }
            else
            {
                IzlazIndexVM model = new IzlazIndexVM
                {
                    izlazi = ctx.Izlaz.Where(x => x.isDeleted == false).Select(x => new IzlazPodaci
                    {
                        Id = x.Id,
                        isDeleted = x.isDeleted,
                        OsobaId = x.OsobaId,
                        Osoba = x.Osoba.Ime + " " + x.Osoba.Prezime,
                        Naziv = x.Naziv,
                        IznosKMSBrojevima = x.IznosKMSBrojevima,
                        IznosKMSlovima = x.IznosKMSlovima,
                        Datum = x.Datum,
                        Obrazlozenje = x.Obrazlozenje
                    }).ToList()
                };
                ViewData["ukupanIznosIzlazaZaOstaleTroskove"] = ukupanIznosIzlazaZaOstaleTroskove;
                return View(model);

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
        public ActionResult Dodaj()
        {
            IzlazDodajVM model = new IzlazDodajVM
            {

            };
            return View("Dodaj", model);
        }
        public ActionResult SpremiNoviIzlaz(IzlazDodajVM model)
        {
            int korisnikId = Autentifikacija.GetLogiraniKorisnik(HttpContext).Osoba.Id;

            Izlaz izlaz = new Izlaz();
            izlaz.isDeleted = false;
            izlaz.OsobaId = korisnikId;
            izlaz.Naziv = model.Naziv;
            izlaz.Datum = KonvertujUDatum_dd_mm_yyyy(model.Datum);
            izlaz.IznosKMSlovima = model.IznosKMSlovima;
            izlaz.IznosKMSBrojevima = Convert.ToDecimal(model.IznosKMSBrojevima);
            izlaz.Obrazlozenje = model.Obrazlozenje;
            ctx.Izlaz.Add(izlaz);
            ctx.SaveChanges();
            return RedirectToAction("Index", "UpravljanjeIzlazima", new { brojTaba = 8 });
        }
        public ActionResult Uredi(int izlazId)
        {
            Izlaz izlaz = ctx.Izlaz.Where(x => x.Id == izlazId).FirstOrDefault();
            IzlazUrediVM model = new IzlazUrediVM
            {
                Id = izlazId,
                isDeleted = izlaz.isDeleted,
                Naziv = izlaz.Naziv,
                Datum = izlaz.Datum.ToString("dd.MM.yyyy"),
                IznosKMSBrojevima = izlaz.IznosKMSBrojevima.ToString(),
                IznosKMSlovima = izlaz.IznosKMSlovima,
                Obrazlozenje = izlaz.Obrazlozenje,
                OsobaId = izlaz.OsobaId
            };
            return View("Uredi", model);
        }
        public ActionResult SpremiIzmjenuIzlaza(IzlazUrediVM model)
        {
            Izlaz izlaz = ctx.Izlaz.Where(x => x.Id == model.Id).FirstOrDefault();
            izlaz.Naziv = model.Naziv;
            izlaz.Datum = KonvertujUDatum_dd_mm_yyyy(model.Datum);
            izlaz.IznosKMSlovima = model.IznosKMSlovima;
            izlaz.IznosKMSBrojevima = Convert.ToDecimal(model.IznosKMSBrojevima);
            izlaz.Obrazlozenje = model.Obrazlozenje;
            ctx.SaveChanges();
            return RedirectToAction("Index", "UpravljanjeIzlazima", new { brojTaba = 8 });

        }
        public JsonResult Obrisi(int izlazId)
        {
            Izlaz izlaz = ctx.Izlaz.Where(x => x.Id == izlazId).FirstOrDefault();
            if (izlaz != null)
                izlaz.isDeleted = true;
            ctx.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}