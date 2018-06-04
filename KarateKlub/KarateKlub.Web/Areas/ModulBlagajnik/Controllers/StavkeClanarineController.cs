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

    public class StavkeClanarineController : Controller
    {
        MojContext ctx = new MojContext();

        // GET: ModulBlagajnik/StavkeClanarine
        public ActionResult Index(int clanarinaId, int izmirena = 0)
        {
            List<StavkeClanarine> stavke = new List<StavkeClanarine>();
            if (izmirena == 0)
            {
                stavke = ctx.StavkeClanarine.Where(x => x.ClanarinaId == clanarinaId && x.isDeleted == false && x.isIzmirenaClanarina == true).ToList();
            }
            else
            {
                stavke = ctx.StavkeClanarine.Where(x => x.ClanarinaId == clanarinaId && x.isDeleted == false && x.isIzmirenaClanarina == false).ToList();

            }
            StavkeClanarineIndexVM model = new StavkeClanarineIndexVM(stavke, izmirena, clanarinaId);
            ViewData["clanarinaId"] = clanarinaId;
            ViewData["izmirena"] = izmirena;

            if (izmirena == 0)
                return View("Index", model);
            else
                return View("Index2", model);
        }
        public ActionResult PregledClanarinaClana(int osobaId, int aktivan, int izmirena)
        {

            List<StavkeClanarine> stavke = new List<StavkeClanarine>();
            if (izmirena == 0)
            {
                stavke = ctx.StavkeClanarine.Where(x => x.ClanKluba.OsobaId == osobaId && x.isDeleted == false && x.isIzmirenaClanarina == true).ToList();
            }
            else
            {
                stavke = ctx.StavkeClanarine.Where(x => x.ClanKluba.OsobaId == osobaId && x.isDeleted == false && x.isIzmirenaClanarina == false).ToList();

            }
            StavkeClanarinePregledClanarinaClanaVM model = new StavkeClanarinePregledClanarinaClanaVM(stavke, izmirena, aktivan, osobaId);
            ViewData["osobaId"] = osobaId;
            ViewData["izmirena"] = izmirena;
            ViewData["aktivan"] = aktivan;

            if (izmirena == 0)
                return View("PregledIzmirenihClanarinaClanaKluba", model);
            else
                return View("PregledNeizmirenihClanarinaClanaKluba", model);
        }

        public ActionResult IzmireneClanarine(string DatumOd = "", string DatumDo = "")
        {
            decimal ukupanIznosUlazaOstvarenogOdClanarina = 0;

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

                List<StavkeClanarine> stavke = new List<StavkeClanarine>();
                stavke = ctx.StavkeClanarine.Where(x => x.isDeleted == false && x.isIzmirenaClanarina == true && x.DatumUplate >= datumOd && x.DatumUplate <= datumDo).ToList();
                StavkeClanarineIndexVM model = new StavkeClanarineIndexVM(stavke);

                for (int i = 0; i < model.stavka.Count(); i++)
                {
                    ukupanIznosUlazaOstvarenogOdClanarina += model.stavka[i].IznosKMBrojevima;
                }
                ViewData["ukupanIznosUlazaOstvarenogOdClanarina"] = ukupanIznosUlazaOstvarenogOdClanarina;

                return View("IzmireneClanarine", model);
            }
            else
            {
                ViewData["ukupanIznosUlazaOstvarenogOdClanarina"] = ukupanIznosUlazaOstvarenogOdClanarina;
                List<StavkeClanarine> stavke = new List<StavkeClanarine>();
                stavke = ctx.StavkeClanarine.Where(x => x.isDeleted == false && x.isIzmirenaClanarina == true).ToList();
                StavkeClanarineIndexVM model = new StavkeClanarineIndexVM(stavke);
                return View("IzmireneClanarine", model);

            }
        }


        public ActionResult NeizmireneClanarine()
        {

            List<StavkeClanarine> stavke = new List<StavkeClanarine>();
            stavke = ctx.StavkeClanarine.Where(x => x.isDeleted == false && x.isIzmirenaClanarina == false).ToList();
            StavkeClanarineIndexVM model = new StavkeClanarineIndexVM(stavke);
            return View("NeizmireneClanarine", model);
        }
        public ActionResult Dodaj(int clanarinaId, int izmirena)
        {
            StavkeClanarineDodajVM model = new StavkeClanarineDodajVM
            {
                izmirena = izmirena,
                clanoviKluba = BindClanoveKluba(),
                ClanarinaId = clanarinaId
            };
            List<StavkeClanarine> stavke = ctx.StavkeClanarine.Where(x => x.isDeleted == false && x.ClanarinaId == clanarinaId).ToList();
            List<string> listaId = new List<string>();
            for (int i = 0; i < model.clanoviKluba.Count(); i++)
            {
                for (int j = 0; j < stavke.Count(); j++)
                {

                    if (stavke[j].ClanKlubaId.ToString() == model.clanoviKluba[i].Value && stavke[j].isDeleted == false)
                    {
                        string value = stavke[j].ClanKlubaId.ToString();
                        listaId.Add(value);

                    }
                }
            }
            for (int i = 0; i < listaId.Count(); i++)
            {
                var item = model.clanoviKluba.First(x => x.Value == listaId[i]);
                model.clanoviKluba.Remove(item);
            }
            model.clanoviKluba.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite člana-" });

            return View("Dodaj", model);
        }
        public ActionResult SpremiNovuStavkuClanarine(StavkeClanarineDodajVM model)
        {
            for (int i = 0; i < model.ClanoviKlubaId.Count(); i++)
            {

                StavkeClanarine stavka = new StavkeClanarine();
                stavka.isDeleted = false;
                stavka.isIzmirenaClanarina = false;
                stavka.ClanKlubaId = model.ClanoviKlubaId[i];
                stavka.ClanarinaId = model.ClanarinaId;

                ctx.StavkeClanarine.Add(stavka);
                ctx.SaveChanges();

            }
            model.izmirena = 1;
            return RedirectToAction("Index", new { clanarinaId = model.ClanarinaId, izmirena = model.izmirena });
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
        public ActionResult Uredi(int stavkaId, int izmirena)
        {
            StavkeClanarine stavka = ctx.StavkeClanarine.Where(x => x.Id == stavkaId).FirstOrDefault();
            StavkeClanarineUrediVM model = new StavkeClanarineUrediVM
            {
                Id = stavkaId,
                isDeleted = stavka.isDeleted,
                IznosKMBrojevima = stavka.IznosKMBrojevima.ToString(),
                IznosKMSlovima = stavka.IznosKMSlovima,
                DatumUplate = KonvertujUString_mm_dd_yyyy(stavka.DatumUplate.ToString()),
                MjestoUplate = stavka.MjestoUplate,
                ClanKlubaId = stavka.ClanKlubaId,
                ClanarinaId = stavka.ClanarinaId,
                BrojPriznanice = stavka.BrojPriznanice,
                isIzmirenaClanarina = stavka.isIzmirenaClanarina,
                izmirena = izmirena

            };
            return View("Uredi", model);

        }
        public ActionResult Uredi2(int stavkaId)
        {
            StavkeClanarine stavka = ctx.StavkeClanarine.Where(x => x.Id == stavkaId).FirstOrDefault();
            StavkeClanarineUrediVM model = new StavkeClanarineUrediVM
            {
                Id = stavkaId,
                isDeleted = stavka.isDeleted,
                IznosKMBrojevima = stavka.IznosKMBrojevima.ToString(),
                IznosKMSlovima = stavka.IznosKMSlovima,
                DatumUplate = KonvertujUString_mm_dd_yyyy(stavka.DatumUplate.ToString()),
                MjestoUplate = stavka.MjestoUplate,
                ClanKlubaId = stavka.ClanKlubaId,
                ClanarinaId = stavka.ClanarinaId,
                BrojPriznanice = stavka.BrojPriznanice,
                isIzmirenaClanarina = stavka.isIzmirenaClanarina

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
        public ActionResult SpremiIzmjenuStavkeClanarine(StavkeClanarineUrediVM model)
        {
            StavkeClanarine stavka = ctx.StavkeClanarine.Where(x => x.Id == model.Id).FirstOrDefault();

            stavka.isIzmirenaClanarina = true;
            stavka.BrojPriznanice = model.BrojPriznanice;
            stavka.IznosKMBrojevima = Convert.ToDecimal(model.IznosKMBrojevima);
            stavka.IznosKMSlovima = model.IznosKMSlovima;
            if (model.DatumUplate != null)
                stavka.DatumUplate = KonvertujUDatum_dd_mm_yyyy(model.DatumUplate);
            stavka.MjestoUplate = model.MjestoUplate;
            ctx.SaveChanges();

            return RedirectToAction("Index", new { clanarinaId = stavka.ClanarinaId, izmirena = model.izmirena });
        }
        public ActionResult SpremiIzmjenuStavkeClanarine2(StavkeClanarineUrediVM model)
        {
            StavkeClanarine stavka = ctx.StavkeClanarine.Where(x => x.Id == model.Id).FirstOrDefault();

            stavka.isIzmirenaClanarina = true;
            stavka.BrojPriznanice = model.BrojPriznanice;
            stavka.IznosKMBrojevima = Convert.ToDecimal(model.IznosKMBrojevima);
            stavka.IznosKMSlovima = model.IznosKMSlovima;
            if (model.DatumUplate != null)
                stavka.DatumUplate = KonvertujUDatum_dd_mm_yyyy(model.DatumUplate);
            stavka.MjestoUplate = model.MjestoUplate;
            ctx.SaveChanges();

            return RedirectToAction("Index", "UpravljanjeClanarinama", new { brojTaba = 3 });

        }

        private List<SelectListItem> BindClanoveKluba()
        {
            return ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Osoba.Ime + " (" + x.Osoba.ImeRoditelja + ") " + x.Osoba.Prezime }).ToList();

        }
        public JsonResult Obrisi(int stavkaId)
        {
            StavkeClanarine stavka = ctx.StavkeClanarine.Where(x => x.Id == stavkaId).FirstOrDefault();
            if (stavka != null)
                stavka.isDeleted = true;
            ctx.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}