using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KarateKlub.Api.Models
{
   
    public class StecenaZvanjaPregledVM
    {
        public class Row
        {
            public string ime;
            public string prezime;
            public string imeRoditelja;
            public string nazivZvanja;
            public string mjestoSticanja;
            public DateTime datumSticanja;
            public string organizator;


        }
        public List<Row> rows { get; set; }
    }
}
