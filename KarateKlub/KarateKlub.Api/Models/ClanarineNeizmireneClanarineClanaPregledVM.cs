using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KarateKlub.Api.Models
{

    public class ClanarineNeizmireneClanarineClanaPregledVM
    {
        public class Row
        {
            public int id { get; set; }
            public string naziv { get; set; }
            public DateTime datumOd { get; set; }
            public DateTime datumDo { get; set; }
        }
        public List<Row> rows { get; set; }
    }
}
