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
    public class StecenaZvanjaController : MyWebApiBaseController
    {
        public StecenaZvanjaController(MyContext db) : base(db)
        {
        }


        [HttpGet("{osobaId}")]
        public ActionResult<StecenaZvanjaPregledVM> Pregled(int osobaId)
        {

            
            var model = new StecenaZvanjaPregledVM
            {
                rows = _db.StecenaZvanjas.Where(s=>s.isDeleted==false && s.OsobaId==osobaId)
                    .OrderByDescending(s => s.Id)
                    .Select(s => new StecenaZvanjaPregledVM.Row
                    {
                        ime=_db.Osobas.Where(k=>k.Id==s.OsobaId).First().Ime+" ",
                        prezime=_db.Osobas.Where(k=>k.Id==s.OsobaId).First().Prezime,
                        imeRoditelja=_db.Osobas.Where(k=>k.Id==osobaId).First().ImeRoditelja,
                        nazivZvanja = _db.ZvanjaUKarateus.Where(z => z.Id == s.ZvanjeUKarateuId).First().Naziv,
                        mjestoSticanja = s.Mjesto,
                        datumSticanja = s.DatumSticanja,
                        organizator = s.Organizator


                    }).ToList()
            };
            return model;
        }
   
  
        [HttpGet("{naziv}/{osobaId}")]
        public ActionResult<StecenaZvanjaPregledVM> PretragaByNazivClanId(string naziv, int osobaId)
        {
          

            StecenaZvanjaPregledVM model = new StecenaZvanjaPregledVM
            {

                rows = _db.StecenaZvanjas.Where(x => x.isDeleted == false && x.OsobaId == osobaId && (x.ZvanjeUKarateu.Naziv.ToLower().Contains(naziv.ToLower()) || x.ZvanjeUKarateu.Naziv.ToLower().StartsWith(naziv.ToLower()))).Select(x => new StecenaZvanjaPregledVM.Row
                {
                    ime = _db.Osobas.Where(k => k.Id == x.OsobaId).First().Ime + " ",
                    prezime = _db.Osobas.Where(k => k.Id == x.OsobaId).First().Prezime,
                    imeRoditelja = _db.Osobas.Where(k => k.Id == osobaId).First().ImeRoditelja,
                    nazivZvanja = _db.ZvanjaUKarateus.Where(s => s.Id == x.ZvanjeUKarateuId).First().Naziv,
                    mjestoSticanja = x.Mjesto,
                    datumSticanja = x.DatumSticanja,
                    organizator = x.Organizator
                }).ToList()
            };

            return model;
        }

        [HttpGet]
        public ActionResult<StecenaZvanjaPregledVM> PregledSvihZvanja()
        {
            StecenaZvanjaPregledVM model = new StecenaZvanjaPregledVM
            {

                rows = _db.StecenaZvanjas.Where(x => x.isDeleted).Select(x => new StecenaZvanjaPregledVM.Row
                {
                    ime = _db.Osobas.Where(k => k.Id == x.OsobaId).First().Ime + " ",
                    prezime = _db.Osobas.Where(k => k.Id == x.OsobaId).First().Prezime,
                    imeRoditelja = _db.Osobas.Where(k => k.Id == x.OsobaId).First().ImeRoditelja,
                    nazivZvanja = _db.ZvanjaUKarateus.Where(s => s.Id == x.ZvanjeUKarateuId).First().Naziv,
                    mjestoSticanja = x.Mjesto,
                    datumSticanja = x.DatumSticanja,
                    organizator = x.Organizator
                }).ToList()
            };

            return model;
        }

        [HttpGet("{naziv}")]
        public ActionResult<StecenaZvanjaPregledVM> PretragaSvihZvanjaByNaziv(string naziv)
        {


            StecenaZvanjaPregledVM model = new StecenaZvanjaPregledVM
            {

                rows = _db.StecenaZvanjas.Where(x => x.isDeleted == false && (
                x.ZvanjeUKarateu.Naziv.ToLower().Contains(naziv.ToLower()) || x.ZvanjeUKarateu.Naziv.ToLower().StartsWith(naziv.ToLower())
                || ((x.Osoba.Ime.ToLower() + " " + x.Osoba.Prezime.ToLower()).StartsWith(naziv.ToLower()) || (x.Osoba.Prezime.ToLower() + " " + x.Osoba.Ime.ToLower()).StartsWith(naziv.ToLower()))


                )).Select(x => new StecenaZvanjaPregledVM.Row
                {
                    ime = _db.Osobas.Where(k => k.Id == x.OsobaId).First().Ime + " ",
                    prezime = _db.Osobas.Where(k => k.Id == x.OsobaId).First().Prezime,
                    imeRoditelja = _db.Osobas.Where(k => k.Id == x.OsobaId).First().ImeRoditelja,
                    nazivZvanja = _db.ZvanjaUKarateus.Where(s => s.Id == x.ZvanjeUKarateuId).First().Naziv,
                    mjestoSticanja = x.Mjesto,
                    datumSticanja = x.DatumSticanja,
                    organizator = x.Organizator
                }).ToList()
            };

            return model;
        }

    }
}