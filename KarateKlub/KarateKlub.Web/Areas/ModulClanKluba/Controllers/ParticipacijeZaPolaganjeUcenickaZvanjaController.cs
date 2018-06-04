using KarateKlub.Data;
using KarateKlub.Data.Models;
using KarateKlub.Web.Areas.ModulClanKluba.Models;
using KarateKlub.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulClanKluba.Controllers
{
    [Autorizacija(false, false, true, false, false)]

    public class ParticipacijeZaPolaganjeUcenickaZvanjaController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulClanKluba/ParticipacijeZaPolaganjeUcenickaZvanja
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PregledParticipacijaClana(int osobaId, int izmirena)
        {
            if (izmirena == 0)
            {
                List<ParticipacijeZaPolaganjeUcenickaZvanja> participacijeZaPolaganjeUcenickaZvanja = new List<ParticipacijeZaPolaganjeUcenickaZvanja>();
                participacijeZaPolaganjeUcenickaZvanja = ctx.ParticipacijeZaPolaganjeUcenickaZvanja.Where(x => x.isDeleted == false && x.UcesnikPolaganjaZaUcenickaZvanja.ClanKluba.OsobaId == osobaId).ToList();
                ParticipacijeZaPolaganjeUcenickaZvanjaIndexVM model = new ParticipacijeZaPolaganjeUcenickaZvanjaIndexVM(participacijeZaPolaganjeUcenickaZvanja, osobaId, izmirena);
                return View("PregledIzmirenihParticipacijaClana", model);
            }
            else
            {
                List<UcesniciPolaganjaZaUcenickaZvanja> listaUcesnik = ctx.UcesniciPolaganjaZaUcenickaZvanja.Where(x => x.isDeleted == false && x.ClanKluba.OsobaId == osobaId).ToList();
                List<ParticipacijeZaPolaganjeUcenickaZvanja> participacije = ctx.ParticipacijeZaPolaganjeUcenickaZvanja.Where(x => x.isDeleted == false && x.UcesnikPolaganjaZaUcenickaZvanja.ClanKluba.OsobaId == osobaId).ToList();
                List<int> ucesnikId = new List<int>();
                List<int> partUcesnikaId = new List<int>();
                List<UcesniciPolaganjaZaUcenickaZvanja> listaUcesnikNijePlatio = new List<UcesniciPolaganjaZaUcenickaZvanja>();
                for (int i = 0; i < listaUcesnik.Count(); i++)
                {
                    ucesnikId.Add(listaUcesnik[i].Id);
                }
                for (int i = 0; i < participacije.Count(); i++)
                {
                    partUcesnikaId.Add(participacije[i].UcesnikPolaganjaZaUcenickaZvanjaId);
                }

                for (int i = 0; i < ucesnikId.Count(); i++)
                {
                    int idUcesnika = ucesnikId[i];
                    if (!partUcesnikaId.Contains(ucesnikId[i]))
                    {
                        UcesniciPolaganjaZaUcenickaZvanja ucesnik = ctx.UcesniciPolaganjaZaUcenickaZvanja.Where(x => x.Id == idUcesnika).FirstOrDefault();
                        listaUcesnikNijePlatio.Add(ucesnik);

                    }
                }
                ParticipacijeZaPolaganjeUcenickaZvanjaNeizmireneParticipacijeVM model = new ParticipacijeZaPolaganjeUcenickaZvanjaNeizmireneParticipacijeVM(listaUcesnikNijePlatio,osobaId,izmirena);


                return View("PregledNeizmirenihParticipacijaClana", model);
            }
        }
    }
}