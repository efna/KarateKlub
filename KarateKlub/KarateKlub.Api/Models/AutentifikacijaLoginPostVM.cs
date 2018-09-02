using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KarateKlub.Api.Models
{
    public class AutentifikacijaLoginPostVM
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string deviceInfo { get; set; }
    }
}
