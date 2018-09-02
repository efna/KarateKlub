using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KarateKlub.Api.Models
{
    public class TakmicariPregledVM
    {
        public class Row {
            public int id;
            public string takmicar;
            public string regBroj;
            public byte[] slika;

        }

        public List<Row> rows;


    }
}
