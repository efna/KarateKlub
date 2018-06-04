using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulSekretar.Models
{
    public class TroskoviPolaganjaUcenickaZvanjaUrediVM
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int PolaganjeUcenickaZvanjaId { get; set; }
        public string Naziv { get; set; }
        public string IznosKMBrojevima { get; set; }
        public string IznosKMSlovima { get; set; }
        public string DatumUplate { get; set; }
        public string Obrazlozenje { get; set; }
    }
}