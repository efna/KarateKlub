using KarateKlub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class SteceneLicencePregledStecenihLicenciVM
    {
        public List<StecenaLicencaPodaci> steceneLicence;
        public int aktivnost { get; set; }
        public List<SelectListItem> Aktivnost { get; set; }
        public int osobaId { get; set; }
        public int aktivan { get; set; }
        public Osoba osoba { get; set; }
        public SteceneLicencePregledStecenihLicenciVM(List<SteceneLicence> listaLicenci, int aktivnostLicence,int osobaId,int aktivan,Osoba osoba)
        {
            steceneLicence = listaLicenci.Select(x => new StecenaLicencaPodaci
            {
                Id = x.Id,
                isDeleted = x.isDeleted,
                VrstaLicenciId = x.VrstaLicenciId,
                VrstaLicence = x.VrstaLicenci.Naziv,
                SeminarId = x.SeminarId,
                Seminar = x.Seminar.NazivSeminara,
                NivoLicenceId = x.NivoLicenceId,
                NivoLicence = x.NivoLicence.Naziv,
                OsobaId = x.OsobaId,
                Osoba = x.Osoba.Ime + " (" + x.Osoba.ImeRoditelja + " )" + x.Osoba.Prezime,
                StecenoZvanje = x.StecenoZvanje,
                DatumSticanja = x.DatumSticanja,
                DatumVazenja = x.DatumVazenja,
                isAktivnaLicenca = x.isAktivnaLicenca,
                Obrazlozenje = x.Obrazlozenje

            }).ToList();
            Aktivnost = new List<SelectListItem>();
            Aktivnost.Add(new SelectListItem { Text = "Sve licence", Value = "0" });
            Aktivnost.Add(new SelectListItem { Text = "Aktivne licence", Value = "1" });

            Aktivnost.Add(new SelectListItem { Text = "Neaktivne licence", Value = "3" });
            this.aktivnost = aktivnostLicence;
            this.osobaId = osobaId;
            this.aktivan = aktivan;
            this.osoba = osoba;
        }
    }
}