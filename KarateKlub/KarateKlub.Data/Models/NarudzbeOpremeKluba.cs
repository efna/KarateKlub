using KarateKlub.Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateKlub.Data.Models
{
    public class NarudzbeOpremeKluba:IEntity
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int OsobaId { get; set; }
        public virtual Osoba Osoba { get; set; }
        public string NazivNarudzbeOpreme { get; set; }
        public DateTime DatumNabavke { get; set; }
        public string Obrazlozenje { get; set; }

    }
}
