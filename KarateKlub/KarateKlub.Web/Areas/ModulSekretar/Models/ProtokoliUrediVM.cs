﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class ProtokoliUrediVM
    {
        public int Id { get; set; }
        public int OsobaId { get; set; }
        public bool IsDeleted { get; set; }
        public string Naziv { get; set; }
        public string Godina { get; set; }
    }
}