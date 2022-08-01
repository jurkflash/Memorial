namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateColumbariumItemsTableData : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO ColumbariumItems(ColumbariumCentreId, SubProductServiceId, CreateDate) VALUES(2,5,GETDATE())");
            Sql("INSERT INTO ColumbariumItems(ColumbariumCentreId, SubProductServiceId, CreateDate) VALUES(2,6,GETDATE())");
            Sql("INSERT INTO ColumbariumItems(ColumbariumCentreId, SubProductServiceId, CreateDate) VALUES(2,7,GETDATE())");
            Sql("INSERT INTO ColumbariumItems(ColumbariumCentreId, SubProductServiceId, CreateDate) VALUES(2,8,GETDATE())");
            Sql("INSERT INTO ColumbariumItems(ColumbariumCentreId, SubProductServiceId, CreateDate) VALUES(2,9,GETDATE())");
            Sql("INSERT INTO ColumbariumItems(ColumbariumCentreId, SubProductServiceId, CreateDate) VALUES(2,10,GETDATE())");
            Sql("INSERT INTO ColumbariumItems(ColumbariumCentreId, SubProductServiceId, CreateDate) VALUES(3,5,GETDATE())");
            Sql("INSERT INTO ColumbariumItems(ColumbariumCentreId, SubProductServiceId, CreateDate) VALUES(3,6,GETDATE())");
            Sql("INSERT INTO ColumbariumItems(ColumbariumCentreId, SubProductServiceId, CreateDate) VALUES(3,7,GETDATE())");
            Sql("INSERT INTO ColumbariumItems(ColumbariumCentreId, SubProductServiceId, CreateDate) VALUES(3,8,GETDATE())");
            Sql("INSERT INTO ColumbariumItems(ColumbariumCentreId, SubProductServiceId, CreateDate) VALUES(3,9,GETDATE())");
            Sql("INSERT INTO ColumbariumItems(ColumbariumCentreId, SubProductServiceId, CreateDate) VALUES(3,10,GETDATE())");
        }
        
        public override void Down()
        {
        }
    }
}
