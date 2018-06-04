using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulTrener.Models
{
    public class RezultatTakmicenjaPodaci
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int TakmicenjeId { get; set; }
        public string Takmicenje { get; set; }
        public int TakmicarId { get; set; }
        public string Takmicar { get; set; }
        public int DisciplinaTakmicenjaId { get; set; }
        public virtual string DisciplinaTakmicenja { get; set; }
        public int StarosnaDobId { get; set; }
        public string StarosnaDob { get; set; }
        public string Kategorija { get; set; }
        public string BrojTakmicaraUKategoriji { get; set; }
        public string BrojPobjeda { get; set; }
        public string BrojPoraza { get; set; }
        public int OsvojenoMjestoNaTakmicenjuId { get; set; }
        public virtual string OsvojenoMjestoNaTakmicenju { get; set; }
        public string Obrazlozenje { get; set; }
        public DateTime Datum { get; set; }
        public DateTime DatumOdrzavanjaTakmicenja { get; set; }
        public string MjestoOdrzavanja { get; set; }
    }
    public class RezultatiTakmicenjaIndexVM
    {
        public List<RezultatTakmicenjaPodaci> rezultatiTakmicenja;
        public string NazivTakmicenja { get; set; }
        public int takmicenjeId { get; set; }


    }
}