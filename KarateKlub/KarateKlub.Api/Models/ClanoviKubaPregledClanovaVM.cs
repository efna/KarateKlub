using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KarateKlub.Api.Models
{
    public class ClanoviKubaPregledClanovaVM
    {
        public class Row {
            public int id;
            public string ime;
            public string prezime;
            public string imeRoditelja;
            public string spol;
            public DateTime datumRodjenja;
            public string mjestoRodjenja;
            public byte[] slika;
            public string kontaktTelefon;
        }

        public List<Row> rows;
    }
}
