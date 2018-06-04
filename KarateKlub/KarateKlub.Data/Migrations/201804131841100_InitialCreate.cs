namespace KarateKlub.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Administratoris",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        OsobaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Osobas", t => t.OsobaId)
                .Index(t => t.OsobaId, name: "Index1")
                .Index(t => t.OsobaId, unique: true, name: "Index2");
            
            CreateTable(
                "dbo.Osobas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        Ime = c.String(),
                        Prezime = c.String(),
                        ImeRoditelja = c.String(),
                        JMBG = c.String(),
                        Spol = c.String(),
                        DatumRodjenja = c.DateTime(nullable: false),
                        MjestoRodjenja = c.String(),
                        KontaktTelefon = c.String(),
                        Email = c.String(),
                        NazivSlike = c.String(),
                        TipSlike = c.String(),
                        Slika = c.Binary(),
                        isAdministrator = c.Boolean(nullable: false),
                        isTrener = c.Boolean(nullable: false),
                        isBlagajnik = c.Boolean(nullable: false),
                        isSekretar = c.Boolean(nullable: false),
                        isClanKluba = c.Boolean(nullable: false),
                        isClanUpravnogOdbora = c.Boolean(nullable: false),
                        isKnjigovodja = c.Boolean(nullable: false),
                        isAktivnaOsoba = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Blagajnicis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        DatumZaposlenja = c.DateTime(nullable: false),
                        OsobaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Osobas", t => t.OsobaId)
                .Index(t => t.OsobaId, name: "Index1")
                .Index(t => t.OsobaId, unique: true, name: "Index2");
            
            CreateTable(
                "dbo.ClanoviKlubas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        DatumUpisa = c.DateTime(nullable: false),
                        ZvanjeUKarateuId = c.Int(nullable: false),
                        StarosnaDobId = c.Int(nullable: false),
                        OsobaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Osobas", t => t.OsobaId)
                .ForeignKey("dbo.StarosneDobis", t => t.StarosnaDobId)
                .ForeignKey("dbo.ZvanjaUKarateus", t => t.ZvanjeUKarateuId)
                .Index(t => t.ZvanjeUKarateuId)
                .Index(t => t.StarosnaDobId)
                .Index(t => t.OsobaId, name: "Index1")
                .Index(t => t.OsobaId, unique: true, name: "Index2");
            
            CreateTable(
                "dbo.StarosneDobis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ZvanjaUKarateus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ClanoviUpravnogOdboras",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        OsobaId = c.Int(nullable: false),
                        UlogeClanovaUpravnogOdboraId = c.Int(nullable: false),
                        DatumIzglasavanja = c.DateTime(nullable: false),
                        Aktivan = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Osobas", t => t.OsobaId)
                .ForeignKey("dbo.UlogeClanovaUpravnogOdboras", t => t.UlogeClanovaUpravnogOdboraId)
                .Index(t => t.OsobaId, name: "Index1")
                .Index(t => t.OsobaId, unique: true, name: "Index2")
                .Index(t => t.UlogeClanovaUpravnogOdboraId);
            
            CreateTable(
                "dbo.UlogeClanovaUpravnogOdboras",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Knjigovodjes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        DatumZaposlenja = c.DateTime(nullable: false),
                        OsobaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Osobas", t => t.OsobaId)
                .Index(t => t.OsobaId, name: "Index1")
                .Index(t => t.OsobaId, unique: true, name: "Index2");
            
            CreateTable(
                "dbo.Sekretaris",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        DatumZaposlenja = c.DateTime(nullable: false),
                        OsobaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Osobas", t => t.OsobaId)
                .Index(t => t.OsobaId, name: "Index1")
                .Index(t => t.OsobaId, unique: true, name: "Index2");
            
            CreateTable(
                "dbo.Treneris",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        DatumZaposlenja = c.DateTime(nullable: false),
                        ZvanjeUKarateuId = c.Int(nullable: false),
                        FunkcijaTreneraId = c.Int(nullable: false),
                        OsobaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FunkcijeTreneras", t => t.FunkcijaTreneraId)
                .ForeignKey("dbo.Osobas", t => t.OsobaId)
                .ForeignKey("dbo.ZvanjaUKarateus", t => t.ZvanjeUKarateuId)
                .Index(t => t.ZvanjeUKarateuId)
                .Index(t => t.FunkcijaTreneraId)
                .Index(t => t.OsobaId, name: "Index1")
                .Index(t => t.OsobaId, unique: true, name: "Index2");
            
            CreateTable(
                "dbo.FunkcijeTreneras",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Clanarines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        DatumKreiranja = c.DateTime(nullable: false),
                        MjesecId = c.Int(nullable: false),
                        Godina = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Mjesecis", t => t.MjesecId)
                .Index(t => t.MjesecId);
            
            CreateTable(
                "dbo.Mjesecis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ClanoviEkipes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        EkipaId = c.Int(nullable: false),
                        TakmicarId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ekipas", t => t.EkipaId)
                .ForeignKey("dbo.Takmicaris", t => t.TakmicarId)
                .Index(t => t.EkipaId)
                .Index(t => t.TakmicarId);
            
            CreateTable(
                "dbo.Ekipas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Takmicaris",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        RegistarskiBroj = c.String(),
                        ClanKlubaId = c.Int(nullable: false),
                        isAktivanTakmicar = c.Boolean(nullable: false),
                        Obrazlozenje = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClanoviKlubas", t => t.ClanKlubaId)
                .Index(t => t.ClanKlubaId);
            
            CreateTable(
                "dbo.DisciplineTakmicenjas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Dokumentis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        StavkaProtokolaId = c.Int(nullable: false),
                        NazivDokumenta = c.String(),
                        TipDokumenta = c.String(),
                        Dokument = c.Binary(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StavkeProtokolas", t => t.StavkaProtokolaId)
                .Index(t => t.StavkaProtokolaId);
            
            CreateTable(
                "dbo.StavkeProtokolas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        ProtokolId = c.Int(nullable: false),
                        BrojProtokola = c.String(),
                        Predmet = c.String(),
                        PodBroj = c.String(),
                        DatumPrijema = c.DateTime(nullable: false),
                        Posiljaoc = c.String(),
                        BrojPosiljke = c.String(),
                        DatumPosiljke = c.DateTime(),
                        MjestoPosiljke = c.String(),
                        OrganizacionaJedinica = c.String(),
                        DatumRazvoda = c.DateTime(),
                        Oznaka = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Protokols", t => t.ProtokolId)
                .Index(t => t.ProtokolId);
            
            CreateTable(
                "dbo.Protokols",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OsobaId = c.Int(nullable: false),
                        isDeleted = c.Boolean(nullable: false),
                        Naziv = c.String(),
                        Godina = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Osobas", t => t.OsobaId)
                .Index(t => t.OsobaId);
            
            CreateTable(
                "dbo.Donacijes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        OsobaId = c.Int(nullable: false),
                        DonatorId = c.Int(nullable: false),
                        BrojUplatnice = c.String(),
                        IznosKMBrojevima = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IznosKMSlovima = c.String(),
                        DatumUplate = c.DateTime(nullable: false),
                        Obrazlozenje = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Donatoris", t => t.DonatorId)
                .ForeignKey("dbo.Osobas", t => t.OsobaId)
                .Index(t => t.OsobaId)
                .Index(t => t.DonatorId);
            
            CreateTable(
                "dbo.Donatoris",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        VrstaLicaId = c.Int(nullable: false),
                        Naziv = c.String(),
                        ImeOsobe = c.String(),
                        PrezimeOsobe = c.String(),
                        KontaktTelefon = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.VrsteLicas", t => t.VrstaLicaId)
                .Index(t => t.VrstaLicaId);
            
            CreateTable(
                "dbo.VrsteLicas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IspisaniClanoviKlubas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        ClanKlubaId = c.Int(nullable: false),
                        DatumIspisa = c.DateTime(nullable: false),
                        RazlogIspisa = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClanoviKlubas", t => t.ClanKlubaId)
                .Index(t => t.ClanKlubaId);
            
            CreateTable(
                "dbo.JediniceMjeres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.KazneTakmicaris",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        TakmicarId = c.Int(nullable: false),
                        DodjeljenoOd = c.String(),
                        DodjeljenoZbog = c.String(),
                        DatumSticanja = c.DateTime(nullable: false),
                        DatumIsteka = c.DateTime(nullable: false),
                        MjestoSticanja = c.String(),
                        Obrazlozenje = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Takmicaris", t => t.TakmicarId)
                .Index(t => t.TakmicarId);
            
            CreateTable(
                "dbo.KlubskaOpremas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        VrstaOpremeKlubaId = c.Int(nullable: false),
                        Kolicina = c.Int(nullable: false),
                        JedinicaMjereId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.JediniceMjeres", t => t.JedinicaMjereId)
                .ForeignKey("dbo.VrsteOpremeKlubas", t => t.VrstaOpremeKlubaId)
                .Index(t => t.VrstaOpremeKlubaId)
                .Index(t => t.JedinicaMjereId);
            
            CreateTable(
                "dbo.VrsteOpremeKlubas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.KorisnickeUloges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.KorisnickiNalozis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        OsobaId = c.Int(nullable: false),
                        KorisnickoIme = c.String(),
                        Lozinka = c.String(),
                        KorisnickaUlogaId = c.Int(nullable: false),
                        isAktivanNalog = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.KorisnickeUloges", t => t.KorisnickaUlogaId)
                .ForeignKey("dbo.Osobas", t => t.OsobaId)
                .Index(t => t.OsobaId)
                .Index(t => t.KorisnickaUlogaId);
            
            CreateTable(
                "dbo.LjekarskiPreglediTakmicaras",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        TakmicarId = c.Int(nullable: false),
                        DatumLjekarskogPregleda = c.DateTime(nullable: false),
                        Dijagnoza = c.String(),
                        ImeDoktora = c.String(),
                        PrezimeDoktora = c.String(),
                        Titula = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Takmicaris", t => t.TakmicarId)
                .Index(t => t.TakmicarId);
            
            CreateTable(
                "dbo.NagradeTakmicaris",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        TakmicarId = c.Int(nullable: false),
                        DodjeljenoOd = c.String(),
                        DodjeljenoZbog = c.String(),
                        DatumDodjele = c.DateTime(nullable: false),
                        MjestoDodjele = c.String(),
                        Obrazlozenje = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Takmicaris", t => t.TakmicarId)
                .Index(t => t.TakmicarId);
            
            CreateTable(
                "dbo.NarudzbeOpremeKlubas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        OsobaId = c.Int(nullable: false),
                        NazivNarudzbeOpreme = c.String(),
                        DatumNabavke = c.DateTime(nullable: false),
                        Obrazlozenje = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Osobas", t => t.OsobaId)
                .Index(t => t.OsobaId);
            
            CreateTable(
                "dbo.NivoLicences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ObnoveLicencis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        SeminarId = c.Int(nullable: false),
                        StecenaLicencaId = c.Int(nullable: false),
                        DatumObnove = c.DateTime(nullable: false),
                        DatumVazenja = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Seminaris", t => t.SeminarId)
                .ForeignKey("dbo.SteceneLicences", t => t.StecenaLicencaId)
                .Index(t => t.SeminarId)
                .Index(t => t.StecenaLicencaId);
            
            CreateTable(
                "dbo.Seminaris",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        NazivSeminara = c.String(),
                        MjestoOdrzavanjaSeminara = c.String(),
                        DatumOdrzavanjaSeminaraOd = c.DateTime(nullable: false),
                        DatumOdrzavanjaSeminaraDo = c.DateTime(nullable: false),
                        OrganizatorSeminara = c.String(),
                        Obrazlozenje = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SteceneLicences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        VrstaLicenciId = c.Int(nullable: false),
                        SeminarId = c.Int(nullable: false),
                        NivoLicenceId = c.Int(nullable: false),
                        OsobaId = c.Int(nullable: false),
                        StecenoZvanje = c.String(),
                        DatumSticanja = c.DateTime(nullable: false),
                        DatumVazenja = c.DateTime(nullable: false),
                        Obrazlozenje = c.String(),
                        isAktivnaLicenca = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NivoLicences", t => t.NivoLicenceId)
                .ForeignKey("dbo.Osobas", t => t.OsobaId)
                .ForeignKey("dbo.Seminaris", t => t.SeminarId)
                .ForeignKey("dbo.VrsteLicencis", t => t.VrstaLicenciId)
                .Index(t => t.VrstaLicenciId)
                .Index(t => t.SeminarId)
                .Index(t => t.NivoLicenceId)
                .Index(t => t.OsobaId);
            
            CreateTable(
                "dbo.VrsteLicencis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OdlukeUpravnogOdboras",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        DonesenaOdluka = c.String(),
                        Obrazlozenje = c.String(),
                        SjednicaUpravnogOdboraId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SjedniceUpravnogOdboras", t => t.SjednicaUpravnogOdboraId)
                .Index(t => t.SjednicaUpravnogOdboraId);
            
            CreateTable(
                "dbo.SjedniceUpravnogOdboras",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        DatumOdrzavanja = c.DateTime(nullable: false),
                        Svrha = c.String(),
                        Obrazlozenje = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OsvojenaMjestaNaTakmicenjus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OtpisanaOpremaKlubas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        VrstaOpremeKlubaId = c.Int(nullable: false),
                        OtpisanaKolicina = c.Int(nullable: false),
                        JedinicaMjereId = c.Int(nullable: false),
                        DatumOtpisaOpreme = c.DateTime(nullable: false),
                        Obrazlozenje = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.JediniceMjeres", t => t.JedinicaMjereId)
                .ForeignKey("dbo.VrsteOpremeKlubas", t => t.VrstaOpremeKlubaId)
                .Index(t => t.VrstaOpremeKlubaId)
                .Index(t => t.JedinicaMjereId);
            
            CreateTable(
                "dbo.ParticipacijeZaPolaganjeUcenickaZvanjas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        PolaganjeUcenickaZvanjaId = c.Int(nullable: false),
                        UcesnikPolaganjaZaUcenickaZvanjaId = c.Int(nullable: false),
                        IznosKMBrojevima = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IznosKMSlovima = c.String(),
                        DatumUplate = c.DateTime(nullable: false),
                        BrojPriznanice = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PolaganjaUcenickaZvanjas", t => t.PolaganjeUcenickaZvanjaId)
                .ForeignKey("dbo.UcesniciPolaganjaZaUcenickaZvanjas", t => t.UcesnikPolaganjaZaUcenickaZvanjaId)
                .Index(t => t.PolaganjeUcenickaZvanjaId)
                .Index(t => t.UcesnikPolaganjaZaUcenickaZvanjaId);
            
            CreateTable(
                "dbo.PolaganjaUcenickaZvanjas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        DatumPolaganja = c.DateTime(nullable: false),
                        MjestoPolaganja = c.String(),
                        OrganizatorPolaganja = c.String(),
                        Obrazlozenje = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UcesniciPolaganjaZaUcenickaZvanjas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        PolaganjeUcenickaZvanjaId = c.Int(nullable: false),
                        ClanKlubaId = c.Int(nullable: false),
                        ZvanjeUKarateuId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClanoviKlubas", t => t.ClanKlubaId)
                .ForeignKey("dbo.PolaganjaUcenickaZvanjas", t => t.PolaganjeUcenickaZvanjaId)
                .ForeignKey("dbo.ZvanjaUKarateus", t => t.ZvanjeUKarateuId)
                .Index(t => t.PolaganjeUcenickaZvanjaId)
                .Index(t => t.ClanKlubaId)
                .Index(t => t.ZvanjeUKarateuId);
            
            CreateTable(
                "dbo.PohvaleTakmicaris",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        TakmicarId = c.Int(nullable: false),
                        DodjeljenoOd = c.String(),
                        DodjeljenoZbog = c.String(),
                        DatumDodjele = c.DateTime(nullable: false),
                        MjestoDodjele = c.String(),
                        Obrazlozenje = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Takmicaris", t => t.TakmicarId)
                .Index(t => t.TakmicarId);
            
            CreateTable(
                "dbo.PrisustvaNaSjednicamaUpravnogOdboras",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        ClanUpravnogOdboraId = c.Int(nullable: false),
                        Prisutan = c.Boolean(nullable: false),
                        SjednicaUpravnogOdboraId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClanoviUpravnogOdboras", t => t.ClanUpravnogOdboraId)
                .ForeignKey("dbo.SjedniceUpravnogOdboras", t => t.SjednicaUpravnogOdboraId)
                .Index(t => t.ClanUpravnogOdboraId)
                .Index(t => t.SjednicaUpravnogOdboraId);
            
            CreateTable(
                "dbo.RazduzenaOpremaKlubaClanovis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        ZaduzenjeOpremeKlubaClanovimaId = c.Int(nullable: false),
                        isRazduzeno = c.Boolean(nullable: false),
                        DatumRazduzenjaOpreme = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ZaduzenjeOpremeKlubaClanovimas", t => t.ZaduzenjeOpremeKlubaClanovimaId)
                .Index(t => t.ZaduzenjeOpremeKlubaClanovimaId);
            
            CreateTable(
                "dbo.ZaduzenjeOpremeKlubaClanovimas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        ClanKlubaId = c.Int(nullable: false),
                        VrstaOpremeKlubaId = c.Int(nullable: false),
                        JedinicaMjereId = c.Int(nullable: false),
                        DatumZaduzenjaOpreme = c.DateTime(nullable: false),
                        Obrazlozenje = c.String(),
                        isAktivnoZaduzenje = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClanoviKlubas", t => t.ClanKlubaId)
                .ForeignKey("dbo.JediniceMjeres", t => t.JedinicaMjereId)
                .ForeignKey("dbo.VrsteOpremeKlubas", t => t.VrstaOpremeKlubaId)
                .Index(t => t.ClanKlubaId)
                .Index(t => t.VrstaOpremeKlubaId)
                .Index(t => t.JedinicaMjereId);
            
            CreateTable(
                "dbo.RegistracijeKlubas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        Naziv = c.String(),
                        DatumRegistracijeKluba = c.DateTime(nullable: false),
                        DatumIstekaRegistracijeKluba = c.DateTime(nullable: false),
                        SavezId = c.Int(nullable: false),
                        OsobaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Osobas", t => t.OsobaId)
                .ForeignKey("dbo.Savezis", t => t.SavezId)
                .Index(t => t.SavezId)
                .Index(t => t.OsobaId);
            
            CreateTable(
                "dbo.Savezis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RegistracijeTakmicaras",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        Naziv = c.String(),
                        DatumRegistracijeTakmicara = c.DateTime(nullable: false),
                        DatumIstekaRegistracijeTakmicara = c.DateTime(nullable: false),
                        OsobaId = c.Int(nullable: false),
                        SavezId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Osobas", t => t.OsobaId)
                .ForeignKey("dbo.Savezis", t => t.SavezId)
                .Index(t => t.OsobaId)
                .Index(t => t.SavezId);
            
            CreateTable(
                "dbo.RegistrovaniTakmicaris",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        ClanKlubaId = c.Int(nullable: false),
                        RegistracijaTakmicaraId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClanoviKlubas", t => t.ClanKlubaId)
                .ForeignKey("dbo.RegistracijeTakmicaras", t => t.RegistracijaTakmicaraId)
                .Index(t => t.ClanKlubaId)
                .Index(t => t.RegistracijaTakmicaraId);
            
            CreateTable(
                "dbo.RezultatiPolaganjaUcenickaZvanjas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        PolaganjeUcenickaZvanjaId = c.Int(nullable: false),
                        UcesnikPolaganjaZaUcenickaZvanjaId = c.Int(nullable: false),
                        isPolozio = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PolaganjaUcenickaZvanjas", t => t.PolaganjeUcenickaZvanjaId)
                .ForeignKey("dbo.UcesniciPolaganjaZaUcenickaZvanjas", t => t.UcesnikPolaganjaZaUcenickaZvanjaId)
                .Index(t => t.PolaganjeUcenickaZvanjaId)
                .Index(t => t.UcesnikPolaganjaZaUcenickaZvanjaId);
            
            CreateTable(
                "dbo.RezultatiTakmicenjas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        TakmicenjeId = c.Int(nullable: false),
                        TakmicarId = c.Int(nullable: false),
                        DisciplinaTakmicenjaId = c.Int(nullable: false),
                        StarosnaDobId = c.Int(nullable: false),
                        Kategorija = c.String(),
                        BrojTakmicaraUKategoriji = c.Int(nullable: false),
                        BrojPobjeda = c.Int(nullable: false),
                        BrojPoraza = c.Int(nullable: false),
                        OsvojenoMjestoNaTakmicenjuId = c.Int(nullable: false),
                        Obrazlozenje = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DisciplineTakmicenjas", t => t.DisciplinaTakmicenjaId)
                .ForeignKey("dbo.OsvojenaMjestaNaTakmicenjus", t => t.OsvojenoMjestoNaTakmicenjuId)
                .ForeignKey("dbo.StarosneDobis", t => t.StarosnaDobId)
                .ForeignKey("dbo.Takmicaris", t => t.TakmicarId)
                .ForeignKey("dbo.Takmicenjas", t => t.TakmicenjeId)
                .Index(t => t.TakmicenjeId)
                .Index(t => t.TakmicarId)
                .Index(t => t.DisciplinaTakmicenjaId)
                .Index(t => t.StarosnaDobId)
                .Index(t => t.OsvojenoMjestoNaTakmicenjuId);
            
            CreateTable(
                "dbo.Takmicenjas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        NazivTakmicenja = c.String(),
                        DatumOdrzavanjaTakmicenja = c.DateTime(nullable: false),
                        MjestoOdrzavanjaTakmicenja = c.String(),
                        OrganizatorTakmicenja = c.String(),
                        SavezId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Savezis", t => t.SavezId)
                .Index(t => t.SavezId);
            
            CreateTable(
                "dbo.RezultatiTakmicenjaEkipnoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        EkipaId = c.Int(nullable: false),
                        TakmicenjeId = c.Int(nullable: false),
                        DisciplinaTakmicenjaId = c.Int(nullable: false),
                        StarosnaDobId = c.Int(nullable: false),
                        Kategorija = c.String(),
                        BrojEkipaUKategoriji = c.Int(nullable: false),
                        BrojPobjeda = c.Int(nullable: false),
                        BrojPoraza = c.Int(nullable: false),
                        OsvojenoMjestoNaTakmicenjuId = c.Int(nullable: false),
                        Obrazlozenje = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DisciplineTakmicenjas", t => t.DisciplinaTakmicenjaId)
                .ForeignKey("dbo.Ekipas", t => t.EkipaId)
                .ForeignKey("dbo.OsvojenaMjestaNaTakmicenjus", t => t.OsvojenoMjestoNaTakmicenjuId)
                .ForeignKey("dbo.StarosneDobis", t => t.StarosnaDobId)
                .ForeignKey("dbo.Takmicenjas", t => t.TakmicenjeId)
                .Index(t => t.EkipaId)
                .Index(t => t.TakmicenjeId)
                .Index(t => t.DisciplinaTakmicenjaId)
                .Index(t => t.StarosnaDobId)
                .Index(t => t.OsvojenoMjestoNaTakmicenjuId);
            
            CreateTable(
                "dbo.Sponzoris",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        VrstaLicaId = c.Int(nullable: false),
                        Naziv = c.String(),
                        ImeKontaktOsobe = c.String(),
                        PrezimeKontaktOsobe = c.String(),
                        KontaktTelefon = c.String(),
                        Email = c.String(),
                        isAktivan = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.VrsteLicas", t => t.VrstaLicaId)
                .Index(t => t.VrstaLicaId);
            
            CreateTable(
                "dbo.Sponzorstvas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        OsobaId = c.Int(nullable: false),
                        SponzorId = c.Int(nullable: false),
                        DatumUplate = c.DateTime(nullable: false),
                        IznosKMBrojevima = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IznosKMSlovima = c.String(),
                        Obrazlozenje = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Osobas", t => t.OsobaId)
                .ForeignKey("dbo.Sponzoris", t => t.SponzorId)
                .Index(t => t.OsobaId)
                .Index(t => t.SponzorId);
            
            CreateTable(
                "dbo.StavkeClanarines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        ClanKlubaId = c.Int(nullable: false),
                        BrojPriznanice = c.String(),
                        IznosKMBrojevima = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IznosKMSlovima = c.String(),
                        MjestoUplate = c.String(),
                        DatumUplate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClanoviKlubas", t => t.ClanKlubaId)
                .Index(t => t.ClanKlubaId);
            
            CreateTable(
                "dbo.StavkeNarudzbeOpremeKlubas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        NarudzbaOpremeKlubaId = c.Int(nullable: false),
                        VrstaOpremeKlubaId = c.Int(nullable: false),
                        KolicinaNabavljeneOpreme = c.Int(nullable: false),
                        JedinicaMjereId = c.Int(nullable: false),
                        MarkaNabavljeneOpreme = c.String(),
                        IsWkfEkfApproved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.JediniceMjeres", t => t.JedinicaMjereId)
                .ForeignKey("dbo.NarudzbeOpremeKlubas", t => t.NarudzbaOpremeKlubaId)
                .ForeignKey("dbo.VrsteOpremeKlubas", t => t.VrstaOpremeKlubaId)
                .Index(t => t.NarudzbaOpremeKlubaId)
                .Index(t => t.VrstaOpremeKlubaId)
                .Index(t => t.JedinicaMjereId);
            
            CreateTable(
                "dbo.StavkeUpisnines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        OsobaId = c.Int(nullable: false),
                        UpisninaId = c.Int(nullable: false),
                        ClanKlubaId = c.Int(nullable: false),
                        BrojPriznanice = c.String(),
                        IznosKMBrojevima = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IznosKMSlovima = c.String(),
                        DatumUplate = c.DateTime(nullable: false),
                        MjestoUplate = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClanoviKlubas", t => t.ClanKlubaId)
                .ForeignKey("dbo.Osobas", t => t.OsobaId)
                .ForeignKey("dbo.Upisnines", t => t.UpisninaId)
                .Index(t => t.OsobaId)
                .Index(t => t.UpisninaId)
                .Index(t => t.ClanKlubaId);
            
            CreateTable(
                "dbo.Upisnines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        OsobaId = c.Int(nullable: false),
                        Naziv = c.String(),
                        DatumKreiranja = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Osobas", t => t.OsobaId)
                .Index(t => t.OsobaId);
            
            CreateTable(
                "dbo.StecenaZvanjas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        OsobaId = c.Int(nullable: false),
                        ZvanjeUKarateuId = c.Int(nullable: false),
                        DatumSticanja = c.DateTime(nullable: false),
                        Mjesto = c.String(),
                        Organizator = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Osobas", t => t.OsobaId)
                .ForeignKey("dbo.ZvanjaUKarateus", t => t.ZvanjeUKarateuId)
                .Index(t => t.OsobaId)
                .Index(t => t.ZvanjeUKarateuId);
            
            CreateTable(
                "dbo.TroskoviNarudzbeOpremeKlubas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        NarudzbaOpremeKlubaId = c.Int(nullable: false),
                        IznosKMBrojevima = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IznosKMSlovima = c.String(),
                        DatumUplate = c.DateTime(nullable: false),
                        Obrazlozenje = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NarudzbeOpremeKlubas", t => t.NarudzbaOpremeKlubaId)
                .Index(t => t.NarudzbaOpremeKlubaId);
            
            CreateTable(
                "dbo.TroskoviPolaganjaUcenickaZvanjas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        PolaganjeUcenickaZvanjaId = c.Int(nullable: false),
                        Naziv = c.String(),
                        IznosKMBrojevima = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IznosKMSlovima = c.String(),
                        DatumUplate = c.DateTime(nullable: false),
                        Obrazlozenje = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PolaganjaUcenickaZvanjas", t => t.PolaganjeUcenickaZvanjaId)
                .Index(t => t.PolaganjeUcenickaZvanjaId);
            
            CreateTable(
                "dbo.TroskoviRegistracijeTakmicaras",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        Naziv = c.String(),
                        DatumUplate = c.DateTime(nullable: false),
                        IznosKMBrojevima = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IznosKMSlovima = c.String(),
                        RegistracijaTakmicaraId = c.Int(nullable: false),
                        Obrazlozenje = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RegistracijeTakmicaras", t => t.RegistracijaTakmicaraId)
                .Index(t => t.RegistracijaTakmicaraId);
            
            CreateTable(
                "dbo.TroskoviRegracijeKlubas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        RegistracijaKlubaId = c.Int(nullable: false),
                        DatumUplate = c.DateTime(nullable: false),
                        IznosKMBrojevima = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IznosKMSlovima = c.String(),
                        Obrazlozenje = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RegistracijeKlubas", t => t.RegistracijaKlubaId)
                .Index(t => t.RegistracijaKlubaId);
            
            CreateTable(
                "dbo.TroskoviSeminaras",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        SeminarId = c.Int(nullable: false),
                        Naziv = c.String(),
                        DatumUplate = c.DateTime(nullable: false),
                        IznosKMBrojevima = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IznosKMSlovima = c.String(),
                        Obrazlozenje = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Seminaris", t => t.SeminarId)
                .Index(t => t.SeminarId);
            
            CreateTable(
                "dbo.TroskoviTakmicenjas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        TakmicenjeId = c.Int(nullable: false),
                        Naziv = c.String(),
                        IznosKMBrojevima = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IznosKMSlovima = c.String(),
                        DatumUplate = c.DateTime(nullable: false),
                        Obrazlozenje = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Takmicenjas", t => t.TakmicenjeId)
                .Index(t => t.TakmicenjeId);
            
            CreateTable(
                "dbo.UcesniciSeminaras",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        SeminariId = c.Int(nullable: false),
                        OsobaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Osobas", t => t.OsobaId)
                .ForeignKey("dbo.Seminaris", t => t.SeminariId)
                .Index(t => t.SeminariId)
                .Index(t => t.OsobaId);
            
            CreateTable(
                "dbo.UplateUposlenicimas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        OsobaId = c.Int(nullable: false),
                        DatumUplate = c.DateTime(nullable: false),
                        IznosKMBrojevima = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IznosKMSLovima = c.String(),
                        SvrhaUplate = c.String(),
                        Obrazlozenje = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Osobas", t => t.OsobaId)
                .Index(t => t.OsobaId);
            
            CreateTable(
                "dbo.ZabraneTakmicaris",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        TakmicarId = c.Int(nullable: false),
                        DodjeljenoOd = c.String(),
                        DodjeljenoZbog = c.String(),
                        DatumSticanja = c.DateTime(nullable: false),
                        DatumIsteka = c.DateTime(nullable: false),
                        MjestoSticanja = c.String(),
                        Obrazlozenje = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Takmicaris", t => t.TakmicarId)
                .Index(t => t.TakmicarId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ZabraneTakmicaris", "TakmicarId", "dbo.Takmicaris");
            DropForeignKey("dbo.UplateUposlenicimas", "OsobaId", "dbo.Osobas");
            DropForeignKey("dbo.UcesniciSeminaras", "SeminariId", "dbo.Seminaris");
            DropForeignKey("dbo.UcesniciSeminaras", "OsobaId", "dbo.Osobas");
            DropForeignKey("dbo.TroskoviTakmicenjas", "TakmicenjeId", "dbo.Takmicenjas");
            DropForeignKey("dbo.TroskoviSeminaras", "SeminarId", "dbo.Seminaris");
            DropForeignKey("dbo.TroskoviRegracijeKlubas", "RegistracijaKlubaId", "dbo.RegistracijeKlubas");
            DropForeignKey("dbo.TroskoviRegistracijeTakmicaras", "RegistracijaTakmicaraId", "dbo.RegistracijeTakmicaras");
            DropForeignKey("dbo.TroskoviPolaganjaUcenickaZvanjas", "PolaganjeUcenickaZvanjaId", "dbo.PolaganjaUcenickaZvanjas");
            DropForeignKey("dbo.TroskoviNarudzbeOpremeKlubas", "NarudzbaOpremeKlubaId", "dbo.NarudzbeOpremeKlubas");
            DropForeignKey("dbo.StecenaZvanjas", "ZvanjeUKarateuId", "dbo.ZvanjaUKarateus");
            DropForeignKey("dbo.StecenaZvanjas", "OsobaId", "dbo.Osobas");
            DropForeignKey("dbo.StavkeUpisnines", "UpisninaId", "dbo.Upisnines");
            DropForeignKey("dbo.Upisnines", "OsobaId", "dbo.Osobas");
            DropForeignKey("dbo.StavkeUpisnines", "OsobaId", "dbo.Osobas");
            DropForeignKey("dbo.StavkeUpisnines", "ClanKlubaId", "dbo.ClanoviKlubas");
            DropForeignKey("dbo.StavkeNarudzbeOpremeKlubas", "VrstaOpremeKlubaId", "dbo.VrsteOpremeKlubas");
            DropForeignKey("dbo.StavkeNarudzbeOpremeKlubas", "NarudzbaOpremeKlubaId", "dbo.NarudzbeOpremeKlubas");
            DropForeignKey("dbo.StavkeNarudzbeOpremeKlubas", "JedinicaMjereId", "dbo.JediniceMjeres");
            DropForeignKey("dbo.StavkeClanarines", "ClanKlubaId", "dbo.ClanoviKlubas");
            DropForeignKey("dbo.Sponzorstvas", "SponzorId", "dbo.Sponzoris");
            DropForeignKey("dbo.Sponzorstvas", "OsobaId", "dbo.Osobas");
            DropForeignKey("dbo.Sponzoris", "VrstaLicaId", "dbo.VrsteLicas");
            DropForeignKey("dbo.RezultatiTakmicenjaEkipnoes", "TakmicenjeId", "dbo.Takmicenjas");
            DropForeignKey("dbo.RezultatiTakmicenjaEkipnoes", "StarosnaDobId", "dbo.StarosneDobis");
            DropForeignKey("dbo.RezultatiTakmicenjaEkipnoes", "OsvojenoMjestoNaTakmicenjuId", "dbo.OsvojenaMjestaNaTakmicenjus");
            DropForeignKey("dbo.RezultatiTakmicenjaEkipnoes", "EkipaId", "dbo.Ekipas");
            DropForeignKey("dbo.RezultatiTakmicenjaEkipnoes", "DisciplinaTakmicenjaId", "dbo.DisciplineTakmicenjas");
            DropForeignKey("dbo.RezultatiTakmicenjas", "TakmicenjeId", "dbo.Takmicenjas");
            DropForeignKey("dbo.Takmicenjas", "SavezId", "dbo.Savezis");
            DropForeignKey("dbo.RezultatiTakmicenjas", "TakmicarId", "dbo.Takmicaris");
            DropForeignKey("dbo.RezultatiTakmicenjas", "StarosnaDobId", "dbo.StarosneDobis");
            DropForeignKey("dbo.RezultatiTakmicenjas", "OsvojenoMjestoNaTakmicenjuId", "dbo.OsvojenaMjestaNaTakmicenjus");
            DropForeignKey("dbo.RezultatiTakmicenjas", "DisciplinaTakmicenjaId", "dbo.DisciplineTakmicenjas");
            DropForeignKey("dbo.RezultatiPolaganjaUcenickaZvanjas", "UcesnikPolaganjaZaUcenickaZvanjaId", "dbo.UcesniciPolaganjaZaUcenickaZvanjas");
            DropForeignKey("dbo.RezultatiPolaganjaUcenickaZvanjas", "PolaganjeUcenickaZvanjaId", "dbo.PolaganjaUcenickaZvanjas");
            DropForeignKey("dbo.RegistrovaniTakmicaris", "RegistracijaTakmicaraId", "dbo.RegistracijeTakmicaras");
            DropForeignKey("dbo.RegistrovaniTakmicaris", "ClanKlubaId", "dbo.ClanoviKlubas");
            DropForeignKey("dbo.RegistracijeTakmicaras", "SavezId", "dbo.Savezis");
            DropForeignKey("dbo.RegistracijeTakmicaras", "OsobaId", "dbo.Osobas");
            DropForeignKey("dbo.RegistracijeKlubas", "SavezId", "dbo.Savezis");
            DropForeignKey("dbo.RegistracijeKlubas", "OsobaId", "dbo.Osobas");
            DropForeignKey("dbo.RazduzenaOpremaKlubaClanovis", "ZaduzenjeOpremeKlubaClanovimaId", "dbo.ZaduzenjeOpremeKlubaClanovimas");
            DropForeignKey("dbo.ZaduzenjeOpremeKlubaClanovimas", "VrstaOpremeKlubaId", "dbo.VrsteOpremeKlubas");
            DropForeignKey("dbo.ZaduzenjeOpremeKlubaClanovimas", "JedinicaMjereId", "dbo.JediniceMjeres");
            DropForeignKey("dbo.ZaduzenjeOpremeKlubaClanovimas", "ClanKlubaId", "dbo.ClanoviKlubas");
            DropForeignKey("dbo.PrisustvaNaSjednicamaUpravnogOdboras", "SjednicaUpravnogOdboraId", "dbo.SjedniceUpravnogOdboras");
            DropForeignKey("dbo.PrisustvaNaSjednicamaUpravnogOdboras", "ClanUpravnogOdboraId", "dbo.ClanoviUpravnogOdboras");
            DropForeignKey("dbo.PohvaleTakmicaris", "TakmicarId", "dbo.Takmicaris");
            DropForeignKey("dbo.ParticipacijeZaPolaganjeUcenickaZvanjas", "UcesnikPolaganjaZaUcenickaZvanjaId", "dbo.UcesniciPolaganjaZaUcenickaZvanjas");
            DropForeignKey("dbo.UcesniciPolaganjaZaUcenickaZvanjas", "ZvanjeUKarateuId", "dbo.ZvanjaUKarateus");
            DropForeignKey("dbo.UcesniciPolaganjaZaUcenickaZvanjas", "PolaganjeUcenickaZvanjaId", "dbo.PolaganjaUcenickaZvanjas");
            DropForeignKey("dbo.UcesniciPolaganjaZaUcenickaZvanjas", "ClanKlubaId", "dbo.ClanoviKlubas");
            DropForeignKey("dbo.ParticipacijeZaPolaganjeUcenickaZvanjas", "PolaganjeUcenickaZvanjaId", "dbo.PolaganjaUcenickaZvanjas");
            DropForeignKey("dbo.OtpisanaOpremaKlubas", "VrstaOpremeKlubaId", "dbo.VrsteOpremeKlubas");
            DropForeignKey("dbo.OtpisanaOpremaKlubas", "JedinicaMjereId", "dbo.JediniceMjeres");
            DropForeignKey("dbo.OdlukeUpravnogOdboras", "SjednicaUpravnogOdboraId", "dbo.SjedniceUpravnogOdboras");
            DropForeignKey("dbo.ObnoveLicencis", "StecenaLicencaId", "dbo.SteceneLicences");
            DropForeignKey("dbo.SteceneLicences", "VrstaLicenciId", "dbo.VrsteLicencis");
            DropForeignKey("dbo.SteceneLicences", "SeminarId", "dbo.Seminaris");
            DropForeignKey("dbo.SteceneLicences", "OsobaId", "dbo.Osobas");
            DropForeignKey("dbo.SteceneLicences", "NivoLicenceId", "dbo.NivoLicences");
            DropForeignKey("dbo.ObnoveLicencis", "SeminarId", "dbo.Seminaris");
            DropForeignKey("dbo.NarudzbeOpremeKlubas", "OsobaId", "dbo.Osobas");
            DropForeignKey("dbo.NagradeTakmicaris", "TakmicarId", "dbo.Takmicaris");
            DropForeignKey("dbo.LjekarskiPreglediTakmicaras", "TakmicarId", "dbo.Takmicaris");
            DropForeignKey("dbo.KorisnickiNalozis", "OsobaId", "dbo.Osobas");
            DropForeignKey("dbo.KorisnickiNalozis", "KorisnickaUlogaId", "dbo.KorisnickeUloges");
            DropForeignKey("dbo.KlubskaOpremas", "VrstaOpremeKlubaId", "dbo.VrsteOpremeKlubas");
            DropForeignKey("dbo.KlubskaOpremas", "JedinicaMjereId", "dbo.JediniceMjeres");
            DropForeignKey("dbo.KazneTakmicaris", "TakmicarId", "dbo.Takmicaris");
            DropForeignKey("dbo.IspisaniClanoviKlubas", "ClanKlubaId", "dbo.ClanoviKlubas");
            DropForeignKey("dbo.Donacijes", "OsobaId", "dbo.Osobas");
            DropForeignKey("dbo.Donacijes", "DonatorId", "dbo.Donatoris");
            DropForeignKey("dbo.Donatoris", "VrstaLicaId", "dbo.VrsteLicas");
            DropForeignKey("dbo.Dokumentis", "StavkaProtokolaId", "dbo.StavkeProtokolas");
            DropForeignKey("dbo.StavkeProtokolas", "ProtokolId", "dbo.Protokols");
            DropForeignKey("dbo.Protokols", "OsobaId", "dbo.Osobas");
            DropForeignKey("dbo.ClanoviEkipes", "TakmicarId", "dbo.Takmicaris");
            DropForeignKey("dbo.Takmicaris", "ClanKlubaId", "dbo.ClanoviKlubas");
            DropForeignKey("dbo.ClanoviEkipes", "EkipaId", "dbo.Ekipas");
            DropForeignKey("dbo.Clanarines", "MjesecId", "dbo.Mjesecis");
            DropForeignKey("dbo.Treneris", "ZvanjeUKarateuId", "dbo.ZvanjaUKarateus");
            DropForeignKey("dbo.Treneris", "OsobaId", "dbo.Osobas");
            DropForeignKey("dbo.Treneris", "FunkcijaTreneraId", "dbo.FunkcijeTreneras");
            DropForeignKey("dbo.Sekretaris", "OsobaId", "dbo.Osobas");
            DropForeignKey("dbo.Knjigovodjes", "OsobaId", "dbo.Osobas");
            DropForeignKey("dbo.ClanoviUpravnogOdboras", "UlogeClanovaUpravnogOdboraId", "dbo.UlogeClanovaUpravnogOdboras");
            DropForeignKey("dbo.ClanoviUpravnogOdboras", "OsobaId", "dbo.Osobas");
            DropForeignKey("dbo.ClanoviKlubas", "ZvanjeUKarateuId", "dbo.ZvanjaUKarateus");
            DropForeignKey("dbo.ClanoviKlubas", "StarosnaDobId", "dbo.StarosneDobis");
            DropForeignKey("dbo.ClanoviKlubas", "OsobaId", "dbo.Osobas");
            DropForeignKey("dbo.Blagajnicis", "OsobaId", "dbo.Osobas");
            DropForeignKey("dbo.Administratoris", "OsobaId", "dbo.Osobas");
            DropIndex("dbo.ZabraneTakmicaris", new[] { "TakmicarId" });
            DropIndex("dbo.UplateUposlenicimas", new[] { "OsobaId" });
            DropIndex("dbo.UcesniciSeminaras", new[] { "OsobaId" });
            DropIndex("dbo.UcesniciSeminaras", new[] { "SeminariId" });
            DropIndex("dbo.TroskoviTakmicenjas", new[] { "TakmicenjeId" });
            DropIndex("dbo.TroskoviSeminaras", new[] { "SeminarId" });
            DropIndex("dbo.TroskoviRegracijeKlubas", new[] { "RegistracijaKlubaId" });
            DropIndex("dbo.TroskoviRegistracijeTakmicaras", new[] { "RegistracijaTakmicaraId" });
            DropIndex("dbo.TroskoviPolaganjaUcenickaZvanjas", new[] { "PolaganjeUcenickaZvanjaId" });
            DropIndex("dbo.TroskoviNarudzbeOpremeKlubas", new[] { "NarudzbaOpremeKlubaId" });
            DropIndex("dbo.StecenaZvanjas", new[] { "ZvanjeUKarateuId" });
            DropIndex("dbo.StecenaZvanjas", new[] { "OsobaId" });
            DropIndex("dbo.Upisnines", new[] { "OsobaId" });
            DropIndex("dbo.StavkeUpisnines", new[] { "ClanKlubaId" });
            DropIndex("dbo.StavkeUpisnines", new[] { "UpisninaId" });
            DropIndex("dbo.StavkeUpisnines", new[] { "OsobaId" });
            DropIndex("dbo.StavkeNarudzbeOpremeKlubas", new[] { "JedinicaMjereId" });
            DropIndex("dbo.StavkeNarudzbeOpremeKlubas", new[] { "VrstaOpremeKlubaId" });
            DropIndex("dbo.StavkeNarudzbeOpremeKlubas", new[] { "NarudzbaOpremeKlubaId" });
            DropIndex("dbo.StavkeClanarines", new[] { "ClanKlubaId" });
            DropIndex("dbo.Sponzorstvas", new[] { "SponzorId" });
            DropIndex("dbo.Sponzorstvas", new[] { "OsobaId" });
            DropIndex("dbo.Sponzoris", new[] { "VrstaLicaId" });
            DropIndex("dbo.RezultatiTakmicenjaEkipnoes", new[] { "OsvojenoMjestoNaTakmicenjuId" });
            DropIndex("dbo.RezultatiTakmicenjaEkipnoes", new[] { "StarosnaDobId" });
            DropIndex("dbo.RezultatiTakmicenjaEkipnoes", new[] { "DisciplinaTakmicenjaId" });
            DropIndex("dbo.RezultatiTakmicenjaEkipnoes", new[] { "TakmicenjeId" });
            DropIndex("dbo.RezultatiTakmicenjaEkipnoes", new[] { "EkipaId" });
            DropIndex("dbo.Takmicenjas", new[] { "SavezId" });
            DropIndex("dbo.RezultatiTakmicenjas", new[] { "OsvojenoMjestoNaTakmicenjuId" });
            DropIndex("dbo.RezultatiTakmicenjas", new[] { "StarosnaDobId" });
            DropIndex("dbo.RezultatiTakmicenjas", new[] { "DisciplinaTakmicenjaId" });
            DropIndex("dbo.RezultatiTakmicenjas", new[] { "TakmicarId" });
            DropIndex("dbo.RezultatiTakmicenjas", new[] { "TakmicenjeId" });
            DropIndex("dbo.RezultatiPolaganjaUcenickaZvanjas", new[] { "UcesnikPolaganjaZaUcenickaZvanjaId" });
            DropIndex("dbo.RezultatiPolaganjaUcenickaZvanjas", new[] { "PolaganjeUcenickaZvanjaId" });
            DropIndex("dbo.RegistrovaniTakmicaris", new[] { "RegistracijaTakmicaraId" });
            DropIndex("dbo.RegistrovaniTakmicaris", new[] { "ClanKlubaId" });
            DropIndex("dbo.RegistracijeTakmicaras", new[] { "SavezId" });
            DropIndex("dbo.RegistracijeTakmicaras", new[] { "OsobaId" });
            DropIndex("dbo.RegistracijeKlubas", new[] { "OsobaId" });
            DropIndex("dbo.RegistracijeKlubas", new[] { "SavezId" });
            DropIndex("dbo.ZaduzenjeOpremeKlubaClanovimas", new[] { "JedinicaMjereId" });
            DropIndex("dbo.ZaduzenjeOpremeKlubaClanovimas", new[] { "VrstaOpremeKlubaId" });
            DropIndex("dbo.ZaduzenjeOpremeKlubaClanovimas", new[] { "ClanKlubaId" });
            DropIndex("dbo.RazduzenaOpremaKlubaClanovis", new[] { "ZaduzenjeOpremeKlubaClanovimaId" });
            DropIndex("dbo.PrisustvaNaSjednicamaUpravnogOdboras", new[] { "SjednicaUpravnogOdboraId" });
            DropIndex("dbo.PrisustvaNaSjednicamaUpravnogOdboras", new[] { "ClanUpravnogOdboraId" });
            DropIndex("dbo.PohvaleTakmicaris", new[] { "TakmicarId" });
            DropIndex("dbo.UcesniciPolaganjaZaUcenickaZvanjas", new[] { "ZvanjeUKarateuId" });
            DropIndex("dbo.UcesniciPolaganjaZaUcenickaZvanjas", new[] { "ClanKlubaId" });
            DropIndex("dbo.UcesniciPolaganjaZaUcenickaZvanjas", new[] { "PolaganjeUcenickaZvanjaId" });
            DropIndex("dbo.ParticipacijeZaPolaganjeUcenickaZvanjas", new[] { "UcesnikPolaganjaZaUcenickaZvanjaId" });
            DropIndex("dbo.ParticipacijeZaPolaganjeUcenickaZvanjas", new[] { "PolaganjeUcenickaZvanjaId" });
            DropIndex("dbo.OtpisanaOpremaKlubas", new[] { "JedinicaMjereId" });
            DropIndex("dbo.OtpisanaOpremaKlubas", new[] { "VrstaOpremeKlubaId" });
            DropIndex("dbo.OdlukeUpravnogOdboras", new[] { "SjednicaUpravnogOdboraId" });
            DropIndex("dbo.SteceneLicences", new[] { "OsobaId" });
            DropIndex("dbo.SteceneLicences", new[] { "NivoLicenceId" });
            DropIndex("dbo.SteceneLicences", new[] { "SeminarId" });
            DropIndex("dbo.SteceneLicences", new[] { "VrstaLicenciId" });
            DropIndex("dbo.ObnoveLicencis", new[] { "StecenaLicencaId" });
            DropIndex("dbo.ObnoveLicencis", new[] { "SeminarId" });
            DropIndex("dbo.NarudzbeOpremeKlubas", new[] { "OsobaId" });
            DropIndex("dbo.NagradeTakmicaris", new[] { "TakmicarId" });
            DropIndex("dbo.LjekarskiPreglediTakmicaras", new[] { "TakmicarId" });
            DropIndex("dbo.KorisnickiNalozis", new[] { "KorisnickaUlogaId" });
            DropIndex("dbo.KorisnickiNalozis", new[] { "OsobaId" });
            DropIndex("dbo.KlubskaOpremas", new[] { "JedinicaMjereId" });
            DropIndex("dbo.KlubskaOpremas", new[] { "VrstaOpremeKlubaId" });
            DropIndex("dbo.KazneTakmicaris", new[] { "TakmicarId" });
            DropIndex("dbo.IspisaniClanoviKlubas", new[] { "ClanKlubaId" });
            DropIndex("dbo.Donatoris", new[] { "VrstaLicaId" });
            DropIndex("dbo.Donacijes", new[] { "DonatorId" });
            DropIndex("dbo.Donacijes", new[] { "OsobaId" });
            DropIndex("dbo.Protokols", new[] { "OsobaId" });
            DropIndex("dbo.StavkeProtokolas", new[] { "ProtokolId" });
            DropIndex("dbo.Dokumentis", new[] { "StavkaProtokolaId" });
            DropIndex("dbo.Takmicaris", new[] { "ClanKlubaId" });
            DropIndex("dbo.ClanoviEkipes", new[] { "TakmicarId" });
            DropIndex("dbo.ClanoviEkipes", new[] { "EkipaId" });
            DropIndex("dbo.Clanarines", new[] { "MjesecId" });
            DropIndex("dbo.Treneris", "Index2");
            DropIndex("dbo.Treneris", "Index1");
            DropIndex("dbo.Treneris", new[] { "FunkcijaTreneraId" });
            DropIndex("dbo.Treneris", new[] { "ZvanjeUKarateuId" });
            DropIndex("dbo.Sekretaris", "Index2");
            DropIndex("dbo.Sekretaris", "Index1");
            DropIndex("dbo.Knjigovodjes", "Index2");
            DropIndex("dbo.Knjigovodjes", "Index1");
            DropIndex("dbo.ClanoviUpravnogOdboras", new[] { "UlogeClanovaUpravnogOdboraId" });
            DropIndex("dbo.ClanoviUpravnogOdboras", "Index2");
            DropIndex("dbo.ClanoviUpravnogOdboras", "Index1");
            DropIndex("dbo.ClanoviKlubas", "Index2");
            DropIndex("dbo.ClanoviKlubas", "Index1");
            DropIndex("dbo.ClanoviKlubas", new[] { "StarosnaDobId" });
            DropIndex("dbo.ClanoviKlubas", new[] { "ZvanjeUKarateuId" });
            DropIndex("dbo.Blagajnicis", "Index2");
            DropIndex("dbo.Blagajnicis", "Index1");
            DropIndex("dbo.Administratoris", "Index2");
            DropIndex("dbo.Administratoris", "Index1");
            DropTable("dbo.ZabraneTakmicaris");
            DropTable("dbo.UplateUposlenicimas");
            DropTable("dbo.UcesniciSeminaras");
            DropTable("dbo.TroskoviTakmicenjas");
            DropTable("dbo.TroskoviSeminaras");
            DropTable("dbo.TroskoviRegracijeKlubas");
            DropTable("dbo.TroskoviRegistracijeTakmicaras");
            DropTable("dbo.TroskoviPolaganjaUcenickaZvanjas");
            DropTable("dbo.TroskoviNarudzbeOpremeKlubas");
            DropTable("dbo.StecenaZvanjas");
            DropTable("dbo.Upisnines");
            DropTable("dbo.StavkeUpisnines");
            DropTable("dbo.StavkeNarudzbeOpremeKlubas");
            DropTable("dbo.StavkeClanarines");
            DropTable("dbo.Sponzorstvas");
            DropTable("dbo.Sponzoris");
            DropTable("dbo.RezultatiTakmicenjaEkipnoes");
            DropTable("dbo.Takmicenjas");
            DropTable("dbo.RezultatiTakmicenjas");
            DropTable("dbo.RezultatiPolaganjaUcenickaZvanjas");
            DropTable("dbo.RegistrovaniTakmicaris");
            DropTable("dbo.RegistracijeTakmicaras");
            DropTable("dbo.Savezis");
            DropTable("dbo.RegistracijeKlubas");
            DropTable("dbo.ZaduzenjeOpremeKlubaClanovimas");
            DropTable("dbo.RazduzenaOpremaKlubaClanovis");
            DropTable("dbo.PrisustvaNaSjednicamaUpravnogOdboras");
            DropTable("dbo.PohvaleTakmicaris");
            DropTable("dbo.UcesniciPolaganjaZaUcenickaZvanjas");
            DropTable("dbo.PolaganjaUcenickaZvanjas");
            DropTable("dbo.ParticipacijeZaPolaganjeUcenickaZvanjas");
            DropTable("dbo.OtpisanaOpremaKlubas");
            DropTable("dbo.OsvojenaMjestaNaTakmicenjus");
            DropTable("dbo.SjedniceUpravnogOdboras");
            DropTable("dbo.OdlukeUpravnogOdboras");
            DropTable("dbo.VrsteLicencis");
            DropTable("dbo.SteceneLicences");
            DropTable("dbo.Seminaris");
            DropTable("dbo.ObnoveLicencis");
            DropTable("dbo.NivoLicences");
            DropTable("dbo.NarudzbeOpremeKlubas");
            DropTable("dbo.NagradeTakmicaris");
            DropTable("dbo.LjekarskiPreglediTakmicaras");
            DropTable("dbo.KorisnickiNalozis");
            DropTable("dbo.KorisnickeUloges");
            DropTable("dbo.VrsteOpremeKlubas");
            DropTable("dbo.KlubskaOpremas");
            DropTable("dbo.KazneTakmicaris");
            DropTable("dbo.JediniceMjeres");
            DropTable("dbo.IspisaniClanoviKlubas");
            DropTable("dbo.VrsteLicas");
            DropTable("dbo.Donatoris");
            DropTable("dbo.Donacijes");
            DropTable("dbo.Protokols");
            DropTable("dbo.StavkeProtokolas");
            DropTable("dbo.Dokumentis");
            DropTable("dbo.DisciplineTakmicenjas");
            DropTable("dbo.Takmicaris");
            DropTable("dbo.Ekipas");
            DropTable("dbo.ClanoviEkipes");
            DropTable("dbo.Mjesecis");
            DropTable("dbo.Clanarines");
            DropTable("dbo.FunkcijeTreneras");
            DropTable("dbo.Treneris");
            DropTable("dbo.Sekretaris");
            DropTable("dbo.Knjigovodjes");
            DropTable("dbo.UlogeClanovaUpravnogOdboras");
            DropTable("dbo.ClanoviUpravnogOdboras");
            DropTable("dbo.ZvanjaUKarateus");
            DropTable("dbo.StarosneDobis");
            DropTable("dbo.ClanoviKlubas");
            DropTable("dbo.Blagajnicis");
            DropTable("dbo.Osobas");
            DropTable("dbo.Administratoris");
        }
    }
}
