namespace KarateKlub.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUlazIzlaz : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Izlazs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        Naziv = c.String(),
                        IznosKMSBrojevima = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IznosKMSlovima = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Datum = c.DateTime(nullable: false),
                        Obrazlozenje = c.String(),
                        OsobaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Osobas", t => t.OsobaId)
                .Index(t => t.OsobaId);
            
            CreateTable(
                "dbo.Ulazs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        Naziv = c.String(),
                        IznosKMSBrojevima = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IznosKMSlovima = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Datum = c.DateTime(nullable: false),
                        Obrazlozenje = c.String(),
                        OsobaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Osobas", t => t.OsobaId)
                .Index(t => t.OsobaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ulazs", "OsobaId", "dbo.Osobas");
            DropForeignKey("dbo.Izlazs", "OsobaId", "dbo.Osobas");
            DropIndex("dbo.Ulazs", new[] { "OsobaId" });
            DropIndex("dbo.Izlazs", new[] { "OsobaId" });
            DropTable("dbo.Ulazs");
            DropTable("dbo.Izlazs");
        }
    }
}
