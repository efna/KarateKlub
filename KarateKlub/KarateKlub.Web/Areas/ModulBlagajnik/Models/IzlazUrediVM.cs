﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulBlagajnik.Models
{
    public class IzlazUrediVM
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public string Naziv { get; set; }
        public string IznosKMSBrojevima { get; set; }
        public string IznosKMSlovima { get; set; }
        public string Datum { get; set; }
        public string Obrazlozenje { get; set; }
        public int OsobaId { get; set; }
    }
}