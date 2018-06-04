namespace KarateKlub.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTabelaUpisiUpisanClanoviEditTabelaUpisnice : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Upisnines", "OsobaId", "dbo.Osobas");
            DropIndex("dbo.Upisnines", new[] { "OsobaId" });
            CreateTable(
                "dbo.Upisis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        Naziv = c.String(),
                        DatumOd = c.DateTime(nullable: false),
                        DatumDo = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UpisaniClanovis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isDeleted = c.Boolean(nullable: false),
                        ClanKlubaId = c.Int(nullable: false),
                        UpisId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClanoviKlubas", t => t.ClanKlubaId)
                .ForeignKey("dbo.Upisis", t => t.UpisId)
                .Index(t => t.ClanKlubaId)
                .Index(t => t.UpisId);
            
            AddColumn("dbo.Upisnines", "UpisId", c => c.Int(nullable: false));
            AddColumn("dbo.Upisnines", "ClanKlubaId", c => c.Int(nullable: false));
            CreateIndex("dbo.Upisnines", "UpisId");
            CreateIndex("dbo.Upisnines", "ClanKlubaId");
            AddForeignKey("dbo.Upisnines", "ClanKlubaId", "dbo.ClanoviKlubas", "Id");
            AddForeignKey("dbo.Upisnines", "UpisId", "dbo.Upisis", "Id");
            DropColumn("dbo.Upisnines", "OsobaId");
            DropColumn("dbo.Upisnines", "DatumKreiranja");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Upisnines", "DatumKreiranja", c => c.DateTime(nullable: false));
            AddColumn("dbo.Upisnines", "OsobaId", c => c.Int(nullable: false));
            DropForeignKey("dbo.UpisaniClanovis", "UpisId", "dbo.Upisis");
            DropForeignKey("dbo.UpisaniClanovis", "ClanKlubaId", "dbo.ClanoviKlubas");
            DropForeignKey("dbo.Upisnines", "UpisId", "dbo.Upisis");
            DropForeignKey("dbo.Upisnines", "ClanKlubaId", "dbo.ClanoviKlubas");
            DropIndex("dbo.UpisaniClanovis", new[] { "UpisId" });
            DropIndex("dbo.UpisaniClanovis", new[] { "ClanKlubaId" });
            DropIndex("dbo.Upisnines", new[] { "ClanKlubaId" });
            DropIndex("dbo.Upisnines", new[] { "UpisId" });
            DropColumn("dbo.Upisnines", "ClanKlubaId");
            DropColumn("dbo.Upisnines", "UpisId");
            DropTable("dbo.UpisaniClanovis");
            DropTable("dbo.Upisis");
            CreateIndex("dbo.Upisnines", "OsobaId");
            AddForeignKey("dbo.Upisnines", "OsobaId", "dbo.Osobas", "Id");
        }
    }
}
