namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCodeToMiscellaneousTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Miscellaneous", "Code", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Miscellaneous", "Code");
        }
    }
}
