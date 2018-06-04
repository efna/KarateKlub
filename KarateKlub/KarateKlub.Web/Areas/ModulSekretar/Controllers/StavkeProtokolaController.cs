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

    public class StavkeProtokolaController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulSekretar/StavkeProtokola
        public ActionResult Index(int protokolId)
        {
            StavkeProtokolaIndexVM model = new StavkeProtokolaIndexVM {
                listaStavkiProtokola=ctx.StavkeProtokola.Where(x=>x.ProtokolId==protokolId && x.isDeleted==false).Select(x=>new StavkeProtokolaPodaci {
                    Id=x.Id,
                    ProtokolId=x.ProtokolId,
                    IsDeleted=x.isDeleted,
                    BrojProtokola=x.BrojProtokola,
                    Predmet=x.Predmet,
                    PodBroj=x.PodBroj,
                    DatumPrijema=x.DatumPrijema.ToString(),
                    Posiljaoc=x.Posiljaoc,
                    MjestoPosiljke = x.MjestoPosiljke,
                    BrojPosiljke=x.BrojPosiljke,
                    DatumPosiljke=x.DatumPosiljke.ToString(),
                    DatumRazvoda =x.DatumRazvoda.ToString(),
                    OrganizacionaJedinica=x.OrganizacionaJedinica,
                    Oznaka=x.Oznaka

                }).ToList(),
                protokolId=protokolId
            };

            for (int i = 0; i < model.listaStavkiProtokola.Count(); i++)
            {
                if(model.listaStavkiProtokola[i].DatumPosiljke!="")
                    model.listaStavkiProtokola[i].DatumPosiljke =Convert.ToDateTime(model.listaStavkiProtokola[i].DatumPosiljke).ToString("dd.MM.yyyy");
                if(model.listaStavkiProtokola[i].DatumRazvoda!="")
                model.listaStavkiProtokola[i].DatumRazvoda = Convert.ToDateTime(model.listaStavkiProtokola[i].DatumRazvoda).ToString("dd.MM.yyyy");
                if (model.listaStavkiProtokola[i].DatumPrijema != "")
                    model.listaStavkiProtokola[i].DatumPrijema= Convert.ToDateTime(model.listaStavkiProtokola[i].DatumPrijema).ToString("dd.MM.yyyy");

            }
            return View(model);
        }

        public ActionResult Dodaj(int protokolId)
        {
            string posljednjiBrojProtokolaUBazi;
            string noviBroj = "";
            List<StavkeProtokola> listaStavka = ctx.StavkeProtokola.Where(x => x.ProtokolId == protokolId && x.isDeleted!=true).ToList();
            if (listaStavka.Count() == 0)
                noviBroj = "1.";
            else
            {
                posljednjiBrojProtokolaUBazi = ctx.StavkeProtokola.Where(x => x.ProtokolId == protokolId && x.isDeleted!=true).OrderByDescending(x => x.Id).FirstOrDefault().BrojProtokola;
                noviBroj = (Convert.ToInt32(posljednjiBrojProtokolaUBazi.Substring(0, posljednjiBrojProtokolaUBazi.IndexOf("."))) + 1).ToString()+".";
            }
            StavkeProtokolaDodajVM model = new StavkeProtokolaDodajVM {
                ProtokolId = protokolId,
                BrojProtokola=noviBroj
            };

            return View("Dodaj", model);

        }


        public ActionResult SpremiNovuStavku(StavkeProtokolaDodajVM model)
        {
            StavkeProtokola stavka = new StavkeProtokola();

            stavka.BrojProtokola = model.BrojProtokola;
            stavka.isDeleted = false;
            stavka.ProtokolId = model.ProtokolId;
            stavka.BrojProtokola = model.BrojProtokola;
            stavka.Predmet = model.Predmet;
            stavka.PodBroj = model.PodBroj;
            stavka.DatumPrijema =KonvertujUDatum_dd_mm_yyyy(model.DatumPrijema);
            stavka.Posiljaoc = model.Posiljaoc;
            stavka.BrojPosiljke = model.BrojPosiljke;
            if (model.DatumPosiljke != null)
                stavka.DatumPosiljke = KonvertujUDatum_dd_mm_yyyy(model.DatumPosiljke);
            else
                stavka.DatumPosiljke = null;
            stavka.MjestoPosiljke = model.MjestoPosiljke;
            stavka.OrganizacionaJedinica = model.OrganizacionaJedinica;
            if (model.DatumRazvoda != null)
                stavka.DatumRazvoda = KonvertujUDatum_dd_mm_yyyy(model.DatumRazvoda);
            else
                stavka.DatumRazvoda = null;
            stavka.Oznaka = model.Oznaka;
            ctx.StavkeProtokola.Add(stavka);
            ctx.SaveChanges();

            return RedirectToAction("Index", new { protokolId = model.ProtokolId });
        }
        public string KonvertujUString_mm_dd_yyyy(string datum)
        {
          
            string dan = datum.Substring(0, 2);
            string mjesec = datum.Substring(3, 2);
            string godina = datum.Substring(6, 4);
            string uneseniDatum = dan + "/" + mjesec + "/" + godina;
            return uneseniDatum;
        }
   

        public DateTime KonvertujUDatum_dd_mm_yyyy(string datum)
        {
     
            int dan = Convert.ToInt32(datum.Substring(0, 2));
            int mjesec = Convert.ToInt32(datum.Substring(3, 2));
            int godina = Convert.ToInt32(datum.Substring(6, 4));
            DateTime uneseniDatum = new DateTime(godina, mjesec, dan);
            return uneseniDatum;
         
       
        }

        public ActionResult Uredi(int stavkaId)
        {
            StavkeProtokola stavka = ctx.StavkeProtokola.Where(x => x.Id == stavkaId).FirstOrDefault();
            StavkeProtokolaUrediVM model = new StavkeProtokolaUrediVM {
                Id=stavka.Id,
                ProtokolId = stavka.ProtokolId,
                isDeleted = stavka.isDeleted,
                BrojProtokola = stavka.BrojProtokola,
                Predmet = stavka.Predmet,
                PodBroj = stavka.PodBroj,
                DatumPrijema = stavka.DatumPrijema.ToString(),
                Posiljaoc = stavka.Posiljaoc,
                MjestoPosiljke = stavka.MjestoPosiljke,
                BrojPosiljke = stavka.BrojPosiljke,
                DatumPosiljke = stavka.DatumPosiljke.ToString(),
                DatumRazvoda = stavka.DatumRazvoda.ToString(),
                OrganizacionaJedinica = stavka.OrganizacionaJedinica,
                Oznaka = stavka.Oznaka
            };
            if (model.DatumPosiljke != "")
                model.DatumPosiljke = Convert.ToDateTime(model.DatumPosiljke).ToString("dd.MM.yyyy");
            if (model.DatumRazvoda != "")
                model.DatumRazvoda = Convert.ToDateTime(model.DatumRazvoda).ToString("dd.MM.yyyy");
            if (model.DatumPrijema != "")
                model.DatumPrijema = Convert.ToDateTime(model.DatumPrijema).ToString("dd.MM.yyyy");

            return View("Uredi", model);
        }

        public ActionResult SpremiIzmjenuStavke(StavkeProtokolaUrediVM model)
        {
            StavkeProtokola stavka = ctx.StavkeProtokola.Where(x => x.Id == model.Id).FirstOrDefault();
            stavka.Predmet = model.Predmet;
            stavka.PodBroj = model.PodBroj;
            stavka.DatumPrijema = KonvertujUDatum_dd_mm_yyyy(model.DatumPrijema);
            stavka.Posiljaoc = model.Posiljaoc;
            stavka.MjestoPosiljke = model.MjestoPosiljke;
            stavka.BrojPosiljke = model.BrojPosiljke;
            if (model.DatumPosiljke != null)
                stavka.DatumPosiljke = KonvertujUDatum_dd_mm_yyyy(model.DatumPosiljke);
            if (model.DatumRazvoda != null)
                stavka.DatumRazvoda = KonvertujUDatum_dd_mm_yyyy(model.DatumRazvoda);
            stavka.OrganizacionaJedinica = model.OrganizacionaJedinica;
            stavka.Oznaka = model.Oznaka;
            ctx.SaveChanges();
            

            return RedirectToAction("Index", new { protokolId = model.ProtokolId });
        }

        public JsonResult Obrisi(int stavkaId)
        {

            StavkeProtokola stavka = ctx.StavkeProtokola.Where(x => x.Id == stavkaId).FirstOrDefault();
            stavka.isDeleted = true;
            ctx.SaveChanges();
           
            List<Dokumenti> dokumenti = ctx.Dokumenti.Where(x => x.StavkaProtokolaId == stavkaId).ToList();
            for (int i = 0; i < dokumenti.Count(); i++)
            {
                dokumenti[i].isDeleted = true;
                ctx.SaveChanges();

            }

            List<StavkeProtokola> listaStavkiCijiJeRbVeci = ctx.StavkeProtokola.Where(x => x.Id > stavka.Id && x.ProtokolId==stavka.ProtokolId && x.isDeleted!=true).ToList();
            if (listaStavkiCijiJeRbVeci.Count() != 0)
            {
                string brojProtokolaStavkeKojaSeBrise = stavka.BrojProtokola;
                listaStavkiCijiJeRbVeci[0].BrojProtokola = brojProtokolaStavkeKojaSeBrise;
                ctx.SaveChanges();
                for (int i = 1; i < listaStavkiCijiJeRbVeci.Count(); i++)
                {
                    
                    listaStavkiCijiJeRbVeci[i].BrojProtokola = (Convert.ToInt32(listaStavkiCijiJeRbVeci[i-1].BrojProtokola.Substring(0, listaStavkiCijiJeRbVeci[i-1].BrojProtokola.IndexOf("."))) + 1).ToString() + ".";
                    ctx.SaveChanges();
                }
            }
           
           
           


            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}