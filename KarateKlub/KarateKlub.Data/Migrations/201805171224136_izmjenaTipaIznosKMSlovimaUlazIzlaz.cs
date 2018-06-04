namespace KarateKlub.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class izmjenaTipaIznosKMSlovimaUlazIzlaz : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Izlazs", "IznosKMSlovima", c => c.String());
            AlterColumn("dbo.Ulazs", "IznosKMSlovima", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ulazs", "IznosKMSlovima", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Izlazs", "IznosKMSlovima", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
