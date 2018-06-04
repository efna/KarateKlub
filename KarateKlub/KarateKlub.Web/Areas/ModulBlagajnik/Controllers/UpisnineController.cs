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

    public class UpisnineController : Controller
    {
        MojContext ctx = new MojContext();

        // GET: ModulBlagajnik/Upisnine
        public ActionResult Index(int upisId, int izmirena = 0)
        {
            List<Upisnine> upisnine = new List<Upisnine>();
            if (izmirena == 0)
            {
                upisnine = ctx.Upisnine.Where(x => x.UpisId == upisId && x.isDeleted == false && x.isIzmirenaUpisnina == true).ToList();
            }
            else
            {
                upisnine = ctx.Upisnine.Where(x => x.UpisId == upisId && x.isDeleted == false && x.isIzmirenaUpisnina == false).ToList();

            }
            UpisnineIndexVM model = new UpisnineIndexVM(upisnine, izmirena, upisId);
            ViewData["upisId"] = upisId;
            ViewData["izmirena"] = izmirena;

            if (izmirena == 0)
                return View("Index", model);
            else
                return View("Index2", model);
        }

        public ActionResult IzmireneUpisnine(string DatumOd = "", string DatumDo = "")
        {
            decimal ukupanIznosUlazaOstvarenogOdUpisnina = 0;

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

                List<Upisnine> upisnine = new List<Upisnine>();
                upisnine = ctx.Upisnine.Where(x => x.isDeleted == false && x.isIzmirenaUpisnina == true && x.DatumUplate >= datumOd && x.DatumUplate <= datumDo).ToList();
                UpisnineIndexVM model = new UpisnineIndexVM(upisnine);

                for (int i = 0; i < model.upisnineClanova.Count(); i++)
                {
                    ukupanIznosUlazaOstvarenogOdUpisnina += model.upisnineClanova[i].IznosKMBrojevima;
                }
                ViewData["ukupanIznosUlazaOstvarenogOdUpisnina"] = ukupanIznosUlazaOstvarenogOdUpisnina;
                return View("IzmireneUpisnine", model);
            }
            else
            {

                List<Upisnine> upisnine = new List<Upisnine>();
                upisnine = ctx.Upisnine.Where(x => x.isDeleted == false && x.isIzmirenaUpisnina == true).ToList();
                UpisnineIndexVM model = new UpisnineIndexVM(upisnine);
                ViewData["ukupanIznosUlazaOstvarenogOdUpisnina"] = ukupanIznosUlazaOstvarenogOdUpisnina;

                return View("IzmireneUpisnine", model);

            }
        }
        public ActionResult GoToUpravljanjePodacimaUpisa(int upisId, int izmirena)
        {

            return RedirectToAction("Index", "UpravljanjePodacimaUpisa", new { upisId = upisId, brojTaba = 2, izmirena = izmirena });
        }

        public ActionResult NeizmireneUpisnine()
        {

            List<Upisnine> upisnine = new List<Upisnine>();
            upisnine = ctx.Upisnine.Where(x => x.isDeleted == false && x.isIzmirenaUpisnina == false).ToList();
            UpisnineIndexVM model = new UpisnineIndexVM(upisnine);


            return View("NeizmireneUpisnine", model);
        }
        public ActionResult Dodaj(int upisId, int izmirena)
        {
            UpisnineDodajVM model = new UpisnineDodajVM
            {
                izmirena = izmirena,
                UpisaniClanovi = BindUpisaneClanove(upisId),
                UpisId = upisId
            };
            List<Upisnine> upisnine = ctx.Upisnine.Where(x => x.isDeleted == false && x.UpisId == upisId).ToList();
            List<string> listaId = new List<string>();
            for (int i = 0; i < model.UpisaniClanovi.Count(); i++)
            {
                for (int j = 0; j < upisnine.Count(); j++)
                {

                    if (upisnine[j].ClanKlubaId.ToString() == model.UpisaniClanovi[i].Value && upisnine[j].isDeleted == false)
                    {
                        string value = upisnine[j].ClanKlubaId.ToString();
                        listaId.Add(value);

                    }
                }
            }
            for (int i = 0; i < listaId.Count(); i++)
            {
                var item = model.UpisaniClanovi.First(x => x.Value == listaId[i]);
                model.UpisaniClanovi.Remove(item);
            }
            model.UpisaniClanovi.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite člana-" });

            return View("Dodaj", model);
        }
        private List<SelectListItem> BindUpisaneClanove(int upisId)
        {
            return ctx.UpisaniClanovi.Where(x => x.isDeleted == false && x.UpisId == upisId).Select(x => new SelectListItem { Value = x.ClanKlubaId.ToString(), Text = x.ClanKluba.Osoba.Ime + " (" + x.ClanKluba.Osoba.ImeRoditelja + ") " + x.ClanKluba.Osoba.Prezime }).ToList();

        }
        public ActionResult SpremiNovuUpisninu(UpisnineDodajVM model)
        {
            for (int i = 0; i < model.UpisaniClanoviId.Count(); i++)
            {

                Upisnine upisnina = new Upisnine();
                upisnina.isDeleted = false;
                upisnina.isIzmirenaUpisnina = false;
                upisnina.ClanKlubaId = model.UpisaniClanoviId[i];
                upisnina.UpisId = model.UpisId;

                ctx.Upisnine.Add(upisnina);
                ctx.SaveChanges();

            }
            model.izmirena = 1;
            return RedirectToAction("Index", "UpravljanjePodacimaUpisa", new { upisId = model.UpisId, brojTaba = 2, izmirena = model.izmirena });

        }
        public string KonvertujUString_mm_dd_yyyy(string datum)
        {
            if (datum != "")
            {
                string dan = datum.Substring(0, 2);
                string mjesec = datum.Substring(3, 2);
                string godina = datum.Substring(6, 4);
                string uneseniDatum = dan + "." + mjesec + "." + godina;
                return uneseniDatum;
            }
            else
                return "";

        }
        public ActionResult Uredi(int upisninaId, int izmirena)
        {
            Upisnine upisnina = ctx.Upisnine.Where(x => x.Id == upisninaId).FirstOrDefault();
            UpisnineUrediVM model = new UpisnineUrediVM
            {
                Id = upisninaId,
                isDeleted = upisnina.isDeleted,
                IznosKMBrojevima = upisnina.IznosKMBrojevima.ToString(),
                IznosKMSlovima = upisnina.IznosKMSlovima,
                DatumUplate = KonvertujUString_mm_dd_yyyy(upisnina.DatumUplate.ToString()),
                MjestoUplate = upisnina.MjestoUplate,
                ClanKlubaId = upisnina.ClanKlubaId,
                UpisId = upisnina.UpisId,
                BrojPriznanice = upisnina.BrojPriznanice,
                isIzmirenaUpisnina = upisnina.isIzmirenaUpisnina,
                izmirena = izmirena


            };

            return View("Uredi", model);

        }
        public ActionResult Uredi2(int upisninaId)
        {
            Upisnine upisnina = ctx.Upisnine.Where(x => x.Id == upisninaId).FirstOrDefault();
            UpisnineUrediVM model = new UpisnineUrediVM
            {
                Id = upisninaId,
                isDeleted = upisnina.isDeleted,
                IznosKMBrojevima = upisnina.IznosKMBrojevima.ToString(),
                IznosKMSlovima = upisnina.IznosKMSlovima,
                DatumUplate = KonvertujUString_mm_dd_yyyy(upisnina.DatumUplate.ToString()),
                MjestoUplate = upisnina.MjestoUplate,
                ClanKlubaId = upisnina.ClanKlubaId,
                UpisId = upisnina.UpisId,
                BrojPriznanice = upisnina.BrojPriznanice,
                isIzmirenaUpisnina = upisnina.isIzmirenaUpisnina


            };

            return View("Uredi2", model);

        }
        public DateTime KonvertujUDatum_dd_mm_yyyy(string datum)
        {

            int dan = Convert.ToInt32(datum.Substring(0, 2));
            int mjesec = Convert.ToInt32(datum.Substring(3, 2));
            int godina = Convert.ToInt32(datum.Substring(6, 4));
            DateTime uneseniDatum = new DateTime(godina, mjesec, dan);
            return uneseniDatum;


        }
        public ActionResult SpremiIzmjenuUpisnine(UpisnineUrediVM model)
        {
            Upisnine upisnina = ctx.Upisnine.Where(x => x.Id == model.Id).FirstOrDefault();


            upisnina.isIzmirenaUpisnina = true;
            upisnina.BrojPriznanice = model.BrojPriznanice;
            upisnina.IznosKMBrojevima = Convert.ToDecimal(model.IznosKMBrojevima);
            upisnina.IznosKMSlovima = model.IznosKMSlovima;
            if (model.DatumUplate != null)
                upisnina.DatumUplate = KonvertujUDatum_dd_mm_yyyy(model.DatumUplate);
            upisnina.MjestoUplate = model.MjestoUplate;
            ctx.SaveChanges();

            return RedirectToAction("Index", "UpravljanjePodacimaUpisa", new { upisId = upisnina.UpisId, brojTaba = 2, izmirena = model.izmirena });

        }
        public ActionResult SpremiIzmjenuUpisnine2(UpisnineUrediVM model)
        {
            Upisnine upisnina = ctx.Upisnine.Where(x => x.Id == model.Id).FirstOrDefault();


            upisnina.isIzmirenaUpisnina = true;
            upisnina.BrojPriznanice = model.BrojPriznanice;
            upisnina.IznosKMBrojevima = Convert.ToDecimal(model.IznosKMBrojevima);
            upisnina.IznosKMSlovima = model.IznosKMSlovima;
            if (model.DatumUplate != null)
                upisnina.DatumUplate = KonvertujUDatum_dd_mm_yyyy(model.DatumUplate);
            upisnina.MjestoUplate = model.MjestoUplate;
            ctx.SaveChanges();

            return RedirectToAction("Index", "UpravljanjeUpisima", new { brojTaba = 3 });

        }


        public JsonResult Obrisi(int upisninaId)
        {
            Upisnine upisnina = ctx.Upisnine.Where(x => x.Id == upisninaId).FirstOrDefault();

            if (upisnina != null)
                upisnina.isDeleted = true;
            ctx.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}