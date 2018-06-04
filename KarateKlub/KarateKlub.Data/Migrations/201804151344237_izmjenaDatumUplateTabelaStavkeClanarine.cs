namespace KarateKlub.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class izmjenaDatumUplateTabelaStavkeClanarine : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StavkeClanarines", "DatumUplate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StavkeClanarines", "DatumUplate", c => c.DateTime(nullable: false));
        }
    }
}
