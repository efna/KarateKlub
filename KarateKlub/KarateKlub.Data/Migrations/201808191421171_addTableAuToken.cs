namespace KarateKlub.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTableAuToken : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AutorizacijskiTokens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Vrijednost = c.String(),
                        KorisnickiNalogId = c.Int(nullable: false),
                        VrijemeEvidentiranja = c.DateTime(nullable: false),
                        IpAdresa = c.String(),
                        deviceInfo = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.KorisnickiNalozis", t => t.KorisnickiNalogId)
                .Index(t => t.KorisnickiNalogId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AutorizacijskiTokens", "KorisnickiNalogId", "dbo.KorisnickiNalozis");
            DropIndex("dbo.AutorizacijskiTokens", new[] { "KorisnickiNalogId" });
            DropTable("dbo.AutorizacijskiTokens");
        }
    }
}
