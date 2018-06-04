﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulBlagajnik.Models
{
    public class TrosakPolaganjaPodaci
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int PolaganjeUcenickaZvanjaId { get; set; }
        public string Naziv { get; set; }
        public decimal IznosKMBrojevima { get; set; }
        public string IznosKMSlovima { get; set; }
        public DateTime DatumUplate { get; set; }
        public string Obrazlozenje { get; set; }
        public string MjestoOdrzavanja { get; set; }
        public DateTime DatumOdrzavanja { get; set; }

    }
    public class TroskoviPolaganjaUcenickaZvanjaIndexVM
    {
        public List<TrosakPolaganjaPodaci> troskoviPolaganja;
    }
}