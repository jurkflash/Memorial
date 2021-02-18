namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddisPurchasedColumnToAncestosTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ancestors", "isPurchased", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ancestors", "isPurchased");
        }
    }
}
