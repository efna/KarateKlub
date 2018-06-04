using KarateKlub.Data;
using KarateKlub.Data.Models;
using KarateKlub.Web.Areas.ModulTrener.Models;
using KarateKlub.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulTrener.Controllers
{
    [Autorizacija(false, false, false, true, false)]

    public class UpravljanjeRezultatimaTakmicenjaController : Controller
    {
        MojContext ctx = new MojContext();

        // GET: ModulTrener/UpravljanjeRezultatimaTakmicenja
        public ActionResult Index(int takmicenjeId, int brojTaba = 1)
        {
            Takmicenja takmicenje = ctx.Takmicenja.Where(x => x.Id == takmicenjeId).FirstOrDefault();
            ViewData["tab"] = brojTaba;
            ViewData["takmicenjeId"] = takmicenjeId;
            ViewData["nazivTakmicenja"] = takmicenje.NazivTakmicenja;
            return View();
        }
        public ActionResult Index2(string DatumOd = "", string DatumDo = "", int brojTaba = 1)
        {
            ViewData["tab"] = brojTaba;
            ViewData["DatumOd"] = DatumOd;
            ViewData["DatumDo"] = DatumDo;
            if (DatumOd == "" && DatumDo == "")
            {

                UpravljanjeRezultatimaTakmicenjaIndex2VM model = new UpravljanjeRezultatimaTakmicenjaIndex2VM
                {
                    DatumOd = DatumOd,
                    DatumDo = DatumDo

                };

                return View("Index2", model);

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

                UpravljanjeRezultatimaTakmicenjaIndex2VM model = new UpravljanjeRezultatimaTakmicenjaIndex2VM
                {
                    DatumOd = KonvertujUString_mm_dd_yyyy(DatumOd),
                    DatumDo = KonvertujUString_mm_dd_yyyy(DatumDo)

                };


                return View("Index2", model);
            }


        }

        public ActionResult GoToIndex2(UpravljanjeRezultatimaTakmicenjaIndex2VM model)
        {

            return RedirectToAction("Index2", new { DatumOd = model.DatumOd, DatumDo = model.DatumDo, brojTaba = 1 });
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