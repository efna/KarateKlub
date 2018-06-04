using KarateKlub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulBlagajnik.Models
{
    public class SponzorPodaci
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int VrstaLicaId { get; set; }
        public string VrstaLica { get; set; }
        public string Naziv { get; set; }
        public string KontaktOsoba { get; set; }
        public string KontaktTelefon { get; set; }
        public string Email { get; set; }
        public bool isAktivan { get; set; }
    }
    public class SponzoriIndexVM
    {
        public List<SponzorPodaci> listaSponzora;
        public int aktivnost { get; set; }
        public List<SelectListItem> Aktivnost { get; set; }
        public SponzoriIndexVM(List<Sponzori> sponzori, int aktivnost)
        {
            listaSponzora = sponzori.Select(x => new SponzorPodaci
            {
                Id = x.Id,
                isDeleted = x.isDeleted,
                VrstaLicaId = x.VrstaLicaId,
                VrstaLica = x.VrstaLica.Naziv,
                Naziv = x.Naziv,
                KontaktOsoba = x.ImeKontaktOsobe + " " + x.PrezimeKontaktOsobe,
                KontaktTelefon = x.KontaktTelefon,
                Email = x.Email,
                isAktivan = x.isAktivan
            }).ToList();

            Aktivnost = new List<SelectListItem>();
            Aktivnost.Add(new SelectListItem { Text = "Aktivni sponzori", Value = "0" });
            Aktivnost.Add(new SelectListItem { Text = "Neaktivni sponzori", Value = "1" });
            Aktivnost.Add(new SelectListItem { Text = "Svi sponzori", Value = "2" });
            this.aktivnost = aktivnost;
        }
    }
}