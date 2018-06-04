using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Areas.ModulBlagajnik.Models
{
    public class UplateUposlenicimaUrediVM
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public int OsobaId { get; set; }
        public string DatumUplate { get; set; }
        public string DatumOd { get; set; }
        public string DatumDo { get; set; }
        public string IznosKMBrojevima { get; set; }
        public string IznosKMSLovima { get; set; }
        public string SvrhaUplate { get; set; }
        public string Obrazlozenje { get; set; }
        public int funkcijaOsobe { get; set; }
    }
}