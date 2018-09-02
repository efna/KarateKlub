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
    public class ClanoviKlubaController : MyWebApiBaseController
    {
        public ClanoviKlubaController(MyContext db) : base(db)
        {
        }
        public ActionResult<ClanoviKubaPregledClanovaVM> PregledClanova()
        {
            ClanoviKubaPregledClanovaVM model = new ClanoviKubaPregledClanovaVM
            {
                rows = _db.Osobas.OrderByDescending(x => x.Prezime).Where(x => x.isDeleted == false && x.isClanKluba == true && x.isAktivnaOsoba == true).OrderByDescending(x => x.Prezime).Select(x => new ClanoviKubaPregledClanovaVM.Row
                {
                    id = _db.ClanoviKlubas.Where(s => s.OsobaId == x.Id).First().Id,
                    ime = x.Ime,
                    prezime = x.Prezime,
                    imeRoditelja = x.ImeRoditelja,
                    spol = x.Spol,
                    datumRodjenja = x.DatumRodjenja,
                    mjestoRodjenja = x.MjestoRodjenja,
                    slika = x.Slika,
                    kontaktTelefon=x.KontaktTelefon
                    

                }).ToList()
            };
            return model;
        }
        [HttpGet("{imePrezime}")]
        public ActionResult<ClanoviKubaPregledClanovaVM> PretragaClanovaByImePrezime(string imePrezime)
        {
            ClanoviKubaPregledClanovaVM model = new ClanoviKubaPregledClanovaVM
            {
                rows = _db.Osobas.OrderByDescending(x=>x.Prezime).Where(x => x.isDeleted == false && x.isClanKluba == true && x.isAktivnaOsoba == true && ((x.Ime.ToLower() + " " + x.Prezime.ToLower()).StartsWith(imePrezime.ToLower()) || (x.Prezime.ToLower() + " " + x.Ime.ToLower()).StartsWith(imePrezime.ToLower()))).Select(x => new ClanoviKubaPregledClanovaVM.Row
                {
                    id = _db.ClanoviKlubas.Where(s => s.OsobaId == x.Id).First().Id,
                    ime = x.Ime,
                    prezime = x.Prezime,
                    imeRoditelja = x.ImeRoditelja,
                    spol = x.Spol,
                    datumRodjenja = x.DatumRodjenja,
                    mjestoRodjenja=x.MjestoRodjenja,
                    slika=x.Slika,
                    kontaktTelefon=x.KontaktTelefon

                }).ToList()
            };
            return model;
        }
    }
}