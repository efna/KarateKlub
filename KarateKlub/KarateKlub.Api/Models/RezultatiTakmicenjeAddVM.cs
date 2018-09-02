using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KarateKlub.Api.Models
{
    public class RezultatiTakmicenjeAddVM
    {
        public int takmicenjeId;
        public int takmicarId;
        public int disciplinaTakmicenjaId;
        public int starosnaDobId;
        public string kategorija;
        public string brojTakmicaraUKategoriji;
        public string brojPobjeda;
        public string brojPoraza;
        public int osvojenoMjestoNaTakmicenjuId;
        public string obrazlozenje;
    }
}
