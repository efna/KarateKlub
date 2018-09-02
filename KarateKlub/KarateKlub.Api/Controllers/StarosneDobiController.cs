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
    public class StarosneDobiController : MyWebApiBaseController
    {
        public StarosneDobiController(MyContext db) : base(db)
        {
        }

        [HttpGet]
        public ActionResult<StarosneDobiPregledVM> GetAll()
        {
            var model = new StarosneDobiPregledVM()
            {
                rows = _db.StarosneDobis.Where(x => x.isDeleted == false)
                    .Select(s => new StarosneDobiPregledVM.Row
                    {
                        id = s.Id,
                        naziv = s.Naziv
                    }).ToList()
            };

            return model;
        }
    }
}