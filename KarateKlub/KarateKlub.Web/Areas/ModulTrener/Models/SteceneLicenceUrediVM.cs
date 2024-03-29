﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulTrener.Models
{
    public class SteceneLicenceUrediVM
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int VrstaLicenciId { get; set; }
        public List<SelectListItem> VrsteLicenci { get; set; }
        public int SeminarId { get; set; }
        public int NivoLicenceId { get; set; }
        public List<SelectListItem> NivoiLicenci { get; set; }
        public int ucesnikId { get; set; }
        public List<SelectListItem> UcesniciSeminara { get; set; }
        public string StecenoZvanje { get; set; }
        public string DatumSticanja { get; set; }
        public string DatumVazenja { get; set; }
        public string Obrazlozenje { get; set; }
        public bool isAktivnaLicenca { get; set; }
        public int aktivnost { get; set; }
    }
}