using KarateKlub.Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateKlub.Data.Models
{
  public  class LjekarskiPreglediTakmicara:IEntity
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int TakmicarId { get; set; }
        public virtual Takmicari Takmicar { get; set; }
        public DateTime DatumLjekarskogPregleda { get; set; }
        public string Dijagnoza { get; set; }
        public string ImeDoktora { get; set; }
        public string PrezimeDoktora { get; set; }
        public string Titula { get; set; }

    }
}
