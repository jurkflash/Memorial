namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateMiscellaneousItemsTableData : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO MiscellaneousItems(MiscellaneousId, SubProductServiceId, CreateDate) VALUES(1,13,GETDATE())");
            Sql("INSERT INTO MiscellaneousItems(MiscellaneousId, SubProductServiceId, CreateDate) VALUES(2,14,GETDATE())");
            Sql("INSERT INTO MiscellaneousItems(MiscellaneousId, SubProductServiceId, CreateDate) VALUES(3,15,GETDATE())");
            Sql("INSERT INTO MiscellaneousItems(MiscellaneousId, SubProductServiceId, CreateDate) VALUES(4,16,GETDATE())");
        }
        
        public override void Down()
        {
        }
    }
}
