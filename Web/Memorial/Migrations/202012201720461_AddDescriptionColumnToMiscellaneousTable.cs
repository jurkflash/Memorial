namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDescriptionColumnToMiscellaneousTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Miscellaneous", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Miscellaneous", "Description");
        }
    }
}
