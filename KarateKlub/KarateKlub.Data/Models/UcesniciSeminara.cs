using KarateKlub.Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateKlub.Data.Models
{
    public class UcesniciSeminara:IEntity
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int SeminariId { get; set; }
        public virtual Seminari Seminar { get; set; }
        public int OsobaId { get; set; }
        public virtual Osoba Osoba { get; set; }

    }
}
