namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateCremationItemsTableData : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO CremationItems(CremationId, SubProductServiceId, CreateDate) VALUES(1,11,GETDATE())");
            Sql("INSERT INTO CremationItems(CremationId, SubProductServiceId, CreateDate) VALUES(1,12,GETDATE())");
        }
        
        public override void Down()
        {
        }
    }
}
