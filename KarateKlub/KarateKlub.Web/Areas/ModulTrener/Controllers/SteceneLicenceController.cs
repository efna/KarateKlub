using KarateKlub.Data;
using KarateKlub.Data.Models;
using KarateKlub.Web.Areas.ModulTrener.Models;
using KarateKlub.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulTrener.Controllers
{
    [Autorizacija(false, false, false, true, false)]

    public class SteceneLicenceController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulTrener/SteceneLicence
        public ActionResult Index(int seminarId)
        {
            SteceneLicenceIndexVM model = new SteceneLicenceIndexVM
            {
                steceneLicence = ctx.SteceneLicence.Where(x => x.isDeleted == false && x.SeminarId == seminarId).Select(x => new StecenaLicencaPodaci
                {
                    Id = x.Id,
                    isDeleted = x.isDeleted,
                    VrstaLicenciId = x.VrstaLicenciId,
                    VrstaLicence = x.VrstaLicenci.Naziv,
                    SeminarId = x.SeminarId,
                    Seminar = x.Seminar.NazivSeminara,
                    NivoLicenceId = x.NivoLicenceId,
                    NivoLicence = x.NivoLicence.Naziv,
                    OsobaId = x.OsobaId,
                    Osoba = x.Osoba.Ime + " (" + x.Osoba.ImeRoditelja + " )" + x.Osoba.Prezime,
                    StecenoZvanje = x.StecenoZvanje,
                    DatumSticanja = x.DatumSticanja,
                    DatumVazenja = x.DatumVazenja,
                    isAktivnaLicenca = x.isAktivnaLicenca,
                    Obrazlozenje = x.Obrazlozenje
                }).ToList()
            };
            ViewData["seminarId"] = seminarId;
            return View(model);
        }
        public ActionResult Index2(int seminarId)
        {
            SteceneLicenceIndexVM model = new SteceneLicenceIndexVM
            {
                steceneLicence = ctx.SteceneLicence.Where(x => x.isDeleted == false).Select(x => new StecenaLicencaPodaci
                {
                    Id = x.Id,
                    isDeleted = x.isDeleted,
                    VrstaLicenciId = x.VrstaLicenciId,
                    VrstaLicence = x.VrstaLicenci.Naziv,
                    SeminarId = x.SeminarId,
                    Seminar = x.Seminar.NazivSeminara,
                    NivoLicenceId = x.NivoLicenceId,
                    NivoLicence = x.NivoLicence.Naziv,
                    OsobaId = x.OsobaId,
                    Osoba = x.Osoba.Ime + " (" + x.Osoba.ImeRoditelja + " )" + x.Osoba.Prezime,
                    StecenoZvanje = x.StecenoZvanje,
                    DatumSticanja = x.DatumSticanja,
                    DatumVazenja = x.DatumVazenja,
                    isAktivnaLicenca = x.isAktivnaLicenca,
                    Obrazlozenje = x.Obrazlozenje
                }).ToList()
            };
            ViewData["seminarId"] = seminarId;
            return View("Index2", model);
        }
        public ActionResult Index3(int aktivnost = 0)
        {
            List<SteceneLicence> licence = new List<SteceneLicence>();
            if (aktivnost == 0)
            {
                licence = ctx.SteceneLicence.Where(x => x.isDeleted == false).ToList();

            }
            else if (aktivnost == 1)
            {

                licence = ctx.SteceneLicence.Where(x => x.isDeleted == false && x.isAktivnaLicenca == true).ToList();

            }
            else
            {
                licence = ctx.SteceneLicence.Where(x => x.isDeleted == false && x.isAktivnaLicenca == false).ToList();

            }
            SteceneLicenceIndex3VM model = new SteceneLicenceIndex3VM(licence, aktivnost);
            ViewData["aktivnost"] = aktivnost;
            return View("Index3", model);
        }
        public ActionResult Dodaj(int seminarId)
        {
            SteceneLicenceDodajVM model = new SteceneLicenceDodajVM
            {
                SeminarId = seminarId,
                NivoiLicenci = BindNivoeLicence(),
                VrsteLicenci = BindVrsteLicenci(),
                UcesniciSeminara = BindUcesnikeSeminara(seminarId)
            };
            model.NivoiLicenci.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite nivo licence-" });
            model.VrsteLicenci.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite vrstu licence-" });
            model.UcesniciSeminara.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite učesnika-" });


            return View("Dodaj", model);

        }

        private List<SelectListItem> BindUcesnikeSeminara(int seminarId)
        {
            return ctx.UcesniciSeminara.Where(x => x.isDeleted == false && x.SeminariId == seminarId).Select(x => new SelectListItem { Value = x.Osoba.Id.ToString(), Text = x.Osoba.Ime + " (" + x.Osoba.ImeRoditelja + ") " + x.Osoba.Prezime }).ToList();

        }

        public DateTime KonvertujUDatum_dd_mm_yyyy(string datum)
        {

            int dan = Convert.ToInt32(datum.Substring(0, 2));
            int mjesec = Convert.ToInt32(datum.Substring(3, 2));
            int godina = Convert.ToInt32(datum.Substring(6, 4));
            DateTime uneseniDatum = new DateTime(godina, mjesec, dan);
            return uneseniDatum;


        }
        public ActionResult SpremiNovuLicencu(SteceneLicenceDodajVM model)
        {
            SteceneLicence licenca = new SteceneLicence();
            licenca.isAktivnaLicenca = true;
            licenca.isDeleted = false;
            licenca.VrstaLicenciId = model.VrstaLicenciId;
            licenca.NivoLicenceId = model.NivoLicenceId;
            licenca.SeminarId = model.SeminarId;
            licenca.OsobaId = model.ucesnikId;
            licenca.StecenoZvanje = model.StecenoZvanje;
            if (model.DatumSticanja != null)
                licenca.DatumSticanja = KonvertujUDatum_dd_mm_yyyy(model.DatumSticanja);
            if (model.DatumVazenja != null)
                licenca.DatumVazenja = KonvertujUDatum_dd_mm_yyyy(model.DatumVazenja);
            licenca.Obrazlozenje = model.Obrazlozenje;
            ctx.SteceneLicence.Add(licenca);
            ctx.SaveChanges();

            return RedirectToAction("Index", "UpravljanjeSeminarima", new { seminarId = model.SeminarId, brojTaba = 2 });
        }
        public ActionResult SpremiIzmjenuLicence(SteceneLicenceUrediVM model)
        {
            SteceneLicence licenca = ctx.SteceneLicence.Where(x => x.Id == model.Id).FirstOrDefault();
            licenca.VrstaLicenciId = model.VrstaLicenciId;
            licenca.NivoLicenceId = model.NivoLicenceId;
            licenca.SeminarId = model.SeminarId;
            licenca.OsobaId = model.ucesnikId;
            licenca.StecenoZvanje = model.StecenoZvanje;
            if (model.DatumSticanja != null)
                licenca.DatumSticanja = KonvertujUDatum_dd_mm_yyyy(model.DatumSticanja);
            if (model.DatumVazenja != null)
                licenca.DatumVazenja = KonvertujUDatum_dd_mm_yyyy(model.DatumVazenja);
            licenca.Obrazlozenje = model.Obrazlozenje;
            licenca.isAktivnaLicenca = model.isAktivnaLicenca;
            ctx.SaveChanges();
            return RedirectToAction("Index", "UpravljanjeSeminarima", new { seminarId = model.SeminarId, brojTaba = 2 });

        }
        public ActionResult Uredi(int licencaId)
        {
            SteceneLicence licenca = ctx.SteceneLicence.Where(x => x.Id == licencaId).FirstOrDefault();
            SteceneLicenceUrediVM model = new SteceneLicenceUrediVM
            {
                Id = licencaId,
                isAktivnaLicenca = licenca.isAktivnaLicenca,
                isDeleted = licenca.isDeleted,
                VrstaLicenciId = licenca.VrstaLicenciId,
                VrsteLicenci = BindVrsteLicenci(),
                NivoLicenceId = licenca.NivoLicenceId,
                NivoiLicenci = BindNivoeLicence(),
                SeminarId = licenca.SeminarId,
                ucesnikId = licenca.OsobaId,
                UcesniciSeminara = BindUcesnikeSeminara(licenca.SeminarId),
                StecenoZvanje = licenca.StecenoZvanje,
                DatumSticanja = licenca.DatumSticanja.ToString("dd.MM.yyyy"),
                DatumVazenja = licenca.DatumVazenja.ToString("dd.MM.yyyy"),
                Obrazlozenje = licenca.Obrazlozenje


            };
            model.NivoiLicenci.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite nivo licence-" });
            model.VrsteLicenci.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite vrstu licence-" });
            model.UcesniciSeminara.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite učesnika-" });


            return View("Uredi", model);
        }
        public ActionResult Uredi2(int licencaId, int aktivnost)
        {
            SteceneLicence licenca = ctx.SteceneLicence.Where(x => x.Id == licencaId).FirstOrDefault();
            SteceneLicenceUrediVM model = new SteceneLicenceUrediVM
            {
                Id = licencaId,
                isAktivnaLicenca = licenca.isAktivnaLicenca,
                isDeleted = licenca.isDeleted,
                VrstaLicenciId = licenca.VrstaLicenciId,
                VrsteLicenci = BindVrsteLicenci(),
                NivoLicenceId = licenca.NivoLicenceId,
                NivoiLicenci = BindNivoeLicence(),
                SeminarId = licenca.SeminarId,
                ucesnikId = licenca.OsobaId,
                UcesniciSeminara = BindUcesnikeSeminara(licenca.SeminarId),
                StecenoZvanje = licenca.StecenoZvanje,
                DatumSticanja = licenca.DatumSticanja.ToString("dd.MM.yyyy"),
                DatumVazenja = licenca.DatumVazenja.ToString("dd.MM.yyyy"),
                Obrazlozenje = licenca.Obrazlozenje,
                aktivnost = aktivnost


            };
            model.NivoiLicenci.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite nivo licence-" });
            model.VrsteLicenci.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite vrstu licence-" });
            model.UcesniciSeminara.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite učesnika-" });


            return View("Uredi2", model);
        }
        public ActionResult SpremiIzmjenuLicence2(SteceneLicenceUrediVM model)
        {
            SteceneLicence licenca = ctx.SteceneLicence.Where(x => x.Id == model.Id).FirstOrDefault();
            licenca.VrstaLicenciId = model.VrstaLicenciId;
            licenca.NivoLicenceId = model.NivoLicenceId;
            licenca.SeminarId = model.SeminarId;
            licenca.OsobaId = model.ucesnikId;
            licenca.StecenoZvanje = model.StecenoZvanje;
            if (model.DatumSticanja != null)
                licenca.DatumSticanja = KonvertujUDatum_dd_mm_yyyy(model.DatumSticanja);
            if (model.DatumVazenja != null)
                licenca.DatumVazenja = KonvertujUDatum_dd_mm_yyyy(model.DatumVazenja);
            licenca.Obrazlozenje = model.Obrazlozenje;
            licenca.isAktivnaLicenca = model.isAktivnaLicenca;
            ctx.SaveChanges();
            return RedirectToAction("Index3", new { aktivnost = model.aktivnost });

        }
        public JsonResult Obrisi(int licencaId)
        {
            SteceneLicence licenca = ctx.SteceneLicence.Where(x => x.Id == licencaId).FirstOrDefault();
            if (licenca != null)
                licenca.isDeleted = true;

            ctx.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        private List<SelectListItem> BindNivoeLicence()
        {
            return ctx.NivoLicence.Where(x => x.isDeleted == false).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv }).ToList();

        }

        private List<SelectListItem> BindVrsteLicenci()
        {
            return ctx.VrsteLicenci.Where(x => x.isDeleted == false).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv }).ToList();

        }

        public ActionResult PregledStecenihLicenci(int osobaId, int aktivnostLicence, int aktivan)
        {
            Osoba osoba = ctx.Osoba.Where(x => x.Id == osobaId).FirstOrDefault();
            List<SteceneLicence> listaLicenci = new List<SteceneLicence>();

            if (aktivnostLicence == 0)
            {
                listaLicenci = ctx.SteceneLicence.Where(x => x.isDeleted == false && x.OsobaId == osobaId).ToList();
            }
            else if (aktivnostLicence == 1)
            {
                listaLicenci = ctx.SteceneLicence.Where(x => x.isDeleted == false && x.OsobaId == osobaId && x.isAktivnaLicenca == true).ToList();

            }
            else
            {
                listaLicenci = ctx.SteceneLicence.Where(x => x.isDeleted == false && x.OsobaId == osobaId && x.isAktivnaLicenca == false).ToList();

            }

            SteceneLicencePregledStecenihLicenciVM model = new SteceneLicencePregledStecenihLicenciVM(listaLicenci, aktivnostLicence, osobaId, aktivan, osoba);


            return View("PregledStecenihLicenci", model);
        }

    }
}