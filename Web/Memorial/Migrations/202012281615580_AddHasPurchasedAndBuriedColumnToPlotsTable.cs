namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHasPurchasedAndBuriedColumnToPlotsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Plots", "hasPurchased", c => c.Boolean(nullable: false));
            AddColumn("dbo.Plots", "hasBuried", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Plots", "hasBuried");
            DropColumn("dbo.Plots", "hasPurchased");
        }
    }
}
