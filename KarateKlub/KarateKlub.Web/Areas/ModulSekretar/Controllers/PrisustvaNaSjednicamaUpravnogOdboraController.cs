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

    public class PrisustvaNaSjednicamaUpravnogOdboraController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulSekretar/PrisustvaNaSjednicamaUpravnogOdbora
        public ActionResult Index(int sjednicaId)
        {
            PrisustvaNaSjednicamaUpravnogOdboraIndexVM model = new PrisustvaNaSjednicamaUpravnogOdboraIndexVM {
                prisustvaNaSjednici = ctx.PrisustvaNaSjednicamaUpravnogOdbora.Where(x => x.isDeleted == false && x.SjednicaUpravnogOdboraId==sjednicaId).Select(x => new PrisutvaNaSjednicamaPodaci {
                    Id = x.Id,
                    isDeleted = x.isDeleted,
                    ClanUpravnogOdboraId = x.ClanUpravnogOdboraId,
                    SjednicaUpravnogOdboraId = x.SjednicaUpravnogOdboraId,
                    Prisutan = x.Prisutan,
                    ImePrezime=x.ClanUpravnogOdbora.Osoba.Ime+" "+x.ClanUpravnogOdbora.Osoba.Prezime,
                    Funkcija=x.ClanUpravnogOdbora.UlogeClanovaUpravnogOdbora.Naziv 

                }).ToList(),
                sjednicaId=sjednicaId
            };
            return View(model);
        }
       public ActionResult Index2(int sjednicaId)
        {
            PrisustvaNaSjednicamaUpravnogOdboraIndexVM model = new PrisustvaNaSjednicamaUpravnogOdboraIndexVM
            {
                prisustvaNaSjednici = ctx.PrisustvaNaSjednicamaUpravnogOdbora.Where(x => x.isDeleted == false && x.SjednicaUpravnogOdboraId==sjednicaId).Select(x => new PrisutvaNaSjednicamaPodaci
                {
                    Id = x.Id,
                    isDeleted = x.isDeleted,
                    ClanUpravnogOdboraId = x.ClanUpravnogOdboraId,
                    SjednicaUpravnogOdboraId = x.SjednicaUpravnogOdboraId,
                    Prisutan = x.Prisutan,
                    ImePrezime = x.ClanUpravnogOdbora.Osoba.Ime + " " + x.ClanUpravnogOdbora.Osoba.Prezime,
                    Funkcija = x.ClanUpravnogOdbora.UlogeClanovaUpravnogOdbora.Naziv

                }).ToList(),
                sjednicaId=sjednicaId
            };
         

            return View("Index2", model);

        }
        public ActionResult SpremiPrisustvo(PrisustvaNaSjednicamaUpravnogOdboraIndexVM model)
        {

         
            foreach (var p in model.prisustvaNaSjednici)
            {
                PrisustvaNaSjednicamaUpravnogOdbora prisustvo = ctx.PrisustvaNaSjednicamaUpravnogOdbora.Where(x => x.Id == p.Id).FirstOrDefault();
                prisustvo.Prisutan = p.Prisutan;
                ctx.SaveChanges();

            }
            return RedirectToAction("Detalji","SjedniceUpravnogOdbora",new {sjednicaId=model.sjednicaId,brojTaba=1});
        }
    }
}