using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulBlagajnik.Models
{
    public class NarudzbeOpremeKlubaUrediVM
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int OsobaId { get; set; }
        public string NazivNarudzbeOpreme { get; set; }
        public string DatumNabavke { get; set; }
        public string ObrazlozenjeNarudzbe { get; set; }
        public string IznosKMBrojevima { get; set; }
        public string IznosKMSlovima { get; set; }
        public string DatumUplate { get; set; }
        public string ObrazlozenjeUplate { get; set; }
    }
}