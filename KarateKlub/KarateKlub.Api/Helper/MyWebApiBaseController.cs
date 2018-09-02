using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KarateKlub.Data;
using KarateKlub.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace KarateKlub.Api.Helper
{
    [Route("api/[controller]/[action]")]

    public abstract class MyWebApiBaseController : Controller
    {
        protected readonly MyContext _db;

        protected MyWebApiBaseController(MyContext db)
        {
            _db = db;
        }

        protected KorisnickiNalozi AuthKorisnickiNalog => HttpContext.GetKorisnikOfAuthToken();
        protected KorisnickiNalozi AuthKorisnik => _db.KorisnickiNalozis.SingleOrDefault(s => s.Id == AuthKorisnickiNalog.Id);
    }

}