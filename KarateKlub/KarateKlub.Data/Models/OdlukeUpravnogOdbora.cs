using KarateKlub.Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateKlub.Data.Models
{
    public class OdlukeUpravnogOdbora:IEntity
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public string DonesenaOdluka { get; set; }
        public string Obrazlozenje { get; set; }
        public int SjednicaUpravnogOdboraId { get; set; }
        public virtual SjedniceUpravnogOdbora SjednicaUpravnogOdbora { get; set; }
    }
}
