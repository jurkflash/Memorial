namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMiscellaneousItemsTableData2 : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE MiscellaneousItems SET isOrder='True'");
            Sql("UPDATE MiscellaneousItems SET isOrder='False' WHERE Id=1");
        }
        
        public override void Down()
        {
        }
    }
}
