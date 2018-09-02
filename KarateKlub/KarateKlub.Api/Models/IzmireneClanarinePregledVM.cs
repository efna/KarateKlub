using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KarateKlub.Api.Models
{
    public class IzmireneClanarinePregledVM
    {
        public class Row
        {
            public int clanarinaId;
            public int clanKlubaId;
            public string clanKluba;
            public string brojPriznanice;
            public string iznosBrojevima;
            public string mjestoUplate;
            public DateTime? datumUplate;
            public DateTime datumOd;
            public DateTime datumDo;
        }
        public List<Row> rows;
    }
}
