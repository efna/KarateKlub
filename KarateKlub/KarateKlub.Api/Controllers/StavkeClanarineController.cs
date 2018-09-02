using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KarateKlub.Api.Helper;
using KarateKlub.Api.Models;
using KarateKlub.Data;
using KarateKlub.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace KarateKlub.Api.Controllers
{
    [MyApiAuthorize]
    public class StavkeClanarineController : MyWebApiBaseController
    {
        public StavkeClanarineController(MyContext db) : base(db)
        {
        }
        public DateTime KonvertujUDatum_dd_mm_yyyy(string datum)
        {

            int dan = Convert.ToInt32(datum.Substring(0, 2));
            int mjesec = Convert.ToInt32(datum.Substring(3, 2));
            int godina = Convert.ToInt32(datum.Substring(6, 4));
            DateTime uneseniDatum = new DateTime(godina, mjesec, dan);
            return uneseniDatum;


        }
        [HttpPost]
        public int Add([FromBody] StavkeClanarineEditVM input)
        {
            StavkeClanarine stavka = _db.StavkeClanarines.Where(x=>x.Id==input.stavkaId).FirstOrDefault();
            stavka.isIzmirenaClanarina = true;
            stavka.BrojPriznanice = input.brojPriznanice;
            stavka.IznosKMBrojevima = Convert.ToDecimal(input.iznosBrojevima);
            stavka.IznosKMSlovima = input.iznosSlovima;
            if (input.datumUplate != null)
                stavka.DatumUplate = KonvertujUDatum_dd_mm_yyyy(input.datumUplate);
            stavka.MjestoUplate = input.mjestoUplate;
            _db.SaveChanges();
            return 0;
        }
    }
}