using KarateKlub.Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateKlub.Data.Models
{
    public class Takmicari:IEntity
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public string RegistarskiBroj { get; set; }
        public int ClanKlubaId { get; set; }
        public virtual ClanoviKluba ClanKluba { get; set; }
        public bool isAktivanTakmicar { get; set; }
        public string Obrazlozenje { get; set; }
    }
}
