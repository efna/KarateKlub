using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KarateKlub.Api.Models
{
    public class NeizmireneClanarinePregledVM
    {
        public class Row
        {
            public int stavkaId;
            public int clanarinaId;
            public int clanKlubaId;
            public string clanKluba;
            public DateTime datumOd;
            public DateTime datumDo;
        }
        public List<Row> rows;
        }
}
