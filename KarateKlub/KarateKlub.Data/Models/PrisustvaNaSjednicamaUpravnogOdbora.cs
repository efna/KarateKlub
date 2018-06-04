using KarateKlub.Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateKlub.Data.Models
{
    public class PrisustvaNaSjednicamaUpravnogOdbora:IEntity
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int  ClanUpravnogOdboraId { get; set; }
        public virtual ClanoviUpravnogOdbora ClanUpravnogOdbora { get; set; }
        public bool Prisutan { get; set; }
        public int SjednicaUpravnogOdboraId { get; set; }
        public virtual SjedniceUpravnogOdbora SjednicaUpravnogOdbora { get; set; }
    }
}
