using KarateKlub.Data;
using KarateKlub.Data.Models;
using KarateKlub.Web.Areas.ModulSekretar.Models;
using KarateKlub.Web.Helper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulSekretar.Controllers
{
    [Autorizacija(false, true, false, false, false)]

    public class ParticipacijeZaPolaganjeUcenickaZvanjaController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulSekretar/ParticipacijeZaPolaganjeUcenickaZvanja
        public ActionResult Index(int polaganjeId)
        {
            List<ParticipacijeZaPolaganjeUcenickaZvanja> participacijeZaPolaganjeUcenickaZvanja = new List<ParticipacijeZaPolaganjeUcenickaZvanja>();
            participacijeZaPolaganjeUcenickaZvanja = ctx.ParticipacijeZaPolaganjeUcenickaZvanja.Where(x => x.isDeleted == false && x.PolaganjeUcenickaZvanjaId == polaganjeId).ToList();
            ParticipacijeZaPolaganjeUcenickaZvanjaIndexVM model = new ParticipacijeZaPolaganjeUcenickaZvanjaIndexVM(participacijeZaPolaganjeUcenickaZvanja);
            ViewData["polaganjeId"] = polaganjeId;
            return View(model);
        }

        public ActionResult IzmireneParticipacije(string DatumOd = "",string DatumDo="")
        {
            decimal ukupanIznosUlazaOstvarenogOdParticipacijaZaPolaganje= 0;

            if (DatumOd != "" && DatumDo != "")
            {
                string danDatumOd = DatumOd.Substring(0, 2);
                string mjesecDatumOd = DatumOd.Substring(3, 2);
                string godinaDatumOd = DatumOd.Substring(6, 4);

                string danDatumDo = DatumDo.Substring(0, 2);
                string mjesecDatumDo = DatumDo.Substring(3, 2);
                string godinaDatumDo = DatumDo.Substring(6, 4);


                string dOd = mjesecDatumOd + "/" + danDatumOd + "/" + godinaDatumOd;
                string dDo = mjesecDatumDo + "/" + danDatumDo + "/" + godinaDatumDo;

                CultureInfo provider = new CultureInfo("en-US");

                DateTime datumOd = DateTime.ParseExact(dOd, "MM/dd/yyyy",
                                  provider);

                DateTime datumDo = DateTime.ParseExact(dDo, "MM/dd/yyyy",
                                  provider);
                List<ParticipacijeZaPolaganjeUcenickaZvanja> participacijeZaPolaganjeUcenickaZvanja = new List<ParticipacijeZaPolaganjeUcenickaZvanja>();
                participacijeZaPolaganjeUcenickaZvanja = ctx.ParticipacijeZaPolaganjeUcenickaZvanja.Where(x => x.isDeleted == false && x.DatumUplate>=datumOd && x.DatumUplate<=datumDo).ToList();
                ParticipacijeZaPolaganjeUcenickaZvanjaIndexVM model = new ParticipacijeZaPolaganjeUcenickaZvanjaIndexVM(participacijeZaPolaganjeUcenickaZvanja);
                for (int i = 0; i < model.participacijeZaPolaganjeUcenickaZvanja.Count(); i++)
                {
                    ukupanIznosUlazaOstvarenogOdParticipacijaZaPolaganje += model.participacijeZaPolaganjeUcenickaZvanja[i].IznosKMBrojevima;
                }
                ViewData["ukupanIznosUlazaOstvarenogOdParticipacijaZaPolaganje"] = ukupanIznosUlazaOstvarenogOdParticipacijaZaPolaganje;
                return View("IzmireneParticipacije", model);
            }
            else
            {
                ViewData["ukupanIznosUlazaOstvarenogOdParticipacijaZaPolaganje"] = ukupanIznosUlazaOstvarenogOdParticipacijaZaPolaganje;

                List<ParticipacijeZaPolaganjeUcenickaZvanja> participacijeZaPolaganjeUcenickaZvanja = new List<ParticipacijeZaPolaganjeUcenickaZvanja>();
                participacijeZaPolaganjeUcenickaZvanja = ctx.ParticipacijeZaPolaganjeUcenickaZvanja.Where(x => x.isDeleted == false).ToList();
                ParticipacijeZaPolaganjeUcenickaZvanjaIndexVM model = new ParticipacijeZaPolaganjeUcenickaZvanjaIndexVM(participacijeZaPolaganjeUcenickaZvanja);

                return View("IzmireneParticipacije", model);
            }
        }
        public ActionResult PregledParticipacijaClana(int osobaId,int aktivan,int izmirena)
        {
            if (izmirena == 0)
            {
                List<ParticipacijeZaPolaganjeUcenickaZvanja> participacijeZaPolaganjeUcenickaZvanja = new List<ParticipacijeZaPolaganjeUcenickaZvanja>();
                participacijeZaPolaganjeUcenickaZvanja = ctx.ParticipacijeZaPolaganjeUcenickaZvanja.Where(x => x.isDeleted == false && x.UcesnikPolaganjaZaUcenickaZvanja.ClanKluba.OsobaId == osobaId).ToList();
                ParticipacijeZaPolaganjeUcenickaZvanjaIndexVM model = new ParticipacijeZaPolaganjeUcenickaZvanjaIndexVM(participacijeZaPolaganjeUcenickaZvanja, osobaId, aktivan, izmirena);
                return View("PregledIzmirenihParticipacijaClana", model);
            }
            else
            {
                List<UcesniciPolaganjaZaUcenickaZvanja> listaUcesnik = ctx.UcesniciPolaganjaZaUcenickaZvanja.Where(x => x.isDeleted == false && x.ClanKluba.OsobaId==osobaId).ToList();
                List<ParticipacijeZaPolaganjeUcenickaZvanja> participacije = ctx.ParticipacijeZaPolaganjeUcenickaZvanja.Where(x => x.isDeleted == false && x.UcesnikPolaganjaZaUcenickaZvanja.ClanKluba.OsobaId == osobaId).ToList();
                List<int> ucesnikId = new List<int>();
                List<int> partUcesnikaId = new List<int>();
                List<UcesniciPolaganjaZaUcenickaZvanja> listaUcesnikNijePlatio = new List<UcesniciPolaganjaZaUcenickaZvanja>();
                for (int i = 0; i < listaUcesnik.Count(); i++)
                {
                    ucesnikId.Add(listaUcesnik[i].Id);
                }
                for (int i = 0; i < participacije.Count(); i++)
                {
                    partUcesnikaId.Add(participacije[i].UcesnikPolaganjaZaUcenickaZvanjaId);
                }

                for (int i = 0; i < ucesnikId.Count(); i++)
                {
                    int idUcesnika = ucesnikId[i];
                    if (!partUcesnikaId.Contains(ucesnikId[i]))
                    {
                        UcesniciPolaganjaZaUcenickaZvanja ucesnik = ctx.UcesniciPolaganjaZaUcenickaZvanja.Where(x => x.Id == idUcesnika).FirstOrDefault();
                        listaUcesnikNijePlatio.Add(ucesnik);

                    }
                }
                ParticipacijeZaPolaganjeUcenickaZvanjaNeizmireneParticipacijeVM model = new ParticipacijeZaPolaganjeUcenickaZvanjaNeizmireneParticipacijeVM(listaUcesnikNijePlatio,osobaId,aktivan,izmirena);

               
                 return View("PregledNeizmirenihParticipacijaClana", model);
            }
        }
        public ActionResult NeizmireneParticipacije(int polaganjeId)
        {
            List<UcesniciPolaganjaZaUcenickaZvanja> ucesnici = ctx.UcesniciPolaganjaZaUcenickaZvanja.Where(x => x.isDeleted == false && x.PolaganjeUcenickaZvanjaId == polaganjeId).ToList();
            List<ParticipacijeZaPolaganjeUcenickaZvanja> participacije = ctx.ParticipacijeZaPolaganjeUcenickaZvanja.Where(x => x.isDeleted == false && x.PolaganjeUcenickaZvanjaId == polaganjeId).ToList();
            List<int> ucesniciId = new List<int>();
            List<int> partUcesniciId = new List<int>();
            List<UcesniciPolaganjaZaUcenickaZvanja> listaUcesnikaKojiNisuPlatili = new List<UcesniciPolaganjaZaUcenickaZvanja>();
            for (int i = 0; i < ucesnici.Count(); i++)
            {
                ucesniciId.Add(ucesnici[i].Id);
            }
            for (int i = 0; i < participacije.Count(); i++)
            {
                partUcesniciId.Add(participacije[i].UcesnikPolaganjaZaUcenickaZvanjaId);
            }

            for (int i = 0; i < ucesniciId.Count(); i++)
            {
                int idUcesnika = ucesniciId[i];
                if (!partUcesniciId.Contains(ucesniciId[i])) {
                    UcesniciPolaganjaZaUcenickaZvanja ucesnik = ctx.UcesniciPolaganjaZaUcenickaZvanja.Where(x => x.Id == idUcesnika).FirstOrDefault();
                    listaUcesnikaKojiNisuPlatili.Add(ucesnik);

                }
            }
            ParticipacijeZaPolaganjeUcenickaZvanjaNeizmireneParticipacijeVM model = new ParticipacijeZaPolaganjeUcenickaZvanjaNeizmireneParticipacijeVM(listaUcesnikaKojiNisuPlatili,polaganjeId);
            ViewData["polaganjeId"] = polaganjeId;

            return View("NeizmireneParticipacijeZaPolaganjeUcenickaZvanja", model);
        }
        public ActionResult UpravljanjeParticipacijamaZaPolaganjeUcenickaZvanja(int polaganjeId, int brojTaba = 1)
        {
            ViewData["tab"] = brojTaba;
            ViewData["polaganjeId"] = polaganjeId;

            return View("UpravljanjeParticipacijamaZaPolaganjeUcenickaZvanja");
        }

        public ActionResult Dodaj(int polaganjeId)
        {
            ParticipacijeZaPolaganjeUcenickaZvanjaDodajVM model = new ParticipacijeZaPolaganjeUcenickaZvanjaDodajVM
            {
                PolaganjeUcenickaZvanjaId = polaganjeId,
                ucesniciPolaganja = BindUcesnikePolaganja(polaganjeId)
            };
            model.ucesniciPolaganja.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite učesnika-" });

            return View("Dodaj", model);
        }
        public ActionResult Dodaj2(int ucesnikId)
        {
            UcesniciPolaganjaZaUcenickaZvanja ucesnik = ctx.UcesniciPolaganjaZaUcenickaZvanja.Where(x => x.Id == ucesnikId).FirstOrDefault();
            ParticipacijeZaPolaganjeUcenickaZvanjaDodajVM model = new ParticipacijeZaPolaganjeUcenickaZvanjaDodajVM
            {
                PolaganjeUcenickaZvanjaId = ucesnik.PolaganjeUcenickaZvanjaId,
                UcesnikPolaganjaZaUcenickaZvanjaId=ucesnikId,
                Ucesnik=ucesnik.ClanKluba.Osoba.Ime+" ("+ucesnik.ClanKluba.Osoba.ImeRoditelja+") "+ucesnik.ClanKluba.Osoba.Prezime+" - "+ucesnik.ClanKluba.ZvanjeUKarateu.Naziv
            };
            ViewData["ucesnik"] = ucesnik;
            return View("Dodaj2", model);
        }
        private List<SelectListItem> BindUcesnikePolaganja(int polaganjeId)
        {
            return ctx.UcesniciPolaganjaZaUcenickaZvanja.Where(x => x.isDeleted == false && x.PolaganjeUcenickaZvanjaId == polaganjeId).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.ClanKluba.Osoba.Ime + " (" + x.ClanKluba.Osoba.ImeRoditelja + ") " + x.ClanKluba.Osoba.Prezime }).ToList();

        }

        public DateTime KonvertujUDatum_dd_mm_yyyy(string datum)
        {

            int dan = Convert.ToInt32(datum.Substring(0, 2));
            int mjesec = Convert.ToInt32(datum.Substring(3, 2));
            int godina = Convert.ToInt32(datum.Substring(6, 4));
            DateTime uneseniDatum = new DateTime(godina, mjesec, dan);
            return uneseniDatum;


        }
        public ActionResult SpremiNovuParticipacijuZaPolaganjeUcenickaZvanja(ParticipacijeZaPolaganjeUcenickaZvanjaDodajVM model)
        {
            ParticipacijeZaPolaganjeUcenickaZvanja participacija = new ParticipacijeZaPolaganjeUcenickaZvanja();
            participacija.isDeleted = false;
            participacija.PolaganjeUcenickaZvanjaId = model.PolaganjeUcenickaZvanjaId;
            participacija.UcesnikPolaganjaZaUcenickaZvanjaId = model.UcesnikPolaganjaZaUcenickaZvanjaId;
            if (model.DatumUplate != null)
                participacija.DatumUplate = KonvertujUDatum_dd_mm_yyyy(model.DatumUplate);
            participacija.IznosKMBrojevima = Convert.ToDecimal(model.IznosKMBrojevima);
            participacija.IznosKMSlovima = model.IznosKMSlovima;
            participacija.BrojPriznanice = model.BrojPriznanice;
            ctx.ParticipacijeZaPolaganjeUcenickaZvanja.Add(participacija);
            ctx.SaveChanges();
            return RedirectToAction("Index", "UpravljanjePolaganjaUcenickaZvanja", new { polaganjeId = model.PolaganjeUcenickaZvanjaId, brojTaba = 3, zvanje = 0, brojTabaParticipacije=2});

        }

        public ActionResult Uredi(int participacijaZaPolaganjeId)
        {
            ParticipacijeZaPolaganjeUcenickaZvanja participacija = ctx.ParticipacijeZaPolaganjeUcenickaZvanja.Where(x => x.Id == participacijaZaPolaganjeId).FirstOrDefault();
            ParticipacijeZaPolaganjeUcenickaZvanjaUrediVM model = new ParticipacijeZaPolaganjeUcenickaZvanjaUrediVM
            {
                Id = participacijaZaPolaganjeId,
                PolaganjeUcenickaZvanjaId = participacija.PolaganjeUcenickaZvanjaId,
                UcesnikPolaganjaZaUcenickaZvanjaId = participacija.UcesnikPolaganjaZaUcenickaZvanjaId,
                DatumUplate = participacija.DatumUplate.ToString("dd.MM.yyyy"),
                BrojPriznanice = participacija.BrojPriznanice,
                IznosKMBrojevima = participacija.IznosKMBrojevima.ToString(),
                IznosKMSlovima = participacija.IznosKMSlovima,
                isDeleted = participacija.isDeleted,
                ucesniciPolaganja = BindUcesnikePolaganja(participacija.PolaganjeUcenickaZvanjaId)

            };
            model.ucesniciPolaganja.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite učesnika-" });

            return View("Uredi", model);
        }
        public ActionResult SpremiIzmjenuParticipacijeZaPolaganjeUcenickaZvanja(ParticipacijeZaPolaganjeUcenickaZvanjaUrediVM model)
        {
            ParticipacijeZaPolaganjeUcenickaZvanja participacija = ctx.ParticipacijeZaPolaganjeUcenickaZvanja.Where(x => x.Id == model.Id).FirstOrDefault();

            participacija.UcesnikPolaganjaZaUcenickaZvanjaId = model.UcesnikPolaganjaZaUcenickaZvanjaId;
            if (model.DatumUplate != null)
                participacija.DatumUplate = KonvertujUDatum_dd_mm_yyyy(model.DatumUplate);
            participacija.IznosKMBrojevima = Convert.ToDecimal(model.IznosKMBrojevima);
            participacija.IznosKMSlovima = model.IznosKMSlovima;
            participacija.BrojPriznanice = model.BrojPriznanice;
            ctx.SaveChanges();
            return RedirectToAction("Index", "UpravljanjePolaganjaUcenickaZvanja", new { polaganjeId = model.PolaganjeUcenickaZvanjaId, brojTaba = 3, zvanje = 0, brojTabaParticipacije=1 });



        }
        public JsonResult Obrisi(int participacijaZaPolaganjeId)
        {

            ParticipacijeZaPolaganjeUcenickaZvanja participacija = ctx.ParticipacijeZaPolaganjeUcenickaZvanja.Where(x => x.Id == participacijaZaPolaganjeId).FirstOrDefault();


            if (participacija != null)
                participacija.isDeleted = true;
            ctx.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}