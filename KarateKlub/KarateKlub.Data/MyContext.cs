using KarateKlub.Data.Models;
using Microsoft.EntityFrameworkCore;


namespace KarateKlub.Data
{
    public class MyContext : DbContext
    {

        public MyContext(DbContextOptions<MyContext> x) : base(x)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


        }
        public DbSet<AutorizacijskiToken> AutorizacijskiTokens { get; set; }
        public DbSet<Osoba> Osobas { get; set; }
        public DbSet<KorisnickiNalozi> KorisnickiNalozis { get; set; }
        public DbSet<KorisnickeUloge> KorisnickeUloges { get; set; }
        public DbSet<Treneri> Treneris { get; set; }
        public DbSet<FunkcijeTrenera> FunkcijeTreneras { get; set; }
        public DbSet<ZvanjaUKarateu> ZvanjaUKarateus { get; set; }
        public DbSet<Blagajnici> Blagajnicis { get; set; }
        public DbSet<Administratori> Administratoris { get; set; }
        public DbSet<Sekretari> Sekretaris { get; set; }
        public DbSet<Knjigovodje> Knjigovodjes { get; set; }
        public DbSet<ClanoviKluba> ClanoviKlubas { get; set; }
        public DbSet<StarosneDobi> StarosneDobis { get; set; }
        public DbSet<Ispisi> Ispisis { get; set; }
        public DbSet<ClanoviUpravnogOdbora> ClanoviUpravnogOdboras { get; set; }
        public DbSet<UlogeClanovaUpravnogOdbora> UlogeClanovaUpravnogOdboras { get; set; }
        public DbSet<SjedniceUpravnogOdbora> SjedniceUpravnogOdboras { get; set; }
        public DbSet<PrisustvaNaSjednicamaUpravnogOdbora> PrisustvaNaSjednicamaUpravnogOdboras { get; set; }
        public DbSet<OdlukeUpravnogOdbora> OdlukeUpravnogOdboras { get; set; }
        public DbSet<Savezi> Savezis { get; set; }
        public DbSet<RegistracijeKluba> RegistracijeKlubas { get; set; }
        public DbSet<TroskoviRegracijeKluba> TroskoviRegracijeKlubas { get; set; }
        public DbSet<RegistracijeTakmicara> RegistracijeTakmicaras { get; set; }
        public DbSet<TroskoviRegistracijeTakmicara> TroskoviRegistracijeTakmicaras { get; set; }
        public DbSet<RegistrovaniTakmicari> RegistrovaniTakmicaris { get; set; }
        public DbSet<LjekarskiPreglediTakmicara> LjekarskiPreglediTakmicaras { get; set; }
        public DbSet<DisciplineTakmicenja> DisciplineTakmicenjas { get; set; }
        public DbSet<OsvojenaMjestaNaTakmicenju> OsvojenaMjestaNaTakmicenjus { get; set; }
        public DbSet<Takmicenja> Takmicenjas { get; set; }
        public DbSet<Takmicari> Takmicaris { get; set; }
        public DbSet<RezultatiTakmicenja> RezultatiTakmicenjas { get; set; }
        public DbSet<TroskoviTakmicenja> TroskoviTakmicenjas { get; set; }
        public DbSet<NarudzbeOpremeKluba> NarudzbeOpremeKlubas { get; set; }
        public DbSet<VrsteOpremeKluba> VrsteOpremeKlubas { get; set; }
        public DbSet<StavkeNarudzbeOpremeKluba> StavkeNarudzbeOpremeKlubas { get; set; }
        public DbSet<TroskoviNarudzbeOpremeKluba> TroskoviNarudzbeOpremeKlubas { get; set; }
        public DbSet<ZaduzenjeOpremeKlubaClanovima> ZaduzenjeOpremeKlubaClanovimas { get; set; }
        public DbSet<RazduzenaOpremaKlubaClanovi> RazduzenaOpremaKlubaClanovis { get; set; }
        public DbSet<OtpisanaOpremaKluba> OtpisanaOpremaKlubas { get; set; }
        public DbSet<JediniceMjere> JediniceMjeres { get; set; }
        public DbSet<PohvaleTakmicari> PohvaleTakmicaris { get; set; }
        public DbSet<NagradeTakmicari> NagradeTakmicaris { get; set; }
        public DbSet<ZabraneTakmicari> ZabraneTakmicaris { get; set; }
        public DbSet<Seminari> Seminaris { get; set; }
        public DbSet<UcesniciSeminara> UcesniciSeminaras { get; set; }
        public DbSet<TroskoviSeminara> TroskoviSeminaras { get; set; }
        public DbSet<VrsteLicenci> VrsteLicencis { get; set; }
        public DbSet<NivoLicence> NivoLicences { get; set; }
        public DbSet<SteceneLicence> SteceneLicences { get; set; }
        public DbSet<VrsteLica> VrsteLicas { get; set; }
        public DbSet<Donatori> Donatoris { get; set; }
        public DbSet<Donacije> Donacijes { get; set; }
        public DbSet<Sponzori> Sponzoris { get; set; }
        public DbSet<Sponzorstva> Sponzorstvas { get; set; }
        public DbSet<Upisnine> Upisnines { get; set; }
        public DbSet<Clanarine> Clanarines { get; set; }
        public DbSet<StavkeClanarine> StavkeClanarines { get; set; }
        public DbSet<PolaganjaUcenickaZvanja> PolaganjaUcenickaZvanjas { get; set; }
        public DbSet<UcesniciPolaganjaZaUcenickaZvanja> UcesniciPolaganjaZaUcenickaZvanjas { get; set; }
        public DbSet<ParticipacijeZaPolaganjeUcenickaZvanja> ParticipacijeZaPolaganjeUcenickaZvanjas { get; set; }
        public DbSet<TroskoviPolaganjaUcenickaZvanja> TroskoviPolaganjaUcenickaZvanjas { get; set; }
        public DbSet<RezultatiPolaganjaUcenickaZvanja> RezultatiPolaganjaUcenickaZvanjas { get; set; }
        public DbSet<StecenaZvanja> StecenaZvanjas { get; set; }
        public DbSet<UplateUposlenicima> UplateUposlenicimas { get; set; }
        public DbSet<Protokol> Protokols { get; set; }
        public DbSet<StavkeProtokola> StavkeProtokolas { get; set; }
        public DbSet<Dokumenti> Dokumentis { get; set; }
        public DbSet<KlubskaOprema> KlubskaOpremas { get; set; }
        public DbSet<Ekipa> Ekipas { get; set; }
        public DbSet<ClanoviEkipe> ClanoviEkipes { get; set; }
        public DbSet<RezultatiTakmicenjaEkipno> RezultatiTakmicenjaEkipnos { get; set; }
        public DbSet<ObnoveLicenci> ObnoveLicencis { get; set; }
        public DbSet<KazneTakmicari> KazneTakmicaris { get; set; }
        public DbSet<Upisi> Upisis { get; set; }
        public DbSet<UpisaniClanovi> UpisaniClanovis { get; set; }
        public DbSet<Ulaz> Ulazs { get; set; }
        public DbSet<Izlaz> Izlazs { get; set; }

    }
}
