namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMiscellaneousItemsTableData3 : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE MiscellaneousItems SET Name=N'回饋 Reciprocate' WHERE Id=4");
        }
        
        public override void Down()
        {
        }
    }
}
