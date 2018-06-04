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

    public class DonacijeController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulSekretar/Donacije
        public ActionResult Index(int donatorId)
        {
            Donacije donacija = new Donacije();
            donacija = ctx.Donacije.Where(x => x.DonatorId == donatorId && x.isDeleted == false).FirstOrDefault();
            DonacijeIndexVM model = new DonacijeIndexVM {
                donacija = ctx.Donacije.Where(x=>x.isDeleted==false && x.DonatorId==donatorId).Select(x => new DonacijaPodaci
                {
                    OsobaId = x.OsobaId,
                    Korisnik = x.Osoba.Ime + " " + x.Osoba.Prezime,
                    DonatorId = x.DonatorId,
                    Donator = x.Donator.ImeOsobe + " " + x.Donator.PrezimeOsobe,
                    BrojUplatnice = x.BrojUplatnice,
                    IznosKMBrojevima = x.IznosKMBrojevima,
                    IznosKMSlovima = x.IznosKMSlovima,
                    Obrazlozenje = x.Obrazlozenje,
                    DatumUplate = x.DatumUplate,
                    Naziv = x.Donator.Naziv
                }).FirstOrDefault()
        };
            return View(model);
        }

        public ActionResult PregledSvihDonacija(string DatumOd = "", string DatumDo = "")
        {
            decimal ukupanIznosUlazaOstvarenogOdDonacija = 0;

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
                List<Donacije> listaDonacija = new List<Donacije>();
                listaDonacija = ctx.Donacije.Where(x => x.isDeleted == false && x.DatumUplate>=datumOd && x.DatumUplate<=datumDo).ToList();
                DonacijeIndexVM model = new DonacijeIndexVM(listaDonacija);
                for (int i = 0; i < model.listaDonacija.Count(); i++)
                {
                    ukupanIznosUlazaOstvarenogOdDonacija += model.listaDonacija[i].IznosKMBrojevima;
                }
                ViewData["ukupanIznosUlazaOstvarenogOdDonacija"] = ukupanIznosUlazaOstvarenogOdDonacija;

                return View("PregledSvihDonacija", model);
            }
            else
            {
                ViewData["ukupanIznosUlazaOstvarenogOdDonacija"] = ukupanIznosUlazaOstvarenogOdDonacija;

                List<Donacije> listaDonacija = new List<Donacije>();
                listaDonacija = ctx.Donacije.Where(x => x.isDeleted == false).ToList();
                DonacijeIndexVM model = new DonacijeIndexVM(listaDonacija);
                return View("PregledSvihDonacija", model);

            }
        }
    }
}