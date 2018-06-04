using KarateKlub.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace KarateKlub.Data
{
    public class MojContext : DbContext
    {
        public MojContext() : base("name=MojConnectionString")
        { }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();


            modelBuilder
           .Entity<Treneri>()
           .Property(t => t.OsobaId)
           .HasColumnAnnotation(
               "Index",
               new IndexAnnotation(new[]
                        {
                            new IndexAttribute("Index1"),
                            new IndexAttribute("Index2") { IsUnique = true }
                        }));
            modelBuilder
         .Entity<Blagajnici>()
         .Property(t => t.OsobaId)
         .HasColumnAnnotation(
             "Index",
             new IndexAnnotation(new[]
                      {
                            new IndexAttribute("Index1"),
                            new IndexAttribute("Index2") { IsUnique = true }
                      }));

            modelBuilder
     .Entity<Administratori>()
     .Property(t => t.OsobaId)
     .HasColumnAnnotation(
         "Index",
         new IndexAnnotation(new[]
                  {
                            new IndexAttribute("Index1"),
                            new IndexAttribute("Index2") { IsUnique = true }
                  }));
            modelBuilder
.Entity<Sekretari>()
.Property(t => t.OsobaId)
.HasColumnAnnotation(
  "Index",
  new IndexAnnotation(new[]
           {
                            new IndexAttribute("Index1"),
                            new IndexAttribute("Index2") { IsUnique = true }
           }));

            modelBuilder
.Entity<Knjigovodje>()
.Property(t => t.OsobaId)
.HasColumnAnnotation(
  "Index",
  new IndexAnnotation(new[]
           {
                            new IndexAttribute("Index1"),
                            new IndexAttribute("Index2") { IsUnique = true }
           }));
            modelBuilder
 .Entity<ClanoviKluba>()
 .Property(t => t.OsobaId)
 .HasColumnAnnotation(
   "Index",
   new IndexAnnotation(new[]
            {
                            new IndexAttribute("Index1"),
                            new IndexAttribute("Index2") { IsUnique = true }
            }));

            modelBuilder
   .Entity<ClanoviUpravnogOdbora>()
   .Property(t => t.OsobaId)
   .HasColumnAnnotation(
     "Index",
     new IndexAnnotation(new[]
              {
                            new IndexAttribute("Index1"),
                            new IndexAttribute("Index2") { IsUnique = true }
              }));


    
        }
        public DbSet<Osoba> Osoba { get; set; }
        public DbSet<KorisnickiNalozi> KorisnickiNalozi { get; set; }
        public DbSet<KorisnickeUloge> KorisnickeUloge { get; set; }
        public DbSet<Treneri> Treneri { get; set; }
        public DbSet<FunkcijeTrenera> FunkcijeTrenera { get; set; }
        public DbSet<ZvanjaUKarateu> ZvanjaUKarateu { get; set; }
        public DbSet<Blagajnici> Blagajnici { get; set; }
        public DbSet<Administratori> Administratori { get; set; }
        public DbSet<Sekretari> Sekretari { get; set; }
        public DbSet<Knjigovodje> Knjigovodje { get; set; }
        public DbSet<ClanoviKluba> ClanoviKluba { get; set; }
        public DbSet<StarosneDobi> StarosneDobi { get; set; }
        public DbSet<Ispisi> Ispisi { get; set; }
        public DbSet<ClanoviUpravnogOdbora> ClanoviUpravnogOdbora { get; set; }
        public DbSet<UlogeClanovaUpravnogOdbora> UlogeClanovaUpravnogOdbora { get; set; }
        public DbSet<SjedniceUpravnogOdbora> SjedniceUpravnogOdbora { get; set; }
        public DbSet<PrisustvaNaSjednicamaUpravnogOdbora> PrisustvaNaSjednicamaUpravnogOdbora { get; set; }
        public DbSet<OdlukeUpravnogOdbora> OdlukeUpravnogOdbora { get; set; }
        public DbSet<Savezi> Savezi { get; set; }
        public DbSet<RegistracijeKluba> RegistracijeKluba { get; set; }
        public DbSet<TroskoviRegracijeKluba> TroskoviRegracijeKluba { get; set; }
        public DbSet<RegistracijeTakmicara> RegistracijeTakmicara { get; set; }
        public DbSet<TroskoviRegistracijeTakmicara> TroskoviRegistracijeTakmicara { get; set; }
        public DbSet<RegistrovaniTakmicari> RegistrovaniTakmicari { get; set; }
        public DbSet<LjekarskiPreglediTakmicara> LjekarskiPreglediTakmicara { get; set; }
        public DbSet<DisciplineTakmicenja> DisciplineTakmicenja { get; set; }
        public DbSet<OsvojenaMjestaNaTakmicenju> OsvojenaMjestaNaTakmicenju { get; set; }
        public DbSet<Takmicenja> Takmicenja { get; set; }
        public DbSet<Takmicari> Takmicari { get; set; }
        public DbSet<RezultatiTakmicenja> RezultatiTakmicenja { get; set; }
        public DbSet<TroskoviTakmicenja> TroskoviTakmicenja { get; set; }
        public DbSet<NarudzbeOpremeKluba> NarudzbeOpremeKluba { get; set; }
        public DbSet<VrsteOpremeKluba> VrsteOpremeKluba { get; set; }
        public DbSet<StavkeNarudzbeOpremeKluba> StavkeNarudzbeOpremeKluba { get; set; }
        public DbSet<TroskoviNarudzbeOpremeKluba> TroskoviNarudzbeOpremeKluba { get; set; }
        public DbSet<ZaduzenjeOpremeKlubaClanovima> ZaduzenjeOpremeKlubaClanovima { get; set; }
        public DbSet<RazduzenaOpremaKlubaClanovi> RazduzenaOpremaKlubaClanovi { get; set; }
        public DbSet<OtpisanaOpremaKluba> OtpisanaOpremaKluba { get; set; }
        public DbSet<JediniceMjere> JediniceMjere { get; set; }
        public DbSet<PohvaleTakmicari> PohvaleTakmicari { get; set; }
        public DbSet<NagradeTakmicari> NagradeTakmicari { get; set; }
        public DbSet<ZabraneTakmicari> ZabraneTakmicari { get; set; }
        public DbSet<Seminari> Seminari { get; set; }
        public DbSet<UcesniciSeminara> UcesniciSeminara { get; set; }
        public DbSet<TroskoviSeminara> TroskoviSeminara { get; set; }
        public DbSet<VrsteLicenci> VrsteLicenci { get; set; }
        public DbSet<NivoLicence> NivoLicence { get; set; }
        public DbSet<SteceneLicence> SteceneLicence { get; set; }
        public DbSet<VrsteLica> VrsteLica { get; set; }
        public DbSet<Donatori> Donatori { get; set; }
        public DbSet<Donacije> Donacije { get; set; }
        public DbSet<Sponzori> Sponzori { get; set; }
        public DbSet<Sponzorstva> Sponzorstva { get; set; }
        public DbSet<Upisnine> Upisnine { get; set; }
        public DbSet<Clanarine> Clanarine { get; set; }
        public DbSet<StavkeClanarine> StavkeClanarine { get; set; }
        public DbSet<PolaganjaUcenickaZvanja> PolaganjaUcenickaZvanja { get; set; }
        public DbSet<UcesniciPolaganjaZaUcenickaZvanja> UcesniciPolaganjaZaUcenickaZvanja { get; set; }
        public DbSet<ParticipacijeZaPolaganjeUcenickaZvanja> ParticipacijeZaPolaganjeUcenickaZvanja { get; set; }
        public DbSet<TroskoviPolaganjaUcenickaZvanja> TroskoviPolaganjaUcenickaZvanja { get; set; }
        public DbSet<RezultatiPolaganjaUcenickaZvanja> RezultatiPolaganjaUcenickaZvanja { get; set; }
        public DbSet<StecenaZvanja> StecenaZvanja { get; set; }
        public DbSet<UplateUposlenicima> UplateUposlenicima { get; set; }
        public DbSet<Protokol> Protokol { get; set; }
        public DbSet<StavkeProtokola> StavkeProtokola { get; set; }
        public DbSet<Dokumenti> Dokumenti { get; set; }
        public DbSet<KlubskaOprema> KlubskaOprema { get; set; }
        public DbSet<Ekipa> Ekipa { get; set; }
        public DbSet<ClanoviEkipe> ClanoviEkipe { get; set; }
        public DbSet<RezultatiTakmicenjaEkipno> RezultatiTakmicenjaEkipno { get; set; }
        public DbSet<ObnoveLicenci> ObnoveLicenci { get; set; }
        public DbSet<KazneTakmicari> KazneTakmicari { get; set; }
        public DbSet<Upisi> Upisi { get; set; }
        public DbSet<UpisaniClanovi> UpisaniClanovi { get; set; }
        public DbSet<Ulaz> Ulaz { get; set; }
        public DbSet<Izlaz> Izlaz { get; set; }







    }
}
