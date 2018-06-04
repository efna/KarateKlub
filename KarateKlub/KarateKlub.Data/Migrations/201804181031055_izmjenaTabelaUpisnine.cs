namespace KarateKlub.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class izmjenaTabelaUpisnine : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StavkeUpisnines", "ClanKlubaId", "dbo.ClanoviKlubas");
            DropForeignKey("dbo.StavkeUpisnines", "OsobaId", "dbo.Osobas");
            DropForeignKey("dbo.StavkeUpisnines", "UpisninaId", "dbo.Upisnines");
            DropIndex("dbo.StavkeUpisnines", new[] { "OsobaId" });
            DropIndex("dbo.StavkeUpisnines", new[] { "UpisninaId" });
            DropIndex("dbo.StavkeUpisnines", new[] { "ClanKlubaId" });
            AddColumn("dbo.Upisnines", "BrojPriznanice", c => c.String());
            AddColumn("dbo.Upisnines", "IznosKMBrojevima", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Upisnines", "IznosKMSlovima", c => c.String());
            AddColumn("dbo.Upisnines", "MjestoUplate", c => c.String());
            AddColumn("dbo.Upisnines", "DatumUplate", c => c.DateTime());
            AddColumn("dbo.Upisnines", "isIzmirenaClanarina", c => c.Boolean(nullable: false));
            DropColumn("dbo.Upisnines", "Naziv");
            DropTable("dbo.StavkeUpisnines");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Upisnines", "Naziv", c => c.String());
            DropColumn("dbo.Upisnines", "isIzmirenaClanarina");
            DropColumn("dbo.Upisnines", "DatumUplate");
            DropColumn("dbo.Upisnines", "MjestoUplate");
            DropColumn("dbo.Upisnines", "IznosKMSlovima");
            DropColumn("dbo.Upisnines", "IznosKMBrojevima");
            DropColumn("dbo.Upisnines", "BrojPriznanice");
            CreateIndex("dbo.StavkeUpisnines", "ClanKlubaId");
            CreateIndex("dbo.StavkeUpisnines", "UpisninaId");
            CreateIndex("dbo.StavkeUpisnines", "OsobaId");
            AddForeignKey("dbo.StavkeUpisnines", "UpisninaId", "dbo.Upisnines", "Id");
            AddForeignKey("dbo.StavkeUpisnines", "OsobaId", "dbo.Osobas", "Id");
            AddForeignKey("dbo.StavkeUpisnines", "ClanKlubaId", "dbo.ClanoviKlubas", "Id");
        }
    }
}
