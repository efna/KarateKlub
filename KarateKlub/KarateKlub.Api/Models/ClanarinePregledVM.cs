using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KarateKlub.Api.Models
{
    public class ClanarinePregledVM
    {
        public class Row {
            public int id;
            public string naziv;
            public DateTime datumOd;
            public DateTime datumDo;
        }
        public List<Row> rows;
    }
}
