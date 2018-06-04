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

    public class UpravljanjeIzlazimaController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulSekretar/UpravljanjeIzlazima
        public ActionResult Index(string DatumOd="",string DatumDo="",int brojTaba=1)
        {
            ViewData["tab"] = brojTaba;
            ViewData["DatumOd"] = DatumOd;
            ViewData["DatumDo"] = DatumDo;
            decimal ukupanIzlazZaOdabraniPeriod = 0;
            if (DatumOd == "" && DatumDo == "")
            {
             
                UpravljanjeIzlazimaIndexVM model = new UpravljanjeIzlazimaIndexVM
                {
                    DatumOd =DatumOd,
                    DatumDo =DatumDo

                };
                ViewData["ukupanIzlazZaOdabraniPeriod"] = ukupanIzlazZaOdabraniPeriod;
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
                List<UplateUposlenicima> uplateUposlenicima = ctx.UplateUposlenicima.Where(x => x.isDeleted == false && x.DatumUplate >= datumOd && x.DatumUplate <= datumDo).ToList();
                decimal ukupanIzlazZaUplateUposlenicima = 0;
                for (int i = 0; i < uplateUposlenicima.Count(); i++)
                {
                    ukupanIzlazZaUplateUposlenicima += uplateUposlenicima[i].IznosKMBrojevima;

                }

                List<TroskoviRegracijeKluba> troskoviRegistracijeKluba = ctx.TroskoviRegracijeKluba.Where(x => x.isDeleted == false && x.DatumUplate >= datumOd && x.DatumUplate <= datumDo).ToList();
                decimal ukupanIzlazZaTroskoveRegistracijeKluba = 0;
                for (int i = 0; i < troskoviRegistracijeKluba.Count(); i++)
                {
                    ukupanIzlazZaTroskoveRegistracijeKluba += troskoviRegistracijeKluba[i].IznosKMBrojevima;

                }
                List<TroskoviRegistracijeTakmicara> troskoviRegistracijeTakmicara = ctx.TroskoviRegistracijeTakmicara.Where(x => x.isDeleted == false && x.DatumUplate >= datumOd && x.DatumUplate <= datumDo).ToList();
                decimal ukupanIzlazZaTroskoveRegistracijeTakmicara = 0;
                for (int i = 0; i < troskoviRegistracijeTakmicara.Count(); i++)
                {
                    ukupanIzlazZaTroskoveRegistracijeTakmicara += troskoviRegistracijeTakmicara[i].IznosKMBrojevima;

                }

                List<TroskoviTakmicenja> troskoviTakmicenja = ctx.TroskoviTakmicenja.Where(x => x.isDeleted == false && x.DatumUplate >= datumOd && x.DatumUplate <= datumDo).ToList();
                decimal ukupanIzlazZaTroskoveTakmicenja = 0;
                for (int i = 0; i < troskoviTakmicenja.Count(); i++)
                {
                    ukupanIzlazZaTroskoveTakmicenja += troskoviTakmicenja[i].IznosKMBrojevima;

                }
                List<TroskoviSeminara> troskoviSeminara = ctx.TroskoviSeminara.Where(x => x.isDeleted == false && x.DatumUplate >= datumOd && x.DatumUplate <= datumDo).ToList();
                decimal ukupanIzlazZaTroskoveSeminara = 0;
                for (int i = 0; i < troskoviSeminara.Count(); i++)
                {
                    ukupanIzlazZaTroskoveSeminara += troskoviSeminara[i].IznosKMBrojevima;

                }
                List<TroskoviPolaganjaUcenickaZvanja> troskoviPolaganja = ctx.TroskoviPolaganjaUcenickaZvanja.Where(x => x.isDeleted == false && x.DatumUplate >= datumOd && x.DatumUplate <= datumDo).ToList();
                decimal ukupanIzlazZaTroskovePolaganja = 0;
                for (int i = 0; i < troskoviPolaganja.Count(); i++)
                {
                    ukupanIzlazZaTroskovePolaganja += troskoviPolaganja[i].IznosKMBrojevima;

                }
                List<TroskoviNarudzbeOpremeKluba> troskoviNarudzbeOpreme = ctx.TroskoviNarudzbeOpremeKluba.Where(x => x.isDeleted == false && x.DatumUplate >= datumOd && x.DatumUplate <= datumDo).ToList();
                decimal ukupanIzlazZaTroskoveNarudzbeOpreme = 0;
                for (int i = 0; i < troskoviNarudzbeOpreme.Count(); i++)
                {
                    ukupanIzlazZaTroskoveNarudzbeOpreme += troskoviNarudzbeOpreme[i].IznosKMBrojevima;

                }
                List<Izlaz> ostaliTroskovi = ctx.Izlaz.Where(x => x.isDeleted == false && x.Datum >= datumOd && x.Datum <= datumDo).ToList();
                decimal ukupanIzlazZaOstaleTroskove = 0;
                for (int i = 0; i < ostaliTroskovi.Count(); i++)
                {
                    ukupanIzlazZaOstaleTroskove += ostaliTroskovi[i].IznosKMSBrojevima;

                }

                ukupanIzlazZaOdabraniPeriod = ukupanIzlazZaUplateUposlenicima + ukupanIzlazZaTroskoveRegistracijeKluba + ukupanIzlazZaTroskoveRegistracijeTakmicara + ukupanIzlazZaTroskoveTakmicenja +
                    ukupanIzlazZaTroskoveSeminara + ukupanIzlazZaTroskovePolaganja + ukupanIzlazZaTroskoveNarudzbeOpreme + ukupanIzlazZaOstaleTroskove;
                UpravljanjeIzlazimaIndexVM model = new UpravljanjeIzlazimaIndexVM
                {
                    DatumOd = KonvertujUString_mm_dd_yyyy(DatumOd),
                    DatumDo = KonvertujUString_mm_dd_yyyy(DatumDo)

                };

                ViewData["ukupanIzlazZaOdabraniPeriod"] = ukupanIzlazZaOdabraniPeriod;
                return View(model);
            }
                
            
        }
     
        public ActionResult GoToUpravljanjeIzlazima(UpravljanjeIzlazimaIndexVM model)
        {

            return RedirectToAction("Index",new { DatumOd = model.DatumOd, DatumDo =model.DatumDo,brojTaba=1 });
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