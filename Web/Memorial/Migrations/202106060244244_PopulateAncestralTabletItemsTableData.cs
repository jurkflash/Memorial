namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateAncestralTabletItemsTableData : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO AncestralTabletItems(AncestralTabletAreaId, SubProductServiceId, CreateDate) VALUES(1,1,GETDATE())");
            Sql("INSERT INTO AncestralTabletItems(AncestralTabletAreaId, SubProductServiceId, CreateDate) VALUES(1,2,GETDATE())");
            Sql("INSERT INTO AncestralTabletItems(AncestralTabletAreaId, SubProductServiceId, CreateDate) VALUES(1,3,GETDATE())");
            Sql("INSERT INTO AncestralTabletItems(AncestralTabletAreaId, SubProductServiceId, CreateDate) VALUES(1,4,GETDATE())");
            Sql("INSERT INTO AncestralTabletItems(AncestralTabletAreaId, SubProductServiceId, CreateDate) VALUES(2,1,GETDATE())");
            Sql("INSERT INTO AncestralTabletItems(AncestralTabletAreaId, SubProductServiceId, CreateDate) VALUES(2,2,GETDATE())");
            Sql("INSERT INTO AncestralTabletItems(AncestralTabletAreaId, SubProductServiceId, CreateDate) VALUES(2,3,GETDATE())");
            Sql("INSERT INTO AncestralTabletItems(AncestralTabletAreaId, SubProductServiceId, CreateDate) VALUES(2,4,GETDATE())");
        }
        
        public override void Down()
        {
        }
    }
}
