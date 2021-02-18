namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveHasPurchasedAndHasBuriedFromPlotsTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Plots", "hasPurchased");
            DropColumn("dbo.Plots", "hasBuried");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Plots", "hasBuried", c => c.Boolean(nullable: false));
            AddColumn("dbo.Plots", "hasPurchased", c => c.Boolean(nullable: false));
        }
    }
}
