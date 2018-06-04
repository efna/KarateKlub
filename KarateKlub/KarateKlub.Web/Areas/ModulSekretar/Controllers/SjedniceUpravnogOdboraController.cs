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

    public class SjedniceUpravnogOdboraController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulSekretar/SjedniceUpravnogOdbora
        public ActionResult Index()
       {
            
            SjedniceUpravnogOdboraIndexVM model = new SjedniceUpravnogOdboraIndexVM
            {
                sjedniceUO = ctx.SjedniceUpravnogOdbora.Where(x => x.isDeleted == false).Select(x => new SjedniciceUpravnogOdboraPodaci
                {
                    Id = x.Id,
                    IsDeleted = x.isDeleted,
                    DatumOdrzavanja = x.DatumOdrzavanja,
                    Svrha = x.Svrha,
                    Obrazlozenje = x.Obrazlozenje

                }).ToList(),
              
            };
      

            return View(model);
        }
        public DateTime KonvertujUDatum_dd_mm_yyyy(string datum)
        {

            int dan = Convert.ToInt32(datum.Substring(0, 2));
            int mjesec = Convert.ToInt32(datum.Substring(3, 2));
            int godina = Convert.ToInt32(datum.Substring(6, 4));
            DateTime uneseniDatum = new DateTime(godina, mjesec, dan);
            return uneseniDatum;


        }
        public ActionResult Dodaj()
        {
            SjedniceUpravnogOdboraDodajVM model = new SjedniceUpravnogOdboraDodajVM {
            DatumOdrzavanja=""
            };
            
            return View("Dodaj", model);
        }
        public ActionResult Uredi(int sjednicaId)
        {
            SjedniceUpravnogOdbora sjednica = ctx.SjedniceUpravnogOdbora.Where(x => x.Id == sjednicaId).FirstOrDefault();
            SjedniceUpravnogOdboraUrediVM model = new SjedniceUpravnogOdboraUrediVM {
                Id=sjednica.Id,
                IsDeleted=sjednica.isDeleted,
                Svrha=sjednica.Svrha,
                Obrazlozenje=sjednica.Obrazlozenje,
                DatumOdrzavanja=sjednica.DatumOdrzavanja.ToString("dd.MM.yyyy")

            };
            
            return View("Uredi",model);
        }
        public ActionResult SpremiNovuSjednicu(SjedniceUpravnogOdboraDodajVM model)
        {
            SjedniceUpravnogOdbora sjednica = new SjedniceUpravnogOdbora();
            sjednica.isDeleted = false;
            sjednica.Svrha = model.Svrha;
            sjednica.Obrazlozenje = model.Obrazlozenje;
            sjednica.DatumOdrzavanja = KonvertujUDatum_dd_mm_yyyy(model.DatumOdrzavanja);
            ctx.SjedniceUpravnogOdbora.Add(sjednica);
            ctx.SaveChanges();
            int sjednicaId = ctx.SjedniceUpravnogOdbora.ToList().OrderByDescending(x => x.Id).FirstOrDefault().Id;

            List<ClanoviUpravnogOdbora> clanoviUpravnogOdbora = ctx.ClanoviUpravnogOdbora.Where(x => x.isDeleted == false && x.Aktivan==true).ToList();
            for (int i = 0; i < clanoviUpravnogOdbora.Count(); i++)
            {
                PrisustvaNaSjednicamaUpravnogOdbora prisustvo = new PrisustvaNaSjednicamaUpravnogOdbora();
                prisustvo.isDeleted = false;
                prisustvo.Prisutan = false;
                prisustvo.SjednicaUpravnogOdboraId = sjednicaId;
                prisustvo.ClanUpravnogOdboraId = clanoviUpravnogOdbora[i].Id;
                ctx.PrisustvaNaSjednicamaUpravnogOdbora.Add(prisustvo);
                ctx.SaveChanges();
            }
            return RedirectToAction("Index","UpravljanjeUpravnimOdborom",new {aktivnost=0,brojTaba=2});
        }

        public ActionResult SpremiIzmjenuSjednice(SjedniceUpravnogOdboraUrediVM model)
        {
            SjedniceUpravnogOdbora sjednica = ctx.SjedniceUpravnogOdbora.Where(x => x.Id == model.Id).FirstOrDefault();

            sjednica.isDeleted = model.IsDeleted;
            sjednica.Svrha = model.Svrha;
            sjednica.Obrazlozenje = model.Obrazlozenje;
            sjednica.DatumOdrzavanja = KonvertujUDatum_dd_mm_yyyy(model.DatumOdrzavanja);
            ctx.SaveChanges();

            return RedirectToAction("Index","UpravljanjeUpravnimOdborom",new {aktivnost=0,brojTaba=2});
        }
        public JsonResult Obrisi(int sjednicaId)
        {

            SjedniceUpravnogOdbora sjednica = ctx.SjedniceUpravnogOdbora.Where(x => x.Id == sjednicaId).FirstOrDefault();
            sjednica.isDeleted = true;
            List<PrisustvaNaSjednicamaUpravnogOdbora> prisustva = ctx.PrisustvaNaSjednicamaUpravnogOdbora.Where(x => x.SjednicaUpravnogOdboraId == sjednica.Id).ToList();
            for (int i = 0; i < prisustva.Count(); i++)
            {
                prisustva[i].isDeleted = true;
            }
            List<OdlukeUpravnogOdbora> odluke = ctx.OdlukeUpravnogOdbora.Where(x => x.SjednicaUpravnogOdboraId == sjednica.Id).ToList();
            for (int i = 0; i < odluke.Count(); i++)
            {
                odluke[i].isDeleted = true;
            }

            ctx.SaveChanges();

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Detalji(int sjednicaId,int brojTaba=1)
        {

            SjedniceUpravnogOdbora sjednica = ctx.SjedniceUpravnogOdbora.Where(x => x.Id == sjednicaId).FirstOrDefault();
            SjedniceUpravnogOdboraDetaljiVM model = new SjedniceUpravnogOdboraDetaljiVM {
            
                sjednica=ctx.SjedniceUpravnogOdbora.Where(z=>z.Id==sjednicaId).Select(z=>new SjedniciceUpravnogOdboraPodaci {
                    Id=z.Id,
                    IsDeleted=z.isDeleted,
                    Svrha=z.Svrha,
                    Obrazlozenje=z.Obrazlozenje,
                    DatumOdrzavanja=z.DatumOdrzavanja


                }).FirstOrDefault(),
                sjednicaId=sjednicaId
           
            };
            ViewData["tab"] = brojTaba;

            return View("Detalji",model);
        }
    }
}