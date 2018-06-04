using KarateKlub.Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateKlub.Data.Models
{
   public class Dokumenti:IEntity
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int StavkaProtokolaId { get; set; }
        public virtual StavkeProtokola StavkaProtokola { get; set; }
        public string NazivDokumenta { get; set; }
        public string TipDokumenta { get; set; }
        public byte[] Dokument { get; set; }
    }
}
