
using KarateKlub.Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateKlub.Data.Models
{
 
    public class Osoba : IEntity
    {
        MojContext ctx = new MojContext();
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string ImeRoditelja { get; set; }
        public string JMBG { get; set; }
        public string Spol { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string MjestoRodjenja { get; set; }
        public string KontaktTelefon { get; set; }
        public string Email { get; set; }
        public string NazivSlike { get; set; }
        public string TipSlike { get; set; }
        public byte[] Slika { get; set; }
        public bool isAdministrator { get; set; }
        public bool isTrener { get; set; }
        public bool isBlagajnik { get; set; }
        public bool isSekretar { get; set; }
        public bool isClanKluba { get; set; }
        public bool isClanUpravnogOdbora { get; set; }
        public bool isKnjigovodja { get; set; }
        public bool isAktivnaOsoba { get; set; }

        public List<Treneri> Treneris { get; set; }
        public Treneri Treneri
        {
            get
            {
                return Treneris.FirstOrDefault();
            }
        }


        public List<Blagajnici> Blagajnicis { get; set; }
        public Blagajnici Blagajnici
        {
            get
            {
                return Blagajnicis.FirstOrDefault();
            }
        }

        public List<Administratori> Administratoris { get; set; }
        public Administratori Administratori
        {
            get
            {
                return Administratoris.FirstOrDefault();
            }
        }

        public List<Sekretari> Sekretaris { get; set; }
        public Sekretari Sekretari
        {
            get
            {
                return Sekretaris.FirstOrDefault();
            }
        }

        public List<Knjigovodje> Knjigovodjes { get; set; }
        public Knjigovodje Knjigovodje
        {
            get
            {
                return Knjigovodjes.FirstOrDefault();
            }
        }

        public List<ClanoviKluba> ClanoviKlubas { get; set; }
        public ClanoviKluba ClanoviKluba
        {
            get
            {
                return ClanoviKlubas.FirstOrDefault();
            }
        }

        public List<ClanoviUpravnogOdbora> ClanoviUpravnogOdboras { get; set; }
        public ClanoviUpravnogOdbora ClanoviUpravnogOdbora
        {
            get
            {
                return ClanoviUpravnogOdboras.FirstOrDefault();
            }
        }

    }
}