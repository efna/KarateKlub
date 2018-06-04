using KarateKlub.Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateKlub.Data.Models
{
  public class SjedniceUpravnogOdbora:IEntity
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public DateTime DatumOdrzavanja { get; set; }
        public string Svrha { get; set; }
        public string Obrazlozenje { get; set; }
        
    }
}
