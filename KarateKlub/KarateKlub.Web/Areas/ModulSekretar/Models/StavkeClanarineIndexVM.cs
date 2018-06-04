using KarateKlub.Data;
using KarateKlub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class StavkaClanarinePodaci
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int ClanarinaId { get; set; }
        public string Clanarina { get; set; }
        public int ClanKlubaId { get; set; }
        public string ClanKluba { get; set; }
        public string BrojPriznanice { get; set; }
        public decimal IznosKMBrojevima { get; set; }
        public string IznosKMSlovima { get; set; }
        public string MjestoUplate { get; set; }
        public string DatumUplate { get; set; }
        public bool isIzmirenaClanarina { get; set; }
    }
    public class StavkeClanarineIndexVM
    {
        MojContext ctx = new MojContext();

        public List<StavkaClanarinePodaci> stavka;
        public int izmirena { get; set; }
        public List<SelectListItem> Izmirenost { get; set; }
        public int clanarinaId { get; set; }
        public Clanarine Clanarina { get; set; }
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
        public StavkeClanarineIndexVM(List<StavkeClanarine> stavke, int izmirena,int clanarinaId)
        {
            stavka = stavke.Select(x => new StavkaClanarinePodaci
            {
                Id = x.Id,
                isDeleted = x.isDeleted,
                ClanarinaId = x.ClanarinaId,
                ClanKlubaId = x.ClanKlubaId,
                Clanarina = x.Clanarina.Naziv + " (" + x.Clanarina.DatumOd.ToString("dd.MM.yyyy") + "-" + x.Clanarina.DatumDo.ToString("dd.MM.yyyy") + " )",
                ClanKluba = x.ClanKluba.Osoba.Ime + " (" + x.ClanKluba.Osoba.ImeRoditelja + ") " + x.ClanKluba.Osoba.Prezime,
                BrojPriznanice = x.BrojPriznanice,
                IznosKMBrojevima = x.IznosKMBrojevima,
                IznosKMSlovima = x.IznosKMSlovima,
                MjestoUplate = x.MjestoUplate,
                DatumUplate =KonvertujUString_mm_dd_yyyy(x.DatumUplate.ToString()),
                isIzmirenaClanarina = x.isIzmirenaClanarina

            }).ToList();
            this.Clanarina= ctx.Clanarine.Where(x => x.Id == clanarinaId).FirstOrDefault();
            this.clanarinaId = clanarinaId;
            Izmirenost = new List<SelectListItem>();
            Izmirenost.Add(new SelectListItem { Text = "Izmirene članarine", Value = "0" });
            Izmirenost.Add(new SelectListItem { Text = "Neizmirene članarine", Value = "1" });

            this.izmirena = izmirena;
        }
   
        public StavkeClanarineIndexVM(List<StavkeClanarine> stavke)
        {
            stavka = stavke.Select(x => new StavkaClanarinePodaci
            {
                Id = x.Id,
                isDeleted = x.isDeleted,
                ClanarinaId = x.ClanarinaId,
                ClanKlubaId = x.ClanKlubaId,
                Clanarina = x.Clanarina.Naziv + " (" + x.Clanarina.DatumOd.ToString("dd.MM.yyyy") + "-" + x.Clanarina.DatumDo.ToString("dd.MM.yyyy") + " )",
                ClanKluba = x.ClanKluba.Osoba.Ime + " (" + x.ClanKluba.Osoba.ImeRoditelja + ") " + x.ClanKluba.Osoba.Prezime,
                BrojPriznanice = x.BrojPriznanice,
                IznosKMBrojevima = x.IznosKMBrojevima,
                IznosKMSlovima = x.IznosKMSlovima,
                MjestoUplate = x.MjestoUplate,
                DatumUplate = KonvertujUString_mm_dd_yyyy(x.DatumUplate.ToString()),
                isIzmirenaClanarina = x.isIzmirenaClanarina

            }).ToList();

        }
    }
    
}