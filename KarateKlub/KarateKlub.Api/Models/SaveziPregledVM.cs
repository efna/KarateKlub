using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KarateKlub.Api.Models
{
    public class SaveziPregledVM
    {
        public class Row {
            public int id;
            public string naziv;
        }
        public List<Row> rows;
    }
}
