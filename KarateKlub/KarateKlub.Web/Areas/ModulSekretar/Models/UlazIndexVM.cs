﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class UlazPodaci
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public string Naziv { get; set; }
        public decimal IznosKMSBrojevima { get; set; }
        public string IznosKMSlovima { get; set; }
        public DateTime Datum { get; set; }
        public string Obrazlozenje { get; set; }
        public int OsobaId { get; set; }
        public string Osoba { get; set; }
    }
    public class UlazIndexVM
    {
        public List<UlazPodaci> ulazi { get; set; }
    }
}