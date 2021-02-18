namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovePriceColumnFromCremationsTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Cremations", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cremations", "Price", c => c.Single(nullable: false));
        }
    }
}
