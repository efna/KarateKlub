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

    public class UpravljanjeUlazimaController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulBlagajnik/UpravljanjeUlazima
        public ActionResult Index(string DatumOd = "", string DatumDo = "", int brojTaba = 1)
        {
            ViewData["tab"] = brojTaba;
            ViewData["DatumOd"] = DatumOd;
            ViewData["DatumDo"] = DatumDo;
            decimal ukupanUlazZaOdabraniPeriod = 0;
            if (DatumOd == "" && DatumDo == "")
            {

                UpravljanjeUlazimaIndeVM model = new UpravljanjeUlazimaIndeVM
                {
                    DatumOd = DatumOd,
                    DatumDo = DatumDo

                };
                ViewData["ukupanUlazZaOdabraniPeriod"] = ukupanUlazZaOdabraniPeriod;
                return View(model);

            }
            else
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
                List<StavkeClanarine> clanarine = ctx.StavkeClanarine.Where(x => x.isDeleted == false && x.DatumUplate >= datumOd && x.DatumUplate <= datumDo).ToList();
                decimal ukupanUlazaOstvarenOdClanarine = 0;
                for (int i = 0; i < clanarine.Count(); i++)
                {
                    ukupanUlazaOstvarenOdClanarine += clanarine[i].IznosKMBrojevima;

                }

                List<Upisnine> upisnine = ctx.Upisnine.Where(x => x.isDeleted == false && x.DatumUplate >= datumOd && x.DatumUplate <= datumDo).ToList();
                decimal ukupanUlazaOstvarenOdUpisnina = 0;
                for (int i = 0; i < upisnine.Count(); i++)
                {
                    ukupanUlazaOstvarenOdUpisnina += upisnine[i].IznosKMBrojevima;

                }
                List<ParticipacijeZaPolaganjeUcenickaZvanja> participacijeZaPolaganje = ctx.ParticipacijeZaPolaganjeUcenickaZvanja.Where(x => x.isDeleted == false && x.DatumUplate >= datumOd && x.DatumUplate <= datumDo).ToList();
                decimal ukupanUlazaOstvarenOdParticipacijaZaPolaganje = 0;
                for (int i = 0; i < participacijeZaPolaganje.Count(); i++)
                {
                    ukupanUlazaOstvarenOdParticipacijaZaPolaganje += participacijeZaPolaganje[i].IznosKMBrojevima;

                }

                List<Donacije> donacije = ctx.Donacije.Where(x => x.isDeleted == false && x.DatumUplate >= datumOd && x.DatumUplate <= datumDo).ToList();
                decimal ukupanUlazaOstvarenOdDonacija = 0;
                for (int i = 0; i < donacije.Count(); i++)
                {
                    ukupanUlazaOstvarenOdDonacija += donacije[i].IznosKMBrojevima;

                }
                List<Sponzorstva> sponzorstva = ctx.Sponzorstva.Where(x => x.isDeleted == false && x.DatumUplate >= datumOd && x.DatumUplate <= datumDo).ToList();
                decimal ukupanUlazaOstvarenOdSponzorstva = 0;
                for (int i = 0; i < sponzorstva.Count(); i++)
                {
                    ukupanUlazaOstvarenOdSponzorstva += sponzorstva[i].IznosKMBrojevima;

                }
                List<Ulaz> ulazi = ctx.Ulaz.Where(x => x.isDeleted == false && x.Datum >= datumOd && x.Datum <= datumDo).ToList();
                decimal ukupanUlazaOstvarenOdOstalihDobiti = 0;
                for (int i = 0; i < ulazi.Count(); i++)
                {
                    ukupanUlazaOstvarenOdOstalihDobiti += ulazi[i].IznosKMSBrojevima;

                }


                ukupanUlazZaOdabraniPeriod = ukupanUlazaOstvarenOdClanarine + ukupanUlazaOstvarenOdUpisnina + ukupanUlazaOstvarenOdParticipacijaZaPolaganje + ukupanUlazaOstvarenOdDonacija +
                    ukupanUlazaOstvarenOdSponzorstva + ukupanUlazaOstvarenOdOstalihDobiti;
                UpravljanjeUlazimaIndeVM model = new UpravljanjeUlazimaIndeVM
                {
                    DatumOd = KonvertujUString_mm_dd_yyyy(DatumOd),
                    DatumDo = KonvertujUString_mm_dd_yyyy(DatumDo)

                };

                ViewData["ukupanUlazZaOdabraniPeriod"] = ukupanUlazZaOdabraniPeriod;
                return View(model);
            }


        }

        public ActionResult GoToUpravljanjeUlazima(UpravljanjeUlazimaIndeVM model)
        {

            return RedirectToAction("Index", new { DatumOd = model.DatumOd, DatumDo = model.DatumDo, brojTaba = 1 });
        }
        public string KonvertujUString_mm_dd_yyyy(string datum)
        {
            string dan = datum.Substring(0, 2);
            string mjesec = datum.Substring(3, 2);
            string godina = datum.Substring(6, 4);
            string uneseniDatum = dan + "/" + mjesec + "/" + godina;
            return uneseniDatum;
        }

        public DateTime KonvertujUDatum_dd_mm_yyyy(string datum)
        {
            int dan = Convert.ToInt32(datum.Substring(0, 2));
            int mjesec = Convert.ToInt32(datum.Substring(3, 2));
            int godina = Convert.ToInt32(datum.Substring(6, 4));
            DateTime uneseniDatum = new DateTime(godina, mjesec, dan);
            return uneseniDatum;
        }
    }
}