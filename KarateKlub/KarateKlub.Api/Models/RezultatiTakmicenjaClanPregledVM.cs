using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KarateKlub.Api.Models
{

    public class RezultatiTakmicenjaClanPregledVM
    {
        public class Row
        {
            public string nazivTakmicenja { get; set; }
            public string mjestoOdrzavanja { get; set; }
            public DateTime datumOdrzavaja { get; set; }
            public string osvojenoMjesto { get; set; }
            public string kategorija { get; set; }
            public string uzrast { get; set; }
            public string disciplina { get; set; }
        }
        public List<Row> rows { get; set; }
    }
}
