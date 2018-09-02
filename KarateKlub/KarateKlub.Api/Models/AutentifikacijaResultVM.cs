using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KarateKlub.Api.Models
{
    public class AutentifikacijaResultVM
    {
        public int osobaId;
        public string username;
        public string ime;
        public string prezime;
        public string token;
        public int tokenId { get; set; }
        public int? korisnickiNalogId;
        public bool isAdministrator;
        public bool isTrener;
        public bool isBlagajnik;
        public bool isSekretar;
        public bool isClanKluba;
        public bool isClanUpravnogOdbora;
        public bool isKnjigovodja;
        public bool isAktivnaOsoba;
        public bool isAktivanNalog;
    }
}
