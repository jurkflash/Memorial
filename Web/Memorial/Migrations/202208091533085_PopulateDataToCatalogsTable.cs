namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDataToCatalogsTable : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT Catalogs ON; " +
                "INSERT INTO Catalogs(Id,SiteId,ProductId,ActiveStatus,CreatedDate) VALUES (1,1,1,1,GETDATE()); " +
                "SET IDENTITY_INSERT Catalogs OFF;");
            Sql("SET IDENTITY_INSERT Catalogs ON; " +
                "INSERT INTO Catalogs(Id,SiteId,ProductId,ActiveStatus,CreatedDate) VALUES (2,1,7,1,GETDATE()); " +
                "SET IDENTITY_INSERT Catalogs OFF;");
            Sql("SET IDENTITY_INSERT Catalogs ON; " +
                "INSERT INTO Catalogs(Id,SiteId,ProductId,ActiveStatus,CreatedDate) VALUES (3,2,1,1,GETDATE()); " +
                "SET IDENTITY_INSERT Catalogs OFF;");
            Sql("SET IDENTITY_INSERT Catalogs ON; " +
                "INSERT INTO Catalogs(Id,SiteId,ProductId,ActiveStatus,CreatedDate) VALUES (4,2,2,1,GETDATE()); " +
                "SET IDENTITY_INSERT Catalogs OFF;");
            Sql("SET IDENTITY_INSERT Catalogs ON; " +
                "INSERT INTO Catalogs(Id,SiteId,ProductId,ActiveStatus,CreatedDate) VALUES (5,2,5,1,GETDATE()); " +
                "SET IDENTITY_INSERT Catalogs OFF;");
            Sql("SET IDENTITY_INSERT Catalogs ON; " +
                "INSERT INTO Catalogs(Id,SiteId,ProductId,ActiveStatus,CreatedDate) VALUES (6,2,6,1,GETDATE()); " +
                "SET IDENTITY_INSERT Catalogs OFF;");
            Sql("SET IDENTITY_INSERT Catalogs ON; " +
                "INSERT INTO Catalogs(Id,SiteId,ProductId,ActiveStatus,CreatedDate) VALUES (7,2,7,1,GETDATE()); " +
                "SET IDENTITY_INSERT Catalogs OFF;");
            Sql("SET IDENTITY_INSERT Catalogs ON; " +
                "INSERT INTO Catalogs(Id,SiteId,ProductId,ActiveStatus,CreatedDate) VALUES (8,3,1,1,GETDATE()); " +
                "SET IDENTITY_INSERT Catalogs OFF;");
            Sql("SET IDENTITY_INSERT Catalogs ON; " +
                "INSERT INTO Catalogs(Id,SiteId,ProductId,ActiveStatus,CreatedDate) VALUES (9,3,2,1,GETDATE()); " +
                "SET IDENTITY_INSERT Catalogs OFF;");
            Sql("SET IDENTITY_INSERT Catalogs ON; " +
                "INSERT INTO Catalogs(Id,SiteId,ProductId,ActiveStatus,CreatedDate) VALUES (10,3,3,1,GETDATE()); " +
                "SET IDENTITY_INSERT Catalogs OFF;");
            Sql("SET IDENTITY_INSERT Catalogs ON; " +
                "INSERT INTO Catalogs(Id,SiteId,ProductId,ActiveStatus,CreatedDate) VALUES (11,3,4,1,GETDATE()); " +
                "SET IDENTITY_INSERT Catalogs OFF;");
            Sql("SET IDENTITY_INSERT Catalogs ON; " +
                "INSERT INTO Catalogs(Id,SiteId,ProductId,ActiveStatus,CreatedDate) VALUES (12,3,5,1,GETDATE()); " +
                "SET IDENTITY_INSERT Catalogs OFF;");
            Sql("SET IDENTITY_INSERT Catalogs ON; " +
                "INSERT INTO Catalogs(Id,SiteId,ProductId,ActiveStatus,CreatedDate) VALUES (13,3,6,1,GETDATE()); " +
                "SET IDENTITY_INSERT Catalogs OFF;");
        }
        
        public override void Down()
        {
        }
    }
}
