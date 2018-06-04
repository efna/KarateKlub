using KarateKlub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulBlagajnik.Models
{
    public class ParticipacijaZaPolaganjeUcenickaZvanjaPodaci
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int PolaganjeUcenickaZvanjaId { get; set; }
        public int UcesnikPolaganjaZaUcenickaZvanjaId { get; set; }
        public string Ucesnik { get; set; }
        public decimal IznosKMBrojevima { get; set; }
        public string IznosKMSlovima { get; set; }
        public DateTime DatumUplate { get; set; }
        public string BrojPriznanice { get; set; }
        public string ZvanjeZaKojePolaze { get; set; }
        public DateTime DatumPolaganja { get; set; }
        public string MjestoPolaganja { get; set; }
    }
    public class ParticipacijeZaPolaganjeUcenickaZvanjaIndexVM
    {
        public List<ParticipacijaZaPolaganjeUcenickaZvanjaPodaci> participacijeZaPolaganjeUcenickaZvanja { get; set; }
        public int aktivan { get; set; }
        public int izmirena { get; set; }
        public int osobaId { get; set; }
        public List<SelectListItem> Izmirenost { get; set; }

        public ParticipacijeZaPolaganjeUcenickaZvanjaIndexVM(List<ParticipacijeZaPolaganjeUcenickaZvanja> participacije)
        {
            participacijeZaPolaganjeUcenickaZvanja = participacije.Select(x => new ParticipacijaZaPolaganjeUcenickaZvanjaPodaci
            {
                Id = x.Id,
                isDeleted = x.isDeleted,
                PolaganjeUcenickaZvanjaId = x.PolaganjeUcenickaZvanjaId,
                UcesnikPolaganjaZaUcenickaZvanjaId = x.UcesnikPolaganjaZaUcenickaZvanjaId,
                Ucesnik = x.UcesnikPolaganjaZaUcenickaZvanja.ClanKluba.Osoba.Ime + " (" + x.UcesnikPolaganjaZaUcenickaZvanja.ClanKluba.Osoba.ImeRoditelja + ") " + x.UcesnikPolaganjaZaUcenickaZvanja.ClanKluba.Osoba.Prezime,
                BrojPriznanice = x.BrojPriznanice,
                IznosKMBrojevima = x.IznosKMBrojevima,
                IznosKMSlovima = x.IznosKMSlovima,
                DatumUplate = x.DatumUplate,
                ZvanjeZaKojePolaze = x.UcesnikPolaganjaZaUcenickaZvanja.ZvanjeUKarateu.Naziv,
                DatumPolaganja = x.PolaganjeUcenickaZvanja.DatumPolaganja,
                MjestoPolaganja = x.PolaganjeUcenickaZvanja.MjestoPolaganja

            }).ToList();


        }
        public ParticipacijeZaPolaganjeUcenickaZvanjaIndexVM(List<ParticipacijeZaPolaganjeUcenickaZvanja> participacije, int osobaId, int aktivan, int izmirena)
        {
            participacijeZaPolaganjeUcenickaZvanja = participacije.Select(x => new ParticipacijaZaPolaganjeUcenickaZvanjaPodaci
            {
                Id = x.Id,
                isDeleted = x.isDeleted,
                PolaganjeUcenickaZvanjaId = x.PolaganjeUcenickaZvanjaId,
                UcesnikPolaganjaZaUcenickaZvanjaId = x.UcesnikPolaganjaZaUcenickaZvanjaId,
                Ucesnik = x.UcesnikPolaganjaZaUcenickaZvanja.ClanKluba.Osoba.Ime + " (" + x.UcesnikPolaganjaZaUcenickaZvanja.ClanKluba.Osoba.ImeRoditelja + ") " + x.UcesnikPolaganjaZaUcenickaZvanja.ClanKluba.Osoba.Prezime,
                BrojPriznanice = x.BrojPriznanice,
                IznosKMBrojevima = x.IznosKMBrojevima,
                IznosKMSlovima = x.IznosKMSlovima,
                DatumUplate = x.DatumUplate,
                ZvanjeZaKojePolaze = x.UcesnikPolaganjaZaUcenickaZvanja.ZvanjeUKarateu.Naziv,
                DatumPolaganja = x.PolaganjeUcenickaZvanja.DatumPolaganja,
                MjestoPolaganja = x.PolaganjeUcenickaZvanja.MjestoPolaganja

            }).ToList();
            Izmirenost = new List<SelectListItem>();
            Izmirenost.Add(new SelectListItem { Text = "Izmirene participacije", Value = "0" });
            Izmirenost.Add(new SelectListItem { Text = "Neizmirene participacije", Value = "1" });
            this.izmirena = izmirena;
            this.aktivan = aktivan;
            this.osobaId = osobaId;
        }

    }
}