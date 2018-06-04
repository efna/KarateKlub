namespace KarateKlub.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class izmjenaKazneTakmicari : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.KazneTakmicaris", "Mjesto", c => c.String());
            DropColumn("dbo.KazneTakmicaris", "MjestoSticanja");
        }
        
        public override void Down()
        {
            AddColumn("dbo.KazneTakmicaris", "MjestoSticanja", c => c.String());
            DropColumn("dbo.KazneTakmicaris", "Mjesto");
        }
    }
}
