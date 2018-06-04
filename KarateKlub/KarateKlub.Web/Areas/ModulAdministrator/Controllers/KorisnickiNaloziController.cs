using KarateKlub.Data;
using KarateKlub.Data.Models;
using KarateKlub.Web.Areas.ModulAdministrator.Models;
using KarateKlub.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulAdministrator.Controllers
{
    [Autorizacija(true,false,false,false,false)]
    public class KorisnickiNaloziController : Controller
    {


        MojContext ctx = new MojContext();
        // GET: ModulAdministrator/KorisnickiNalozi
        public ActionResult Index(int aktivan = 0, int uloga = 0,int brojTaba=1)
        {
            List<KorisnickiNalozi> nalozi = new List<KorisnickiNalozi>();
            if (uloga == 0)
            {
                if (aktivan == 1)
                {
                    nalozi = ctx.KorisnickiNalozi.Where(x => x.isDeleted == false && x.isAktivanNalog == true && x.KorisnickaUloga.Naziv == "Administrator").ToList();

                }
                else if (aktivan == 2)
                {
                    nalozi = ctx.KorisnickiNalozi.Where(x => x.isDeleted == false && x.isAktivanNalog == false && x.KorisnickaUloga.Naziv == "Administrator").ToList();
                }
                else
                    nalozi = ctx.KorisnickiNalozi.Where(x => x.isDeleted == false && x.KorisnickaUloga.Naziv == "Administrator").ToList();
            }
            else if (uloga == 1)
            {
                if (aktivan == 1)
                {
                    nalozi = ctx.KorisnickiNalozi.Where(x => x.isDeleted == false && x.isAktivanNalog == true && x.KorisnickaUloga.Naziv == "Blagajnik").ToList();

                }
                else if (aktivan == 2)
                {
                    nalozi = ctx.KorisnickiNalozi.Where(x => x.isDeleted == false && x.isAktivanNalog == false && x.KorisnickaUloga.Naziv == "Blagajnik").ToList();
                }
                else
                    nalozi = ctx.KorisnickiNalozi.Where(x => x.isDeleted == false && x.KorisnickaUloga.Naziv == "Blagajnik").ToList();
            }
            else if (uloga == 2)
            {
                if (aktivan == 1)
                {
                    nalozi = ctx.KorisnickiNalozi.Where(x => x.isDeleted == false && x.isAktivanNalog == true && x.KorisnickaUloga.Naziv == "Sekretar").ToList();

                }
                else if (aktivan == 2)
                {
                    nalozi = ctx.KorisnickiNalozi.Where(x => x.isDeleted == false && x.isAktivanNalog == false && x.KorisnickaUloga.Naziv == "Sekretar").ToList();
                }
                else
                    nalozi = ctx.KorisnickiNalozi.Where(x => x.isDeleted == false && x.KorisnickaUloga.Naziv == "Sekretar").ToList();
            }
            else if (uloga == 3)
            {
                if (aktivan == 1)
                {
                    nalozi = ctx.KorisnickiNalozi.Where(x => x.isDeleted == false && x.isAktivanNalog == true && x.KorisnickaUloga.Naziv == "Trener").ToList();

                }
                else if (aktivan == 2)
                {
                    nalozi = ctx.KorisnickiNalozi.Where(x => x.isDeleted == false && x.isAktivanNalog == false && x.KorisnickaUloga.Naziv == "Trener").ToList();
                }
                else
                    nalozi = ctx.KorisnickiNalozi.Where(x => x.isDeleted == false && x.KorisnickaUloga.Naziv == "Trener").ToList();
            }
            else
            {
                if (aktivan == 1)
                {
                    nalozi = ctx.KorisnickiNalozi.Where(x => x.isDeleted == false && x.isAktivanNalog == true && x.KorisnickaUloga.Naziv == "Član").ToList();

                }
                else if (aktivan == 2)
                {
                    nalozi = ctx.KorisnickiNalozi.Where(x => x.isDeleted == false && x.isAktivanNalog == false && x.KorisnickaUloga.Naziv == "Član").ToList();
                }
                else
                    nalozi = ctx.KorisnickiNalozi.Where(x => x.isDeleted == false && x.KorisnickaUloga.Naziv == "Član").ToList();

            }
            KorisnickiNaloziIndexVM model = new KorisnickiNaloziIndexVM(nalozi, aktivan, uloga);
            ViewData["tab"] = brojTaba;
            return View("Index", model);


        }



        public ActionResult PrikazKorisnickihNaloga(int aktivan = 0, int uloga = 0)
        {
   

            List<KorisnickiNalozi> nalozi = new List<KorisnickiNalozi>();
            if (uloga == 0)
            {
                if (aktivan == 1)
                {
                    nalozi = ctx.KorisnickiNalozi.Where(x => x.isAktivanNalog == true && x.isDeleted == false && x.KorisnickaUloga.Naziv == "Administrator").ToList();
                }
                else if (aktivan == 2)
                {
                    nalozi = ctx.KorisnickiNalozi.Where(x => x.isAktivanNalog == false && x.isDeleted == false && x.KorisnickaUloga.Naziv == "Administrator").ToList();

                }
                else
                    nalozi = ctx.KorisnickiNalozi.Where(x => x.isDeleted == false && x.KorisnickaUloga.Naziv == "Administrator").ToList();

            }
            else if (uloga == 1)
            {


                if (aktivan == 1)
                {
                    nalozi = ctx.KorisnickiNalozi.Where(x => x.isAktivanNalog == true && x.isDeleted == false && x.KorisnickaUloga.Naziv == "Blagajnik").ToList();
                }
                else if (aktivan == 2)
                {
                    nalozi = ctx.KorisnickiNalozi.Where(x => x.isAktivanNalog == false && x.isDeleted == false && x.KorisnickaUloga.Naziv == "Blagajnik").ToList();

                }
                else
                    nalozi = ctx.KorisnickiNalozi.Where(x => x.isDeleted == false && x.KorisnickaUloga.Naziv == "Blagajnik").ToList();


            }
            else if (uloga == 2)
            {
                if (aktivan == 1)
                {
                    nalozi = ctx.KorisnickiNalozi.Where(x => x.isDeleted == false && x.isAktivanNalog == true && x.KorisnickaUloga.Naziv == "Sekretar").ToList();

                }
                else if (aktivan == 2)
                {
                    nalozi = ctx.KorisnickiNalozi.Where(x => x.isDeleted == false && x.isAktivanNalog == false && x.KorisnickaUloga.Naziv == "Sekretar").ToList();
                }
                else
                    nalozi = ctx.KorisnickiNalozi.Where(x => x.isDeleted == false && x.KorisnickaUloga.Naziv == "Sekretar").ToList();
            }
            else if (uloga == 3)
            {
                if (aktivan == 1)
                {
                    nalozi = ctx.KorisnickiNalozi.Where(x => x.isDeleted == false && x.isAktivanNalog == true && x.KorisnickaUloga.Naziv == "Trener").ToList();

                }
                else if (aktivan == 2)
                {
                    nalozi = ctx.KorisnickiNalozi.Where(x => x.isDeleted == false && x.isAktivanNalog == false && x.KorisnickaUloga.Naziv == "Trener").ToList();
                }
                else
                    nalozi = ctx.KorisnickiNalozi.Where(x => x.isDeleted == false && x.KorisnickaUloga.Naziv == "Trener").ToList();
            }
            else
            {
                if (aktivan == 1)
                {
                    nalozi = ctx.KorisnickiNalozi.Where(x => x.isDeleted == false && x.isAktivanNalog == true && x.KorisnickaUloga.Naziv == "Član").ToList();

                }
                else if (aktivan == 2)
                {
                    nalozi = ctx.KorisnickiNalozi.Where(x => x.isDeleted == false && x.isAktivanNalog == false && x.KorisnickaUloga.Naziv == "Član").ToList();
                }
                else
                    nalozi = ctx.KorisnickiNalozi.Where(x => x.isDeleted == false && x.KorisnickaUloga.Naziv == "Član").ToList();

            }
        
            KorisnickiNaloziIndexVM model = new KorisnickiNaloziIndexVM(nalozi, aktivan, uloga);
            ViewData["aktivan"] = aktivan;
            if (uloga == 0)
                return View("PrikazKorisnickihNaloga", model);
            else if (uloga == 1)
                return View("PrikazBlagajnika", model);
            else if (uloga == 2)
                return View("PrikazSekretara", model);
            else if (uloga == 3)
                return View("PrikazTrenera", model);
            else
                return View("PrikazClanovaKluba", model);

        }

        public ActionResult Dodaj(bool IzIzbornika = true,int aktivnost=0)
        {
            KorisnickiNaloziDodajVM model = new KorisnickiNaloziDodajVM
            {
                isAktivnaOsoba = true,
                ZvanjaUKarateu = BindZvanjaUKarateu(),
                FunkcijaTrenera = BindFunkcijeTrenera(),
                KorisnickeUloge = BindKorisnickeUloge(),
                StarosneDobi = BindStarosneDobi(),
                isDeleted = false,
                listaKorisnickihNaloga = BindKorisnickeNaloge(),
                aktivnost=aktivnost
            };
            model.ZvanjaUKarateu.Insert(0, new SelectListItem { Value = null, Text = "-Odaberi zvanje-" });
            model.FunkcijaTrenera.Insert(0, new SelectListItem { Value = null, Text = "-Odaberi funkciju-" });
            model.StarosneDobi.Insert(0, new SelectListItem { Value = null, Text = "-Odaberi starosnu dob-" });
            model.KorisnickeUloge.Insert(0, new SelectListItem { Value = null, Text = "-Odaberi ulogu-" });
           

            ViewData["opcija"] = IzIzbornika;

            if (IzIzbornika == false)
                return View("Dodaj2", model);
            else
                return View("Dodaj", model);
        }

        private List<string> BindKorisnickeNaloge()
        {
            return ctx.KorisnickiNalozi.Select(x=>x.KorisnickoIme).ToList();
        }

        private List<SelectListItem> BindStarosneDobi()
        {
            return ctx.StarosneDobi.Where(x=>x.isDeleted==false).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv }).ToList();
        }

        private List<SelectListItem> BindKorisnickeUloge()
        {
            return ctx.KorisnickeUloge.Where(x=>x.isDeleted==false).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv }).ToList();
        }

        private List<SelectListItem> BindFunkcijeTrenera()
        {
            return ctx.FunkcijeTrenera.Where(x=>x.isDeleted==false).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv }).ToList();
        }

        private List<SelectListItem> BindZvanjaUKarateu()
        {
            return ctx.ZvanjaUKarateu.Where(x=>x.isDeleted==false).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv }).ToList();
        }

        [HttpPost]
        public JsonResult CheckUsername(string username)
        {
            bool isValid = true;
            List<KorisnickiNalozi> listaNaloga = ctx.KorisnickiNalozi.ToList();
            for (int i = 0; i < listaNaloga.Count(); i++)
            {
                if (listaNaloga[i].KorisnickoIme == username)
                {
                    isValid = false;

                    return Json(isValid);
                }
            }

            return Json(isValid);
        }
        [HttpPost]
        public ActionResult Spremi(KorisnickiNaloziDodajVM model)
                {

            string uloga = ctx.KorisnickeUloge.Where(x => x.Id == model.KorisnickaUlogaId).FirstOrDefault().Naziv;
            Osoba osoba = new Osoba();
            
            osoba.isAktivnaOsoba = model.isAktivnaOsoba;
            osoba.isDeleted = model.isDeleted;
            osoba.Ime = model.Ime;
            osoba.Prezime = model.Prezime;
            osoba.ImeRoditelja = model.ImeRoditelja;
            osoba.JMBG = model.JMBG;
            osoba.DatumRodjenja =KonvertujUDatum_dd_mm_yyyy(model.DatumRodjenja);
            osoba.MjestoRodjenja = model.MjestoRodjenja;
            osoba.Spol = model.Spol;
            osoba.KontaktTelefon = model.KontaktTelefon;
            osoba.Email = model.Email;

            
            if(uloga=="Administrator")
            {
                osoba.isAdministrator = true;
                osoba.isBlagajnik = false;
                osoba.isSekretar = false;
                osoba.isClanKluba = false;
                osoba.isTrener = false;
                osoba.isClanUpravnogOdbora = false;
                osoba.isKnjigovodja = false;
               
            }
            
            else if(uloga=="Blagajnik")
            {
                osoba.isAdministrator = false;
                osoba.isBlagajnik = true;
                osoba.isSekretar = false;
                osoba.isClanKluba = false;
                osoba.isTrener = false;
                osoba.isClanUpravnogOdbora = false;
                osoba.isKnjigovodja = false;
            }
          
            else if(uloga=="Trener")
            {
                osoba.isAdministrator = false;
                osoba.isBlagajnik = false;
                osoba.isSekretar = false;
                osoba.isClanKluba = false;
                osoba.isTrener = true;
                osoba.isClanUpravnogOdbora = false;
                osoba.isKnjigovodja = false;
            }
            
            else if(uloga=="Sekretar")
            {
                osoba.isAdministrator = false;
                osoba.isBlagajnik = false;
                osoba.isSekretar = true;
                osoba.isClanKluba = false;
                osoba.isTrener = false;
                osoba.isClanUpravnogOdbora = false;
                osoba.isKnjigovodja = false;
            }
            else
            {
                osoba.isAdministrator = false;
                osoba.isBlagajnik = false;
                osoba.isSekretar = false;
                osoba.isClanKluba = true;
                osoba.isTrener = false;
                osoba.isClanUpravnogOdbora = false;
                osoba.isKnjigovodja = false;
            }
            if (model.s ==null)
            {
                osoba.NazivSlike = null;
                osoba.TipSlike = null;
                osoba.Slika = null;
               

            }
            else
            {
                osoba.NazivSlike = model.s.FileName;
                osoba.TipSlike = model.s.ContentType;

                byte[] slika = new byte[model.s.ContentLength];
                model.s.InputStream.Read(slika, 0, model.s.ContentLength);
                osoba.Slika = slika;
            }

            ctx.Osoba.Add(osoba);
            ctx.SaveChanges();
            int OsobaId = ctx.Osoba.ToList().OrderByDescending(x=>x.Id).FirstOrDefault().Id;

            KorisnickiNalozi nalog = new KorisnickiNalozi();
            nalog.isDeleted = false;
            nalog.isAktivanNalog = true;
            nalog.OsobaId = OsobaId;
            nalog.KorisnickaUlogaId = model.KorisnickaUlogaId;
            nalog.KorisnickoIme = model.KorisnickoIme;
            nalog.Lozinka = model.Lozinka;
            ctx.KorisnickiNalozi.Add(nalog);
            ctx.SaveChanges();

            if (uloga == "Administrator")
            {
                Administratori administrator = new Administratori();
                administrator.OsobaId = OsobaId;
                administrator.isDeleted = false;
                ctx.Administratori.Add(administrator);
                ctx.SaveChanges();
            }
            else if (uloga == "Blagajnik")
            {
                Blagajnici blagajnik = new Blagajnici();
                blagajnik.OsobaId = OsobaId;
                blagajnik.isDeleted = false;
                blagajnik.DatumZaposlenja =KonvertujUDatum_dd_mm_yyyy(model.DatumZaposlenja);
                ctx.Blagajnici.Add(blagajnik);
                ctx.SaveChanges();
            }
            else if (uloga=="Trener")
            {
                Treneri trener = new Treneri();
                trener.OsobaId = OsobaId;
                trener.isDeleted = false;
                trener.DatumZaposlenja = KonvertujUDatum_dd_mm_yyyy(model.DatumZaposlenja);
                trener.FunkcijaTreneraId = model.FunkcijaTreneraId;
                trener.ZvanjeUKarateuId = model.ZvanjeUKarateuId;
                ctx.Treneri.Add(trener);
                ctx.SaveChanges();
            }
            else if (uloga=="Sekretar")
            {
                Sekretari sekretar = new Sekretari();
                sekretar.OsobaId = OsobaId;
                sekretar.isDeleted = false;
                sekretar.DatumZaposlenja = KonvertujUDatum_dd_mm_yyyy(model.DatumZaposlenja);
                ctx.Sekretari.Add(sekretar);
                ctx.SaveChanges();
            }
            else
            {
                ClanoviKluba clan = new ClanoviKluba();
                clan.OsobaId = OsobaId;
                clan.isDeleted = false;
                clan.DatumUpisa =KonvertujUDatum_dd_mm_yyyy(model.DatumUpisa);
                clan.StarosnaDobId = model.StarosnaDobId;
                clan.ZvanjeUKarateuId = model.ZvanjeUKarateuId;
                ctx.ClanoviKluba.Add(clan);
                ctx.SaveChanges();

            }
           
            if (osoba.isAdministrator == true)
                return RedirectToAction("Index", new {aktivan=model.aktivnost,uloga=0,brojTaba=1 });

            else if (osoba.isBlagajnik == true)
                return RedirectToAction("Index", new { aktivan = model.aktivnost, uloga = 0, brojTaba = 2 });

            else if (osoba.isSekretar == true)
                return RedirectToAction("Index", new { aktivan = model.aktivnost, uloga = 0, brojTaba = 3 });

            else if (osoba.isTrener == true)
                return RedirectToAction("Index", new { aktivan = model.aktivnost, uloga = 0, brojTaba = 4 });

            else
                return RedirectToAction("Index", new { aktivan = model.aktivnost, uloga = 0, brojTaba = 5 });

        }
        public ActionResult Detalji(int osobaId, int ulogaId, int aktivan)
        {
            ViewData["aktivnost"] = aktivan;
            KorisnickiNalozi nalog = ctx.KorisnickiNalozi.Where(x => x.OsobaId == osobaId && x.KorisnickaUlogaId == ulogaId).FirstOrDefault();
            KorisnickiNaloziDetaljiVM model = new KorisnickiNaloziDetaljiVM
            {

                NalogId = nalog.Id,
                isAktivanNalog = nalog.isAktivanNalog,
                OsobaId = nalog.OsobaId,
                Ime = nalog.Osoba.Ime,
                KorisnickaUlogaId = nalog.KorisnickaUlogaId,
                Prezime = nalog.Osoba.Prezime,
                ImeRoditelja = nalog.Osoba.ImeRoditelja,
                DatumRodjenja = nalog.Osoba.DatumRodjenja,
                MjestoRodjenja = nalog.Osoba.MjestoRodjenja,
                JMBG = nalog.Osoba.JMBG,
                Spol = nalog.Osoba.Spol,
                KontaktTelefon = nalog.Osoba.KontaktTelefon,
                Email = nalog.Osoba.Email,
                Slika = nalog.Osoba.Slika,
                TipSlike = nalog.Osoba.TipSlike,
                NazivSlike = nalog.Osoba.NazivSlike,
                KorisnickaUloga = nalog.KorisnickaUloga.Naziv,
                aktivnost=aktivan
            };
            if (nalog.Osoba.isAdministrator == true)
            {
                ViewData["tab"] = 1;
            }
            else if (nalog.Osoba.isBlagajnik == true)
            {
                ViewData["tab"] = 2;
            }
            else if (nalog.Osoba.isSekretar == true)
            {
                ViewData["tab"] = 3;
            }
            else if (nalog.Osoba.isTrener == true)
            {
                ViewData["tab"] = 4;
            }
            else
            {
                ViewData["tab"] = 5;
            }
            return View("Detalji",model);
        }

        public ActionResult GetImage(int osobaId,int ulogaId)
        {
            KorisnickiNalozi nalog = ctx.KorisnickiNalozi.Where(x => x.OsobaId == osobaId && x.KorisnickaUlogaId==ulogaId).FirstOrDefault();
            return File(nalog.Osoba.Slika, nalog.Osoba.TipSlike);

        }
        [HttpPost]
        public ActionResult DodajSliku(KorisnickiNaloziDetaljiVM model)
        {
            if (model.s == null)
            {
                return RedirectToAction("Detalji", new { osobaId = model.OsobaId, ulogaId = model.KorisnickaUlogaId });

            }
            else
            {
                Osoba osoba = ctx.Osoba.Where(y => y.Id == model.OsobaId).FirstOrDefault();
                osoba.NazivSlike = model.s.FileName;
                osoba.TipSlike = model.s.ContentType;

                byte[] slika = new byte[model.s.ContentLength];
                model.s.InputStream.Read(slika, 0, model.s.ContentLength);
                osoba.Slika = slika;
                ctx.SaveChanges();
            }  
            
            return RedirectToAction("Detalji", new { osobaId = model.OsobaId, ulogaId = model.KorisnickaUlogaId,aktivan=model.aktivnost});
        }


        public ActionResult DeaktivirajNalog(int osobaId,int ulogaId,int aktivan)
        {
            KorisnickiNalozi nalog = ctx.KorisnickiNalozi.Where(x => x.OsobaId == osobaId && x.KorisnickaUlogaId == ulogaId).FirstOrDefault();
            if (nalog != null)
            {
                nalog.isAktivanNalog = false;
                nalog.Osoba.isAktivnaOsoba = false;
                ctx.SaveChanges();
            }
            return RedirectToAction("Detalji", new { osobaId = osobaId, ulogaId = ulogaId,aktivan=aktivan});
        }

        public ActionResult AktivirajNalog(int osobaId, int ulogaId,int aktivan)
        {
            KorisnickiNalozi nalog = ctx.KorisnickiNalozi.Where(x => x.OsobaId == osobaId && x.KorisnickaUlogaId == ulogaId).FirstOrDefault();
            if (nalog != null)
            {
                nalog.isAktivanNalog = true;
                nalog.Osoba.isAktivnaOsoba = true;
                ctx.SaveChanges();
            }
            return RedirectToAction("Detalji", new { osobaId = osobaId, ulogaId = ulogaId,aktivan=aktivan });
        }
        public string KonvertujUString_mm_dd_yyyy(string datum)
        {
            string dan = datum.Substring(0, 2);
            string mjesec = datum.Substring(3, 2);
            string godina = datum.Substring(6, 4);
            string uneseniDatum = dan + "/" + mjesec + "/" + godina;
            return uneseniDatum;
        }

        public DateTime KonvertujUDatum_dd_mm_yyyy(string datum)
        {
            int dan = Convert.ToInt32(datum.Substring(0, 2));
            int mjesec = Convert.ToInt32(datum.Substring(3, 2));
            int godina = Convert.ToInt32(datum.Substring(6, 4));
            DateTime uneseniDatum = new DateTime(godina, mjesec, dan);
            return uneseniDatum;
        }
      
        public ActionResult KorisnickiPodaci(int osobaId,int ulogaId,int aktivan)
        {
            KorisnickiNalozi nalog = ctx.KorisnickiNalozi.Where(x => x.OsobaId == osobaId && x.KorisnickaUlogaId == ulogaId).FirstOrDefault();
            KorisnickiNaloziKorisnickiPodaciVM model = new KorisnickiNaloziKorisnickiPodaciVM {
                OsobaId = nalog.OsobaId,
                NalogId=nalog.Id,
                KorisnickoIme=nalog.KorisnickoIme,
                Lozinka=nalog.Lozinka,
                isAktivanNalog=nalog.isAktivanNalog,
                KorisnickaUlogaId=nalog.KorisnickaUlogaId,
                KorisnickeUloge=BindKorisnickeUloge(),
                aktivnost=aktivan
            };
            model.KorisnickeUloge.Insert(0, new SelectListItem { Value = null, Text = "-Odaberi ulogu-" });

            return View("KorisnickiPodaci", model);
        }
        [HttpPost]
      public ActionResult SpremiKorisnickePodatke(KorisnickiNaloziKorisnickiPodaciVM model)
        {
            KorisnickiNalozi nalog = ctx.KorisnickiNalozi.Where(x => x.OsobaId == model.OsobaId && x.KorisnickaUlogaId == model.KorisnickaUlogaId).FirstOrDefault();
            if (model.NovaLozinka != null)
            {
                nalog.Lozinka = model.NovaLozinka;
                ctx.SaveChanges();
            }
            return RedirectToAction("Detalji", new { osobaId = model.OsobaId, ulogaId = model.KorisnickaUlogaId,aktivan=model.aktivnost });
        }
        public ActionResult KlubskiPodaci(int osobaId,int ulogaId,int aktivan)
        {
            KorisnickiNalozi nalog = ctx.KorisnickiNalozi.Where(x => x.OsobaId == osobaId && x.KorisnickaUlogaId == ulogaId).FirstOrDefault();
            if (nalog.KorisnickaUloga.Naziv == "Blagajnik")
            {
                KorisnickiNaloziKlubskiPodaciVM model = new KorisnickiNaloziKlubskiPodaciVM {
                    DatumZaposlenja =KonvertujUString_mm_dd_yyyy(ctx.Blagajnici.Where(x=>x.OsobaId==nalog.OsobaId).FirstOrDefault().DatumZaposlenja.ToString()),
                    KorisnickaUloga=nalog.KorisnickaUloga.Naziv,
                    isAktivanNalog=nalog.isAktivanNalog,
                    KorisnickaUlogaId=nalog.KorisnickaUlogaId,
                    aktivnost=aktivan
                };

                return View("KlubskiPodaci", model);
            }
            else if (nalog.KorisnickaUloga.Naziv == "Sekretar")
            {
                KorisnickiNaloziKlubskiPodaciVM model = new KorisnickiNaloziKlubskiPodaciVM
                {
                    DatumZaposlenja =KonvertujUString_mm_dd_yyyy(ctx.Sekretari.Where(x => x.OsobaId == nalog.OsobaId).FirstOrDefault().DatumZaposlenja.ToString()),
                    KorisnickaUloga = nalog.KorisnickaUloga.Naziv,
                    isAktivanNalog = nalog.isAktivanNalog,
                    KorisnickaUlogaId=nalog.KorisnickaUlogaId,
                    aktivnost=aktivan
                };

                return View("KlubskiPodaci", model);
            }
            else if (nalog.KorisnickaUloga.Naziv == "Trener")
            {
                KorisnickiNaloziKlubskiPodaciVM model = new KorisnickiNaloziKlubskiPodaciVM
                {
                    DatumZaposlenja =KonvertujUString_mm_dd_yyyy(ctx.Treneri.Where(x => x.OsobaId == nalog.OsobaId).FirstOrDefault().DatumZaposlenja.ToString()),

                    KorisnickaUloga = nalog.KorisnickaUloga.Naziv,
                    funkcijaTreneraId = ctx.Treneri.Where(x => x.OsobaId == nalog.OsobaId).FirstOrDefault().FunkcijaTreneraId,
                    FunckijeTrenera = BindFunkcijeTrenera(),
                    ZvanjeUKarateuId = ctx.Treneri.Where(x => x.OsobaId == nalog.OsobaId).FirstOrDefault().ZvanjeUKarateuId,
                    ZvanjaUKarateu = BindZvanjaUKarateu(),
                    isAktivanNalog = nalog.isAktivanNalog,
                    KorisnickaUlogaId = nalog.KorisnickaUlogaId,
                    aktivnost=aktivan

                };
                model.ZvanjaUKarateu.Insert(0, new SelectListItem { Value = null, Text = "-Odaberi zvanje-" });
                model.FunckijeTrenera.Insert(0, new SelectListItem { Value = null, Text = "-Odaberi funkciju-" });
               
                return View("KlubskiPodaci", model);
            }
            else if (nalog.KorisnickaUloga.Naziv == "Član")
            {
                KorisnickiNaloziKlubskiPodaciVM model = new KorisnickiNaloziKlubskiPodaciVM
                {
                    KorisnickaUloga = nalog.KorisnickaUloga.Naziv,
                    ZvanjeUKarateuId = ctx.ClanoviKluba.Where(x => x.OsobaId == nalog.OsobaId).FirstOrDefault().ZvanjeUKarateuId,
                    ZvanjaUKarateu = BindZvanjaUKarateu(),
                    StarosnaDobId= ctx.ClanoviKluba.Where(x => x.OsobaId == nalog.OsobaId).FirstOrDefault().StarosnaDobId,
                    StarosneDobi =BindStarosneDobi(),
                    DatumUpisa=KonvertujUString_mm_dd_yyyy(ctx.ClanoviKluba.Where(x => x.OsobaId == nalog.OsobaId).FirstOrDefault().DatumUpisa.ToString()),
                    isAktivanNalog = nalog.isAktivanNalog,
                    KorisnickaUlogaId=nalog.KorisnickaUlogaId,
                    aktivnost=aktivan
                };
                model.ZvanjaUKarateu.Insert(0, new SelectListItem { Value = null, Text = "-Odaberi zvanje-" });
            model.StarosneDobi.Insert(0, new SelectListItem { Value = null, Text = "-Odaberi starosnu dob-" });
                return View("KlubskiPodaci", model);
            }
            return View();
           
        }

        [HttpPost]
        public ActionResult SpremiKlubskePodatke(KorisnickiNaloziKlubskiPodaciVM model)
        {
            KorisnickiNalozi nalog = ctx.KorisnickiNalozi.Where(x => x.OsobaId == model.OsobaId && x.KorisnickaUlogaId == model.KorisnickaUlogaId).FirstOrDefault();
            if (model.KorisnickaUloga == "Blagajnik")
            {
                ctx.Blagajnici.Where(x => x.OsobaId == nalog.OsobaId).FirstOrDefault().DatumZaposlenja =KonvertujUDatum_dd_mm_yyyy(model.DatumZaposlenja);

                ctx.SaveChanges();
                return RedirectToAction("Detalji", new { osobaId = model.OsobaId, ulogaId = model.KorisnickaUlogaId,aktivan=model.aktivnost });

            }
            else if (model.KorisnickaUloga == "Sekretar")
            {
                ctx.Sekretari.Where(x => x.OsobaId == nalog.OsobaId).FirstOrDefault().DatumZaposlenja =KonvertujUDatum_dd_mm_yyyy(model.DatumZaposlenja);

                ctx.SaveChanges();
                return RedirectToAction("Detalji", new { osobaId = model.OsobaId, ulogaId = model.KorisnickaUlogaId,aktivan=model.aktivnost });
            }
            else if (model.KorisnickaUloga == "Trener")
            {
                ctx.Treneri.Where(x => x.OsobaId == nalog.OsobaId).FirstOrDefault().DatumZaposlenja=KonvertujUDatum_dd_mm_yyyy(model.DatumZaposlenja);
                ctx.Treneri.Where(x => x.OsobaId == nalog.OsobaId).FirstOrDefault().FunkcijaTreneraId = model.funkcijaTreneraId;
                ctx.Treneri.Where(x => x.OsobaId == nalog.OsobaId).FirstOrDefault().ZvanjeUKarateuId = model.ZvanjeUKarateuId;

                ctx.SaveChanges();
                return RedirectToAction("Detalji", new { osobaId = model.OsobaId, ulogaId = model.KorisnickaUlogaId,aktivan=model.aktivnost });
            }
            else if (model.KorisnickaUloga == "Član")
            {
                ctx.ClanoviKluba.Where(x => x.OsobaId == nalog.OsobaId).FirstOrDefault().DatumUpisa = KonvertujUDatum_dd_mm_yyyy(model.DatumUpisa);
                ctx.ClanoviKluba.Where(x => x.OsobaId == nalog.OsobaId).FirstOrDefault().StarosnaDobId = model.StarosnaDobId;
                ctx.ClanoviKluba.Where(x => x.OsobaId == nalog.OsobaId).FirstOrDefault().ZvanjeUKarateuId = model.ZvanjeUKarateuId;

                ctx.SaveChanges();
                return RedirectToAction("Detalji", new { osobaId = model.OsobaId, ulogaId = model.KorisnickaUlogaId,aktivan=model.aktivnost});
            }
            else
            return RedirectToAction("Detalji", new { osobaId = model.OsobaId, ulogaId = model.KorisnickaUlogaId, aktivan = model.aktivnost });
        }

        public ActionResult LicniPodaci(int osobaId,int ulogaId,int aktivan)
        {
          
            KorisnickiNalozi nalog = ctx.KorisnickiNalozi.Where(x => x.OsobaId == osobaId && x.KorisnickaUlogaId == ulogaId).FirstOrDefault();

            KorisnickNaloziLicniPodaciVM model = new KorisnickNaloziLicniPodaciVM {
                NalogId = nalog.Id,
                OsobaId = nalog.OsobaId,
                KorisnickaUlogaId = nalog.KorisnickaUlogaId,
                isAktivanNalog = nalog.isAktivanNalog,
                Ime = ctx.Osoba.Where(x => x.Id == nalog.OsobaId).FirstOrDefault().Ime,
                Prezime = ctx.Osoba.Where(x => x.Id == nalog.OsobaId).FirstOrDefault().Prezime,
                ImeRoditelja = ctx.Osoba.Where(x => x.Id == nalog.OsobaId).FirstOrDefault().ImeRoditelja,
                JMBG = ctx.Osoba.Where(x => x.Id == nalog.OsobaId).FirstOrDefault().JMBG,
                Spol = ctx.Osoba.Where(x => x.Id == nalog.OsobaId).FirstOrDefault().Spol,
                DatumRodjenja = KonvertujUString_mm_dd_yyyy(ctx.Osoba.Where(x => x.Id == nalog.OsobaId).FirstOrDefault().DatumRodjenja.ToString()),
                MjestoRodjenja = ctx.Osoba.Where(x => x.Id == nalog.OsobaId).FirstOrDefault().MjestoRodjenja,
                KontaktTelefon = ctx.Osoba.Where(x => x.Id == nalog.OsobaId).FirstOrDefault().KontaktTelefon,
                Email = ctx.Osoba.Where(x => x.Id == nalog.OsobaId).FirstOrDefault().Email,
                KorisnickaUloga = nalog.KorisnickaUloga.Naziv,
                aktivnost = aktivan

            };

            return View("LicniPodaci", model);
        }
        [HttpPost]
        public ActionResult SpremiLicnePodatke(KorisnickNaloziLicniPodaciVM model)
        {
            KorisnickiNalozi nalog = ctx.KorisnickiNalozi.Where(x => x.OsobaId == model.OsobaId && x.KorisnickaUlogaId == model.KorisnickaUlogaId).FirstOrDefault();
            ctx.Osoba.Where(x => x.Id == nalog.OsobaId).FirstOrDefault().Ime=model.Ime;
            ctx.Osoba.Where(x => x.Id == nalog.OsobaId).FirstOrDefault().Prezime = model.Prezime;
            ctx.Osoba.Where(x => x.Id == nalog.OsobaId).FirstOrDefault().ImeRoditelja = model.ImeRoditelja;
            ctx.Osoba.Where(x => x.Id == nalog.OsobaId).FirstOrDefault().DatumRodjenja =KonvertujUDatum_dd_mm_yyyy(model.DatumRodjenja);
            ctx.Osoba.Where(x => x.Id == nalog.OsobaId).FirstOrDefault().MjestoRodjenja = model.MjestoRodjenja;
            ctx.Osoba.Where(x => x.Id == nalog.OsobaId).FirstOrDefault().KontaktTelefon = model.KontaktTelefon;
            ctx.Osoba.Where(x => x.Id == nalog.OsobaId).FirstOrDefault().JMBG = model.JMBG;
            ctx.Osoba.Where(x => x.Id == nalog.OsobaId).FirstOrDefault().Email = model.Email;
            ctx.Osoba.Where(x => x.Id == nalog.OsobaId).FirstOrDefault().Spol = model.Spol;

            ctx.SaveChanges();

            return RedirectToAction("Detalji", new { osobaId = model.OsobaId, ulogaId = model.KorisnickaUlogaId,aktivan=model.aktivnost});
        }

        public ActionResult PrikazNalogaNaCekanju()
        {
            List<Osoba> osobe = new List<Osoba>();
            List<Osoba> listaOsoba = new List<Osoba>();
            List<KorisnickiNalozi> listaNaloga = new List<KorisnickiNalozi>();
            listaNaloga = ctx.KorisnickiNalozi.Where(x => x.isDeleted == false).ToList();
            List<int> listaOsobaIdNalozi = new List<int>();
            for (int i = 0; i < listaNaloga.Count; i++)
            {
                listaOsobaIdNalozi.Add(listaNaloga[i].OsobaId);
            }
          
                osobe = ctx.Osoba.Where(x => x.isDeleted == false && x.isAktivnaOsoba == true && (x.isTrener == true || x.isBlagajnik==true || x.isSekretar==true || x.isClanKluba==true)).ToList();
           
            
         
            for (int i = 0; i < osobe.Count(); i++)
            {
                if (!listaOsobaIdNalozi.Contains(osobe[i].Id)) {
                    listaOsoba.Add(osobe[i]);

                }
            }
          
            KorisnickiNaloziPrikazNalogaNaCekanjuVM model = new KorisnickiNaloziPrikazNalogaNaCekanjuVM(listaOsoba);
            return View("PrikazNalogaNaCekanju",model); 
        }

        public ActionResult DodajKorisnickiNalog(int osobaId)
        {
            KorisnickiNaloziDodajKorisnickiNalogVM model = new KorisnickiNaloziDodajKorisnickiNalogVM {
                OsobaId=osobaId
            };
            Osoba osoba = ctx.Osoba.Where(x => x.Id == model.OsobaId).FirstOrDefault();
            string uloga = "";
            if (osoba.isTrener == true)
                uloga = "Trener";
            else if (osoba.isBlagajnik == true)
                uloga = "Blagajnik";
            else if (osoba.isSekretar == true)
                uloga = "Sekretar";
            else
                uloga = "Član";
            model.OsobaImePrezime = osoba.Ime + " (" + osoba.ImeRoditelja + ") " + osoba.Prezime+" - "+uloga;
            return View("DodajKorisnickiNalog", model);
        }

        public ActionResult SpremiNoviKorisnickiNalog(KorisnickiNaloziDodajKorisnickiNalogVM model)
        {
            KorisnickiNalozi nalog = new KorisnickiNalozi();
            Osoba osoba = ctx.Osoba.Where(x => x.Id == model.OsobaId).FirstOrDefault();
          
            nalog.isAktivanNalog = true;
            nalog.isDeleted = false;
            nalog.OsobaId = model.OsobaId;
            if (osoba.isTrener == true)
                nalog.KorisnickaUlogaId = ctx.KorisnickeUloge.Where(x => x.Naziv == "Trener").FirstOrDefault().Id;
            else if(osoba.isBlagajnik==true)
                nalog.KorisnickaUlogaId = ctx.KorisnickeUloge.Where(x => x.Naziv == "Blagajnik").FirstOrDefault().Id;
            else if (osoba.isSekretar == true)
                nalog.KorisnickaUlogaId = ctx.KorisnickeUloge.Where(x => x.Naziv == "Sekretar").FirstOrDefault().Id;
            else
                nalog.KorisnickaUlogaId = ctx.KorisnickeUloge.Where(x => x.Naziv == "Član").FirstOrDefault().Id;

            nalog.Lozinka = model.Lozinka;
            nalog.KorisnickoIme = model.KorisnickoIme;
            ctx.KorisnickiNalozi.Add(nalog);
            ctx.SaveChanges();
            return RedirectToAction("PrikazNalogaNaCekanju");
        }
    }
}

