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
    public class DisciplineTakmicenjaController : MyWebApiBaseController
    {
        public DisciplineTakmicenjaController(MyContext db) : base(db)
        {
        }

        [HttpGet]
        public ActionResult<DisciplineTakmicenjaPregledVM> GetAll()
        {

            var model = new DisciplineTakmicenjaPregledVM()
        {
            rows = _db.DisciplineTakmicenjas.Where(x => x.isDeleted == false)
                .Select(s => new DisciplineTakmicenjaPregledVM.Row
                {
                    id = s.Id,
                    naziv = s.Naziv
                }).ToList()
                };

                return model;
            }
    }
}