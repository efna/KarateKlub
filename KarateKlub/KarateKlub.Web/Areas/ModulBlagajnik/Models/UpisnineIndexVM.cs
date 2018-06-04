using KarateKlub.Data;
using KarateKlub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulBlagajnik.Models
{
    public class UpisninaPodaci
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int UpisId { get; set; }
        public string Upis { get; set; }
        public int ClanKlubaId { get; set; }
        public string ClanKluba { get; set; }
        public string BrojPriznanice { get; set; }
        public decimal IznosKMBrojevima { get; set; }
        public string IznosKMSlovima { get; set; }
        public string MjestoUplate { get; set; }
        public string DatumUplate { get; set; }
        public bool isIzmirenaUpisnina { get; set; }
    }
    public class UpisnineIndexVM
    {
        MojContext ctx = new MojContext();

        public List<UpisninaPodaci> upisnineClanova;
        public int izmirena { get; set; }
        public int upisId { get; set; }
        public Upisi Upis { get; set; }

        public List<SelectListItem> Izmirenost { get; set; }
        public string KonvertujUString_mm_dd_yyyy(string datum)
        {
            if (datum != "")
            {
                string dan = datum.Substring(0, 2);
                string mjesec = datum.Substring(3, 2);
                string godina = datum.Substring(6, 4);
                string uneseniDatum = dan + "." + mjesec + "." + godina;
                return uneseniDatum;
            }
            else
                return "";

        }
        public UpisnineIndexVM(List<Upisnine> upisnine, int izmirena, int upisId)
        {
            upisnineClanova = upisnine.Select(x => new UpisninaPodaci
            {
                Id = x.Id,
                isDeleted = x.isDeleted,
                UpisId = x.UpisId,
                Upis = x.Upis.Naziv + " (" + x.Upis.DatumOd.ToString("dd.MM.yyyy") + "-" + x.Upis.DatumDo.ToString("dd.MM.yyyy") + " )",
                ClanKlubaId = x.ClanKlubaId,
                ClanKluba = x.ClanKluba.Osoba.Ime + " (" + x.ClanKluba.Osoba.ImeRoditelja + ") " + x.ClanKluba.Osoba.Prezime,
                BrojPriznanice = x.BrojPriznanice,
                IznosKMBrojevima = x.IznosKMBrojevima,
                IznosKMSlovima = x.IznosKMSlovima,
                MjestoUplate = x.MjestoUplate,
                DatumUplate = KonvertujUString_mm_dd_yyyy(x.DatumUplate.ToString()),
                isIzmirenaUpisnina = x.isIzmirenaUpisnina

            }).ToList();
            this.Upis = ctx.Upisi.Where(x => x.Id == upisId).FirstOrDefault();
            this.upisId = upisId;
            Izmirenost = new List<SelectListItem>();
            Izmirenost.Add(new SelectListItem { Text = "Izmirene upisnine", Value = "0" });
            Izmirenost.Add(new SelectListItem { Text = "Neizmirene upisnine", Value = "1" });

            this.izmirena = izmirena;
        }

        public UpisnineIndexVM(List<Upisnine> upisnine)
        {
            upisnineClanova = upisnine.Select(x => new UpisninaPodaci
            {
                Id = x.Id,
                isDeleted = x.isDeleted,
                UpisId = x.UpisId,
                ClanKlubaId = x.ClanKlubaId,
                Upis = x.Upis.Naziv + " (" + x.Upis.DatumOd.ToString("dd.MM.yyyy") + "-" + x.Upis.DatumDo.ToString("dd.MM.yyyy") + " )",
                ClanKluba = x.ClanKluba.Osoba.Ime + " (" + x.ClanKluba.Osoba.ImeRoditelja + ") " + x.ClanKluba.Osoba.Prezime,
                BrojPriznanice = x.BrojPriznanice,
                IznosKMBrojevima = x.IznosKMBrojevima,
                IznosKMSlovima = x.IznosKMSlovima,
                MjestoUplate = x.MjestoUplate,
                DatumUplate = KonvertujUString_mm_dd_yyyy(x.DatumUplate.ToString()),
                isIzmirenaUpisnina = x.isIzmirenaUpisnina

            }).ToList();

        }
    }
}