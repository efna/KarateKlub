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
    public class SaveziController : MyWebApiBaseController
    {
        public SaveziController(MyContext db) : base(db)
        {
        }
        [HttpGet]
        public ActionResult<SaveziPregledVM> GetAll()
        {
            var model = new SaveziPregledVM()
            {
                rows = _db.Savezis.Where(x=>x.isDeleted==false)
                    .Select(s => new SaveziPregledVM.Row
                    {
                        id = s.Id,
                        naziv = s.Naziv
                    }).ToList()
            };

            return model;
        }
    }
}