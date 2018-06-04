using KarateKlub.Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateKlub.Data.Models
{
   public class PolaganjaUcenickaZvanja:IEntity
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public DateTime DatumPolaganja { get; set; }
        public string MjestoPolaganja { get; set; }
        public string OrganizatorPolaganja { get; set; }
        public string Obrazlozenje { get; set; }
    }
}
