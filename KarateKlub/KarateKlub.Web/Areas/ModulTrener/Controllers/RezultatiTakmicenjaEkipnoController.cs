using KarateKlub.Data;
using KarateKlub.Data.Models;
using KarateKlub.Web.Areas.ModulTrener.Models;
using KarateKlub.Web.Helper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulTrener.Controllers
{
    [Autorizacija(false, false, false, true, false)]

    public class RezultatiTakmicenjaEkipnoController : Controller
    {
        MojContext ctx = new MojContext();

        // GET: ModulTrener/RezultatiTakmicenjaEkipno
        public ActionResult Index(int takmicenjeId)
        {

            RezultatiTakmicenjEkipnoIndexVM model = new RezultatiTakmicenjEkipnoIndexVM
            {
                rezultatiEkipno = ctx.RezultatiTakmicenjaEkipno.Where(x => x.TakmicenjeId == takmicenjeId && x.isDeleted == false).Select(x => new RezultatiTakmicenjaEkipnoPodaci
                {
                    Id = x.Id,
                    isDeleted = x.isDeleted,
                    TakmicenjeId = x.TakmicenjeId,
                    Takmicenja = x.Takmicenje.NazivTakmicenja,
                    EkipaId = x.EkipaId,
                    Ekipa = x.Ekipa.Naziv,
                    DisciplinaTakmicenjaId = x.DisciplinaTakmicenjaId,
                    DisciplinaTakmicenja = x.DisciplinaTakmicenja.Naziv,
                    Kategorija = x.Kategorija,
                    BrojEkipaUKategoriji = x.BrojEkipaUKategoriji.ToString(),
                    BrojPobjeda = x.BrojPobjeda.ToString(),
                    BrojPoraza = x.BrojPoraza.ToString(),
                    OsvojenoMjestoNaTakmicenjuId = x.OsvojenoMjestoNaTakmicenjuId,
                    OsvojenoMjestoNaTakmicenju = x.OsvojenoMjestoNaTakmicenju.Naziv,
                    Obrazlozenje = x.Obrazlozenje,
                    StarosnaDobId = x.StarosnaDobId,
                    StarosnaDob = x.StarosnaDob.Naziv

                }).ToList(),
                takmicenjeId = takmicenjeId
            };
            for (int i = 0; i < model.rezultatiEkipno.Count(); i++)
            {
                if (model.rezultatiEkipno[i].BrojPobjeda == "0")
                    model.rezultatiEkipno[i].BrojPobjeda = "/";
                if (model.rezultatiEkipno[i].BrojPoraza == "0")
                    model.rezultatiEkipno[i].BrojPoraza = "/";
                if (model.rezultatiEkipno[i].BrojEkipaUKategoriji == "0")
                    model.rezultatiEkipno[i].BrojEkipaUKategoriji = "/";
            }

            return View(model);
        }
        public ActionResult PregledSvihRezultata(string DatumOd = "", string DatumDo = "")
        {
            if (DatumOd != "" && DatumDo != "")
            {
                string danDatumOd = DatumOd.Substring(0, 2);
                string mjesecDatumOd = DatumOd.Substring(3, 2);
                string godinaDatumOd = DatumOd.Substring(6, 4);

                string danDatumDo = DatumDo.Substring(0, 2);
                string mjesecDatumDo = DatumDo.Substring(3, 2);
                string godinaDatumDo = DatumDo.Substring(6, 4);


                string dOd = mjesecDatumOd + "/" + danDatumOd + "/" + godinaDatumOd;
                string dDo = mjesecDatumDo + "/" + danDatumDo + "/" + godinaDatumDo;

                CultureInfo provider = new CultureInfo("en-US");

                DateTime datumOd = DateTime.ParseExact(dOd, "MM/dd/yyyy",
                                  provider);

                DateTime datumDo = DateTime.ParseExact(dDo, "MM/dd/yyyy",
                                  provider);

                RezultatiTakmicenjEkipnoIndexVM model = new RezultatiTakmicenjEkipnoIndexVM
                {
                    rezultatiEkipno = ctx.RezultatiTakmicenjaEkipno.Where(x => x.isDeleted == false && x.Takmicenje.DatumOdrzavanjaTakmicenja >= datumOd && x.Takmicenje.DatumOdrzavanjaTakmicenja <= datumDo).Select(x => new RezultatiTakmicenjaEkipnoPodaci
                    {
                        Id = x.Id,
                        isDeleted = x.isDeleted,
                        TakmicenjeId = x.TakmicenjeId,
                        Takmicenja = x.Takmicenje.NazivTakmicenja,
                        EkipaId = x.EkipaId,
                        Ekipa = x.Ekipa.Naziv,
                        DisciplinaTakmicenjaId = x.DisciplinaTakmicenjaId,
                        DisciplinaTakmicenja = x.DisciplinaTakmicenja.Naziv,
                        Kategorija = x.Kategorija,
                        BrojEkipaUKategoriji = x.BrojEkipaUKategoriji.ToString(),
                        BrojPobjeda = x.BrojPobjeda.ToString(),
                        BrojPoraza = x.BrojPoraza.ToString(),
                        OsvojenoMjestoNaTakmicenjuId = x.OsvojenoMjestoNaTakmicenjuId,
                        OsvojenoMjestoNaTakmicenju = x.OsvojenoMjestoNaTakmicenju.Naziv,
                        Obrazlozenje = x.Obrazlozenje,
                        StarosnaDobId = x.StarosnaDobId,
                        StarosnaDob = x.StarosnaDob.Naziv,
                        DatumOdrzavanjaTakmicenja = x.Takmicenje.DatumOdrzavanjaTakmicenja,
                        MjestoOdrzavanja = x.Takmicenje.MjestoOdrzavanjaTakmicenja

                    }).ToList()
                };
                for (int i = 0; i < model.rezultatiEkipno.Count(); i++)
                {
                    if (model.rezultatiEkipno[i].BrojPobjeda == "0")
                        model.rezultatiEkipno[i].BrojPobjeda = "/";
                    if (model.rezultatiEkipno[i].BrojPoraza == "0")
                        model.rezultatiEkipno[i].BrojPoraza = "/";
                    if (model.rezultatiEkipno[i].BrojEkipaUKategoriji == "0")
                        model.rezultatiEkipno[i].BrojEkipaUKategoriji = "/";
                }

                return View("PregledSvihRezultata", model);
            }
            else
            {
                RezultatiTakmicenjEkipnoIndexVM model = new RezultatiTakmicenjEkipnoIndexVM
                {
                    rezultatiEkipno = ctx.RezultatiTakmicenjaEkipno.Where(x => x.isDeleted == false).Select(x => new RezultatiTakmicenjaEkipnoPodaci
                    {
                        Id = x.Id,
                        isDeleted = x.isDeleted,
                        TakmicenjeId = x.TakmicenjeId,
                        Takmicenja = x.Takmicenje.NazivTakmicenja,
                        EkipaId = x.EkipaId,
                        Ekipa = x.Ekipa.Naziv,
                        DisciplinaTakmicenjaId = x.DisciplinaTakmicenjaId,
                        DisciplinaTakmicenja = x.DisciplinaTakmicenja.Naziv,
                        Kategorija = x.Kategorija,
                        BrojEkipaUKategoriji = x.BrojEkipaUKategoriji.ToString(),
                        BrojPobjeda = x.BrojPobjeda.ToString(),
                        BrojPoraza = x.BrojPoraza.ToString(),
                        OsvojenoMjestoNaTakmicenjuId = x.OsvojenoMjestoNaTakmicenjuId,
                        OsvojenoMjestoNaTakmicenju = x.OsvojenoMjestoNaTakmicenju.Naziv,
                        Obrazlozenje = x.Obrazlozenje,
                        StarosnaDobId = x.StarosnaDobId,
                        StarosnaDob = x.StarosnaDob.Naziv,
                        DatumOdrzavanjaTakmicenja = x.Takmicenje.DatumOdrzavanjaTakmicenja,
                        MjestoOdrzavanja = x.Takmicenje.MjestoOdrzavanjaTakmicenja

                    }).ToList()
                };
                for (int i = 0; i < model.rezultatiEkipno.Count(); i++)
                {
                    if (model.rezultatiEkipno[i].BrojPobjeda == "0")
                        model.rezultatiEkipno[i].BrojPobjeda = "/";
                    if (model.rezultatiEkipno[i].BrojPoraza == "0")
                        model.rezultatiEkipno[i].BrojPoraza = "/";
                    if (model.rezultatiEkipno[i].BrojEkipaUKategoriji == "0")
                        model.rezultatiEkipno[i].BrojEkipaUKategoriji = "/";
                }

                return View("PregledSvihRezultata", model);
            }
        }

        public ActionResult Dodaj(int takmicenjeId)
        {
            RezultatiTakmicenjaEkipnoDodajVM model = new RezultatiTakmicenjaEkipnoDodajVM
            {
                clanoviEkipe = BindTakmicare(),
                disciplinaTakmicenja = BindDisciplineTakmicenja(),
                osvojenaMjestaNaTakmicenju = BindOsvojenaMjestaNaTakmicenju(),
                TakmicenjeId = takmicenjeId,
                StarosneDobi = BindStarosneDobi()
            };
            model.StarosneDobi.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite starosnu dob-" });
            model.clanoviEkipe.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite takmičare-" });
            model.disciplinaTakmicenja.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite disciplinu-" });
            model.osvojenaMjestaNaTakmicenju.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite osvojeno mjesto-" });


            return View("Dodaj", model);
        }



        private List<SelectListItem> BindStarosneDobi()
        {
            return ctx.StarosneDobi.Where(x => x.isDeleted == false).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv }).ToList();

        }

        private List<SelectListItem> BindOsvojenaMjestaNaTakmicenju()
        {
            return ctx.OsvojenaMjestaNaTakmicenju.Where(x => x.isDeleted == false).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv }).ToList();

        }

        private List<SelectListItem> BindDisciplineTakmicenja()
        {
            return ctx.DisciplineTakmicenja.Where(x => x.isDeleted == false).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv }).ToList();

        }

        private List<SelectListItem> BindTakmicare()
        {
            return ctx.Takmicari.Where(x => x.isDeleted == false && x.isAktivanTakmicar == true).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.ClanKluba.Osoba.Ime + " (" + x.ClanKluba.Osoba.ImeRoditelja + ") " + x.ClanKluba.Osoba.Prezime }).ToList();

        }

        public ActionResult SpremiNoviRezultatTakmicenjaEkipno(RezultatiTakmicenjaEkipnoDodajVM model)
        {
            Ekipa ekipa = new Ekipa();
            ekipa.isDeleted = false;
            ekipa.Naziv = model.NazivEkipe;
            ctx.Ekipa.Add(ekipa);
            ctx.SaveChanges();
            int EkipaId = ctx.Ekipa.ToList().OrderByDescending(x => x.Id).FirstOrDefault().Id;

            for (int i = 0; i < model.clanoviEkipeId.Count(); i++)
            {
                ClanoviEkipe clan = new ClanoviEkipe();
                clan.isDeleted = false;
                clan.TakmicarId = model.clanoviEkipeId[i];
                clan.EkipaId = EkipaId;
                ctx.ClanoviEkipe.Add(clan);
                ctx.SaveChanges();
            }

            RezultatiTakmicenjaEkipno rezultat = new RezultatiTakmicenjaEkipno();
            rezultat.TakmicenjeId = model.TakmicenjeId;
            rezultat.isDeleted = false;
            rezultat.EkipaId = EkipaId;
            rezultat.OsvojenoMjestoNaTakmicenjuId = model.OsvojenoMjestoNaTakmicenjuId;
            rezultat.DisciplinaTakmicenjaId = model.DisciplinaTakmicenjaId;
            rezultat.BrojPobjeda = Convert.ToInt32(model.BrojPobjeda);
            rezultat.BrojPoraza = Convert.ToInt32(model.BrojPoraza);
            rezultat.Obrazlozenje = model.Obrazlozenje;
            rezultat.Kategorija = model.Kategorija;
            rezultat.BrojEkipaUKategoriji = Convert.ToInt32(model.BrojEkipaUKategoriji);
            rezultat.StarosnaDobId = model.StarosnaDobId;
            ctx.RezultatiTakmicenjaEkipno.Add(rezultat);
            ctx.SaveChanges();

            return RedirectToAction("Index", "UpravljanjeRezultatimaTakmicenja", new { takmicenjeId = model.TakmicenjeId, brojTaba = 2 });



        }
        public ActionResult Uredi(int rezultatId)
        {
            RezultatiTakmicenjaEkipno rezultat = ctx.RezultatiTakmicenjaEkipno.Where(x => x.Id == rezultatId).FirstOrDefault();
            RezultatiTakmicenjaEkipnoUrediVM model = new RezultatiTakmicenjaEkipnoUrediVM
            {
                Id = rezultat.Id,
                isDeleted = rezultat.isDeleted,
                TakmicenjeId = rezultat.TakmicenjeId,
                DisciplinaTakmicenjaId = rezultat.DisciplinaTakmicenjaId,
                disciplinaTakmicenja = BindDisciplineTakmicenja(),
                Kategorija = rezultat.Kategorija,
                BrojEkipaUKategoriji = rezultat.BrojEkipaUKategoriji.ToString(),
                BrojPobjeda = rezultat.BrojPobjeda.ToString(),
                BrojPoraza = rezultat.BrojPoraza.ToString(),
                OsvojenoMjestoNaTakmicenjuId = rezultat.OsvojenoMjestoNaTakmicenjuId,
                osvojenaMjestaNaTakmicenju = BindOsvojenaMjestaNaTakmicenju(),
                Obrazlozenje = rezultat.Obrazlozenje,
                NazivEkipe = rezultat.Ekipa.Naziv,
                clanoviEkipe = BindTakmicare(),
                EkipaId = rezultat.EkipaId,
                StarosnaDobId = rezultat.StarosnaDobId,
                StarosneDobi = BindStarosneDobi()
            };
            List<ClanoviEkipe> clanoviEkipe = ctx.ClanoviEkipe.Where(x => x.isDeleted == false && x.EkipaId == rezultat.EkipaId).ToList();
            List<int> clanoviId = new List<int>();
            for (int i = 0; i < clanoviEkipe.Count(); i++)
            {
                clanoviId.Add(clanoviEkipe[i].TakmicarId);

            }
            model.clanoviEkipeId = clanoviId;
            model.StarosneDobi.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite starosnu dob-" });
            model.clanoviEkipe.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite takmičare-" });
            model.disciplinaTakmicenja.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite disciplinu-" });
            model.osvojenaMjestaNaTakmicenju.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite osvojeno mjesto-" });

            return View("Uredi", model);
        }
        public ActionResult SpremiIzmjenuRezultataTakmicenjaEkipno(RezultatiTakmicenjaEkipnoUrediVM model)
        {
            Ekipa ekipa = ctx.Ekipa.Where(x => x.Id == model.EkipaId).FirstOrDefault();
            ekipa.Naziv = model.NazivEkipe;
            ctx.SaveChanges();
            List<ClanoviEkipe> clanoviEkipe = ctx.ClanoviEkipe.Where(x => x.EkipaId == ekipa.Id && x.isDeleted == false).ToList();
            List<int> listaClanoviEkipeId = new List<int>();
            for (int i = 0; i < clanoviEkipe.Count(); i++)
            {
                int takmicarId = clanoviEkipe[i].TakmicarId;
                listaClanoviEkipeId.Add(takmicarId);
            }
            for (int i = 0; i < model.clanoviEkipeId.Count(); i++)
            {
                int takmicarId = model.clanoviEkipeId[i];

                if (!listaClanoviEkipeId.Contains(takmicarId))
                {
                    ClanoviEkipe clan = new ClanoviEkipe();
                    clan.isDeleted = false;
                    clan.TakmicarId = model.clanoviEkipeId[i];
                    clan.EkipaId = ekipa.Id;
                    ctx.ClanoviEkipe.Add(clan);
                    ctx.SaveChanges();
                }
            }
            List<int> odabraniClanoviEkipeId = new List<int>();
            for (int i = 0; i < model.clanoviEkipeId.Count(); i++)
            {
                int takmicarId = model.clanoviEkipeId[i];
                odabraniClanoviEkipeId.Add(takmicarId);
            }
            for (int i = 0; i < listaClanoviEkipeId.Count(); i++)
            {
                int takmicarId = listaClanoviEkipeId[i];
                if (!odabraniClanoviEkipeId.Contains(takmicarId))
                {
                    ClanoviEkipe clan = ctx.ClanoviEkipe.Where(x => x.TakmicarId == takmicarId && x.isDeleted == false).FirstOrDefault();
                    if (clan != null)
                    {
                        clan.isDeleted = true;
                        ctx.SaveChanges();
                    }

                }


            }

            RezultatiTakmicenjaEkipno rezultat = ctx.RezultatiTakmicenjaEkipno.Where(x => x.Id == model.Id).FirstOrDefault();
            rezultat.OsvojenoMjestoNaTakmicenjuId = model.OsvojenoMjestoNaTakmicenjuId;
            rezultat.DisciplinaTakmicenjaId = model.DisciplinaTakmicenjaId;
            rezultat.BrojPobjeda = Convert.ToInt32(model.BrojPobjeda);
            rezultat.BrojPoraza = Convert.ToInt32(model.BrojPoraza);
            rezultat.Obrazlozenje = model.Obrazlozenje;
            rezultat.Kategorija = model.Kategorija;
            rezultat.BrojEkipaUKategoriji = Convert.ToInt32(model.BrojEkipaUKategoriji);
            rezultat.StarosnaDobId = model.StarosnaDobId;
            ctx.SaveChanges();

            return RedirectToAction("Index", "UpravljanjeRezultatimaTakmicenja", new { takmicenjeId = model.TakmicenjeId, brojTaba = 2 });



        }
        public JsonResult Obrisi(int rezultatId)
        {
            RezultatiTakmicenjaEkipno rezultatTakmicenja = ctx.RezultatiTakmicenjaEkipno.Where(x => x.Id == rezultatId).FirstOrDefault();
            List<ClanoviEkipe> clanoviEkipe = ctx.ClanoviEkipe.Where(x => x.EkipaId == rezultatTakmicenja.EkipaId && x.isDeleted == false).ToList();
            Ekipa ekipa = ctx.Ekipa.Where(x => x.Id == rezultatTakmicenja.EkipaId).FirstOrDefault();
            for (int i = 0; i < clanoviEkipe.Count(); i++)
            {
                clanoviEkipe[i].isDeleted = true;
                ctx.SaveChanges();
            }
            if (ekipa != null)
                ekipa.isDeleted = true;

            if (rezultatTakmicenja != null)
                rezultatTakmicenja.isDeleted = true;

            ctx.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}