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
    public class ClanarineController : MyWebApiBaseController
    {
        public ClanarineController(MyContext db) : base(db)
        {
        }
        [HttpGet("{osobaId}")]
        public ClanarineIzmireneClanarineClanaPregledVM IzmireneClanarineClanaPregled(int osobaId)
        {
         
            ClanarineIzmireneClanarineClanaPregledVM model = new ClanarineIzmireneClanarineClanaPregledVM
            {
                rows = _db.StavkeClanarines.Where(x => x.ClanKluba.OsobaId == osobaId && x.isIzmirenaClanarina == true && x.isDeleted == false).Select(x => new ClanarineIzmireneClanarineClanaPregledVM.Row
                {
                    id = x.Id,
                    naziv =_db.Clanarines.Where(s=>s.Id==x.ClanarinaId).First().Naziv,
                    datumUplate = x.DatumUplate,
                    iznos = x.IznosKMBrojevima.ToString() + " KM",
                    mjestoUplate = x.MjestoUplate,
                    datumOd = _db.Clanarines.Where(s => s.Id == x.ClanarinaId).First().DatumOd,
                    datumDo = _db.Clanarines.Where(s => s.Id == x.ClanarinaId).First().DatumDo,
                    brojPriznanice = x.BrojPriznanice

                }).ToList()
            };
            return model;
        }

        [HttpGet("{osobaId}")]
        public ClanarineNeizmireneClanarineClanaPregledVM NeizmireneClanarineClanaPregled(int osobaId)
        {
       
            ClanarineNeizmireneClanarineClanaPregledVM model = new ClanarineNeizmireneClanarineClanaPregledVM
            {
                rows = _db.StavkeClanarines.Where(x => x.ClanKluba.OsobaId == osobaId && x.isIzmirenaClanarina == false && x.isDeleted == false).Select(x => new ClanarineNeizmireneClanarineClanaPregledVM.Row
                {
                    id = x.Id,
                    naziv = _db.Clanarines.Where(s => s.Id == x.ClanarinaId).First().Naziv,
                    datumOd = _db.Clanarines.Where(s => s.Id == x.ClanarinaId).First().DatumOd,
                    datumDo = _db.Clanarines.Where(s => s.Id == x.ClanarinaId).First().DatumDo
                }).ToList()
            };
            return model;
        }

        [HttpGet("{osobaId}/{naziv}")]
        public ClanarineIzmireneClanarineClanaPregledVM PretragaIzmirenihClanarinaClanaByclanIdNaziv(int osobaId, string naziv)
        {
        
            ClanarineIzmireneClanarineClanaPregledVM model = new ClanarineIzmireneClanarineClanaPregledVM
            {
                rows = _db.StavkeClanarines.Where(x => x.ClanKluba.OsobaId == osobaId && x.isIzmirenaClanarina == true && (x.Clanarina.Naziv.ToLower().Contains(naziv.ToLower()) ||
                x.Clanarina.Naziv.ToLower().StartsWith(naziv.ToLower())
                )).Select(x => new ClanarineIzmireneClanarineClanaPregledVM.Row
                {
                    id = x.Id,
                    naziv = _db.Clanarines.Where(s => s.Id == x.ClanarinaId).First().Naziv,
                    datumUplate = x.DatumUplate,
                    iznos = x.IznosKMBrojevima.ToString() + " KM",
                    mjestoUplate = x.MjestoUplate,
                    datumOd = _db.Clanarines.Where(s => s.Id == x.ClanarinaId).First().DatumOd,
                    datumDo = _db.Clanarines.Where(s => s.Id == x.ClanarinaId).First().DatumDo,
                    brojPriznanice = x.BrojPriznanice

                }).ToList()
            };
            return model;
        }
        [HttpGet("{osobaId}/{naziv}")]
        public ClanarineNeizmireneClanarineClanaPregledVM PretragaNeizmirenihClanarinaClanaByclanIdNaziv(int osobaId, string naziv)
        {
            ClanarineNeizmireneClanarineClanaPregledVM model = new ClanarineNeizmireneClanarineClanaPregledVM
            {
                rows = _db.StavkeClanarines.Where(x => x.ClanKluba.OsobaId == osobaId && x.isIzmirenaClanarina == false
                && (x.Clanarina.Naziv.ToLower().Contains(naziv.ToLower()) ||
                x.Clanarina.Naziv.ToLower().StartsWith(naziv.ToLower()))

                ).Select(x => new ClanarineNeizmireneClanarineClanaPregledVM.Row
                {
                    id = x.Id,
                    naziv = _db.Clanarines.Where(s => s.Id == x.ClanarinaId).First().Naziv,
                    datumOd = _db.Clanarines.Where(s => s.Id == x.ClanarinaId).First().DatumOd,
                    datumDo = _db.Clanarines.Where(s => s.Id == x.ClanarinaId).First().DatumDo
                }).ToList()
            };
            return model;
        }
        [HttpGet]
        public ActionResult<ClanarinePregledVM> PregledSvihClanarina()
        {
            ClanarinePregledVM model = new ClanarinePregledVM {
                rows = _db.Clanarines.Where(x => x.isDeleted == false).Select(x => new ClanarinePregledVM.Row() {
                    id=x.Id,
                    naziv=x.Naziv,
                    datumOd=x.DatumOd,
                    datumDo=x.DatumDo

                }).ToList()

            };
            return model;
        }
        [HttpGet("{naziv}")]
        public ActionResult<ClanarinePregledVM> PretragaSvihClanarinaByNazivClanarine(string naziv)
        {
            ClanarinePregledVM model = new ClanarinePregledVM
            {
                rows = _db.Clanarines.Where(x => x.isDeleted == false && (x.Naziv.ToLower().Contains(naziv.ToLower()) ||
                x.Naziv.ToLower().StartsWith(naziv.ToLower())


                )).Select(x => new ClanarinePregledVM.Row()
                {
                    id = x.Id,
                    naziv = x.Naziv,
                    datumOd = x.DatumOd,
                    datumDo = x.DatumDo

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
        public int Add([FromBody] ClanarineAddVM input)
        {
            _db.Clanarines.Add(new Clanarine
            {
                isDeleted=false,
                Naziv=input.naziv,
                DatumOd=KonvertujUDatum_dd_mm_yyyy(input.datumOd),
                DatumDo=KonvertujUDatum_dd_mm_yyyy(input.datumDo)
             
            });
            _db.SaveChanges();
            int clanarinaId = _db.Clanarines.ToList().OrderByDescending(x => x.Id).FirstOrDefault().Id;

            List<ClanoviKluba> clanovi = _db.ClanoviKlubas.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true).ToList();
            for (int i = 0; i < clanovi.Count(); i++)
            {
                StavkeClanarine stavka = new StavkeClanarine();

                stavka.isDeleted = false;
                stavka.isIzmirenaClanarina = false;
                stavka.ClanKlubaId = clanovi[i].Id;
                stavka.ClanarinaId = clanarinaId;
                stavka.DatumUplate = null;
                _db.StavkeClanarines.Add(stavka);
                _db.SaveChanges();
            }
            return 0;
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
                    slika = s.Slika


                }).SingleOrDefault();

            return model;
        }
        [HttpGet("{clanarinaId}")]
        public ActionResult<IzmireneClanarinePregledVM> IzmireneClanarinePregled(int clanarinaId)
        {

            IzmireneClanarinePregledVM model = new IzmireneClanarinePregledVM
            {
                rows = _db.StavkeClanarines.Where(x => x.isIzmirenaClanarina == true && x.isDeleted == false && x.ClanarinaId == clanarinaId).Select(x => new IzmireneClanarinePregledVM.Row
                {
                    clanarinaId=x.ClanarinaId,
                    clanKlubaId=x.ClanKlubaId,
                    brojPriznanice = x.BrojPriznanice,
                    iznosBrojevima = x.IznosKMBrojevima.ToString(),
                    mjestoUplate=x.MjestoUplate,
                    datumUplate=x.DatumUplate
                    
    }).ToList()
            };
            for (int i = 0; i < model.rows.Count(); i++)
            {

                int clanarinaID = model.rows[i].clanarinaId;
                int clanKlubaId = model.rows[i].clanKlubaId;
                int osobaId = _db.ClanoviKlubas.Where(s => s.Id == clanKlubaId && s.isDeleted == false).First().OsobaId;
               
                OsobaPregledVM osoba = GetOsobu(osobaId);
                model.rows[i].clanKluba = osoba.ime + " (" + osoba.imeRoditelja + ") " + osoba.prezime;

                Clanarine clanarina = _db.Clanarines.Where(x => x.isDeleted == false && x.Id == clanarinaID).FirstOrDefault();
                model.rows[i].datumOd =clanarina.DatumOd;
                model.rows[i].datumDo = clanarina.DatumDo;


            }

            return model;
        }
        [HttpGet("{clanarinaId}")]
        public ActionResult<NeizmireneClanarinePregledVM> NeizmireneClanarinePregled(int clanarinaId)
        {

            NeizmireneClanarinePregledVM model = new NeizmireneClanarinePregledVM
            {
                rows = _db.StavkeClanarines.Where(x => x.isIzmirenaClanarina == false && x.isDeleted == false && x.ClanarinaId == clanarinaId).Select(x => new NeizmireneClanarinePregledVM.Row
                {
                    stavkaId=x.Id,
                    clanarinaId=x.ClanarinaId,
                    clanKlubaId=x.ClanKlubaId

                }).ToList()
            };
            for (int i = 0; i < model.rows.Count(); i++)
            {

                int clanarinaID = model.rows[i].clanarinaId;
                int clanKlubaId = model.rows[i].clanKlubaId;
                int osobaId = _db.ClanoviKlubas.Where(s => s.Id == clanKlubaId && s.isDeleted == false).First().OsobaId;

                OsobaPregledVM osoba = GetOsobu(osobaId);
                model.rows[i].clanKluba = osoba.ime + " (" + osoba.imeRoditelja + ") " + osoba.prezime;

                Clanarine clanarina = _db.Clanarines.Where(x => x.isDeleted == false && x.Id == clanarinaID).FirstOrDefault();
                model.rows[i].datumOd = clanarina.DatumOd;
                model.rows[i].datumDo = clanarina.DatumDo;
                
            }

            return model;
        }
        [HttpGet("{clanarinaId}/{imePrezime}")]
        public ActionResult<IzmireneClanarinePregledVM> IzmireneClanarinePretraga(int clanarinaId,string imePrezime)
        {

            IzmireneClanarinePregledVM model = new IzmireneClanarinePregledVM
            {
                rows = _db.StavkeClanarines.Where(x => x.isIzmirenaClanarina == true && x.isDeleted == false && x.ClanarinaId == clanarinaId
                && (((x.ClanKluba.Osoba.Ime.ToLower() + " " + x.ClanKluba.Osoba.Prezime.ToLower()).StartsWith(imePrezime.ToLower()) || (x.ClanKluba.Osoba.Prezime.ToLower() + " " + x.ClanKluba.Osoba.Ime.ToLower()).StartsWith(imePrezime.ToLower()))
                )).Select(x => new IzmireneClanarinePregledVM.Row
                {
                    clanarinaId = x.ClanarinaId,
                    clanKlubaId = x.ClanKlubaId,
                    brojPriznanice = x.BrojPriznanice,
                    iznosBrojevima = x.IznosKMBrojevima.ToString(),
                    mjestoUplate = x.MjestoUplate,
                    datumUplate = x.DatumUplate

                }).ToList()
            };
            for (int i = 0; i < model.rows.Count(); i++)
            {

                int clanarinaID = model.rows[i].clanarinaId;
                int clanKlubaId = model.rows[i].clanKlubaId;
                int osobaId = _db.ClanoviKlubas.Where(s => s.Id == clanKlubaId && s.isDeleted == false).First().OsobaId;

                OsobaPregledVM osoba = GetOsobu(osobaId);
                model.rows[i].clanKluba = osoba.ime + " (" + osoba.imeRoditelja + ") " + osoba.prezime;

                Clanarine clanarina = _db.Clanarines.Where(x => x.isDeleted == false && x.Id == clanarinaID).FirstOrDefault();
                model.rows[i].datumOd = clanarina.DatumOd;
                model.rows[i].datumDo = clanarina.DatumDo;


            }

            return model;
        }
        [HttpGet("{clanarinaId}/{imePrezime}")]
        public ActionResult<NeizmireneClanarinePregledVM> NeizmireneClanarinePretraga(int clanarinaId,string imePrezime)
        {

            NeizmireneClanarinePregledVM model = new NeizmireneClanarinePregledVM
            {
                rows = _db.StavkeClanarines.Where(x => x.isIzmirenaClanarina == false && x.isDeleted == false && x.ClanarinaId == clanarinaId
                  && (((x.ClanKluba.Osoba.Ime.ToLower() + " " + x.ClanKluba.Osoba.Prezime.ToLower()).StartsWith(imePrezime.ToLower()) || (x.ClanKluba.Osoba.Prezime.ToLower() + " " + x.ClanKluba.Osoba.Ime.ToLower()).StartsWith(imePrezime.ToLower()))
                )).Select(x => new NeizmireneClanarinePregledVM.Row
                {
                    stavkaId = x.Id,
                    clanarinaId = x.ClanarinaId,
                    clanKlubaId = x.ClanKlubaId

                }).ToList()
            };
            for (int i = 0; i < model.rows.Count(); i++)
            {

                int clanarinaID = model.rows[i].clanarinaId;
                int clanKlubaId = model.rows[i].clanKlubaId;
                int osobaId = _db.ClanoviKlubas.Where(s => s.Id == clanKlubaId && s.isDeleted == false).First().OsobaId;

                OsobaPregledVM osoba = GetOsobu(osobaId);
                model.rows[i].clanKluba = osoba.ime + " (" + osoba.imeRoditelja + ") " + osoba.prezime;

                Clanarine clanarina = _db.Clanarines.Where(x => x.isDeleted == false && x.Id == clanarinaID).FirstOrDefault();
                model.rows[i].datumOd = clanarina.DatumOd;
                model.rows[i].datumDo = clanarina.DatumDo;

            }

            return model;
        }
        [HttpDelete("{clanarinaId}")]
        public ActionResult Remove(int clanarinaId)
        {
            Clanarine clanarina = _db.Clanarines.Where(x => x.Id == clanarinaId).FirstOrDefault();
            if (clanarina != null)
                clanarina.isDeleted = true;
            List<StavkeClanarine> stavke = _db.StavkeClanarines.Where(x => x.ClanarinaId == clanarina.Id).ToList();
            for (int i = 0; i < stavke.Count(); i++)
            {
                stavke[i].isDeleted = true;

            }
            _db.SaveChanges();
            return Ok();
        }
    }
}