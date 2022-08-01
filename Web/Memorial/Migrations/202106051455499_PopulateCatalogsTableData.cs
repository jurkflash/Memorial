namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateCatalogsTableData : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Catalogs(SiteId,ProductId,CreateDate) VALUES(1,1,GETDATE())");
            Sql("INSERT INTO Catalogs(SiteId,ProductId,CreateDate) VALUES(1,7,GETDATE())");
            Sql("INSERT INTO Catalogs(SiteId,ProductId,CreateDate) VALUES(2,1,GETDATE())");
            Sql("INSERT INTO Catalogs(SiteId,ProductId,CreateDate) VALUES(2,2,GETDATE())");
            Sql("INSERT INTO Catalogs(SiteId,ProductId,CreateDate) VALUES(2,5,GETDATE())");
            Sql("INSERT INTO Catalogs(SiteId,ProductId,CreateDate) VALUES(2,6,GETDATE())");
            Sql("INSERT INTO Catalogs(SiteId,ProductId,CreateDate) VALUES(2,7,GETDATE())");
            Sql("INSERT INTO Catalogs(SiteId,ProductId,CreateDate) VALUES(3,1,GETDATE())");
            Sql("INSERT INTO Catalogs(SiteId,ProductId,CreateDate) VALUES(3,2,GETDATE())");
            Sql("INSERT INTO Catalogs(SiteId,ProductId,CreateDate) VALUES(3,3,GETDATE())");
            Sql("INSERT INTO Catalogs(SiteId,ProductId,CreateDate) VALUES(3,4,GETDATE())");
            Sql("INSERT INTO Catalogs(SiteId,ProductId,CreateDate) VALUES(3,5,GETDATE())");
        }
        
        public override void Down()
        {
        }
    }
}
