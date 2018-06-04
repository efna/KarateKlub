using KarateKlub.Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateKlub.Data.Models
{
   public class KorisnickiNalozi:IEntity
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int OsobaId { get; set; }
        public virtual Osoba Osoba { get; set; }
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public int KorisnickaUlogaId { get; set; }
        public virtual KorisnickeUloge KorisnickaUloga { get; set; }
        public bool isAktivanNalog { get; set; }
    }
}
