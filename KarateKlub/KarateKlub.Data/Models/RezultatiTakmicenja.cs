using KarateKlub.Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateKlub.Data.Models
{
  public class RezultatiTakmicenja:IEntity
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int TakmicenjeId { get; set; }
        public virtual Takmicenja Takmicenje { get; set; }
        public int TakmicarId { get; set; }
        public virtual Takmicari Takmicar { get; set; }
        public int DisciplinaTakmicenjaId { get; set; }
        public virtual DisciplineTakmicenja DisciplinaTakmicenja { get; set; }
        public int StarosnaDobId { get; set; }
        public virtual StarosneDobi StarosnaDob { get; set; }
        public string Kategorija { get; set; }
        public int BrojTakmicaraUKategoriji { get; set; }
        public int BrojPobjeda { get; set; }
        public int BrojPoraza { get; set; }
        public int OsvojenoMjestoNaTakmicenjuId { get; set; }
        public virtual OsvojenaMjestaNaTakmicenju OsvojenoMjestoNaTakmicenju { get; set; }
        public string Obrazlozenje { get; set; }

    }
}
