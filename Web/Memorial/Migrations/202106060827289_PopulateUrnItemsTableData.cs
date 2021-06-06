namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateUrnItemsTableData : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO UrnItems(UrnId, SubProductServiceId, CreateDate) VALUES(1,17,GETDATE())");
            Sql("INSERT INTO UrnItems(UrnId, SubProductServiceId, CreateDate) VALUES(2,17,GETDATE())");
        }
        
        public override void Down()
        {
        }
    }
}
