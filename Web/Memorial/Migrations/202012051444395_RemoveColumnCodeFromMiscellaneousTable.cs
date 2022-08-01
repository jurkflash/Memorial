namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveColumnCodeFromMiscellaneousTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Miscellaneous", "Code");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Miscellaneous", "Code", c => c.String(nullable: false, maxLength: 10));
        }
    }
}
