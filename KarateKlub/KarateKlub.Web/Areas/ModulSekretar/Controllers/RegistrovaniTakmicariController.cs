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

    public class RegistrovaniTakmicariController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulSekretar/RegistrovaniTakmicari
        public ActionResult Index(int registracijaId, int savez)
        {
            RegistrovaniTakmicariIndexVM model = new RegistrovaniTakmicariIndexVM
            {
                registrovaniTakmicari = ctx.RegistrovaniTakmicari.Where(x => x.isDeleted == false && x.RegistracijaTakmicaraId == registracijaId).Select(x => new RegistrovaniTakmicarPodaci
                {
                    Id = x.Id,
                    Takmicar = x.ClanKluba.Osoba.Ime + " (" + x.ClanKluba.Osoba.ImeRoditelja + ") " + x.ClanKluba.Osoba.Prezime,
                    DatumRodjenja = x.ClanKluba.Osoba.DatumRodjenja,
                    MjestoRodjenja = x.ClanKluba.Osoba.MjestoRodjenja,
                    JMBG = x.ClanKluba.Osoba.JMBG,
                    ZvanjeUKarateu = x.ClanKluba.ZvanjeUKarateu.Naziv,
                    StarosnaDob = x.ClanKluba.StarosnaDob.Naziv
                }).ToList(),
                registracijaTakmicaraId = registracijaId
            };
            ViewData["savez"] = savez;
            return View(model);
        }


        public ActionResult Dodaj(int registracijaId, int savez)
        {
            RegistrovaniTakmicariDodajVM model = new RegistrovaniTakmicariDodajVM
            {
                savez = savez,
                takmicariKluba = BindTakmicare(),
                RegistracijaTakmicaraId = registracijaId

            };
            model.takmicariKluba.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite takmičare-" });
            List<RegistrovaniTakmicari> registrovaniTakmicari = ctx.RegistrovaniTakmicari.Where(x => x.RegistracijaTakmicaraId == registracijaId && x.isDeleted==false).ToList();
            List<string> listaId = new List<string>();
            for (int i = 0; i < model.takmicariKluba.Count(); i++)
            {
                for (int j = 0; j < registrovaniTakmicari.Count(); j++)
                {

                    if (registrovaniTakmicari[j].ClanKlubaId.ToString() ==model.takmicariKluba[i].Value && registrovaniTakmicari[j].isDeleted==false)
                    {
                        string value = registrovaniTakmicari[j].ClanKlubaId.ToString();
                        listaId.Add(value);
                        
                    }
                    }
            }
            for (int i = 0; i < listaId.Count(); i++)
            {
                var item = model.takmicariKluba.First(x => x.Value == listaId[i]);
                model.takmicariKluba.Remove(item);
            }
            return View("Dodaj", model);
        }

        private List<SelectListItem> BindTakmicare()
        {
          
                return ctx.Takmicari.Where(x => x.isDeleted == false && x.ClanKluba.Osoba.isAktivnaOsoba == true && x.isAktivanTakmicar == true).Select(x => new SelectListItem { Value = x.ClanKlubaId.ToString(), Text = x.ClanKluba.Osoba.Ime + " (" + x.ClanKluba.Osoba.ImeRoditelja + ") " + x.ClanKluba.Osoba.Prezime }).ToList();

        }
        public ActionResult Uredi(int registracijaId, int savez)
        {
            List<RegistrovaniTakmicari> takmicari = ctx.RegistrovaniTakmicari.Where(x => x.RegistracijaTakmicaraId == registracijaId && x.isDeleted == false).ToList();
            List<int> clanKlubaId = new List<int>();
            for (int i = 0; i < takmicari.Count(); i++)
            {
                clanKlubaId.Add(takmicari[i].ClanKlubaId);
            }
            RegistrovaniTakmicariUrediVM model = new RegistrovaniTakmicariUrediVM
            {
                takmicariKluba = BindTakmicare()


            };
            model.takmicariKluba.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite takmičare-" });
            model.ClanKlubaId = clanKlubaId;
            model.RegistracijaTakmicaraId = registracijaId;
            return View("Uredi", model);
        }

       

        public ActionResult SpremiRegistrovaneTakmicare(RegistrovaniTakmicariDodajVM model)
        {
            RegistrovaniTakmicari registrovaniTakmicar;
            for (int i = 0; i < model.ClanKlubaId.Count(); i++)
            {
                registrovaniTakmicar = new RegistrovaniTakmicari();
                registrovaniTakmicar.isDeleted = false;
                registrovaniTakmicar.RegistracijaTakmicaraId = model.RegistracijaTakmicaraId;
                registrovaniTakmicar.ClanKlubaId = model.ClanKlubaId[i];
                ctx.RegistrovaniTakmicari.Add(registrovaniTakmicar);
                ctx.SaveChanges();
            }
            return RedirectToAction("Index", new { registracijaId = model.RegistracijaTakmicaraId, savez = model.savez });
        }


        public JsonResult Obrisi(int registrovaniTakmicarId)
        {

            RegistrovaniTakmicari takmicar = ctx.RegistrovaniTakmicari.Where(x => x.Id == registrovaniTakmicarId).FirstOrDefault();
           takmicar.isDeleted = true;
            ctx.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}