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
    public class TakmicenjaController : MyWebApiBaseController
    {
        public TakmicenjaController(MyContext db) : base(db)
        {
        }
       [HttpGet]
        public ActionResult<TakmicenjaPregledVM> PregledSvihTakmicenja(){
            TakmicenjaPregledVM model = new TakmicenjaPregledVM {
                rows=_db.Takmicenjas.Where(x=>x.isDeleted==false).Select(x=>new TakmicenjaPregledVM.Row {
                    id=x.Id,
                    nazivTakmicenja=x.NazivTakmicenja,
                    mjestoOdrzavanjaTakmicenja=x.MjestoOdrzavanjaTakmicenja,
                    datumOdrzavanjaTakmicenja=x.DatumOdrzavanjaTakmicenja,
                    organizatorTakmicenja=x.OrganizatorTakmicenja,
                    savezId=x.SavezId,
                    nazivSaveza=_db.Savezis.Where(z=>z.Id==x.SavezId).First().Naziv
                }).ToList()
            };
            return model;
     }
        [HttpGet("{naziv}")]
        public ActionResult<TakmicenjaPregledVM> PretragaSvihTakmicenjaByNaziv(string naziv)
        {
            TakmicenjaPregledVM model = new TakmicenjaPregledVM
            {
                rows = _db.Takmicenjas.Where(x => x.isDeleted == false
                && (x.NazivTakmicenja.ToLower().Contains(naziv.ToLower()) ||
                x.NazivTakmicenja.ToLower().StartsWith(naziv.ToLower()))
                ).Select(x => new TakmicenjaPregledVM.Row
                {
                    id = x.Id,
                    nazivTakmicenja = x.NazivTakmicenja,
                    mjestoOdrzavanjaTakmicenja = x.MjestoOdrzavanjaTakmicenja,
                    datumOdrzavanjaTakmicenja = x.DatumOdrzavanjaTakmicenja,
                    organizatorTakmicenja = x.OrganizatorTakmicenja,
                    savezId = x.SavezId,
                    nazivSaveza = _db.Savezis.Where(z => z.Id == x.SavezId).First().Naziv
                }).ToList()
            };
            return model;
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
        public int Add([FromBody] TakmicenjeAddVM input)
        {
            _db.Takmicenjas.Add(new Takmicenja
            {
                isDeleted = false,
                NazivTakmicenja = input.naziv,
                DatumOdrzavanjaTakmicenja = KonvertujUDatum_dd_mm_yyyy(input.datumOdrzavanja),
                MjestoOdrzavanjaTakmicenja = input.mjestoOdrzavanja,
                OrganizatorTakmicenja=input.organizator,
                SavezId=input.savezId

            });
            _db.SaveChanges();
            return 0;
        }
        [HttpDelete("{takmicenjeId}")]
        public ActionResult Remove(int takmicenjeId)
        {
            Takmicenja takmicenje = _db.Takmicenjas.Where(x => x.Id == takmicenjeId).FirstOrDefault();
            if(takmicenje!=null)
            takmicenje.isDeleted = true;
            List<RezultatiTakmicenja> rezultati = _db.RezultatiTakmicenjas.Where(x => x.TakmicenjeId == takmicenje.Id).ToList();
            for (int i = 0; i < rezultati.Count(); i++)
            {
                rezultati[i].isDeleted = true;

            }
            _db.SaveChanges();
            return Ok();
        }
    }
}