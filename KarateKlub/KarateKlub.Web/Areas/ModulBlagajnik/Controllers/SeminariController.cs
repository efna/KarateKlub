using KarateKlub.Data;
using KarateKlub.Data.Models;
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

    public class SeminariController : Controller
    {
        MojContext ctx = new MojContext();

        // GET: ModulBlagajnik/Seminari
        public ActionResult Index()
        {
            SeminariIndexVM model = new SeminariIndexVM
            {
                seminari = ctx.Seminari.Where(x => x.isDeleted == false).Select(x => new SeminarPodaci
                {
                    Id = x.Id,
                    isDeleted = x.isDeleted,
                    NazivSeminara = x.NazivSeminara,
                    DatumOdrzavanjaSeminaraOd = x.DatumOdrzavanjaSeminaraOd,
                    DatumOdrzavanjaSeminaraDo = x.DatumOdrzavanjaSeminaraDo,
                    Obrazlozenje = x.Obrazlozenje,
                    OrganizatorSeminara = x.OrganizatorSeminara,
                    MjestoOdrzavanjaSeminara = x.MjestoOdrzavanjaSeminara
                }).ToList()
            };
            return View(model);
        }
        public ActionResult Dodaj()
        {
            SeminariDodajVM model = new SeminariDodajVM
            {
                ucesnici = BindUcesnikeSeminara()
            };
            model.ucesnici.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite učesnike-" });

            return View("Dodaj", model);
        }

        private List<SelectListItem> BindUcesnikeSeminara()
        {
            return ctx.Osoba.Where(x => x.isDeleted == false && x.isAktivnaOsoba == true).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Ime + " (" + x.ImeRoditelja + ") " + x.Prezime }).ToList();

        }
        public DateTime KonvertujUDatum_dd_mm_yyyy(string datum)
        {

            int dan = Convert.ToInt32(datum.Substring(0, 2));
            int mjesec = Convert.ToInt32(datum.Substring(3, 2));
            int godina = Convert.ToInt32(datum.Substring(6, 4));
            DateTime uneseniDatum = new DateTime(godina, mjesec, dan);
            return uneseniDatum;


        }

        public ActionResult SpremiNoviSeminar(SeminariDodajVM model)
        {
            Seminari seminar = new Seminari();
            seminar.isDeleted = false;
            seminar.NazivSeminara = model.NazivSeminara;
            seminar.MjestoOdrzavanjaSeminara = model.MjestoOdrzavanjaSeminara;
            seminar.OrganizatorSeminara = model.OrganizatorSeminara;
            seminar.Obrazlozenje = model.Obrazlozenje;
            if (seminar.DatumOdrzavanjaSeminaraOd != null)
                seminar.DatumOdrzavanjaSeminaraOd = KonvertujUDatum_dd_mm_yyyy(model.DatumOdrzavanjaSeminaraOd);
            if (seminar.DatumOdrzavanjaSeminaraDo != null)
                seminar.DatumOdrzavanjaSeminaraDo = KonvertujUDatum_dd_mm_yyyy(model.DatumOdrzavanjaSeminaraDo);
            ctx.Seminari.Add(seminar);
            ctx.SaveChanges();
            int seminarId = ctx.Seminari.ToList().OrderByDescending(x => x.Id).FirstOrDefault().Id;
            for (int i = 0; i < model.ucesniciId.Count(); i++)
            {
                UcesniciSeminara ucesnik = new UcesniciSeminara();
                ucesnik.isDeleted = false;
                ucesnik.SeminariId = seminarId;
                ucesnik.OsobaId = model.ucesniciId[i];
                ctx.UcesniciSeminara.Add(ucesnik);
                ctx.SaveChanges();

            }
            return RedirectToAction("Index", "Seminari", new { });
        }
        public ActionResult Uredi(int seminarId)
        {
            Seminari seminar = ctx.Seminari.Where(x => x.Id == seminarId).FirstOrDefault();
            SeminariUrediVM model = new SeminariUrediVM
            {
                Id = seminarId,
                isDeleted = seminar.isDeleted,
                NazivSeminara = seminar.NazivSeminara,
                OrganizatorSeminara = seminar.OrganizatorSeminara,
                DatumOdrzavanjaSeminaraOd = seminar.DatumOdrzavanjaSeminaraOd.ToString("dd.MM.yyyy"),
                DatumOdrzavanjaSeminaraDo = seminar.DatumOdrzavanjaSeminaraDo.ToString("dd.MM.yyyy"),
                MjestoOdrzavanjaSeminara = seminar.MjestoOdrzavanjaSeminara,
                Obrazlozenje = seminar.Obrazlozenje,
                ucesnici = BindUcesnikeSeminara()
            };
            List<UcesniciSeminara> ucesniciSeminara = ctx.UcesniciSeminara.Where(x => x.isDeleted == false && x.SeminariId == seminarId).ToList();
            List<int> ucesniciSeminaraId = new List<int>();
            for (int i = 0; i < ucesniciSeminara.Count(); i++)
            {
                ucesniciSeminaraId.Add(ucesniciSeminara[i].OsobaId);

            }
            model.ucesniciId = ucesniciSeminaraId;
            model.ucesnici.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite učesnike-" });

            return View("Uredi", model);
        }
        public ActionResult SpremiIzmjenuSeminara(SeminariUrediVM model)
        {
            Seminari seminar = ctx.Seminari.Where(x => x.Id == model.Id).FirstOrDefault();
            seminar.NazivSeminara = model.NazivSeminara;
            seminar.MjestoOdrzavanjaSeminara = model.MjestoOdrzavanjaSeminara;
            seminar.OrganizatorSeminara = model.OrganizatorSeminara;
            seminar.Obrazlozenje = model.Obrazlozenje;
            if (seminar.DatumOdrzavanjaSeminaraOd != null)
                seminar.DatumOdrzavanjaSeminaraOd = KonvertujUDatum_dd_mm_yyyy(model.DatumOdrzavanjaSeminaraOd);
            if (seminar.DatumOdrzavanjaSeminaraDo != null)
                seminar.DatumOdrzavanjaSeminaraDo = KonvertujUDatum_dd_mm_yyyy(model.DatumOdrzavanjaSeminaraDo);
            ctx.SaveChanges();
            List<UcesniciSeminara> ucesniciSeminara = ctx.UcesniciSeminara.Where(x => x.SeminariId == seminar.Id && x.isDeleted == false).ToList();
            List<int> listaUcesnikaId = new List<int>();
            for (int i = 0; i < ucesniciSeminara.Count(); i++)
            {
                int ucesnikId = ucesniciSeminara[i].OsobaId;
                listaUcesnikaId.Add(ucesnikId);
            }
            for (int i = 0; i < model.ucesniciId.Count(); i++)
            {
                int ucesnikId = model.ucesniciId[i];

                if (!listaUcesnikaId.Contains(ucesnikId))
                {
                    UcesniciSeminara ucesnik = new UcesniciSeminara();
                    ucesnik.isDeleted = false;
                    ucesnik.SeminariId = seminar.Id;
                    ucesnik.OsobaId = model.ucesniciId[i];
                    ctx.UcesniciSeminara.Add(ucesnik);
                    ctx.SaveChanges();
                }
            }
            List<int> odabraniUcesniciId = new List<int>();
            for (int i = 0; i < model.ucesniciId.Count(); i++)
            {
                int ucesnikId = model.ucesniciId[i];
                odabraniUcesniciId.Add(ucesnikId);
            }
            for (int i = 0; i < listaUcesnikaId.Count(); i++)
            {
                int ucesnikId = listaUcesnikaId[i];
                if (!odabraniUcesniciId.Contains(ucesnikId))
                {
                    UcesniciSeminara ucesnik = ctx.UcesniciSeminara.Where(x => x.OsobaId == ucesnikId && x.isDeleted == false).FirstOrDefault();
                    if (ucesnik != null)
                    {
                        ucesnik.isDeleted = true;
                        ctx.SaveChanges();
                    }

                }


            }

            return RedirectToAction("Index", "Seminari", new { });
        }
        public JsonResult Obrisi(int seminarId)
        {
            Seminari seminar = ctx.Seminari.Where(x => x.Id == seminarId).FirstOrDefault();
            List<UcesniciSeminara> ucesnici = ctx.UcesniciSeminara.Where(x => x.SeminariId == seminarId && x.isDeleted == false).ToList();
            List<TroskoviSeminara> troskovi = ctx.TroskoviSeminara.Where(x => x.SeminarId == seminarId && x.isDeleted == false).ToList();
            List<SteceneLicence> licence = ctx.SteceneLicence.Where(x => x.SeminarId == seminarId && x.isDeleted == false).ToList();
            List<ObnoveLicenci> obnove = ctx.ObnoveLicenci.Where(x => x.SeminarId == seminarId && x.isDeleted == false).ToList();

            for (int i = 0; i < troskovi.Count(); i++)
            {
                troskovi[i].isDeleted = true;
                ctx.SaveChanges();
            }
            for (int i = 0; i < obnove.Count(); i++)
            {
                obnove[i].isDeleted = true;
                ctx.SaveChanges();
            }
            for (int i = 0; i < licence.Count(); i++)
            {
                licence[i].isDeleted = true;
                ctx.SaveChanges();
            }
            for (int i = 0; i < ucesnici.Count(); i++)
            {
                ucesnici[i].isDeleted = true;
                ctx.SaveChanges();
            }
            if (seminar != null)
                seminar.isDeleted = true;

            ctx.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}