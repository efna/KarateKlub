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


    public class UlazController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulSekretar/Ulaz
        public ActionResult Index(string DatumOd="",string DatumDo="")
        {
            decimal ukupanIznosUlazaOstvarenogOdOstalihDobiti = 0;

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
                UlazIndexVM model = new UlazIndexVM
                {

                    ulazi = ctx.Ulaz.Where(x => x.isDeleted == false && x.Datum>=datumOd && x.Datum<=datumDo).Select(x => new UlazPodaci
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
                for (int i = 0; i < model.ulazi.Count(); i++)
                {
                    ukupanIznosUlazaOstvarenogOdOstalihDobiti += model.ulazi[i].IznosKMSBrojevima;
                }
                ViewData["ukupanIznosUlazaOstvarenogOdOstalihDobiti"] = ukupanIznosUlazaOstvarenogOdOstalihDobiti;
                return View(model);
            }
            else
            {
                UlazIndexVM model = new UlazIndexVM
                {

                    ulazi = ctx.Ulaz.Where(x => x.isDeleted == false).Select(x => new UlazPodaci
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
                ViewData["ukupanIznosUlazaOstvarenogOdOstalihDobiti"] = ukupanIznosUlazaOstvarenogOdOstalihDobiti;

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
            UlazDodajVM model = new UlazDodajVM {

            };
            return View("Dodaj", model);
        }
        public ActionResult SpremiNoviUlaz(UlazDodajVM model)
        {
            int korisnikId = Autentifikacija.GetLogiraniKorisnik(HttpContext).Osoba.Id;

            Ulaz ulaz = new Ulaz();
            ulaz.isDeleted = false;
            ulaz.OsobaId = korisnikId;
            ulaz.Naziv = model.Naziv;
            ulaz.Datum = KonvertujUDatum_dd_mm_yyyy(model.Datum);
            ulaz.IznosKMSlovima = model.IznosKMSlovima;
            ulaz.IznosKMSBrojevima=Convert.ToDecimal(model.IznosKMSBrojevima);
            ulaz.Obrazlozenje = model.Obrazlozenje;
            ctx.Ulaz.Add(ulaz);
            ctx.SaveChanges();
            return RedirectToAction("Index","UpravljanjeUlazima",new {brojTaba=6});
        }
       public ActionResult Uredi(int ulazId)
        {
            Ulaz ulaz = ctx.Ulaz.Where(x => x.Id == ulazId).FirstOrDefault();
            UlazUrediVM model = new UlazUrediVM {
                Id=ulazId,
                isDeleted=ulaz.isDeleted,
                Naziv=ulaz.Naziv,
                Datum=ulaz.Datum.ToString("dd.MM.yyyy"),
                IznosKMSBrojevima=ulaz.IznosKMSBrojevima.ToString(),
                IznosKMSlovima=ulaz.IznosKMSlovima,
                Obrazlozenje=ulaz.Obrazlozenje,
                OsobaId=ulaz.OsobaId
            };
            return View("Uredi", model);
        }
        public ActionResult SpremiIzmjenuUlaza(UlazUrediVM model)
        {
            Ulaz ulaz = ctx.Ulaz.Where(x => x.Id == model.Id).FirstOrDefault();
            ulaz.Naziv = model.Naziv;
            ulaz.Datum = KonvertujUDatum_dd_mm_yyyy(model.Datum);
            ulaz.IznosKMSlovima = model.IznosKMSlovima;
            ulaz.IznosKMSBrojevima = Convert.ToDecimal(model.IznosKMSBrojevima);
            ulaz.Obrazlozenje = model.Obrazlozenje;
            ctx.SaveChanges();
            return RedirectToAction("Index","UpravljanjeUlazima", new { brojTaba = 6 });

        }
        public JsonResult Obrisi(int ulazId)
        {
            Ulaz ulaz = ctx.Ulaz.Where(x => x.Id == ulazId).FirstOrDefault();
            if (ulaz != null)
                ulaz.isDeleted = true;
            ctx.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}   