using KarateKlub.Data;
using KarateKlub.Data.Models;
using KarateKlub.Web.Areas.ModulSekretar.Models;
using KarateKlub.Web.Helper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulSekretar.Controllers
{
    [Autorizacija(false, true, false, false, false)]

    public class RezultatiTakmicenjaController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulSekretar/RezultatiTakmicenja
        public ActionResult Index(int takmicenjeId)
        {
            RezultatiTakmicenjaIndexVM model = new RezultatiTakmicenjaIndexVM {
               rezultatiTakmicenja=ctx.RezultatiTakmicenja.Where(x=>x.TakmicenjeId==takmicenjeId && x.isDeleted==false).Select(x=>new RezultatTakmicenjaPodaci {
                   Id=x.Id,
                   isDeleted=x.isDeleted,
                   TakmicenjeId=x.TakmicenjeId,
                   Takmicenje=x.Takmicenje.NazivTakmicenja,
                   TakmicarId=x.TakmicarId,
                   Takmicar=x.Takmicar.ClanKluba.Osoba.Ime+" ("+x.Takmicar.ClanKluba.Osoba.ImeRoditelja+") "+x.Takmicar.ClanKluba.Osoba.Prezime,
                   DisciplinaTakmicenjaId=x.DisciplinaTakmicenjaId,
                   DisciplinaTakmicenja=x.DisciplinaTakmicenja.Naziv,
                   Kategorija=x.Kategorija,
                   BrojTakmicaraUKategoriji=x.BrojTakmicaraUKategoriji.ToString(),
                   BrojPobjeda=x.BrojPobjeda.ToString(),
                   BrojPoraza=x.BrojPoraza.ToString(),
                   OsvojenoMjestoNaTakmicenjuId=x.OsvojenoMjestoNaTakmicenjuId,
                   OsvojenoMjestoNaTakmicenju=x.OsvojenoMjestoNaTakmicenju.Naziv,
                   Obrazlozenje=x.Obrazlozenje,
                   StarosnaDobId=x.StarosnaDobId,
                   StarosnaDob=x.StarosnaDob.Naziv
               }).ToList(),
               NazivTakmicenja=ctx.Takmicenja.Where(x=>x.Id==takmicenjeId).FirstOrDefault().NazivTakmicenja,
               takmicenjeId=takmicenjeId
            };
            for (int i = 0; i < model.rezultatiTakmicenja.Count(); i++)
            {
                if (model.rezultatiTakmicenja[i].BrojPobjeda == "0")
                    model.rezultatiTakmicenja[i].BrojPobjeda = "/";
                if (model.rezultatiTakmicenja[i].BrojPoraza == "0")
                    model.rezultatiTakmicenja[i].BrojPoraza = "/";
                if (model.rezultatiTakmicenja[i].BrojTakmicaraUKategoriji == "0")
                    model.rezultatiTakmicenja[i].BrojTakmicaraUKategoriji = "/";
            }
            return View(model);
        }
        public ActionResult Index2(int takmicarId)
        {
            RezultatiTakmicenjaIndexVM model = new RezultatiTakmicenjaIndexVM
            {
                rezultatiTakmicenja = ctx.RezultatiTakmicenja.Where(x => x.TakmicarId == takmicarId && x.isDeleted == false).Select(x => new RezultatTakmicenjaPodaci
                {
                    Id = x.Id,
                    isDeleted = x.isDeleted,
                    TakmicenjeId = x.TakmicenjeId,
                    Takmicenje = x.Takmicenje.NazivTakmicenja,
                    TakmicarId = x.TakmicarId,
                    Takmicar = x.Takmicar.ClanKluba.Osoba.Ime + " (" + x.Takmicar.ClanKluba.Osoba.ImeRoditelja + ") " + x.Takmicar.ClanKluba.Osoba.Prezime,
                    DisciplinaTakmicenjaId = x.DisciplinaTakmicenjaId,
                    DisciplinaTakmicenja = x.DisciplinaTakmicenja.Naziv,
                    Kategorija = x.Kategorija,
                    BrojTakmicaraUKategoriji = x.BrojTakmicaraUKategoriji.ToString(),
                    BrojPobjeda = x.BrojPobjeda.ToString(),
                    BrojPoraza = x.BrojPoraza.ToString(),
                    OsvojenoMjestoNaTakmicenjuId = x.OsvojenoMjestoNaTakmicenjuId,
                    OsvojenoMjestoNaTakmicenju = x.OsvojenoMjestoNaTakmicenju.Naziv,
                    Obrazlozenje = x.Obrazlozenje,
                    StarosnaDobId = x.StarosnaDobId,
                    StarosnaDob = x.StarosnaDob.Naziv,
                    Datum=x.Takmicenje.DatumOdrzavanjaTakmicenja
                }).ToList()
                
            };
            for (int i = 0; i < model.rezultatiTakmicenja.Count(); i++)
            {
                if (model.rezultatiTakmicenja[i].BrojPobjeda == "0")
                    model.rezultatiTakmicenja[i].BrojPobjeda = "/";
                if (model.rezultatiTakmicenja[i].BrojPoraza == "0")
                    model.rezultatiTakmicenja[i].BrojPoraza = "/";
                if (model.rezultatiTakmicenja[i].BrojTakmicaraUKategoriji == "0")
                    model.rezultatiTakmicenja[i].BrojTakmicaraUKategoriji = "/";
            }
            return View("Index2",model);
        }
        public ActionResult PregledSvihRezultata(string DatumOd="",string DatumDo="")
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
                RezultatiTakmicenjaIndexVM model = new RezultatiTakmicenjaIndexVM
                {
                    rezultatiTakmicenja = ctx.RezultatiTakmicenja.Where(x => x.isDeleted == false && x.Takmicenje.DatumOdrzavanjaTakmicenja>=datumOd && x.Takmicenje.DatumOdrzavanjaTakmicenja<=datumDo).Select(x => new RezultatTakmicenjaPodaci
                    {
                        Id = x.Id,
                        isDeleted = x.isDeleted,
                        TakmicenjeId = x.TakmicenjeId,
                        Takmicenje = x.Takmicenje.NazivTakmicenja,
                        TakmicarId = x.TakmicarId,
                        Takmicar = x.Takmicar.ClanKluba.Osoba.Ime + " (" + x.Takmicar.ClanKluba.Osoba.ImeRoditelja + ") " + x.Takmicar.ClanKluba.Osoba.Prezime,
                        DisciplinaTakmicenjaId = x.DisciplinaTakmicenjaId,
                        DisciplinaTakmicenja = x.DisciplinaTakmicenja.Naziv,
                        Kategorija = x.Kategorija,
                        BrojTakmicaraUKategoriji = x.BrojTakmicaraUKategoriji.ToString(),
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
                for (int i = 0; i < model.rezultatiTakmicenja.Count(); i++)
                {
                    if (model.rezultatiTakmicenja[i].BrojPobjeda == "0")
                        model.rezultatiTakmicenja[i].BrojPobjeda = "/";
                    if (model.rezultatiTakmicenja[i].BrojPoraza == "0")
                        model.rezultatiTakmicenja[i].BrojPoraza = "/";
                    if (model.rezultatiTakmicenja[i].BrojTakmicaraUKategoriji == "0")
                        model.rezultatiTakmicenja[i].BrojTakmicaraUKategoriji = "/";
                }
                return View("PregledSvihRezultata", model);
            }
            else
            {

                RezultatiTakmicenjaIndexVM model = new RezultatiTakmicenjaIndexVM
                {
                    rezultatiTakmicenja = ctx.RezultatiTakmicenja.Where(x => x.isDeleted == false).Select(x => new RezultatTakmicenjaPodaci
                    {
                        Id = x.Id,
                        isDeleted = x.isDeleted,
                        TakmicenjeId = x.TakmicenjeId,
                        Takmicenje = x.Takmicenje.NazivTakmicenja,
                        TakmicarId = x.TakmicarId,
                        Takmicar = x.Takmicar.ClanKluba.Osoba.Ime + " (" + x.Takmicar.ClanKluba.Osoba.ImeRoditelja + ") " + x.Takmicar.ClanKluba.Osoba.Prezime,
                        DisciplinaTakmicenjaId = x.DisciplinaTakmicenjaId,
                        DisciplinaTakmicenja = x.DisciplinaTakmicenja.Naziv,
                        Kategorija = x.Kategorija,
                        BrojTakmicaraUKategoriji = x.BrojTakmicaraUKategoriji.ToString(),
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
                for (int i = 0; i < model.rezultatiTakmicenja.Count(); i++)
                {
                    if (model.rezultatiTakmicenja[i].BrojPobjeda == "0")
                        model.rezultatiTakmicenja[i].BrojPobjeda = "/";
                    if (model.rezultatiTakmicenja[i].BrojPoraza == "0")
                        model.rezultatiTakmicenja[i].BrojPoraza = "/";
                    if (model.rezultatiTakmicenja[i].BrojTakmicaraUKategoriji == "0")
                        model.rezultatiTakmicenja[i].BrojTakmicaraUKategoriji = "/";
                }
                return View("PregledSvihRezultata", model);
            }
         

        }
        public ActionResult Dodaj(int takmicenjeId)
        {
            RezultatiTakmicenjaDodajVM model = new RezultatiTakmicenjaDodajVM {
               takmicari=BindTakmicare(),
               disciplinaTakmicenja=BindDisciplineTakmicenja(),
               osvojenaMjestaNaTakmicenju=BindOsvojenaMjestaNaTakmicenju(),
               TakmicenjeId=takmicenjeId,
               StarosneDobi=BindStarosneDobi()
            };
            model.StarosneDobi.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite starosnu dob-" });
            model.takmicari.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite takmičara-" });
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
            return ctx.Takmicari.OrderBy(x=>x.ClanKluba.Osoba.Ime).Where(x => x.isDeleted == false && x.isAktivanTakmicar==true).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.ClanKluba.Osoba.Ime + " (" + x.ClanKluba.Osoba.ImeRoditelja + ") " + x.ClanKluba.Osoba.Prezime }).ToList();

        }

        public ActionResult SpremiNoviRezultatTakmicenja(RezultatiTakmicenjaDodajVM model)
        {
            RezultatiTakmicenja rezultat = new RezultatiTakmicenja();
            rezultat.TakmicenjeId = model.TakmicenjeId;
            rezultat.isDeleted = false;
            rezultat.TakmicarId = model.TakmicarId;
            rezultat.OsvojenoMjestoNaTakmicenjuId=model.OsvojenoMjestoNaTakmicenjuId;
            rezultat.DisciplinaTakmicenjaId = model.DisciplinaTakmicenjaId;
            rezultat.BrojPobjeda =Convert.ToInt32(model.BrojPobjeda);
            rezultat.BrojPoraza =Convert.ToInt32(model.BrojPoraza);
            rezultat.Obrazlozenje = model.Obrazlozenje;
            rezultat.Kategorija = model.Kategorija;
            rezultat.BrojTakmicaraUKategoriji =Convert.ToInt32(model.BrojTakmicaraUKategoriji);
            rezultat.StarosnaDobId = model.StarosnaDobId;
            ctx.RezultatiTakmicenja.Add(rezultat);
            ctx.SaveChanges();

            return RedirectToAction("Index","UpravljanjeTakmicenjima", new { takmicenjeId = model.TakmicenjeId,brojTaba=1,brojTabaRezultati=1 });

        }
        public ActionResult Uredi(int rezultatId)
        {
            RezultatiTakmicenja rezultatTakmicenja = ctx.RezultatiTakmicenja.Where(x => x.Id == rezultatId).FirstOrDefault();
            RezultatiTakmicenjaUrediVM model = new RezultatiTakmicenjaUrediVM {
                Id=rezultatId,
                isDeleted=rezultatTakmicenja.isDeleted,
                TakmicarId=rezultatTakmicenja.TakmicarId,
                TakmicenjeId=rezultatTakmicenja.TakmicenjeId,
                OsvojenoMjestoNaTakmicenjuId=rezultatTakmicenja.OsvojenoMjestoNaTakmicenjuId,
                osvojenaMjestaNaTakmicenju=BindOsvojenaMjestaNaTakmicenju(),
                DisciplinaTakmicenjaId=rezultatTakmicenja.DisciplinaTakmicenjaId,
                disciplinaTakmicenja=BindDisciplineTakmicenja(),
                takmicari=BindTakmicare(),
                BrojPobjeda=rezultatTakmicenja.BrojPobjeda.ToString(),
                BrojPoraza=rezultatTakmicenja.BrojPoraza.ToString(),
                Obrazlozenje=rezultatTakmicenja.Obrazlozenje,
                Kategorija=rezultatTakmicenja.Kategorija,
                BrojTakmicaraUKategoriji=rezultatTakmicenja.BrojTakmicaraUKategoriji.ToString(),
                StarosneDobi=BindStarosneDobi(),
                StarosnaDobId=rezultatTakmicenja.StarosnaDobId
            };
            model.StarosneDobi.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite starosnu dob-" });
            model.takmicari.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite takmičara-" });
            model.disciplinaTakmicenja.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite disciplinu-" });
            model.osvojenaMjestaNaTakmicenju.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite osvojeno mjesto-" });
         
            return View("Uredi",model);
        }
        public ActionResult SpremiIzmjenuRezultataTakmicenja(RezultatiTakmicenjaUrediVM model)
        {
            RezultatiTakmicenja rezultatTakmicenja = ctx.RezultatiTakmicenja.Where(x => x.Id == model.Id).FirstOrDefault();

            rezultatTakmicenja.TakmicarId = model.TakmicarId;
            rezultatTakmicenja.OsvojenoMjestoNaTakmicenjuId = model.OsvojenoMjestoNaTakmicenjuId;
            rezultatTakmicenja.DisciplinaTakmicenjaId = model.DisciplinaTakmicenjaId;
            rezultatTakmicenja.BrojPobjeda = Convert.ToInt32(model.BrojPobjeda);
            rezultatTakmicenja.BrojPoraza = Convert.ToInt32(model.BrojPoraza);
            rezultatTakmicenja.Obrazlozenje = model.Obrazlozenje;
            rezultatTakmicenja.Kategorija = model.Kategorija;
            rezultatTakmicenja.BrojTakmicaraUKategoriji = Convert.ToInt32(model.BrojTakmicaraUKategoriji);
            rezultatTakmicenja.StarosnaDobId = model.StarosnaDobId;
            ctx.SaveChanges();

            return RedirectToAction("Index", "UpravljanjeTakmicenjima", new { takmicenjeId = model.TakmicenjeId, brojTaba = 1, brojTabaRezultati = 1 });


        }
        public JsonResult Obrisi(int rezultatId)
        {
            RezultatiTakmicenja rezultatTakmicenja = ctx.RezultatiTakmicenja.Where(x => x.Id == rezultatId).FirstOrDefault();
            if (rezultatTakmicenja != null)
                rezultatTakmicenja.isDeleted = true;
            ctx.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}