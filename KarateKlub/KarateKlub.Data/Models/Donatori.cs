using KarateKlub.Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateKlub.Data.Models
{
   public class Donatori:IEntity
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int VrstaLicaId { get; set; }
        public virtual VrsteLica VrstaLica { get; set; }
        public string Naziv { get; set; }
        public string ImeOsobe { get; set; }
        public string PrezimeOsobe { get; set; }
        public string KontaktTelefon { get; set; }
        public string Email { get; set; }
    }
}
