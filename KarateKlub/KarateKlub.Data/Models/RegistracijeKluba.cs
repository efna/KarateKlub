using KarateKlub.Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateKlub.Data.Models
{
    public class RegistracijeKluba:IEntity
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public string Naziv { get; set; }
        public DateTime DatumRegistracijeKluba { get; set; }
        public DateTime DatumIstekaRegistracijeKluba { get; set; }
        public int SavezId { get; set; }
        public virtual Savezi Savez { get; set; }
        public int OsobaId { get; set; }
        public virtual Osoba Osoba { get; set; }
        
    }
}
