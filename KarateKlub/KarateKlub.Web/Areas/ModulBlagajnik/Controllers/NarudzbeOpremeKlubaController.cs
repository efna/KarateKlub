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

    public class NarudzbeOpremeKlubaController : Controller
    {
        MojContext ctx = new MojContext();

        // GET: ModulBlagajnik/NarudzbeOpremeKluba
        public ActionResult Index()
        {
            NarudzbeOpremeKlubaIndexVM model = new NarudzbeOpremeKlubaIndexVM
            {
                narudzbeOpremeKluba = ctx.NarudzbeOpremeKluba.Where(x => x.isDeleted == false).Select(x => new NarudzbeOpremeKlubaPodaci
                {
                    Id = x.Id,
                    isDeleted = x.isDeleted,
                    OsobaId = x.Osoba.Id,
                    NazivNarudzbeOpreme = x.NazivNarudzbeOpreme,
                    DatumNabavke = x.DatumNabavke,
                    Obrazlozenje = x.Obrazlozenje,
                    Korisnik = x.Osoba.Ime + " " + x.Osoba.Prezime,
                    trosak = ctx.TroskoviNarudzbeOpremeKluba.Where(y => y.isDeleted == false && y.NarudzbaOpremeKlubaId == x.Id).Select(y => new TroskoviNarudzbePodaci
                    {
                        DatumUplate = y.DatumUplate,
                        IznosKMBrojevi = y.IznosKMBrojevima,
                        IznosKMSlova = y.IznosKMSlovima,
                        Obrazlozenje = y.Obrazlozenje
                    }).FirstOrDefault()
                }).ToList()
            };

            return View(model);
        }
        public ActionResult PregledSvihTroskova(string DatumOd, string DatumDo)
        {

            decimal ukupanIznosIzlazaZaTroskoveNarudzba = 0;

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
                NarudzbeOpremeKlubaIndexVM model = new NarudzbeOpremeKlubaIndexVM
                {
                    troskoviNarudzbe = ctx.TroskoviNarudzbeOpremeKluba.Where(x => x.isDeleted == false && x.DatumUplate >= datumOd && x.DatumUplate <= datumDo).Select(x => new TroskoviNarudzbePodaci
                    {
                        DatumUplate = x.DatumUplate,
                        IznosKMBrojevi = x.IznosKMBrojevima,
                        IznosKMSlova = x.IznosKMSlovima,
                        Obrazlozenje = x.Obrazlozenje,
                        narudzba = ctx.NarudzbeOpremeKluba.Where(y => y.isDeleted == false && y.Id == x.NarudzbaOpremeKlubaId).Select(y => new NarudzbeOpremeKlubaPodaci
                        {
                            Id = y.Id,
                            isDeleted = y.isDeleted,
                            OsobaId = y.Osoba.Id,
                            NazivNarudzbeOpreme = y.NazivNarudzbeOpreme,
                            DatumNabavke = y.DatumNabavke,
                            Obrazlozenje = y.Obrazlozenje,
                            Korisnik = y.Osoba.Ime + " " + y.Osoba.Prezime
                        }).FirstOrDefault()
                    }).ToList()
                };
                for (int i = 0; i < model.troskoviNarudzbe.Count(); i++)
                {
                    ukupanIznosIzlazaZaTroskoveNarudzba += model.troskoviNarudzbe[i].IznosKMBrojevi;
                }
                ViewData["ukupanIznosIzlazaZaTroskoveNarudzba"] = ukupanIznosIzlazaZaTroskoveNarudzba;

                return View("PregledSvihTroskova", model);
            }
            else
            {
                NarudzbeOpremeKlubaIndexVM model = new NarudzbeOpremeKlubaIndexVM
                {
                    troskoviNarudzbe = ctx.TroskoviNarudzbeOpremeKluba.Where(x => x.isDeleted == false).Select(x => new TroskoviNarudzbePodaci
                    {
                        DatumUplate = x.DatumUplate,
                        IznosKMBrojevi = x.IznosKMBrojevima,
                        IznosKMSlova = x.IznosKMSlovima,
                        Obrazlozenje = x.Obrazlozenje,
                        narudzba = ctx.NarudzbeOpremeKluba.Where(y => y.isDeleted == false && y.Id == x.NarudzbaOpremeKlubaId).Select(y => new NarudzbeOpremeKlubaPodaci
                        {
                            Id = y.Id,
                            isDeleted = y.isDeleted,
                            OsobaId = y.Osoba.Id,
                            NazivNarudzbeOpreme = y.NazivNarudzbeOpreme,
                            DatumNabavke = y.DatumNabavke,
                            Obrazlozenje = y.Obrazlozenje,
                            Korisnik = y.Osoba.Ime + " " + y.Osoba.Prezime
                        }).FirstOrDefault()
                    }).ToList()
                };
                ViewData["ukupanIznosIzlazaZaTroskoveNarudzba"] = ukupanIznosIzlazaZaTroskoveNarudzba;
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
        public ActionResult Dodaj()
        {
            NarudzbeOpremeKlubaDodajVM model = new NarudzbeOpremeKlubaDodajVM
            {
                DatumNabavke = "",
                DatumUplate = ""
            };
            return View("Dodaj", model);
        }
        public ActionResult Uredi(int narudzbaId)
        {
            NarudzbeOpremeKluba narudzba = ctx.NarudzbeOpremeKluba.Where(x => x.Id == narudzbaId).FirstOrDefault();
            TroskoviNarudzbeOpremeKluba trosak = ctx.TroskoviNarudzbeOpremeKluba.Where(x => x.NarudzbaOpremeKlubaId == narudzbaId).FirstOrDefault();
            NarudzbeOpremeKlubaUrediVM model = new NarudzbeOpremeKlubaUrediVM
            {
                Id = narudzbaId,
                NazivNarudzbeOpreme = narudzba.NazivNarudzbeOpreme,
                DatumNabavke = narudzba.DatumNabavke.ToString("dd.MM.yyyy"),
                ObrazlozenjeNarudzbe = narudzba.Obrazlozenje,
                ObrazlozenjeUplate = trosak.Obrazlozenje,
                DatumUplate = trosak.DatumUplate.ToString("dd.MM.yyyy"),
                IznosKMBrojevima = trosak.IznosKMBrojevima.ToString(),
                IznosKMSlovima = trosak.IznosKMSlovima
            };
            if (model.IznosKMBrojevima == "0,00")
                model.IznosKMBrojevima = "";
            return View("Uredi", model);
        }
        public ActionResult SpremiNovuNarudzbu(NarudzbeOpremeKlubaDodajVM model)
        {
            NarudzbeOpremeKluba narudzba = new NarudzbeOpremeKluba();
            int korisnikId = Autentifikacija.GetLogiraniKorisnik(HttpContext).Osoba.Id;
            narudzba.isDeleted = false;
            narudzba.NazivNarudzbeOpreme = model.NazivNarudzbeOpreme;
            narudzba.OsobaId = korisnikId;
            narudzba.DatumNabavke = KonvertujUDatum_dd_mm_yyyy(model.DatumNabavke);
            narudzba.Obrazlozenje = model.ObrazlozenjeNarudzbe;
            ctx.NarudzbeOpremeKluba.Add(narudzba);
            ctx.SaveChanges();
            int narudzbaId = ctx.NarudzbeOpremeKluba.ToList().OrderByDescending(x => x.Id).FirstOrDefault().Id;
            TroskoviNarudzbeOpremeKluba trosak = new TroskoviNarudzbeOpremeKluba();
            trosak.isDeleted = false;
            trosak.NarudzbaOpremeKlubaId = narudzbaId;
            if (model.DatumUplate != null)
                trosak.DatumUplate = KonvertujUDatum_dd_mm_yyyy(model.DatumUplate);
            trosak.IznosKMSlovima = model.IznosKMSlovima;
            trosak.IznosKMBrojevima = Convert.ToDecimal(model.IznosKMBrojevima);
            trosak.Obrazlozenje = model.ObrazlozenjeUplate;
            ctx.TroskoviNarudzbeOpremeKluba.Add(trosak);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SpremiIzmjenuNarudzbe(NarudzbeOpremeKlubaUrediVM model)
        {
            NarudzbeOpremeKluba narudzba = ctx.NarudzbeOpremeKluba.Where(x => x.Id == model.Id).FirstOrDefault();
            TroskoviNarudzbeOpremeKluba trosak = ctx.TroskoviNarudzbeOpremeKluba.Where(x => x.NarudzbaOpremeKlubaId == model.Id).FirstOrDefault();

            if (narudzba != null)
            {
                narudzba.NazivNarudzbeOpreme = model.NazivNarudzbeOpreme;
                narudzba.DatumNabavke = KonvertujUDatum_dd_mm_yyyy(model.DatumNabavke);
                narudzba.Obrazlozenje = model.ObrazlozenjeNarudzbe;
                if (model.DatumUplate != null)
                    trosak.DatumUplate = KonvertujUDatum_dd_mm_yyyy(model.DatumUplate);
                trosak.IznosKMBrojevima = Convert.ToDecimal(model.IznosKMBrojevima);
                trosak.IznosKMSlovima = model.IznosKMSlovima;
                trosak.Obrazlozenje = model.ObrazlozenjeUplate;

                ctx.SaveChanges();
            }
            return RedirectToAction("Index");


        }
        public JsonResult Obrisi(int narudzbaId)
        {

            NarudzbeOpremeKluba narudzba = ctx.NarudzbeOpremeKluba.Where(x => x.Id == narudzbaId).FirstOrDefault();
            narudzba.isDeleted = true;
            List<StavkeNarudzbeOpremeKluba> stavkeNaruzdbe = ctx.StavkeNarudzbeOpremeKluba.Where(x => x.NarudzbaOpremeKlubaId == narudzba.Id).ToList();
            if (stavkeNaruzdbe.Count() != 0)
            {
                for (int i = 0; i < stavkeNaruzdbe.Count(); i++)
                {
                    stavkeNaruzdbe[i].isDeleted = true;
                }
            }
            TroskoviNarudzbeOpremeKluba trosak = ctx.TroskoviNarudzbeOpremeKluba.Where(x => x.NarudzbaOpremeKlubaId == narudzbaId).FirstOrDefault();
            if (trosak != null)
                trosak.isDeleted = true;
            ctx.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

    }
}
