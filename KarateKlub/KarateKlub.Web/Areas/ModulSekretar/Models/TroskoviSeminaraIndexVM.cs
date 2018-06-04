using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class TrosakSeminaraPodaci
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int SeminarId { get; set; }
        public string Naziv { get; set; }
        public decimal IznosKMBrojevima { get; set; }
        public string IznosKMSlovima { get; set; }
        public DateTime DatumUplate { get; set; }
        public string Obrazlozenje { get; set; }
        public string Seminar { get; set; }
        public string NazivSeminara { get; set; }
        public string MjestoOdrzavanja { get; set; }
        public DateTime DatumOd { get; set; }
        public DateTime DatumDo { get; set; }


    }

    public class TroskoviSeminaraIndexVM
    {
        public List<TrosakSeminaraPodaci> troskoviSeminara;

    }
}