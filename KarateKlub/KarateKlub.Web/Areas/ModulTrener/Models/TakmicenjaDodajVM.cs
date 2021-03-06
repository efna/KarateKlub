﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulTrener.Models
{
    public class TakmicenjaDodajVM
    {
        public string NazivTakmicenja { get; set; }
        public string DatumOdrzavanjaTakmicenja { get; set; }
        public string MjestoOdrzavanjaTakmicenja { get; set; }
        public string OrganizatorTakmicenja { get; set; }
        public int SavezId { get; set; }
        public List<SelectListItem> savezi { get; set; }
    }
}