using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KarateKlub.Api.Models
{
    public class TakmicenjaPregledVM
    {
        public class Row {
            public int id;
            public string nazivTakmicenja;
            public DateTime datumOdrzavanjaTakmicenja;
            public string mjestoOdrzavanjaTakmicenja;
            public string organizatorTakmicenja;
            public int savezId;
            public string nazivSaveza;
        }
        public List<Row> rows;
    }
}
