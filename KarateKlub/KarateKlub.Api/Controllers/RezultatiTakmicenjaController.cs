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
    public class RezultatiTakmicenjaController : MyWebApiBaseController
    {
        public RezultatiTakmicenjaController(MyContext db) : base(db)
        {
        }
        [HttpGet("{osobaId}")]
        public ActionResult<RezultatiTakmicenjaClanPregledVM> PregledRezultataClana(int osobaId)
        {
            RezultatiTakmicenjaClanPregledVM model = new RezultatiTakmicenjaClanPregledVM
            {
                rows = _db.RezultatiTakmicenjas.Where(x => x.Takmicar.ClanKluba.OsobaId == osobaId && x.isDeleted == false).Select(x => new RezultatiTakmicenjaClanPregledVM.Row
                {
                    nazivTakmicenja = _db.Takmicenjas.Where(s=>s.Id==x.TakmicenjeId).First().NazivTakmicenja,
                    mjestoOdrzavanja = _db.Takmicenjas.Where(s => s.Id == x.TakmicenjeId).First().MjestoOdrzavanjaTakmicenja,
                    datumOdrzavaja = _db.Takmicenjas.Where(s => s.Id == x.TakmicenjeId).First().DatumOdrzavanjaTakmicenja,
                    osvojenoMjesto =_db.OsvojenaMjestaNaTakmicenjus.Where(s=>s.Id==x.OsvojenoMjestoNaTakmicenjuId).First().Naziv,
                    kategorija = x.Kategorija,
                    uzrast =_db.StarosneDobis.Where(s => s.Id==x.StarosnaDobId).First().Naziv,
                    disciplina = _db.DisciplineTakmicenjas.Where(s => s.Id == x.DisciplinaTakmicenjaId).First().Naziv

                }).ToList()
            };
            return model;
        }
        [HttpGet("{osobaId}/{naziv}")]
        public ActionResult<RezultatiTakmicenjaClanPregledVM> PretragaRezultataClana(int osobaId, string naziv)
        {
            
            RezultatiTakmicenjaClanPregledVM model = new RezultatiTakmicenjaClanPregledVM
            {
                rows = _db.RezultatiTakmicenjas.Where(x => x.Takmicar.ClanKluba.OsobaId == osobaId && x.isDeleted == false
                && (x.Takmicenje.NazivTakmicenja.ToLower().Contains(naziv.ToLower()) || x.Takmicenje.NazivTakmicenja.ToLower().StartsWith(naziv.ToLower())
                )).Select(x => new RezultatiTakmicenjaClanPregledVM.Row
                {
                    nazivTakmicenja = _db.Takmicenjas.Where(s => s.Id == x.TakmicenjeId).First().NazivTakmicenja,
                    mjestoOdrzavanja = _db.Takmicenjas.Where(s => s.Id == x.TakmicenjeId).First().MjestoOdrzavanjaTakmicenja,
                    datumOdrzavaja = _db.Takmicenjas.Where(s => s.Id == x.TakmicenjeId).First().DatumOdrzavanjaTakmicenja,
                    osvojenoMjesto = _db.OsvojenaMjestaNaTakmicenjus.Where(s => s.Id == x.OsvojenoMjestoNaTakmicenjuId).First().Naziv,
                    kategorija = x.Kategorija,
                    uzrast = _db.StarosneDobis.Where(s => s.Id == x.StarosnaDobId).First().Naziv,
                    disciplina = _db.DisciplineTakmicenjas.Where(s => s.Id == x.DisciplinaTakmicenjaId).First().Naziv

                }).ToList()
            };
            return model;
        }
        public OsobaPregledVM GetOsobu(int osobaId)
        {

            OsobaPregledVM model = _db.Osobas
                .Where(w => w.Id == osobaId)
                .Select(s => new OsobaPregledVM
                {
                    ime = s.Ime,
                    prezime = s.Prezime,
                    imeRoditelja = s.ImeRoditelja

                }).SingleOrDefault();

            return model;
        }
        [HttpGet("{takmicenjeId}")]
        public ActionResult<RezultatiTakmicenjaTrenerPregledVM> PregledRezultataTakmicenjaByTrener(int takmicenjeId)
        {

            RezultatiTakmicenjaTrenerPregledVM model = new RezultatiTakmicenjaTrenerPregledVM
            {
                rows = _db.RezultatiTakmicenjas.Where(x => x.TakmicenjeId==takmicenjeId && x.isDeleted == false).Select(x => new RezultatiTakmicenjaTrenerPregledVM.Row
                {
                 id=x.Id,
                 takmicarId=x.TakmicarId,
                 starosnaDob=_db.StarosneDobis.Where(s=>s.Id==x.StarosnaDobId).First().Naziv,
                 kategorija=x.Kategorija,
                 disciplina=_db.DisciplineTakmicenjas.Where(s=>s.Id==x.DisciplinaTakmicenjaId).First().Naziv,
                 osvojenoMjesto=_db.OsvojenaMjestaNaTakmicenjus.Where(s=>s.Id==x.OsvojenoMjestoNaTakmicenjuId).First().Naziv,
                 brojPobjeda=x.BrojPobjeda,
                 brojPoraza=x.BrojPoraza,
                 brojTakmicaraUKategoriji=x.BrojTakmicaraUKategoriji
                    

                }).ToList()
            };
            for (int i = 0; i < model.rows.Count(); i++)
            {
                int takmicarId = model.rows[i].takmicarId;
                int clanKlubaId = _db.Takmicaris.Where(s => s.Id == takmicarId && s.isDeleted == false).First().ClanKlubaId;
                int osobaId= _db.ClanoviKlubas.Where(s => s.Id == clanKlubaId && s.isDeleted==false).First().OsobaId;

                OsobaPregledVM osoba = GetOsobu(osobaId);




                model.rows[i].takmicar =osoba.ime+" ("+osoba.imeRoditelja+") "+osoba.prezime;
            }
            return model;
          //<binding protocol="https" bindingInformation="*:44344:192.168.0.104 />
                
    }

        [HttpGet("{takmicenjeId}/{naziv}")]
        public ActionResult<RezultatiTakmicenjaTrenerPregledVM> PretragaRezultataTakmicenjaByTrener(int takmicenjeId,string naziv)
        {

            RezultatiTakmicenjaTrenerPregledVM model = new RezultatiTakmicenjaTrenerPregledVM
            {
                rows = _db.RezultatiTakmicenjas.Where(x => x.TakmicenjeId == takmicenjeId && x.isDeleted == false &&(x.OsvojenoMjestoNaTakmicenju.Naziv.ToLower().Contains(naziv.ToLower()) || x.OsvojenoMjestoNaTakmicenju.Naziv.ToLower().StartsWith(naziv.ToLower())
                || ((x.Takmicar.ClanKluba.Osoba.Ime.ToLower() + " " + x.Takmicar.ClanKluba.Osoba.Prezime.ToLower()).StartsWith(naziv.ToLower()) || (x.Takmicar.ClanKluba.Osoba.Prezime.ToLower() + " " + x.Takmicar.ClanKluba.Osoba.Ime.ToLower()).StartsWith(naziv.ToLower())))

                )
               .Select(x => new RezultatiTakmicenjaTrenerPregledVM.Row
                {
                    id = x.Id,
                    takmicarId = x.TakmicarId,
                    starosnaDob = _db.StarosneDobis.Where(s => s.Id == x.StarosnaDobId).First().Naziv,
                    kategorija = x.Kategorija,
                    disciplina = _db.DisciplineTakmicenjas.Where(s => s.Id == x.DisciplinaTakmicenjaId).First().Naziv,
                    osvojenoMjesto = _db.OsvojenaMjestaNaTakmicenjus.Where(s => s.Id == x.OsvojenoMjestoNaTakmicenjuId).First().Naziv,
                    brojPobjeda = x.BrojPobjeda,
                    brojPoraza = x.BrojPoraza,
                    brojTakmicaraUKategoriji = x.BrojTakmicaraUKategoriji


                }).ToList()
            };
            for (int i = 0; i < model.rows.Count(); i++)
            {
                int takmicarId = model.rows[i].takmicarId;
                int clanKlubaId = _db.Takmicaris.Where(s => s.Id == takmicarId && s.isDeleted == false).First().ClanKlubaId;
                int osobaId = _db.ClanoviKlubas.Where(s => s.Id == clanKlubaId && s.isDeleted == false).First().OsobaId;

                OsobaPregledVM osoba = GetOsobu(osobaId);




                model.rows[i].takmicar = osoba.ime + " ("+osoba.imeRoditelja+") " + osoba.prezime;
            }
            return model;
            
        }
        [HttpPost]
        public int Add([FromBody] RezultatiTakmicenjeAddVM input)
        {
            RezultatiTakmicenja rezultat = new RezultatiTakmicenja();
            rezultat.isDeleted = false;
            rezultat.TakmicenjeId = input.takmicenjeId;
            rezultat.TakmicarId = input.takmicarId;
            rezultat.StarosnaDobId = input.starosnaDobId;
            rezultat.DisciplinaTakmicenjaId = input.disciplinaTakmicenjaId;
            rezultat.OsvojenoMjestoNaTakmicenjuId = input.osvojenoMjestoNaTakmicenjuId;
            rezultat.Kategorija = input.kategorija;
            if (input.brojPobjeda !="")
                rezultat.BrojPobjeda = Convert.ToInt32(input.brojPobjeda);
            if (input.brojPoraza != "")
                rezultat.BrojPoraza = Convert.ToInt32(input.brojPoraza);
            if (input.brojTakmicaraUKategoriji != "")
                rezultat.BrojTakmicaraUKategoriji = Convert.ToInt32(input.brojTakmicaraUKategoriji);
            rezultat.Obrazlozenje = input.obrazlozenje;
            _db.RezultatiTakmicenjas.Add(rezultat);
            _db.SaveChanges();
            return 0;
        }

        [HttpDelete("{rezultatTakmicenjaId}")]
        public ActionResult Remove(int rezultatTakmicenjaId)
        {
            RezultatiTakmicenja rezultat = _db.RezultatiTakmicenjas.Where(x => x.Id == rezultatTakmicenjaId).FirstOrDefault();
            if (rezultat != null)
                rezultat.isDeleted = true;

            _db.SaveChanges();
            return Ok();
        }

    }
}