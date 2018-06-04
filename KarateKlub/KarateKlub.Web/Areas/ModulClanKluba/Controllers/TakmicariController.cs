using KarateKlub.Data;
using KarateKlub.Data.Models;
using KarateKlub.Web.Areas.ModulClanKluba.Models;
using KarateKlub.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulClanKluba.Controllers
{
    [Autorizacija(false, false, true, false, false)]

    public class TakmicariController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulClanKluba/Takmicari
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UpravljanjeTakmicarskomKnjizicom(int brojTaba = 1)
        {
            int osobaId = Autentifikacija.GetLogiraniKorisnik(HttpContext).OsobaId;
            int takmicarId = ctx.Takmicari.Where(x => x.isDeleted == false && x.ClanKluba.OsobaId == osobaId).FirstOrDefault().Id;
            ViewData["takmicarId"] = takmicarId;
            ViewData["tab"] = brojTaba;

            return View("UpravljanjeTakmicarskomKnjizicom");
        }
        public ActionResult PodaciTakmicara(int takmicarId)
        {
            Takmicari takmicar = ctx.Takmicari.Where(x => x.Id == takmicarId).FirstOrDefault();
            TakmicariPodaciTakmicaraVM model = new TakmicariPodaciTakmicaraVM
            {
                Id = takmicar.Id,
                ClanId = takmicar.ClanKlubaId,
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
                NazivSlike = takmicar.ClanKluba.Osoba.NazivSlike

            };
            return View("PodaciTakmicara", model);
        }

        public ActionResult GetImage(int takmicarId)
        {
            Takmicari takmicar = ctx.Takmicari.Where(x => x.Id == takmicarId).FirstOrDefault();
            return File(takmicar.ClanKluba.Osoba.Slika, takmicar.ClanKluba.Osoba.TipSlike);

        }

    }
}