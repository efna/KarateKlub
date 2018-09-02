using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateKlub.Data.Models
{
   public class AutorizacijskiToken
    {
        public int Id { get; set; }
        public string Vrijednost { get; set; }
        public int KorisnickiNalogId { get; set; }
        public virtual KorisnickiNalozi KorisnickiNalog { get; set; }
        public DateTime VrijemeEvidentiranja { get; set; }
        public string IpAdresa { get; set; }
        public string deviceInfo { get; set; }
    }
}
