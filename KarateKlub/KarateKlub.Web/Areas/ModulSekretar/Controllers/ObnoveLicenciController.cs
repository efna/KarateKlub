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

    public class ObnoveLicenciController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulSekretar/ObnoveLicenci
        public ActionResult Index(int seminarId)
        {
            ObnoveLicenciIndexVM model = new ObnoveLicenciIndexVM
            {
              obnoveLicenci=ctx.ObnoveLicenci.Where(x=>x.isDeleted==false && x.SeminarId == seminarId).Select(x =>new ObnovaLicencePodaci {
                  Id=x.Id,
                  isDeleted=x.isDeleted,
                  SeminarId=x.SeminarId,
                  Osoba=x.StecenaLicenca.Osoba.Ime+" ("+x.StecenaLicenca.Osoba.ImeRoditelja+") "+x.StecenaLicenca.Osoba.Prezime,
                  StecenaLicencaId=x.StecenaLicencaId,
                  StecenoZvanje=x.StecenaLicenca.StecenoZvanje,
                  VrstaLicence=x.StecenaLicenca.VrstaLicenci.Naziv,
                  NivoLicence=x.StecenaLicenca.NivoLicence.Naziv,
                  DatumObnove=x.DatumObnove,
                  DatumVazenja=x.DatumVazenja,
                  DatumSticanja=x.StecenaLicenca.DatumSticanja
              }).ToList(),
             
            };
            ViewData["seminarId"] = seminarId;
            return View(model);
        }
        public ActionResult Index2(int licencaId,int aktivnost)
        {
            ObnoveLicenciIndexVM model = new ObnoveLicenciIndexVM
            {
                obnoveLicenci = ctx.ObnoveLicenci.Where(x => x.isDeleted == false && x.StecenaLicencaId==licencaId).Select(x => new ObnovaLicencePodaci
                {
                    Id = x.Id,
                    isDeleted = x.isDeleted,
                    SeminarId = x.SeminarId,
                    Osoba = x.StecenaLicenca.Osoba.Ime + " (" + x.StecenaLicenca.Osoba.ImeRoditelja + ") " + x.StecenaLicenca.Osoba.Prezime,
                    StecenaLicencaId = x.StecenaLicencaId,
                    StecenoZvanje = x.StecenaLicenca.StecenoZvanje,
                    VrstaLicence = x.StecenaLicenca.VrstaLicenci.Naziv,
                    NivoLicence = x.StecenaLicenca.NivoLicence.Naziv,
                    DatumObnove = x.DatumObnove,
                    DatumVazenja = x.DatumVazenja,
                    DatumSticanja = x.StecenaLicenca.DatumSticanja
                }).ToList(),

            };
            ViewData["aktivnost"] = aktivnost;
            return View("Index2",model);
        }
        public ActionResult Dodaj(int stecenaLicencaId,int seminarId)
        {
            SteceneLicence licenca = ctx.SteceneLicence.Where(x => x.Id == stecenaLicencaId).FirstOrDefault();
            ObnoveLicenciDodajVM model = new ObnoveLicenciDodajVM {
                StecenaLicencaId=stecenaLicencaId,
                SeminarId=seminarId
            };
            return View("Dodaj", model);
        }
        public DateTime KonvertujUDatum_dd_mm_yyyy(string datum)
        {

            int dan = Convert.ToInt32(datum.Substring(0, 2));
            int mjesec = Convert.ToInt32(datum.Substring(3, 2));
            int godina = Convert.ToInt32(datum.Substring(6, 4));
            DateTime uneseniDatum = new DateTime(godina, mjesec, dan);
            return uneseniDatum;


        }
        public ActionResult SpremiNovuObnovuLicence(ObnoveLicenciDodajVM model)
        {
            
            ObnoveLicenci obnova = new ObnoveLicenci();
            obnova.isDeleted = false;
            obnova.SeminarId = model.SeminarId;
            obnova.StecenaLicencaId = model.StecenaLicencaId;
            if (model.DatumObnove != null)
                obnova.DatumObnove = KonvertujUDatum_dd_mm_yyyy(model.DatumObnove);
            if (model.DatumVazenja != null)
                obnova.DatumVazenja = KonvertujUDatum_dd_mm_yyyy(model.DatumVazenja);
            ctx.ObnoveLicenci.Add(obnova);
            ctx.SaveChanges();
            return RedirectToAction("Index", "UpravljanjeSeminarima", new { seminarId = model.SeminarId, brojTaba = 3});
            
        }

        public ActionResult Uredi(int obnovaLicenceId)
        {
            ObnoveLicenci obnova = ctx.ObnoveLicenci.Where(x => x.Id == obnovaLicenceId).FirstOrDefault();
            ObnoveLicenciUrediVM model = new ObnoveLicenciUrediVM {
                Id=obnovaLicenceId,
                StecenaLicencaId=obnova.StecenaLicencaId,
                SeminarId=obnova.SeminarId,
                DatumObnove=obnova.DatumObnove.ToString("dd.MM.yyyy"),
                DatumVazenja=obnova.DatumVazenja.ToString("dd.MM.yyyy")
            };

            return View("Uredi", model);

        }
        public ActionResult Uredi2(int obnovaLicenceId,int aktivnost)
        {
            ObnoveLicenci obnova = ctx.ObnoveLicenci.Where(x => x.Id == obnovaLicenceId).FirstOrDefault();
            ObnoveLicenciUrediVM model = new ObnoveLicenciUrediVM
            {
                Id = obnovaLicenceId,
                StecenaLicencaId = obnova.StecenaLicencaId,
                SeminarId = obnova.SeminarId,
                DatumObnove = obnova.DatumObnove.ToString("dd.MM.yyyy"),
                DatumVazenja = obnova.DatumVazenja.ToString("dd.MM.yyyy"),
                aktivnost=aktivnost
            };

            return View("Uredi2", model);

        }
        public ActionResult SpremiIzmjenuObnoveLicence(ObnoveLicenciUrediVM model)
        {
            ObnoveLicenci obnova = ctx.ObnoveLicenci.Where(x => x.Id == model.Id).FirstOrDefault();
            if (model.DatumObnove != null)
                obnova.DatumObnove = KonvertujUDatum_dd_mm_yyyy(model.DatumObnove);
            if (model.DatumVazenja != null)
                obnova.DatumVazenja = KonvertujUDatum_dd_mm_yyyy(model.DatumVazenja);
            ctx.SaveChanges();
            return RedirectToAction("Index", "UpravljanjeSeminarima", new { seminarId = model.SeminarId, brojTaba = 3 });

        }
        public ActionResult SpremiIzmjenuObnoveLicence2(ObnoveLicenciUrediVM model)
        {
            ObnoveLicenci obnova = ctx.ObnoveLicenci.Where(x => x.Id == model.Id).FirstOrDefault();
            if (model.DatumObnove != null)
                obnova.DatumObnove = KonvertujUDatum_dd_mm_yyyy(model.DatumObnove);
            if (model.DatumVazenja != null)
                obnova.DatumVazenja = KonvertujUDatum_dd_mm_yyyy(model.DatumVazenja);
            ctx.SaveChanges();
            return RedirectToAction("Index2", new { licencaId=model.StecenaLicencaId,aktivnost=model.aktivnost });

        }
        public JsonResult Obrisi(int obnovaLicenceId)
        {

            ObnoveLicenci obnova = ctx.ObnoveLicenci.Where(x => x.Id == obnovaLicenceId).FirstOrDefault();
            
            if (obnova != null)
                obnova.isDeleted = true;
            ctx.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}