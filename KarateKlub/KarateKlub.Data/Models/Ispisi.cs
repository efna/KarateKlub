﻿using KarateKlub.Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateKlub.Data.Models
{
  public  class Ispisi:IEntity
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int ClanKlubaId{ get; set; }
        public virtual ClanoviKluba ClanKluba{ get; set; }
        public DateTime DatumIspisa { get; set; }
        public string RazlogIspisa { get; set; }
    }
}
