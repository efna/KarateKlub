namespace KarateKlub.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IzmjenaClanarine : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Clanarines", "MjesecId", "dbo.Mjesecis");
            DropIndex("dbo.Clanarines", new[] { "MjesecId" });
            AddColumn("dbo.Clanarines", "Naziv", c => c.String());
            AddColumn("dbo.Clanarines", "DatumOd", c => c.DateTime(nullable: false));
            AddColumn("dbo.Clanarines", "DatumDo", c => c.DateTime(nullable: false));
            AddColumn("dbo.StavkeClanarines", "ClanarinaId", c => c.Int(nullable: false));
            CreateIndex("dbo.StavkeClanarines", "ClanarinaId");
            AddForeignKey("dbo.StavkeClanarines", "ClanarinaId", "dbo.Clanarines", "Id");
            DropColumn("dbo.Clanarines", "DatumKreiranja");
            DropColumn("dbo.Clanarines", "MjesecId");
            DropColumn("dbo.Clanarines", "Godina");
            DropTable("dbo.Mjesecis");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Mjesecis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Clanarines", "Godina", c => c.Int(nullable: false));
            AddColumn("dbo.Clanarines", "MjesecId", c => c.Int(nullable: false));
            AddColumn("dbo.Clanarines", "DatumKreiranja", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.StavkeClanarines", "ClanarinaId", "dbo.Clanarines");
            DropIndex("dbo.StavkeClanarines", new[] { "ClanarinaId" });
            DropColumn("dbo.StavkeClanarines", "ClanarinaId");
            DropColumn("dbo.Clanarines", "DatumDo");
            DropColumn("dbo.Clanarines", "DatumOd");
            DropColumn("dbo.Clanarines", "Naziv");
            CreateIndex("dbo.Clanarines", "MjesecId");
            AddForeignKey("dbo.Clanarines", "MjesecId", "dbo.Mjesecis", "Id");
        }
    }
}
