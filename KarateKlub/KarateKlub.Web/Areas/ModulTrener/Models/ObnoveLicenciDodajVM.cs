﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulTrener.Models
{
    public class ObnoveLicenciDodajVM
    {
        public int SeminarId { get; set; }
        public int StecenaLicencaId { get; set; }
        public string DatumObnove { get; set; }
        public string DatumVazenja { get; set; }
    }
}