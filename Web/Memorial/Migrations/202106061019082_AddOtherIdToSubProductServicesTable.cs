namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOtherIdToSubProductServicesTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SubProductServices", "OtherId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SubProductServices", "OtherId");
        }
    }
}
