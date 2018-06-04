namespace KarateKlub.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class izmjenaNazivaIsIzmirenaUpisninaTabelaUpisnine : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Upisnines", "isIzmirenaUpisnina", c => c.Boolean(nullable: false));
            DropColumn("dbo.Upisnines", "isIzmirenaClanarina");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Upisnines", "isIzmirenaClanarina", c => c.Boolean(nullable: false));
            DropColumn("dbo.Upisnines", "isIzmirenaUpisnina");
        }
    }
}
