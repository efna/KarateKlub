using KarateKlub.Data;
using KarateKlub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class ZaduzenjeOpremeKlubaClanovimaPodaci
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int ClanKlubaId { get; set; }
        public string ClanKluba { get; set; }
        public int VrstaOpremeKlubaId { get; set; }
        public string VrstaOpremeKluba { get; set; }
        public int JedinicaMjereId { get; set; }
        public string JedinicaMjere { get; set; }
        public DateTime DatumZaduzenjaOpreme { get; set; }
        public string Obrazlozenje { get; set; }
        public bool isAktivnoZaduzenje { get; set; }
        public RazduzenaOpremaKlubaClanovi razduzenaOprema { get; set; }

    }

    public class ZaduzenjeOpremeKlubaClanovimaIndexVM
    {
        MojContext ctx = new MojContext();
        public List<ZaduzenjeOpremeKlubaClanovimaPodaci> zaduzenjaOpremaKlubaClanovima;
        public int aktivnost { get; set; }
        public List<SelectListItem> Aktivnost { get; set; }
        public ZaduzenjeOpremeKlubaClanovimaIndexVM(List<ZaduzenjeOpremeKlubaClanovima> zaduzenja, int aktivnost)
        {
            zaduzenjaOpremaKlubaClanovima = zaduzenja.Select(x => new ZaduzenjeOpremeKlubaClanovimaPodaci
            {
                Id = x.Id,
                isDeleted = x.isDeleted,
                ClanKlubaId = x.ClanKlubaId,
                ClanKluba = x.ClanKluba.Osoba.Ime + " (" + x.ClanKluba.Osoba.ImeRoditelja + ") " + x.ClanKluba.Osoba.Prezime,
                VrstaOpremeKlubaId = x.VrstaOpremeKlubaId,
                JedinicaMjereId = x.JedinicaMjereId,
                VrstaOpremeKluba = x.VrstaOpremeKluba.Naziv,
                JedinicaMjere = x.JedinicaMjere.Naziv,
                DatumZaduzenjaOpreme = x.DatumZaduzenjaOpreme,
                Obrazlozenje = x.Obrazlozenje,
                isAktivnoZaduzenje = x.isAktivnoZaduzenje,
                razduzenaOprema=ctx.RazduzenaOpremaKlubaClanovi.Where(y=>y.ZaduzenjeOpremeKlubaClanovimaId==x.Id).FirstOrDefault()


            }).ToList();
            
            Aktivnost = new List<SelectListItem>();
            Aktivnost.Add(new SelectListItem { Text = "Zadužena oprema", Value = "0" });
            Aktivnost.Add(new SelectListItem { Text = "Razdužena oprema", Value = "1" });
            Aktivnost.Add(new SelectListItem { Text = "Svi podaci", Value = "3" });
            this.aktivnost = aktivnost;
        }
    }
}