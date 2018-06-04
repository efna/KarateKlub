namespace KarateKlub.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIsIzmirenaClanarinaTabelaStavkeClanarine : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StavkeClanarines", "isIzmirenaClanarina", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StavkeClanarines", "isIzmirenaClanarina");
        }
    }
}
