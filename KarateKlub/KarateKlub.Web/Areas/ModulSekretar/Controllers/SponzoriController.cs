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

    public class SponzoriController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulSekretar/Sponzori
        public ActionResult Index(int aktivnost)
        {
            List<Sponzori> sponzori = new List<Sponzori>();

            if (aktivnost==0)
            {
                sponzori = ctx.Sponzori.Where(x => x.isAktivan == true && x.isDeleted == false).ToList();
            }
            else if(aktivnost==1)
            {
                sponzori = ctx.Sponzori.Where(x => x.isAktivan == false && x.isDeleted == false).ToList();

            }
            else
            {
                sponzori = ctx.Sponzori.Where(x =>x.isDeleted == false).ToList();

            }
            SponzoriIndexVM model = new SponzoriIndexVM(sponzori, aktivnost);
            return View(model);
        }
        public ActionResult GoToUpravljanjeSponzorstvimaDonacijama(int aktivnost)
        {
            return RedirectToAction("Index", "UpravljanjeSponzorstvimaDonacijama", new { brojTaba = 1, aktivnost = aktivnost });
        }

        public ActionResult Dodaj(int aktivnost)
        {
            SponzoriDodajVM model = new SponzoriDodajVM {
                vrsteLica=BindVrsteLica(),
                aktivnost=aktivnost
            };
            model.vrsteLica.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite vrstu lica-" });

            return View("Dodaj", model);
        }

        private List<SelectListItem> BindVrsteLica()
        {
            return ctx.VrsteLica.Where(x=>x.isDeleted==false).Select(x => new SelectListItem { Value = x.Id.ToString(), Text =x.Naziv }).ToList();

        }

        public ActionResult SpremiNovogSponzora(SponzoriDodajVM model)
        {
            Sponzori sponzor = new Sponzori();
            sponzor.isDeleted = false;
            sponzor.isAktivan = true;
            sponzor.Naziv = model.Naziv;
            sponzor.ImeKontaktOsobe = model.ImeKontaktOsobe;
            sponzor.PrezimeKontaktOsobe = model.PrezimeKontaktOsobe;
            sponzor.KontaktTelefon = model.KontaktTelefon;
            sponzor.Email = model.Email;
            sponzor.VrstaLicaId = model.VrstaLicaId;
            ctx.Sponzori.Add(sponzor);
            ctx.SaveChanges();
            return RedirectToAction("Index", "UpravljanjeSponzorstvimaDonacijama", new { brojTaba = 1, aktivnost = model.aktivnost });
        }

        public ActionResult Uredi(int sponzorId,int aktivnost)
        {
            Sponzori sponzor = ctx.Sponzori.Where(x => x.Id == sponzorId).FirstOrDefault();
            SponzoriUrediVM model = new SponzoriUrediVM {
            Id=sponzorId,
            isDeleted=sponzor.isDeleted,
            isAktivan=sponzor.isAktivan,
            Naziv=sponzor.Naziv,
            ImeKontaktOsobe=sponzor.ImeKontaktOsobe,
            PrezimeKontaktOsobe=sponzor.PrezimeKontaktOsobe,
            KontaktTelefon=sponzor.KontaktTelefon,
            Email=sponzor.Email,
            VrstaLicaId=sponzor.VrstaLicaId,
            vrsteLica = BindVrsteLica(),
            aktivnost=aktivnost
            };
            model.vrsteLica.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite vrstu lica-" });

            return View("Uredi", model);
        }

        public ActionResult SpremiIzmjenuSponzora(SponzoriUrediVM model)
        {
            Sponzori sponzor = ctx.Sponzori.Where(x => x.Id == model.Id).FirstOrDefault();
            sponzor.VrstaLicaId = model.VrstaLicaId;
            sponzor.ImeKontaktOsobe = model.ImeKontaktOsobe;
            sponzor.PrezimeKontaktOsobe = model.PrezimeKontaktOsobe;
            sponzor.KontaktTelefon = model.KontaktTelefon;
            sponzor.Email = model.Email;
            sponzor.Naziv = model.Naziv;
            sponzor.isAktivan = model.isAktivan;
            ctx.SaveChanges();
            return RedirectToAction("Index", "UpravljanjeSponzorstvimaDonacijama", new { brojTaba = 1, aktivnost = model.aktivnost });

        }

        public JsonResult Obrisi(int sponzorId)
        {

            Sponzori sponzor = ctx.Sponzori.Where(x => x.Id == sponzorId).FirstOrDefault();
            List<Sponzorstva> sponzosrtva = ctx.Sponzorstva.Where(x => x.SponzorId == sponzorId && x.isDeleted == false).ToList();
            for (int i = 0; i < sponzosrtva.Count(); i++)
            {
                sponzosrtva[i].isDeleted = true;
                ctx.SaveChanges();
            }
            if (sponzor != null)
                sponzor.isDeleted = true;

            ctx.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}