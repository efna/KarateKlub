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

    public class ClanoviKlubaController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulSekretar/ClanoviKluba
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GoToUpravljanjeClanovimaKluba(int zvanje)
        {

            return RedirectToAction("Index", "UpravljanjeClanovimaKluba", new { brojTaba = 2, aktivan = 0, zvanje = zvanje });
        }

        public ActionResult GoToUpravljanjeClanovimaKluba2(int aktivan)
        {

            return RedirectToAction("Index", "UpravljanjeClanovimaKluba", new { brojTaba = 1, aktivan = aktivan, zvanje = 0 });
        }
        public ActionResult PregledClanovaKluba(int aktivan)
        {
            List<ClanoviKluba> listaClanova = new List<ClanoviKluba>();
            if (aktivan == 0)
            {
                listaClanova = ctx.ClanoviKluba.Where(x => x.isDeleted == false).ToList();
            }
            else if (aktivan == 1)
            {
                listaClanova = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba==true).ToList();

            }
            else
            {
                listaClanova = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == false).ToList();

            }
            ClanoviKlubaPregledClanovaKlubaVM model = new ClanoviKlubaPregledClanovaKlubaVM(listaClanova, aktivan);


            ViewData["aktivan"] = aktivan;
            return View("PregledClanovaKluba",model);
        }
        public ActionResult PregledClanovaKlubaPoPojasevima(int zvanje)
        {
            List<ClanoviKluba> listaClanova = new List<ClanoviKluba>();

            if (zvanje == 0)
            {
                listaClanova = ctx.ClanoviKluba.Where(x => x.isDeleted == false &&
                
                (x.ZvanjeUKarateu.Naziv.Contains("et") || x.ZvanjeUKarateu.Naziv.Contains("ET") || x.ZvanjeUKarateu.Naziv.Contains("eT")
                || x.ZvanjeUKarateu.Naziv.Contains("Et")
                ) && x.Osoba.isAktivnaOsoba == true).ToList();
            }
            else if (zvanje == 1)
            {
                listaClanova = ctx.ClanoviKluba.Where(x => x.isDeleted == false &&
                (x.ZvanjeUKarateu.Naziv.Contains("ut") || x.ZvanjeUKarateu.Naziv.Contains("UT")
              || x.ZvanjeUKarateu.Naziv.Contains("uT") || x.ZvanjeUKarateu.Naziv.Contains("Ut")
              || (x.ZvanjeUKarateu.Naziv.Contains("V") && !x.ZvanjeUKarateu.Naziv.Contains("I")))
                && x.Osoba.isAktivnaOsoba == true).ToList();

            }
            else if (zvanje == 2)
            {

                listaClanova = ctx.ClanoviKluba.Where(x => x.isDeleted == false &&
                (x.ZvanjeUKarateu.Naziv.Contains("ra") || x.ZvanjeUKarateu.Naziv.Contains("RA") || x.ZvanjeUKarateu.Naziv.Contains("rA") || x.ZvanjeUKarateu.Naziv.Contains("Ra")
              || x.ZvanjeUKarateu.Naziv.Contains("IV"))
                && x.Osoba.isAktivnaOsoba == true).ToList();

            }
            else if (zvanje == 3)
            {
                listaClanova = ctx.ClanoviKluba.Where(x => x.isDeleted == false &&
               (x.ZvanjeUKarateu.Naziv.Contains("el") || x.ZvanjeUKarateu.Naziv.Contains("EL") || x.ZvanjeUKarateu.Naziv.Contains("eL") ||
              x.ZvanjeUKarateu.Naziv.Contains("El") || x.ZvanjeUKarateu.Naziv.Contains("III"))
                && x.Osoba.isAktivnaOsoba == true).ToList();

            }
            else if (zvanje == 4)
            {
                listaClanova = ctx.ClanoviKluba.Where(x => x.isDeleted == false &&
                (x.ZvanjeUKarateu.Naziv.Contains("la") || x.ZvanjeUKarateu.Naziv.Contains("LA") || x.ZvanjeUKarateu.Naziv.Contains("lA") || x.ZvanjeUKarateu.Naziv.Contains("La")
              || (x.ZvanjeUKarateu.Naziv.Contains("II") && !x.ZvanjeUKarateu.Naziv.Contains("III")))
                && x.Osoba.isAktivnaOsoba == true).ToList();

            }
            else if (zvanje == 5)
            {

                listaClanova = ctx.ClanoviKluba.Where(x => x.isDeleted == false &&
               (x.ZvanjeUKarateu.Naziv.Contains("me") || x.ZvanjeUKarateu.Naziv.Contains("ME") || x.ZvanjeUKarateu.Naziv.Contains("mE") || x.ZvanjeUKarateu.Naziv.Contains("Me") || (x.ZvanjeUKarateu.Naziv.Contains("I") && !x.ZvanjeUKarateu.Naziv.Contains("V") && !x.ZvanjeUKarateu.Naziv.Contains("II")
              && !x.ZvanjeUKarateu.Naziv.Contains("III")
              ))
                && x.Osoba.isAktivnaOsoba == true).ToList();

            }
            else
            {
                listaClanova = ctx.ClanoviKluba.Where(x => x.isDeleted == false &&

               (x.ZvanjeUKarateu.Naziv.Contains("rn") || x.ZvanjeUKarateu.Naziv.Contains("RN") || x.ZvanjeUKarateu.Naziv.Contains("rN") || x.ZvanjeUKarateu.Naziv.Contains("Rn") || x.ZvanjeUKarateu.Naziv.Contains("1") || x.ZvanjeUKarateu.Naziv.Contains("2") ||
              x.ZvanjeUKarateu.Naziv.Contains("3") || x.ZvanjeUKarateu.Naziv.Contains("4") || x.ZvanjeUKarateu.Naziv.Contains("5") || x.ZvanjeUKarateu.Naziv.Contains("6") || x.ZvanjeUKarateu.Naziv.Contains("7")
              || x.ZvanjeUKarateu.Naziv.Contains("8") || x.ZvanjeUKarateu.Naziv.Contains("9") || x.ZvanjeUKarateu.Naziv.Contains("10"))
                && x.Osoba.isAktivnaOsoba == true).ToList();

            }

            ClanoviKlubaPregledClanovaKlubaPoPojasevimaVM model = new ClanoviKlubaPregledClanovaKlubaPoPojasevimaVM(listaClanova, zvanje);
            ViewData["zvanje"] = zvanje;
            return View("PregledClanovaKlubaPoPojasevima", model);
        }

        public ActionResult StrukturaClanovaKluba()
        {
            ClanoviKlubaStrukturaClanovaKlubaVM model = new ClanoviKlubaStrukturaClanovaKlubaVM();

            model.pocetniciDjecaŽ = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("et") || x.ZvanjeUKarateu.Naziv.Contains("ET") || x.ZvanjeUKarateu.Naziv.Contains("eT") || x.ZvanjeUKarateu.Naziv.Contains("Et"))
              && x.StarosnaDob.Naziv.Contains("je") && x.Osoba.Spol == "Ž").Count();
            model.pocetniciDjecaM = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("et") || x.ZvanjeUKarateu.Naziv.Contains("ET") || x.ZvanjeUKarateu.Naziv.Contains("eT") || x.ZvanjeUKarateu.Naziv.Contains("Et"))
              && x.StarosnaDob.Naziv.Contains("je") && x.Osoba.Spol == "M").Count();
            model.pocetniciKadetiŽ = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("et") || x.ZvanjeUKarateu.Naziv.Contains("ET") || x.ZvanjeUKarateu.Naziv.Contains("eT") || x.ZvanjeUKarateu.Naziv.Contains("Et"))
              && x.StarosnaDob.Naziv.Contains("de") && x.Osoba.Spol == "Ž").Count();
            model.pocetniciKadetiM = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("et") || x.ZvanjeUKarateu.Naziv.Contains("ET") || x.ZvanjeUKarateu.Naziv.Contains("eT") || x.ZvanjeUKarateu.Naziv.Contains("Et"))
              && x.StarosnaDob.Naziv.Contains("de") && x.Osoba.Spol == "M").Count();
            model.pocetniciJunioriŽ = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("et") || x.ZvanjeUKarateu.Naziv.Contains("ET") || x.ZvanjeUKarateu.Naziv.Contains("eT") || x.ZvanjeUKarateu.Naziv.Contains("Et"))
              && x.StarosnaDob.Naziv.Contains("un") && x.Osoba.Spol == "Ž").Count();
            model.pocetniciJunioriM = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("et") || x.ZvanjeUKarateu.Naziv.Contains("ET") || x.ZvanjeUKarateu.Naziv.Contains("eT") || x.ZvanjeUKarateu.Naziv.Contains("Et"))
              && x.StarosnaDob.Naziv.Contains("un") && x.Osoba.Spol == "M").Count();
            model.pocetniciSenioriŽ = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("et") || x.ZvanjeUKarateu.Naziv.Contains("ET") || x.ZvanjeUKarateu.Naziv.Contains("eT") || x.ZvanjeUKarateu.Naziv.Contains("Et"))
              && x.StarosnaDob.Naziv.Contains("en") && x.Osoba.Spol == "Ž").Count();
            model.pocetniciSenioriM = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("et") || x.ZvanjeUKarateu.Naziv.Contains("ET") || x.ZvanjeUKarateu.Naziv.Contains("eT") || x.ZvanjeUKarateu.Naziv.Contains("Et"))
              && x.StarosnaDob.Naziv.Contains("en") && x.Osoba.Spol == "M").Count();


            model.zutiPojasDjecaŽ = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("ut") || x.ZvanjeUKarateu.Naziv.Contains("UT")
              || x.ZvanjeUKarateu.Naziv.Contains("uT") || x.ZvanjeUKarateu.Naziv.Contains("Ut")
              || (x.ZvanjeUKarateu.Naziv.Contains("V") && !x.ZvanjeUKarateu.Naziv.Contains("I"))) && x.StarosnaDob.Naziv.Contains("je") && x.Osoba.Spol == "Ž").Count();
            model.zutiPojasDjecaM = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("ut") || x.ZvanjeUKarateu.Naziv.Contains("UT")
              || x.ZvanjeUKarateu.Naziv.Contains("uT") || x.ZvanjeUKarateu.Naziv.Contains("Ut")
              || (x.ZvanjeUKarateu.Naziv.Contains("V") && !x.ZvanjeUKarateu.Naziv.Contains("I"))) && x.StarosnaDob.Naziv.Contains("je") && x.Osoba.Spol == "M").Count();
            model.zutiPojasKadetiŽ = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("ut") || x.ZvanjeUKarateu.Naziv.Contains("UT")
              || x.ZvanjeUKarateu.Naziv.Contains("uT") || x.ZvanjeUKarateu.Naziv.Contains("Ut")
              || (x.ZvanjeUKarateu.Naziv.Contains("V") && !x.ZvanjeUKarateu.Naziv.Contains("I"))) && x.StarosnaDob.Naziv.Contains("de") && x.Osoba.Spol == "Ž").Count();
            model.zutiPojasKadetiM = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("ut") || x.ZvanjeUKarateu.Naziv.Contains("UT")
              || x.ZvanjeUKarateu.Naziv.Contains("uT") || x.ZvanjeUKarateu.Naziv.Contains("Ut")
              || (x.ZvanjeUKarateu.Naziv.Contains("V") && !x.ZvanjeUKarateu.Naziv.Contains("I"))) && x.StarosnaDob.Naziv.Contains("de") && x.Osoba.Spol == "M").Count();
            model.zutiPojasJunioriŽ = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("ut") || x.ZvanjeUKarateu.Naziv.Contains("UT")
              || x.ZvanjeUKarateu.Naziv.Contains("uT") || x.ZvanjeUKarateu.Naziv.Contains("Ut")
              || (x.ZvanjeUKarateu.Naziv.Contains("V") && !x.ZvanjeUKarateu.Naziv.Contains("I"))) && x.StarosnaDob.Naziv.Contains("un") && x.Osoba.Spol == "Ž").Count();
            model.zutiPojasJunioriM = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("ut") || x.ZvanjeUKarateu.Naziv.Contains("UT")
              || x.ZvanjeUKarateu.Naziv.Contains("uT") || x.ZvanjeUKarateu.Naziv.Contains("Ut")
              || (x.ZvanjeUKarateu.Naziv.Contains("V") && !x.ZvanjeUKarateu.Naziv.Contains("I"))) && x.StarosnaDob.Naziv.Contains("un") && x.Osoba.Spol == "M").Count();
            model.zutiPojasSenioriŽ = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("ut") || x.ZvanjeUKarateu.Naziv.Contains("UT")
              || x.ZvanjeUKarateu.Naziv.Contains("uT") || x.ZvanjeUKarateu.Naziv.Contains("Ut")
              || (x.ZvanjeUKarateu.Naziv.Contains("V") && !x.ZvanjeUKarateu.Naziv.Contains("I"))) && (x.StarosnaDob.Naziv.Contains("en") || x.StarosnaDob.Naziv.Contains("21")) && x.Osoba.Spol == "Ž").Count();
            model.zutiPojasSenioriM = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
               && (x.ZvanjeUKarateu.Naziv.Contains("ut") || x.ZvanjeUKarateu.Naziv.Contains("UT")
               || x.ZvanjeUKarateu.Naziv.Contains("uT") || x.ZvanjeUKarateu.Naziv.Contains("Ut")
               || (x.ZvanjeUKarateu.Naziv.Contains("V") && !x.ZvanjeUKarateu.Naziv.Contains("I"))) && (x.StarosnaDob.Naziv.Contains("en") || x.StarosnaDob.Naziv.Contains("21")) && x.Osoba.Spol == "M").Count();

            model.oranzPojasDjecaŽ = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("ra") || x.ZvanjeUKarateu.Naziv.Contains("RA") || x.ZvanjeUKarateu.Naziv.Contains("rA") || x.ZvanjeUKarateu.Naziv.Contains("Ra")
              || x.ZvanjeUKarateu.Naziv.Contains("IV")) && x.StarosnaDob.Naziv.Contains("je") && x.Osoba.Spol == "Ž").Count();
            model.oranzPojasDjecaM = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("ra") || x.ZvanjeUKarateu.Naziv.Contains("RA") || x.ZvanjeUKarateu.Naziv.Contains("rA") || x.ZvanjeUKarateu.Naziv.Contains("Ra")
              || x.ZvanjeUKarateu.Naziv.Contains("IV")) && x.StarosnaDob.Naziv.Contains("je") && x.Osoba.Spol == "M").Count();
            model.oranzPojasKadetiŽ = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("ra") || x.ZvanjeUKarateu.Naziv.Contains("RA") || x.ZvanjeUKarateu.Naziv.Contains("rA") || x.ZvanjeUKarateu.Naziv.Contains("Ra")
              || x.ZvanjeUKarateu.Naziv.Contains("IV")) && x.StarosnaDob.Naziv.Contains("de") && x.Osoba.Spol == "Ž").Count();
            model.oranzPojasKadetiM = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("ra") || x.ZvanjeUKarateu.Naziv.Contains("RA") || x.ZvanjeUKarateu.Naziv.Contains("rA") || x.ZvanjeUKarateu.Naziv.Contains("Ra")
              || x.ZvanjeUKarateu.Naziv.Contains("IV")) && x.StarosnaDob.Naziv.Contains("de") && x.Osoba.Spol == "M").Count();
            model.oranzPojasJunioriŽ = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("ra") || x.ZvanjeUKarateu.Naziv.Contains("RA") || x.ZvanjeUKarateu.Naziv.Contains("rA") || x.ZvanjeUKarateu.Naziv.Contains("Ra")
              || x.ZvanjeUKarateu.Naziv.Contains("IV")) && x.StarosnaDob.Naziv.Contains("un") && x.Osoba.Spol == "Ž").Count();
            model.oranzPojasJunioriM = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("ra") || x.ZvanjeUKarateu.Naziv.Contains("RA") || x.ZvanjeUKarateu.Naziv.Contains("rA") || x.ZvanjeUKarateu.Naziv.Contains("Ra")
              || x.ZvanjeUKarateu.Naziv.Contains("IV")) && x.StarosnaDob.Naziv.Contains("un") && x.Osoba.Spol == "M").Count();
            model.oranzPojasSenioriŽ = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("ra") || x.ZvanjeUKarateu.Naziv.Contains("RA") || x.ZvanjeUKarateu.Naziv.Contains("rA") || x.ZvanjeUKarateu.Naziv.Contains("Ra")
              || x.ZvanjeUKarateu.Naziv.Contains("IV")) && (x.StarosnaDob.Naziv.Contains("en") || x.StarosnaDob.Naziv.Contains("21")) && x.Osoba.Spol == "Ž").Count();
            model.oranzPojasSenioriM = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
               && (x.ZvanjeUKarateu.Naziv.Contains("ra") || x.ZvanjeUKarateu.Naziv.Contains("RA") || x.ZvanjeUKarateu.Naziv.Contains("rA") || x.ZvanjeUKarateu.Naziv.Contains("Ra")
              || x.ZvanjeUKarateu.Naziv.Contains("IV")) && (x.StarosnaDob.Naziv.Contains("en") || x.StarosnaDob.Naziv.Contains("21")) && x.Osoba.Spol == "M").Count();

            model.zeleniPojasDjecaŽ = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("el") || x.ZvanjeUKarateu.Naziv.Contains("EL") || x.ZvanjeUKarateu.Naziv.Contains("eL") ||
              x.ZvanjeUKarateu.Naziv.Contains("El") || x.ZvanjeUKarateu.Naziv.Contains("III")) && x.StarosnaDob.Naziv.Contains("je") && x.Osoba.Spol == "Ž").Count();
            model.zeleniPojasDjecaM = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("el") || x.ZvanjeUKarateu.Naziv.Contains("EL") || x.ZvanjeUKarateu.Naziv.Contains("eL") ||
              x.ZvanjeUKarateu.Naziv.Contains("El") || x.ZvanjeUKarateu.Naziv.Contains("III")) && x.StarosnaDob.Naziv.Contains("je") && x.Osoba.Spol == "M").Count();
            model.zeleniPojasKadetiŽ = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("el") || x.ZvanjeUKarateu.Naziv.Contains("EL") || x.ZvanjeUKarateu.Naziv.Contains("eL") ||
              x.ZvanjeUKarateu.Naziv.Contains("El") || x.ZvanjeUKarateu.Naziv.Contains("III")) && x.StarosnaDob.Naziv.Contains("de") && x.Osoba.Spol == "Ž").Count();
            model.zeleniPojasKadetiM = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("el") || x.ZvanjeUKarateu.Naziv.Contains("EL") || x.ZvanjeUKarateu.Naziv.Contains("eL") ||
              x.ZvanjeUKarateu.Naziv.Contains("El") || x.ZvanjeUKarateu.Naziv.Contains("III")) && x.StarosnaDob.Naziv.Contains("de") && x.Osoba.Spol == "M").Count();
            model.zeleniPojasJunioriŽ = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("el") || x.ZvanjeUKarateu.Naziv.Contains("EL") || x.ZvanjeUKarateu.Naziv.Contains("eL") ||
              x.ZvanjeUKarateu.Naziv.Contains("El") || x.ZvanjeUKarateu.Naziv.Contains("III")) && x.StarosnaDob.Naziv.Contains("un") && x.Osoba.Spol == "Ž").Count();
            model.zeleniPojasJunioriM = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("el") || x.ZvanjeUKarateu.Naziv.Contains("EL") || x.ZvanjeUKarateu.Naziv.Contains("eL") ||
              x.ZvanjeUKarateu.Naziv.Contains("El") || x.ZvanjeUKarateu.Naziv.Contains("III")) && x.StarosnaDob.Naziv.Contains("un") && x.Osoba.Spol == "M").Count();
            model.zeleniPojasSenioriŽ = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("el") || x.ZvanjeUKarateu.Naziv.Contains("EL") || x.ZvanjeUKarateu.Naziv.Contains("eL") ||
              x.ZvanjeUKarateu.Naziv.Contains("El") || x.ZvanjeUKarateu.Naziv.Contains("III")) && (x.StarosnaDob.Naziv.Contains("en") || x.StarosnaDob.Naziv.Contains("21")) && x.Osoba.Spol == "Ž").Count();
            model.zeleniPojasSenioriM = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
               && (x.ZvanjeUKarateu.Naziv.Contains("el") || x.ZvanjeUKarateu.Naziv.Contains("EL") || x.ZvanjeUKarateu.Naziv.Contains("eL") ||
              x.ZvanjeUKarateu.Naziv.Contains("El") || x.ZvanjeUKarateu.Naziv.Contains("III")) && (x.StarosnaDob.Naziv.Contains("en") || x.StarosnaDob.Naziv.Contains("21")) && x.Osoba.Spol == "M").Count();

            model.plaviPojasDjecaŽ = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("la") || x.ZvanjeUKarateu.Naziv.Contains("LA") || x.ZvanjeUKarateu.Naziv.Contains("lA") || x.ZvanjeUKarateu.Naziv.Contains("La")
              || (x.ZvanjeUKarateu.Naziv.Contains("II") && !x.ZvanjeUKarateu.Naziv.Contains("III"))) && x.StarosnaDob.Naziv.Contains("je") && x.Osoba.Spol == "Ž").Count();
            model.plaviPojasDjecaM = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("la") || x.ZvanjeUKarateu.Naziv.Contains("LA") || x.ZvanjeUKarateu.Naziv.Contains("lA") || x.ZvanjeUKarateu.Naziv.Contains("La")
              || (x.ZvanjeUKarateu.Naziv.Contains("II") && !x.ZvanjeUKarateu.Naziv.Contains("III"))) && x.StarosnaDob.Naziv.Contains("je") && x.Osoba.Spol == "M").Count();
            model.plaviPojasKadetiŽ = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("la") || x.ZvanjeUKarateu.Naziv.Contains("LA") || x.ZvanjeUKarateu.Naziv.Contains("lA") || x.ZvanjeUKarateu.Naziv.Contains("La")
              || (x.ZvanjeUKarateu.Naziv.Contains("II") && !x.ZvanjeUKarateu.Naziv.Contains("III"))) && x.StarosnaDob.Naziv.Contains("de") && x.Osoba.Spol == "Ž").Count();
            model.plaviPojasKadetiM = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("la") || x.ZvanjeUKarateu.Naziv.Contains("LA") || x.ZvanjeUKarateu.Naziv.Contains("lA") || x.ZvanjeUKarateu.Naziv.Contains("La")
              || (x.ZvanjeUKarateu.Naziv.Contains("II") && !x.ZvanjeUKarateu.Naziv.Contains("III"))) && x.StarosnaDob.Naziv.Contains("de") && x.Osoba.Spol == "M").Count();
            model.plaviPojasJunioriŽ = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("la") || x.ZvanjeUKarateu.Naziv.Contains("LA") || x.ZvanjeUKarateu.Naziv.Contains("lA") || x.ZvanjeUKarateu.Naziv.Contains("La")
              || (x.ZvanjeUKarateu.Naziv.Contains("II") && !x.ZvanjeUKarateu.Naziv.Contains("III"))) && x.StarosnaDob.Naziv.Contains("un") && x.Osoba.Spol == "Ž").Count();
            model.plaviPojasJunioriM = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("la") || x.ZvanjeUKarateu.Naziv.Contains("LA") || x.ZvanjeUKarateu.Naziv.Contains("lA") || x.ZvanjeUKarateu.Naziv.Contains("La")
              || (x.ZvanjeUKarateu.Naziv.Contains("II") && !x.ZvanjeUKarateu.Naziv.Contains("III"))) && x.StarosnaDob.Naziv.Contains("un") && x.Osoba.Spol == "M").Count();
            model.plaviPojasSenioriŽ = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("la") || (x.ZvanjeUKarateu.Naziv.Contains("II") && !x.ZvanjeUKarateu.Naziv.Contains("III"))) &&
              (x.StarosnaDob.Naziv.Contains("en") || x.StarosnaDob.Naziv.Contains("21"))
              && x.Osoba.Spol == "Ž").Count();
            model.plaviPojasSenioriM = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
               && (x.ZvanjeUKarateu.Naziv.Contains("la") || x.ZvanjeUKarateu.Naziv.Contains("LA") || x.ZvanjeUKarateu.Naziv.Contains("lA") || x.ZvanjeUKarateu.Naziv.Contains("La")
              || (x.ZvanjeUKarateu.Naziv.Contains("II") && !x.ZvanjeUKarateu.Naziv.Contains("III"))) &&
               (x.StarosnaDob.Naziv.Contains("en") || x.StarosnaDob.Naziv.Contains("21"))
               && x.Osoba.Spol == "M").Count();

            model.smedjiPojasDjecaŽ = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("me") || x.ZvanjeUKarateu.Naziv.Contains("ME") || x.ZvanjeUKarateu.Naziv.Contains("mE") || x.ZvanjeUKarateu.Naziv.Contains("Me") || (x.ZvanjeUKarateu.Naziv.Contains("I") && !x.ZvanjeUKarateu.Naziv.Contains("V") && !x.ZvanjeUKarateu.Naziv.Contains("II")
              && !x.ZvanjeUKarateu.Naziv.Contains("III")
              )) && x.StarosnaDob.Naziv.Contains("je") && x.Osoba.Spol == "Ž").Count();
            model.smedjiPojasDjecaM = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true &&
             (x.ZvanjeUKarateu.Naziv.Contains("me") || x.ZvanjeUKarateu.Naziv.Contains("ME") || x.ZvanjeUKarateu.Naziv.Contains("mE") || x.ZvanjeUKarateu.Naziv.Contains("Me") || (x.ZvanjeUKarateu.Naziv.Contains("I") && !x.ZvanjeUKarateu.Naziv.Contains("V") && !x.ZvanjeUKarateu.Naziv.Contains("II")
              && !x.ZvanjeUKarateu.Naziv.Contains("III")
              )) && x.StarosnaDob.Naziv.Contains("je") && x.Osoba.Spol == "M").Count();
            model.smedjiPojasKadetiŽ = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("me") || x.ZvanjeUKarateu.Naziv.Contains("ME") || x.ZvanjeUKarateu.Naziv.Contains("mE") || x.ZvanjeUKarateu.Naziv.Contains("Me") || (x.ZvanjeUKarateu.Naziv.Contains("I") && !x.ZvanjeUKarateu.Naziv.Contains("V") && !x.ZvanjeUKarateu.Naziv.Contains("II")
              && !x.ZvanjeUKarateu.Naziv.Contains("III")
              )) && x.StarosnaDob.Naziv.Contains("de") && x.Osoba.Spol == "Ž").Count();
            model.smedjiPojasKadetiM = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("me") || x.ZvanjeUKarateu.Naziv.Contains("ME") || x.ZvanjeUKarateu.Naziv.Contains("mE") || x.ZvanjeUKarateu.Naziv.Contains("Me") || (x.ZvanjeUKarateu.Naziv.Contains("I") && !x.ZvanjeUKarateu.Naziv.Contains("V") && !x.ZvanjeUKarateu.Naziv.Contains("II")
              && !x.ZvanjeUKarateu.Naziv.Contains("III")
              )) && x.StarosnaDob.Naziv.Contains("de") && x.Osoba.Spol == "M").Count();
            model.smedjiPojasJunioriŽ = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("me") || x.ZvanjeUKarateu.Naziv.Contains("ME") || x.ZvanjeUKarateu.Naziv.Contains("mE") || x.ZvanjeUKarateu.Naziv.Contains("Me") || (x.ZvanjeUKarateu.Naziv.Contains("I") && !x.ZvanjeUKarateu.Naziv.Contains("V") && !x.ZvanjeUKarateu.Naziv.Contains("II")
              && !x.ZvanjeUKarateu.Naziv.Contains("III")
              )) && x.StarosnaDob.Naziv.Contains("un") && x.Osoba.Spol == "Ž").Count();
            model.smedjiPojasJunioriM = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("me") || x.ZvanjeUKarateu.Naziv.Contains("ME") || x.ZvanjeUKarateu.Naziv.Contains("mE") || x.ZvanjeUKarateu.Naziv.Contains("Me") || (x.ZvanjeUKarateu.Naziv.Contains("I") && !x.ZvanjeUKarateu.Naziv.Contains("V") && !x.ZvanjeUKarateu.Naziv.Contains("II")
              && !x.ZvanjeUKarateu.Naziv.Contains("III")
              )) && x.StarosnaDob.Naziv.Contains("un") && x.Osoba.Spol == "M").Count();
            model.smedjiPojasSenioriŽ = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("me") || x.ZvanjeUKarateu.Naziv.Contains("ME") || x.ZvanjeUKarateu.Naziv.Contains("mE") || x.ZvanjeUKarateu.Naziv.Contains("Me") || (x.ZvanjeUKarateu.Naziv.Contains("I") && !x.ZvanjeUKarateu.Naziv.Contains("V") && !x.ZvanjeUKarateu.Naziv.Contains("II")
              && !x.ZvanjeUKarateu.Naziv.Contains("III")
              )) && (x.StarosnaDob.Naziv.Contains("en") || x.StarosnaDob.Naziv.Contains("21")) && x.Osoba.Spol == "Ž").Count();
            model.smedjiPojasSenioriM = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
               && (x.ZvanjeUKarateu.Naziv.Contains("me") || x.ZvanjeUKarateu.Naziv.Contains("ME") || x.ZvanjeUKarateu.Naziv.Contains("mE") || x.ZvanjeUKarateu.Naziv.Contains("Me") || (x.ZvanjeUKarateu.Naziv.Contains("I") && !x.ZvanjeUKarateu.Naziv.Contains("V") && !x.ZvanjeUKarateu.Naziv.Contains("II")
              && !x.ZvanjeUKarateu.Naziv.Contains("III")
              )) &&
               (x.StarosnaDob.Naziv.Contains("en") || x.StarosnaDob.Naziv.Contains("21"))
               && x.Osoba.Spol == "M").Count();


            model.crniPojasDjecaŽ = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("rn") || x.ZvanjeUKarateu.Naziv.Contains("RN") || x.ZvanjeUKarateu.Naziv.Contains("rN") || x.ZvanjeUKarateu.Naziv.Contains("Rn") || x.ZvanjeUKarateu.Naziv.Contains("1") || x.ZvanjeUKarateu.Naziv.Contains("2") ||
              x.ZvanjeUKarateu.Naziv.Contains("3") || x.ZvanjeUKarateu.Naziv.Contains("4") || x.ZvanjeUKarateu.Naziv.Contains("5") || x.ZvanjeUKarateu.Naziv.Contains("6") || x.ZvanjeUKarateu.Naziv.Contains("7")
              || x.ZvanjeUKarateu.Naziv.Contains("8") || x.ZvanjeUKarateu.Naziv.Contains("9") || x.ZvanjeUKarateu.Naziv.Contains("10"))
              && x.StarosnaDob.Naziv.Contains("je") && x.Osoba.Spol == "Ž").Count();
            model.crniPojasDjecaM = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("rn") || x.ZvanjeUKarateu.Naziv.Contains("RN") || x.ZvanjeUKarateu.Naziv.Contains("rN") || x.ZvanjeUKarateu.Naziv.Contains("Rn") || x.ZvanjeUKarateu.Naziv.Contains("1") || x.ZvanjeUKarateu.Naziv.Contains("2") ||
              x.ZvanjeUKarateu.Naziv.Contains("3") || x.ZvanjeUKarateu.Naziv.Contains("4") || x.ZvanjeUKarateu.Naziv.Contains("5") || x.ZvanjeUKarateu.Naziv.Contains("6") || x.ZvanjeUKarateu.Naziv.Contains("7")
              || x.ZvanjeUKarateu.Naziv.Contains("8") || x.ZvanjeUKarateu.Naziv.Contains("9") || x.ZvanjeUKarateu.Naziv.Contains("10"))
              && x.StarosnaDob.Naziv.Contains("je") && x.Osoba.Spol == "Ž").Count();
            model.crniPojasKadetiŽ = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("rn") || x.ZvanjeUKarateu.Naziv.Contains("RN") || x.ZvanjeUKarateu.Naziv.Contains("rN") || x.ZvanjeUKarateu.Naziv.Contains("Rn") || x.ZvanjeUKarateu.Naziv.Contains("1") || x.ZvanjeUKarateu.Naziv.Contains("2") ||
              x.ZvanjeUKarateu.Naziv.Contains("3") || x.ZvanjeUKarateu.Naziv.Contains("4") || x.ZvanjeUKarateu.Naziv.Contains("5") || x.ZvanjeUKarateu.Naziv.Contains("6") || x.ZvanjeUKarateu.Naziv.Contains("7")
              || x.ZvanjeUKarateu.Naziv.Contains("8") || x.ZvanjeUKarateu.Naziv.Contains("9") || x.ZvanjeUKarateu.Naziv.Contains("10"))
              && x.StarosnaDob.Naziv.Contains("de") && x.Osoba.Spol == "Ž").Count();
            model.crniPojasKadetiM = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("rn") || x.ZvanjeUKarateu.Naziv.Contains("RN") || x.ZvanjeUKarateu.Naziv.Contains("rN") || x.ZvanjeUKarateu.Naziv.Contains("Rn") || x.ZvanjeUKarateu.Naziv.Contains("1") || x.ZvanjeUKarateu.Naziv.Contains("2") ||
              x.ZvanjeUKarateu.Naziv.Contains("3") || x.ZvanjeUKarateu.Naziv.Contains("4") || x.ZvanjeUKarateu.Naziv.Contains("5") || x.ZvanjeUKarateu.Naziv.Contains("6") || x.ZvanjeUKarateu.Naziv.Contains("7")
              || x.ZvanjeUKarateu.Naziv.Contains("8") || x.ZvanjeUKarateu.Naziv.Contains("9") || x.ZvanjeUKarateu.Naziv.Contains("10"))
              && x.StarosnaDob.Naziv.Contains("de") && x.Osoba.Spol == "Ž").Count();
            model.crniPojasJunioriŽ = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("rn") || x.ZvanjeUKarateu.Naziv.Contains("RN") || x.ZvanjeUKarateu.Naziv.Contains("rN") || x.ZvanjeUKarateu.Naziv.Contains("Rn") || x.ZvanjeUKarateu.Naziv.Contains("1") || x.ZvanjeUKarateu.Naziv.Contains("2") ||
              x.ZvanjeUKarateu.Naziv.Contains("3") || x.ZvanjeUKarateu.Naziv.Contains("4") || x.ZvanjeUKarateu.Naziv.Contains("5") || x.ZvanjeUKarateu.Naziv.Contains("6") || x.ZvanjeUKarateu.Naziv.Contains("7")
              || x.ZvanjeUKarateu.Naziv.Contains("8") || x.ZvanjeUKarateu.Naziv.Contains("9") || x.ZvanjeUKarateu.Naziv.Contains("10"))
              && x.StarosnaDob.Naziv.Contains("un") && x.Osoba.Spol == "Ž").Count();
            model.crniPojasJunioriM = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("rn") || x.ZvanjeUKarateu.Naziv.Contains("RN") || x.ZvanjeUKarateu.Naziv.Contains("rN") || x.ZvanjeUKarateu.Naziv.Contains("Rn") || x.ZvanjeUKarateu.Naziv.Contains("1") || x.ZvanjeUKarateu.Naziv.Contains("2") ||
              x.ZvanjeUKarateu.Naziv.Contains("3") || x.ZvanjeUKarateu.Naziv.Contains("4") || x.ZvanjeUKarateu.Naziv.Contains("5") || x.ZvanjeUKarateu.Naziv.Contains("6") || x.ZvanjeUKarateu.Naziv.Contains("7")
              || x.ZvanjeUKarateu.Naziv.Contains("8") || x.ZvanjeUKarateu.Naziv.Contains("9") || x.ZvanjeUKarateu.Naziv.Contains("10"))
              && x.StarosnaDob.Naziv.Contains("un") && x.Osoba.Spol == "Ž").Count();
            model.crniPojasSenioriŽ = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
              && (x.ZvanjeUKarateu.Naziv.Contains("rn") || x.ZvanjeUKarateu.Naziv.Contains("RN") || x.ZvanjeUKarateu.Naziv.Contains("rN") || x.ZvanjeUKarateu.Naziv.Contains("Rn") || x.ZvanjeUKarateu.Naziv.Contains("1") || x.ZvanjeUKarateu.Naziv.Contains("2") ||
              x.ZvanjeUKarateu.Naziv.Contains("3") || x.ZvanjeUKarateu.Naziv.Contains("4") || x.ZvanjeUKarateu.Naziv.Contains("5") || x.ZvanjeUKarateu.Naziv.Contains("6") || x.ZvanjeUKarateu.Naziv.Contains("7")
              || x.ZvanjeUKarateu.Naziv.Contains("8") || x.ZvanjeUKarateu.Naziv.Contains("9") || x.ZvanjeUKarateu.Naziv.Contains("10"))
              && (x.StarosnaDob.Naziv.Contains("en") || x.StarosnaDob.Naziv.Contains("21")) && x.Osoba.Spol == "Ž").Count();
            model.crniPojasSenioriM = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true
               && (x.ZvanjeUKarateu.Naziv.Contains("rn") || x.ZvanjeUKarateu.Naziv.Contains("RN") || x.ZvanjeUKarateu.Naziv.Contains("rN") || x.ZvanjeUKarateu.Naziv.Contains("Rn") || x.ZvanjeUKarateu.Naziv.Contains("1") || x.ZvanjeUKarateu.Naziv.Contains("2") ||
              x.ZvanjeUKarateu.Naziv.Contains("3") || x.ZvanjeUKarateu.Naziv.Contains("4") || x.ZvanjeUKarateu.Naziv.Contains("5") || x.ZvanjeUKarateu.Naziv.Contains("6") || x.ZvanjeUKarateu.Naziv.Contains("7")
              || x.ZvanjeUKarateu.Naziv.Contains("8") || x.ZvanjeUKarateu.Naziv.Contains("9") || x.ZvanjeUKarateu.Naziv.Contains("10"))
              && (x.StarosnaDob.Naziv.Contains("en") || x.StarosnaDob.Naziv.Contains("21")) && x.Osoba.Spol == "Ž").Count();


            model.ukupanBrojDjeceŽ = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true && x.Osoba.Spol == "Ž" && x.StarosnaDob.Naziv.Contains("je")).Count();
            model.ukupanBrojDjeceM = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true && x.Osoba.Spol == "M" && x.StarosnaDob.Naziv.Contains("je")).Count();
            model.ukupanBrojDjece = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true && x.StarosnaDob.Naziv.Contains("je")).Count();


            model.ukupanBrojKadetaŽ = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true && x.Osoba.Spol == "Ž" && x.StarosnaDob.Naziv.Contains("de")).Count();
            model.ukupanBrojKadetaM = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true && x.Osoba.Spol == "M" && x.StarosnaDob.Naziv.Contains("de")).Count();
            model.ukupanBrojKadeta = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true && x.StarosnaDob.Naziv.Contains("de")).Count();

            model.ukupanBrojJunioraŽ = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true && x.Osoba.Spol == "Ž" && x.StarosnaDob.Naziv.Contains("un")).Count();
            model.ukupanBrojJunioraM = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true && x.Osoba.Spol == "M" && x.StarosnaDob.Naziv.Contains("un")).Count();
            model.ukupanBrojJuniora = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true && x.StarosnaDob.Naziv.Contains("un")).Count();


            model.ukupanBrojSenioraŽ = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true && x.Osoba.Spol == "Ž" && (x.StarosnaDob.Naziv.Contains("en") ||
            x.StarosnaDob.Naziv.Contains("21"))).Count();
            model.ukupanBrojSenioraM = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true && x.Osoba.Spol == "M" &&
            (x.StarosnaDob.Naziv.Contains("en") ||
            x.StarosnaDob.Naziv.Contains("21"))).Count();
            model.ukupanBrojSeniora = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true &&
            (x.StarosnaDob.Naziv.Contains("en") ||
            x.StarosnaDob.Naziv.Contains("21"))
            ).Count();

            model.ukupanBrojPocetnika = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true &&
            (x.ZvanjeUKarateu.Naziv.Contains("et") || x.ZvanjeUKarateu.Naziv.Contains("ET") || x.ZvanjeUKarateu.Naziv.Contains("eT") ||
            x.ZvanjeUKarateu.Naziv.Contains("Et"))).Count();


            model.ukupanBrojZutiPojas = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true &&
            (x.ZvanjeUKarateu.Naziv.Contains("ut") || x.ZvanjeUKarateu.Naziv.Contains("UT") || x.ZvanjeUKarateu.Naziv.Contains("uT") || 
            x.ZvanjeUKarateu.Naziv.Contains("Ut"))).Count();

            model.ukupanBrojOranzPojas = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true &&
            (x.ZvanjeUKarateu.Naziv.Contains("ra") || x.ZvanjeUKarateu.Naziv.Contains("RA") || x.ZvanjeUKarateu.Naziv.Contains("rA") ||
            x.ZvanjeUKarateu.Naziv.Contains("Ra"))).Count();

            model.ukupanBrojZeleniPojas = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true &&
            (x.ZvanjeUKarateu.Naziv.Contains("el") || x.ZvanjeUKarateu.Naziv.Contains("EL") || x.ZvanjeUKarateu.Naziv.Contains("eL") ||
            x.ZvanjeUKarateu.Naziv.Contains("El"))).Count();
            
            model.ukupanBrojPlaviPojas = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true &&
            (x.ZvanjeUKarateu.Naziv.Contains("la") || x.ZvanjeUKarateu.Naziv.Contains("LA") || x.ZvanjeUKarateu.Naziv.Contains("lA") ||
            x.ZvanjeUKarateu.Naziv.Contains("La"))).Count();

            model.ukupanBrojSmedjiPojas = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true &&
            (x.ZvanjeUKarateu.Naziv.Contains("me") || x.ZvanjeUKarateu.Naziv.Contains("ME") || x.ZvanjeUKarateu.Naziv.Contains("mE") ||
            x.ZvanjeUKarateu.Naziv.Contains("Me"))).Count();

            model.ukupanBrojCrniPojas = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true &&
            (x.ZvanjeUKarateu.Naziv.Contains("rn") || x.ZvanjeUKarateu.Naziv.Contains("RN") || x.ZvanjeUKarateu.Naziv.Contains("rN") ||
             x.ZvanjeUKarateu.Naziv.Contains("Rn") ||
            x.ZvanjeUKarateu.Naziv.Contains("1") || x.ZvanjeUKarateu.Naziv.Contains("2") || x.ZvanjeUKarateu.Naziv.Contains("3") || x.ZvanjeUKarateu.Naziv.Contains("4")
            || x.ZvanjeUKarateu.Naziv.Contains("5") || x.ZvanjeUKarateu.Naziv.Contains("6") || x.ZvanjeUKarateu.Naziv.Contains("7") || x.ZvanjeUKarateu.Naziv.Contains("8") ||
             x.ZvanjeUKarateu.Naziv.Contains("9") || x.ZvanjeUKarateu.Naziv.Contains("10")
            )).Count();

            model.ukupanBrojClanova = ctx.ClanoviKluba.Where(x => x.isDeleted == false && x.Osoba.isAktivnaOsoba == true).Count();
            return View("StrukturaClanovaKluba", model);
        }
        public ActionResult UpravljanjeDetaljimaClana(int osobaId, int aktivan, int brojTaba = 1,int aktivnostLicence=0,int brojTabaUplate=1,int izmirena=0)
        {
            Osoba osoba = ctx.Osoba.Where(x => x.Id == osobaId).FirstOrDefault();
            ViewData["osobaId"] = osobaId;
            ViewData["aktivan"] = aktivan;
            ViewData["tab"] = brojTaba;
            ViewData["osoba"] = osoba;
            ViewData["aktivnostLicence"] = aktivnostLicence;
            ViewData["brojTabaUplate"] = brojTabaUplate;
            ViewData["izmirena"] = izmirena;


            return View("UpravljanjeDetaljimaClana");
        }
        public ActionResult GoToUpravljanjeDetaljimaClana(int osobaId, int aktivan, int aktivnost)
        {

            return RedirectToAction("UpravljanjeDetaljimaClana", new { osobaId = osobaId, aktivan = aktivan, brojTaba = 3, aktivnostLicence = aktivnost });
        }
        public ActionResult GoToUpravljanjeUplatamaClana(int osobaId,int aktivan,int izmirena)
        {

            return RedirectToAction("UpravljanjeDetaljimaClana", new { osobaId = osobaId, aktivan = aktivan, brojTaba = 4, aktivnostLicence = 0, brojTabaUplate = 1, izmirena = izmirena });
        }
        public ActionResult GoToUpravljanjeUplatamaClana2(int osobaId, int aktivan, int izmirena)
        {

            return RedirectToAction("UpravljanjeDetaljimaClana", new { osobaId = osobaId, aktivan = aktivan, brojTaba = 4, aktivnostLicence = 0, brojTabaUplate = 2, izmirena = izmirena });
        }
        public ActionResult GoToPregledClanova(int aktivan)
        {

            return RedirectToAction("Index", "UpravljanjeClanovimaKluba", new { brojTaba=1,aktivan=aktivan,zvanje=0});
        }

        public ActionResult Dodaj(int aktivan)
        {
            ClanoviKlubaDodajVM model = new ClanoviKlubaDodajVM
            {
                aktivan = aktivan,
                StarosneDobi = BindStarosneDobi(),
                ZvanjaUKarateu = BindZvanjaUKarateu()

            };
            model.ZvanjaUKarateu.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite zvanje-" });
            model.StarosneDobi.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite starosnu dob-" });
            return View("Dodaj", model);
        }

        private List<SelectListItem> BindStarosneDobi()
        {
            return ctx.StarosneDobi.Where(x => x.isDeleted == false).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv }).ToList();
        }

        private List<SelectListItem> BindZvanjaUKarateu()
        {
            return ctx.ZvanjaUKarateu.Where(x => x.isDeleted == false).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv }).ToList();
        }

        public DateTime KonvertujUDatum_dd_mm_yyyy(string datum)
        {
            int dan = Convert.ToInt32(datum.Substring(0, 2));
            int mjesec = Convert.ToInt32(datum.Substring(3, 2));
            int godina = Convert.ToInt32(datum.Substring(6, 4));
            DateTime uneseniDatum = new DateTime(godina, mjesec, dan);
            return uneseniDatum;
        }

        public ActionResult SpremiNovogClana(ClanoviKlubaDodajVM model)
        {
            Osoba osoba = new Osoba();
            osoba.isDeleted = false;
            osoba.isAktivnaOsoba = true;
            osoba.Ime = model.Ime;
            osoba.Prezime = model.Prezime;
            osoba.ImeRoditelja = model.ImeRoditelja;
            osoba.JMBG = model.JMBG;
            osoba.DatumRodjenja = KonvertujUDatum_dd_mm_yyyy(model.DatumRodjenja);
            osoba.MjestoRodjenja = model.MjestoRodjenja;
            osoba.Spol = model.Spol;
            osoba.KontaktTelefon = model.KontaktTelefon;
            osoba.Email = model.Email;
            osoba.isAdministrator = false;
            osoba.isBlagajnik = false;
            osoba.isSekretar = false;
            osoba.isClanKluba = true;
            osoba.isTrener = false;
            osoba.isClanUpravnogOdbora = false;
            osoba.isKnjigovodja = false;
            if (model.s == null)
            {
                osoba.NazivSlike = null;
                osoba.TipSlike = null;
                osoba.Slika = null;


            }
            else
            {
                osoba.NazivSlike = model.s.FileName;
                osoba.TipSlike = model.s.ContentType;

                byte[] slika = new byte[model.s.ContentLength];
                model.s.InputStream.Read(slika, 0, model.s.ContentLength);
                osoba.Slika = slika;
            }

            ctx.Osoba.Add(osoba);
            ctx.SaveChanges();
            int OsobaId = ctx.Osoba.ToList().OrderByDescending(x => x.Id).FirstOrDefault().Id;
            ClanoviKluba clan = new ClanoviKluba();
            clan.OsobaId = OsobaId;
            clan.isDeleted = false;
            clan.DatumUpisa = KonvertujUDatum_dd_mm_yyyy(model.DatumUpisa);
            clan.StarosnaDobId = model.StarosnaDobId;
            clan.ZvanjeUKarateuId = model.ZvanjeUKarateuId;
            ctx.ClanoviKluba.Add(clan);
            ctx.SaveChanges();

            return RedirectToAction("GoToPregledClanova", "ClanoviKluba", new { aktivan = model.aktivan });
        }

        public JsonResult Obrisi(int osobaId)
        {
            Osoba osoba = ctx.Osoba.Where(x => x.Id == osobaId).FirstOrDefault();
            ClanoviKluba clan = ctx.ClanoviKluba.Where(x => x.OsobaId == osobaId).FirstOrDefault();
           
            if (clan != null)
                clan.isDeleted = true;
            if (osoba != null)
                osoba.isDeleted = true;
            ctx.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult Detalji(int osobaId, int aktivan)
        {
            ViewData["aktivan"] = aktivan;
            ViewData["osobaId"] = osobaId;

            Osoba osoba = ctx.Osoba.Where(x => x.Id == osobaId).FirstOrDefault();
            ClanoviKluba clan = ctx.ClanoviKluba.Where(x => x.OsobaId == osobaId && x.isDeleted == false).FirstOrDefault();
            ClanoviKlubaDetaljiVM model = new ClanoviKlubaDetaljiVM
            {
                isAktivnaOsoba = osoba.isAktivnaOsoba,
                OsobaId = osobaId,
                Ime = osoba.Ime,
                Prezime = osoba.Prezime,
                ImeRoditelja = osoba.ImeRoditelja,
                DatumRodjenja = osoba.DatumRodjenja,
                MjestoRodjenja = osoba.MjestoRodjenja,
                JMBG = osoba.JMBG,
                Spol = osoba.Spol,
                KontaktTelefon = osoba.KontaktTelefon,
                Email = osoba.Email,
                Slika = osoba.Slika,
                TipSlike = osoba.TipSlike,
                NazivSlike = osoba.NazivSlike,
                aktivan = aktivan,
                DatumUpisa = clan.DatumUpisa,
                ZvanjeUKarateu=clan.ZvanjeUKarateu.Naziv,
                StarosnaDob=clan.StarosnaDob.Naziv
            };
            return View("Detalji", model);
        }
        public ActionResult GetImage(int osobaId)
        {
            Osoba osoba = ctx.Osoba.Where(x => x.Id == osobaId).FirstOrDefault();
            return File(osoba.Slika, osoba.TipSlike);

        }
        [HttpPost]
        public ActionResult DodajSliku(ClanoviKlubaDetaljiVM model)
        {
            if (model.s == null)
            {
                return RedirectToAction("UpravljanjeDetaljimaClana", new { osobaId = model.OsobaId, aktivan = model.aktivan, brojTaba = 1 });

            }
            else
            {
                Osoba osoba = ctx.Osoba.Where(y => y.Id == model.OsobaId).FirstOrDefault();
                osoba.NazivSlike = model.s.FileName;
                osoba.TipSlike = model.s.ContentType;

                byte[] slika = new byte[model.s.ContentLength];
                model.s.InputStream.Read(slika, 0, model.s.ContentLength);
                osoba.Slika = slika;
                ctx.SaveChanges();
            }

            return RedirectToAction("UpravljanjeDetaljimaClana", new { osobaId = model.OsobaId, aktivan = model.aktivan, brojTaba = 1 });

        }

        public ActionResult DeaktivirajClana(int osobaId, int aktivan)
        {
            Osoba osoba = ctx.Osoba.Where(x => x.Id == osobaId).FirstOrDefault();
            if (osoba != null)
            {
                osoba.isAktivnaOsoba = false;
                KorisnickiNalozi nalog = ctx.KorisnickiNalozi.Where(x => x.OsobaId == osobaId).FirstOrDefault();
                if (nalog != null)
                    nalog.isAktivanNalog = false;
                ctx.SaveChanges();
            }
            return RedirectToAction("UpravljanjeDetaljimaClana", new { osobaId = osobaId, aktivan = aktivan, brojTaba = 1 });

        }

        public ActionResult AktivirajClana(int osobaId, int aktivan)
        {
            Osoba osoba = ctx.Osoba.Where(x => x.Id == osobaId).FirstOrDefault();
            if (osoba != null)
            {
                osoba.isAktivnaOsoba = true;
                KorisnickiNalozi nalog = ctx.KorisnickiNalozi.Where(x => x.OsobaId == osobaId).FirstOrDefault();
                if (nalog != null)
                    nalog.isAktivanNalog = true;
                ctx.SaveChanges();
            }
            return RedirectToAction("UpravljanjeDetaljimaClana", new { osobaId = osobaId, aktivan = aktivan, brojTaba = 1 });

        }
        public string KonvertujUString_mm_dd_yyyy(string datum)
        {
            string dan = datum.Substring(0, 2);
            string mjesec = datum.Substring(3, 2);
            string godina = datum.Substring(6, 4);
            string uneseniDatum = dan + "/" + mjesec + "/" + godina;
            return uneseniDatum;
        }


        public ActionResult LicniPodaciClana(int osobaId, int aktivan)
        {

            Osoba osoba = ctx.Osoba.Where(x => x.Id == osobaId).FirstOrDefault();
            ClanoviKlubaLicniPodaciClanaVM model = new ClanoviKlubaLicniPodaciClanaVM
            {
                OsobaId = osobaId,
                isAktivnaOsoba = osoba.isAktivnaOsoba,
                Ime = osoba.Ime,
                Prezime = osoba.Prezime,
                ImeRoditelja = osoba.ImeRoditelja,
                JMBG = osoba.JMBG,
                Spol = osoba.Spol,
                DatumRodjenja = KonvertujUString_mm_dd_yyyy(osoba.DatumRodjenja.ToString()),
                MjestoRodjenja = osoba.MjestoRodjenja,
                KontaktTelefon = osoba.KontaktTelefon,
                Email = osoba.Email,
                aktivan = aktivan
            };

            return View("LicniPodaciClana", model);
        }
        [HttpPost]
        public ActionResult SpremiLicnePodatkeClana(ClanoviKlubaLicniPodaciClanaVM model)
        {
            Osoba osoba = ctx.Osoba.Where(x => x.Id == model.OsobaId).FirstOrDefault();

            osoba.Ime = model.Ime;
            osoba.Prezime = model.Prezime;
            osoba.ImeRoditelja = model.ImeRoditelja;
            osoba.DatumRodjenja = KonvertujUDatum_dd_mm_yyyy(model.DatumRodjenja);
            osoba.MjestoRodjenja = model.MjestoRodjenja;
            osoba.KontaktTelefon = model.KontaktTelefon;
            osoba.JMBG = model.JMBG;
            osoba.Email = model.Email;
            osoba.Spol = model.Spol;

            ctx.SaveChanges();

            return RedirectToAction("UpravljanjeDetaljimaClana", new { osobaId = model.OsobaId, aktivan = model.aktivan, brojTaba = 1 });

        }

        public ActionResult KlubskiPodaciClana(int osobaId, int aktivan)
        {
            Osoba osoba = ctx.Osoba.Where(x => x.Id == osobaId).FirstOrDefault();
            ClanoviKluba clan= ctx.ClanoviKluba.Where(x => x.OsobaId == osobaId).FirstOrDefault();
            ClanoviKlubaKlubskiPodaciClanaVM model = new ClanoviKlubaKlubskiPodaciClanaVM
            {
         
                DatumUpisa=clan.DatumUpisa.ToString("dd.MM.yyyy"),
                starosnaDobId = clan.StarosnaDobId,
                starosneDobi = BindStarosneDobi(),
                ZvanjeUKarateuId = clan.ZvanjeUKarateuId,
                ZvanjaUKarateu = BindZvanjaUKarateu(),
                aktivan = aktivan,
                isAktivnaOsoba = osoba.isAktivnaOsoba
            };
            model.ZvanjaUKarateu.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite zvanje-" });
            model.starosneDobi.Insert(0, new SelectListItem { Value = null, Text = "-Odaberite starosnu dob-" });

            return View("KlubskiPodaciClana", model);

        }
        [HttpPost]
        public ActionResult SpremiKlubskePodatkeClana(ClanoviKlubaKlubskiPodaciClanaVM model)
        {
            Osoba osoba = ctx.Osoba.Where(x => x.Id == model.OsobaId).FirstOrDefault();
            ClanoviKluba clan = ctx.ClanoviKluba.Where(x => x.OsobaId == model.OsobaId).FirstOrDefault();

            clan.DatumUpisa = KonvertujUDatum_dd_mm_yyyy(model.DatumUpisa);
            clan.StarosnaDobId = model.starosnaDobId;
            clan.ZvanjeUKarateuId = model.ZvanjeUKarateuId;
            ctx.SaveChanges();
            return RedirectToAction("UpravljanjeDetaljimaClana", new { osobaId = model.OsobaId, aktivan = model.aktivan, brojTaba = 1 });

        }

    }
}
