namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeCodeColumnToRequiredForMiscellaneousTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Miscellaneous", "Code", c => c.String(nullable: false, maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Miscellaneous", "Code", c => c.String());
        }
    }
}
