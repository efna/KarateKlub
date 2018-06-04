using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulBlagajnik.Models
{
    public class TroskoviTakmicenjaPodaci
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int TakmicenjeId { get; set; }
        public string Naziv { get; set; }
        public string Takmicenje { get; set; }
        public decimal IznosKMBrojevima { get; set; }
        public string IznosKMSlovima { get; set; }
        public DateTime DatumUplate { get; set; }
        public string Obrazlozenje { get; set; }
        public DateTime DatumOdravanjaTakmicenja { get; set; }
        public string MjestOdrzavanjaTakmicenja { get; set; }

    }
    public class TroskoviTakmicenjaIndexVM
    {
        public List<TroskoviTakmicenjaPodaci> troskoviTakmicenja;
        public string NazivTakmicenja { get; set; }
        public int takmicenjeId { get; set; }
    }
}