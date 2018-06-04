using KarateKlub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class NeizmireneParticipacijePodaci
    {
        public int clanKlubaId { get; set; }
        public int PolaganjeUcenickaZvanjaId { get; set; }
        public int UcesnikPolaganjaZaUcenickaZvanjaId { get; set; }
        public string Ucesnik { get; set; }
        public string ZvanjeZaKojePolaze { get; set; }
        public DateTime DatumPolaganja { get; set; }
        public string MjestoPolaganje { get; set; }
    }
    public class ParticipacijeZaPolaganjeUcenickaZvanjaNeizmireneParticipacijeVM
    {
        public List<NeizmireneParticipacijePodaci> neizmireneParticipacije { get; set; }

        public int aktivan { get; set; }
        public int izmirena { get; set; }
        public int osobaId { get; set; }
        public List<SelectListItem> Izmirenost { get; set; }

        public ParticipacijeZaPolaganjeUcenickaZvanjaNeizmireneParticipacijeVM(List<UcesniciPolaganjaZaUcenickaZvanja> listaUcesnikaKojiNisuPlatili,int polaganjeId)
        {
            neizmireneParticipacije = listaUcesnikaKojiNisuPlatili.Select(x => new NeizmireneParticipacijePodaci
            {
                clanKlubaId = x.ClanKlubaId,
                PolaganjeUcenickaZvanjaId = polaganjeId,
                UcesnikPolaganjaZaUcenickaZvanjaId = x.Id,
                Ucesnik = x.ClanKluba.Osoba.Ime + " (" + x.ClanKluba.Osoba.ImeRoditelja + ") " + x.ClanKluba.Osoba.Prezime,
                ZvanjeZaKojePolaze = x.ZvanjeUKarateu.Naziv,
                DatumPolaganja = x.PolaganjeUcenickaZvanja.DatumPolaganja,
                MjestoPolaganje = x.PolaganjeUcenickaZvanja.MjestoPolaganja
            }).ToList();

        }
        public ParticipacijeZaPolaganjeUcenickaZvanjaNeizmireneParticipacijeVM(List<UcesniciPolaganjaZaUcenickaZvanja> listaUcesnikNijePlatio, int osobaId, int aktivan, int izmirena)
        {
            neizmireneParticipacije = listaUcesnikNijePlatio.Select(x => new NeizmireneParticipacijePodaci
            {
                clanKlubaId = x.ClanKlubaId,
                UcesnikPolaganjaZaUcenickaZvanjaId = x.Id,
                Ucesnik = x.ClanKluba.Osoba.Ime + " (" + x.ClanKluba.Osoba.ImeRoditelja + ") " + x.ClanKluba.Osoba.Prezime,
                ZvanjeZaKojePolaze = x.ZvanjeUKarateu.Naziv,
                DatumPolaganja = x.PolaganjeUcenickaZvanja.DatumPolaganja,
                MjestoPolaganje = x.PolaganjeUcenickaZvanja.MjestoPolaganja
            }).ToList();
            Izmirenost = new List<SelectListItem>();
            Izmirenost.Add(new SelectListItem { Text = "Izmirene participacije", Value = "0" });
            Izmirenost.Add(new SelectListItem { Text = "NeizmireneParticipacije", Value = "1" });
            this.izmirena = izmirena;
            this.aktivan = aktivan;
            this.osobaId = osobaId;
        }
    }
}