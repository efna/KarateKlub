using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KarateKlub.Api.Helper;
using KarateKlub.Api.Models;
using KarateKlub.Data;
using Microsoft.AspNetCore.Mvc;

namespace KarateKlub.Api.Controllers
{
    [MyApiAuthorize]
    public class OsvojenaMjestaNaTakmicenjuController : MyWebApiBaseController
    {
        public OsvojenaMjestaNaTakmicenjuController(MyContext db) : base(db)
        {
        }

        [HttpGet]
        public ActionResult<OsvojenaMjestaNaTakmicenjuPregledVM> GetAll()
        {

            var model = new OsvojenaMjestaNaTakmicenjuPregledVM()
            {
                rows = _db.OsvojenaMjestaNaTakmicenjus.Where(x => x.isDeleted == false)
                .Select(s => new OsvojenaMjestaNaTakmicenjuPregledVM.Row
                {
                    id = s.Id,
                    naziv = s.Naziv
                }).ToList()
            };

            return model;
        }
    }
}
