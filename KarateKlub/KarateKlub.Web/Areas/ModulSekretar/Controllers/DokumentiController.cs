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

    public class DokumentiController : Controller
    {
        MojContext ctx = new MojContext();
     
        // GET: ModulSekretar/Dokumenti
        public ActionResult Index(int stavkaId)
        {
            DokumentiIndexVM model = new DokumentiIndexVM {
                listaDokumenata = ctx.Dokumenti.Where(x => x.StavkaProtokolaId == stavkaId && x.isDeleted==false).Select(x => new DokumentPodaci {
                    Id = x.Id,
                    stavkaId = x.StavkaProtokolaId,
                    NazivDokumenta = x.NazivDokumenta,
                    TipDokumenta=x.TipDokumenta,
                    Dokument=x.Dokument

               }).ToList(),
                protokolId=ctx.StavkeProtokola.Where(x=>x.Id==stavkaId).FirstOrDefault().ProtokolId,
                stavkaId=stavkaId
            };

            return View(model);
        }
        public ActionResult PregledSvihDokumenata()
        {
            DokumentiPregledSvihDokumenataVM model = new DokumentiPregledSvihDokumenataVM
            {
                listaDokumenata = ctx.Dokumenti.Where(x=>x.isDeleted == false).Select(x => new DokumentPodaci
                {
                    Id = x.Id,
                    stavkaId = x.StavkaProtokolaId,
                    NazivDokumenta = x.NazivDokumenta,
                    TipDokumenta = x.TipDokumenta,
                    Dokument = x.Dokument,
                    PodBroj=x.StavkaProtokola.PodBroj

                }).ToList()
            };

            return View("PregledSvihDokumenata", model);
        }
        public ActionResult DownloadDokumenta(int dokumentId)
        {
            Dokumenti dokument = ctx.Dokumenti.Where(x => x.Id == dokumentId).FirstOrDefault();

            if (dokument.TipDokumenta == "application/pdf")
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content - Disposition", "attachment; filename = " + dokument.NazivDokumenta);
                Response.OutputStream.Write(dokument.Dokument, 0, dokument.Dokument.Length);
                Response.Flush();
                return RedirectToAction("Index", new { stavkaId = dokument.StavkaProtokolaId });

            }
            else
            return File(dokument.Dokument,dokument.TipDokumenta, dokument.NazivDokumenta);

        }
        public JsonResult Obrisi(int dokumentId)
        {


            ctx.Dokumenti.Where(x => x.Id == dokumentId).FirstOrDefault().isDeleted = true;
            ctx.SaveChanges();

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Dodaj(int stavkaId)
        {
            DokumentiDodajVM model = new DokumentiDodajVM
            {
                stavkaId = stavkaId
            };

            return View("Dodaj", model);
        }
        [HttpPost]
        public ActionResult SpremiNoviDokument(DokumentiDodajVM model)
        {
            if (model.d != null)
            {
                Dokumenti dokument = new Dokumenti();
                dokument.isDeleted = false;
                dokument.StavkaProtokolaId = model.stavkaId;
                dokument.NazivDokumenta = model.d.FileName;
                dokument.TipDokumenta = model.d.ContentType;
                byte[] noviDokument = new byte[model.d.ContentLength];
                model.d.InputStream.Read(noviDokument, 0, model.d.ContentLength);
                dokument.Dokument = noviDokument;
                ctx.Dokumenti.Add(dokument);
                ctx.SaveChanges();

            }

            return RedirectToAction("Index", new { stavkaId = model.stavkaId });
        }

    }
}