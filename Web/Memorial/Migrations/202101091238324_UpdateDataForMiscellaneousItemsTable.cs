namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDataForMiscellaneousItemsTable : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE MiscellaneousItems SET Name=N'收據 Receipt' WHERE Id=1");
        }
        
        public override void Down()
        {
        }
    }
}
