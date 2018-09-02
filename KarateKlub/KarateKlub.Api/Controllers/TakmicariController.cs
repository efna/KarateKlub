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
    public class TakmicariController : MyWebApiBaseController
    {
        public TakmicariController(MyContext db) : base(db)
        {
        }
        public OsobaPregledVM GetOsobu(int osobaId)
        {

            OsobaPregledVM model = _db.Osobas
                .Where(w => w.Id == osobaId)
                .Select(s => new OsobaPregledVM
                {
                    ime = s.Ime,
                    prezime = s.Prezime,
                    imeRoditelja = s.ImeRoditelja,
                    slika=s.Slika
                    

                }).SingleOrDefault();

            return model;
        }
        [HttpGet]
        public ActionResult<TakmicariPregledVM> PregledTakmicara()
        {
            TakmicariPregledVM model = new TakmicariPregledVM {
                rows=_db.Takmicaris.Where(x=>x.isDeleted==false && x.isAktivanTakmicar == true).Select(x=>new TakmicariPregledVM.Row {
                    id=x.Id,
                    regBroj=x.RegistarskiBroj
                }).ToList()
            };
            for (int i = 0; i < model.rows.Count(); i++)
            {
                int takmicarId = model.rows[i].id;
                int clanKlubaId = _db.Takmicaris.Where(s => s.Id == takmicarId && s.isDeleted == false).First().ClanKlubaId;
                int osobaId = _db.ClanoviKlubas.Where(s => s.Id == clanKlubaId && s.isDeleted == false).First().OsobaId;

                OsobaPregledVM osoba = GetOsobu(osobaId);
                
                model.rows[i].takmicar = osoba.ime + " (" + osoba.imeRoditelja + " )" + osoba.prezime;
                model.rows[i].slika = osoba.slika;
            }

            return model;
        }
        [HttpGet("{imePrezime}")]
        public ActionResult<TakmicariPregledVM> PretragaTakmicaraByImePrezime(string imePrezime)
        {
            TakmicariPregledVM model = new TakmicariPregledVM
            {
                rows = _db.Takmicaris.Where(x => x.isDeleted == false && x.isAktivanTakmicar==true
                && (((x.ClanKluba.Osoba.Ime.ToLower() + " " + x.ClanKluba.Osoba.Prezime.ToLower()).StartsWith(imePrezime.ToLower()) || (x.ClanKluba.Osoba.Prezime.ToLower() + " " + x.ClanKluba.Osoba.Ime.ToLower()).StartsWith(imePrezime.ToLower()))
                
                )).Select(x => new TakmicariPregledVM.Row
                {
                    id = x.Id,
                    regBroj = x.RegistarskiBroj
                }).ToList()
            };
            for (int i = 0; i < model.rows.Count(); i++)
            {
                int takmicarId = model.rows[i].id;
                int clanKlubaId = _db.Takmicaris.Where(s => s.Id == takmicarId && s.isDeleted == false).First().ClanKlubaId;
                int osobaId = _db.ClanoviKlubas.Where(s => s.Id == clanKlubaId && s.isDeleted == false).First().OsobaId;

                OsobaPregledVM osoba = GetOsobu(osobaId);

                model.rows[i].takmicar = osoba.ime + " (" + osoba.imeRoditelja + " )" + osoba.prezime;
                model.rows[i].slika = osoba.slika;
            }

            return model;
        }
    }
}