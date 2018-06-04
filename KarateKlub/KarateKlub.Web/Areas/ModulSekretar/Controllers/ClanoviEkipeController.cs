using KarateKlub.Data;
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

    public class ClanoviEkipeController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulSekretar/ClanoviEkipe
        public ActionResult Index(int ekipaId)
        {
            ClanoviEkipeIndexVM model = new ClanoviEkipeIndexVM
            {
                clanoviEkipe = ctx.ClanoviEkipe.Where(x => x.isDeleted == false && x.EkipaId == ekipaId).Select(x => new ClanEkipePodaci
                {
                    Id=x.Id,
                    EkipaId=x.EkipaId,
                    TakmicarId=x.TakmicarId,
                    Takmicar=x.Takmicar.ClanKluba.Osoba.Ime+" ("+x.Takmicar.ClanKluba.Osoba.ImeRoditelja+") "+x.Takmicar.ClanKluba.Osoba.Prezime,
                    ZvanjeUKarateu=x.Takmicar.ClanKluba.ZvanjeUKarateu.Naziv,
                    StarosnaDob=x.Takmicar.ClanKluba.StarosnaDob.Naziv
                   

                }).ToList()
            };
      
            return View(model);
        }
    }
}