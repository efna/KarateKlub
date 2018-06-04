using KarateKlub.Data;
using KarateKlub.Data.Models;
using KarateKlub.Web.Areas.ModulSekretar.Models;
using KarateKlub.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulSekretar.Controllers
{
    [Autorizacija(false, true, false, false, false)]

    public class TakmicariController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulSekretar/Takmicari
        public ActionResult Index(int aktivnost)
        {
            List<Takmicari> takmicari = new List<Takmicari>();
            if (aktivnost == 0)
            {
                takmicari = ctx.Takmicari.Where(x => x.isDeleted == false && x.isAktivanTakmicar == true).ToList();
            }
            else if(aktivnost==1)
            {
                takmicari = ctx.Takmicari.Where(x => x.isDeleted == false && x.isAktivanTakmicar == false).ToList();

            }
            else
            {
                takmicari = ctx.Takmicari.Where(x => x.isDeleted == false).ToList();

            }
            TakmicariIndexVM model = new TakmicariIndexVM(takmicari, aktivnost);
            ViewData["aktivnost"] = aktivnost;
            return View(model);
        }
        public ActionResult GoToUpravljanjeTakmicenjimaTakmicarima(int aktivnost) {
            return RedirectToAction("Index", "UpravljanjeTakmicenjimaTakmicarima", new { brojTaba = 2, aktivnost = aktivnost });

        }
        public ActionResult Dodaj(int aktivnost)
        {
            TakmicariDodajVM model = new TakmicariDodajVM
            {
                aktivnost = aktivnost,
                clanoviKluba=BindClanoveKluba()
            };
            model.clanoviKluba.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite člana kluba-" });
            List<Takmicari> takmicari = ctx.Takmicari.Where(x=>x.isDeleted == false).ToList();
            List<string> listaId = new List<string>();
            for (int i = 0; i < model.clanoviKluba.Count(); i++)
            {
                for (int j = 0; j < takmicari.Count(); j++)
                {

                    if (takmicari[j].ClanKlubaId.ToString() == model.clanoviKluba[i].Value && takmicari[j].isDeleted == false)
                    {
                        string value = takmicari[j].ClanKlubaId.ToString();
                        listaId.Add(value);

                    }
                }
            }
            for (int i = 0; i < listaId.Count(); i++)
            {
                var item = model.clanoviKluba.First(x => x.Value == listaId[i]);
                model.clanoviKluba.Remove(item);
            }
            return View("Dodaj",model);
        }

        private List<SelectListItem> BindClanoveKluba()
        {
            return ctx.ClanoviKluba.Where(x => x.isDeleted == false).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Osoba.Ime+" ("+x.Osoba.ImeRoditelja+") "+x.Osoba.Prezime}).ToList();
             
        }
        public ActionResult PodaciTakmicara(int takmicarId,int aktivnost)
        {
            Takmicari takmicar = ctx.Takmicari.Where(x => x.Id == takmicarId).FirstOrDefault();
            TakmicariPodaciTakmicaraVM model = new TakmicariPodaciTakmicaraVM
            {
                Id=takmicar.Id,
                ClanId =takmicar.ClanKlubaId,
                isAktivan = takmicar.isAktivanTakmicar,
                OsobaId = takmicar.ClanKluba.OsobaId,
                Ime = takmicar.ClanKluba.Osoba.Ime,
                Prezime = takmicar.ClanKluba.Osoba.Prezime,
                ImeRoditelja = takmicar.ClanKluba.Osoba.ImeRoditelja,
                DatumRodjenja = takmicar.ClanKluba.Osoba.DatumRodjenja,
                MjestoRodjenja = takmicar.ClanKluba.Osoba.MjestoRodjenja,
                JMBG = takmicar.ClanKluba.Osoba.JMBG,
                Spol = takmicar.ClanKluba.Osoba.Spol,
                KontaktTelefon = takmicar.ClanKluba.Osoba.KontaktTelefon,
                Email = takmicar.ClanKluba.Osoba.Email,
                Slika = takmicar.ClanKluba.Osoba.Slika,
                TipSlike = takmicar.ClanKluba.Osoba.TipSlike,
                NazivSlike = takmicar.ClanKluba.Osoba.NazivSlike,
                aktivnost = aktivnost

            };
            ViewData["aktivnost"] = aktivnost;
            return View("PodaciTakmicara", model);
        }

        public ActionResult GetImage(int takmicarId)
        {
            Takmicari takmicar = ctx.Takmicari.Where(x => x.Id == takmicarId).FirstOrDefault();
            return File(takmicar.ClanKluba.Osoba.Slika, takmicar.ClanKluba.Osoba.TipSlike);

        }
        [HttpPost]
        public ActionResult DodajSliku(TakmicariPodaciTakmicaraVM model)
        {
            if (model.s == null)
            {
                return RedirectToAction("Index", "UpravljanjeTakmicarskomKnjizicom", new { takmicarId = model.Id, aktivnost = model.aktivnost,brojTaba=1});

            }
            else
            {
                Takmicari takmicar = ctx.Takmicari.Where(y => y.Id == model.Id).FirstOrDefault();
                takmicar.ClanKluba.Osoba.NazivSlike = model.s.FileName;
                takmicar.ClanKluba.Osoba.TipSlike = model.s.ContentType;

                byte[] slika = new byte[model.s.ContentLength];
                model.s.InputStream.Read(slika, 0, model.s.ContentLength);
                takmicar.ClanKluba.Osoba.Slika = slika;
                ctx.SaveChanges();
            }

            return RedirectToAction("Index", "UpravljanjeTakmicarskomKnjizicom", new { takmicarId = model.Id, aktivnost = model.aktivnost, brojTaba = 1 });

        }
        public ActionResult SpremiNovogTakmicara(TakmicariDodajVM model)
        {
            Takmicari takmicar = new Takmicari();
            takmicar.isAktivanTakmicar = true;
            takmicar.isDeleted = false;
            takmicar.ClanKlubaId = model.ClanKlubaId;
            takmicar.RegistarskiBroj = model.RegistarskiBroj;
            ctx.Takmicari.Add(takmicar);
            ctx.SaveChanges();


            return RedirectToAction("Index", "UpravljanjeTakmicenjimaTakmicarima", new { brojTaba = 2, aktivnost = model.aktivnost });
        }

        public ActionResult Uredi(int takmicarId,int aktivnost)
        {
            Takmicari takmicar = ctx.Takmicari.Where(x => x.Id == takmicarId).FirstOrDefault();
            TakmicariUrediVM model = new TakmicariUrediVM {
                Id=takmicarId,
                isAktivanTakmicar=takmicar.isAktivanTakmicar,
                isDeleted=takmicar.isDeleted,
                Obrazlozenje=takmicar.Obrazlozenje,
                RegistarskiBroj=takmicar.RegistarskiBroj,
                ClanKlubaId=takmicar.ClanKlubaId
            };
          
            return View("Uredi", model);

        }
        public ActionResult SpremiIzmjenuTakmicara(TakmicariUrediVM model)
        {
            Takmicari takmicar = ctx.Takmicari.Where(x => x.Id == model.Id).FirstOrDefault();

            takmicar.isAktivanTakmicar = model.isAktivanTakmicar;
            takmicar.RegistarskiBroj = model.RegistarskiBroj;
            takmicar.Obrazlozenje = model.Obrazlozenje;
            ctx.SaveChanges();


            return RedirectToAction("Index", "UpravljanjeTakmicenjimaTakmicarima", new { brojTaba = 2, aktivnost = model.aktivnost });
        }
        public JsonResult Obrisi(int takmicarId)
        {

            Takmicari takmicar = ctx.Takmicari.Where(x => x.Id == takmicarId).FirstOrDefault();
            takmicar.isDeleted = true;
            LjekarskiPreglediTakmicara ljekarskiPregledi = ctx.LjekarskiPreglediTakmicara.Where(x => x.TakmicarId==takmicarId).FirstOrDefault();
            if(ljekarskiPregledi!=null)
            ljekarskiPregledi.isDeleted = true;
            ctx.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeaktivirajTakmicara(int takmicarId, int aktivnost)
        {
            Takmicari takmicar = ctx.Takmicari.Where(x => x.Id == takmicarId).FirstOrDefault();
            if (takmicar != null)
            {
                takmicar.isAktivanTakmicar = false;
                ctx.SaveChanges();
            }
            return RedirectToAction("Index", "UpravljanjeTakmicarskomKnjizicom", new { takmicarId =takmicarId, aktivnost = aktivnost, brojTaba = 1 });

        }

        public ActionResult AktivirajTakmicara(int takmicarId, int aktivnost)
        {
            Takmicari takmicar = ctx.Takmicari.Where(x => x.Id == takmicarId).FirstOrDefault();
            if (takmicar != null)
            {
                takmicar.isAktivanTakmicar = true;
                ctx.SaveChanges();
            }
            return RedirectToAction("Index", "UpravljanjeTakmicarskomKnjizicom", new { takmicarId = takmicarId, aktivnost = aktivnost, brojTaba = 1 });
        }
    }
}