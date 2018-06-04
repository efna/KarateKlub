using KarateKlub.Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateKlub.Data.Models
{
   public class Takmicenja:IEntity
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public string NazivTakmicenja { get; set; }
        public DateTime DatumOdrzavanjaTakmicenja { get; set; }
        public string MjestoOdrzavanjaTakmicenja { get; set; }
        public string OrganizatorTakmicenja { get; set; }
        public int SavezId { get; set; }
        public virtual Savezi Savez { get; set; }

    }
}
