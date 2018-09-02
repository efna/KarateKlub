using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KarateKlub.Api.Models
{
    public class RezultatiTakmicenjaTrenerPregledVM
    {
        public class Row {
            public int id;
            public int takmicarId;
            public string takmicar;
            public string starosnaDob;
            public string kategorija;
            public string disciplina;
            public string osvojenoMjesto;
            public int brojTakmicaraUKategoriji;
            public int brojPobjeda;
            public int brojPoraza;

        }
        public List<Row> rows;
    }
}
