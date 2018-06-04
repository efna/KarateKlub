namespace KarateKlub.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class izmjenaTabelaUplateUposlenicima : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UplateUposlenicimas", "DatumOd", c => c.DateTime(nullable: false));
            AddColumn("dbo.UplateUposlenicimas", "DatumDo", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UplateUposlenicimas", "DatumDo");
            DropColumn("dbo.UplateUposlenicimas", "DatumOd");
        }
    }
}
