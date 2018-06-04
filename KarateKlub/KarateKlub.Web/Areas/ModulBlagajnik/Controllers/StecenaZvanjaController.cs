using KarateKlub.Data;
using KarateKlub.Web.Areas.ModulBlagajnik.Models;
using KarateKlub.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulBlagajnik.Controllers
{
    [Autorizacija(false, false, false, false, true)]

    public class StecenaZvanjaController : Controller
    {

        MojContext ctx = new MojContext();
        // GET: ModulBlagajnik/StecenaZvanja
        public ActionResult PregledStecenihZvanjaClana(int osobaId)
        {
            StecenaZvanjaPregledStecenihZvanjaClanaVM model = new StecenaZvanjaPregledStecenihZvanjaClanaVM
            {
                stecenaZvanja = ctx.StecenaZvanja.Where(x => x.isDeleted == false && x.OsobaId == osobaId).Select(x => new StecenoZvanjePodaci
                {
                    ZvanjeUKarateuId = x.ZvanjeUKarateuId,
                    OsobaId = x.OsobaId,
                    ZvanjeUKarateu = x.ZvanjeUKarateu.Naziv,
                    Osoba = x.Osoba.Ime + " (" + x.Osoba.ImeRoditelja + ") " + x.Osoba.Prezime,
                    DatumSticanja = x.DatumSticanja,
                    Mjesto = x.Mjesto,
                    Organizator = x.Organizator
                }).ToList()
            };
            return View("PregledStecenihZvanjaClana", model);
        }
    }
}