namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDataToCatalogsTable : DbMigration
    {
        public override void Up()
        {
            Sql("DELETE FROM Catalogs; DBCC CHECKIDENT('Catalogs', RESEED, 0); ");

            Sql("SET IDENTITY_INSERT Catalogs ON; " +
                "INSERT INTO Catalogs(Id,Name,SiteId) VALUES (1,'Plot',1); " +
                "SET IDENTITY_INSERT Catalogs OFF;");
            Sql("SET IDENTITY_INSERT Catalogs ON; " +
                "INSERT INTO Catalogs(Id,Name,SiteId) VALUES (2,'Plot',2); " +
                "SET IDENTITY_INSERT Catalogs OFF;");
            Sql("SET IDENTITY_INSERT Catalogs ON; " +
                "INSERT INTO Catalogs(Id,Name,SiteId) VALUES (3,'Plot',3); " +
                "SET IDENTITY_INSERT Catalogs OFF;");
            Sql("SET IDENTITY_INSERT Catalogs ON; " +
                "INSERT INTO Catalogs(Id,Name,SiteId) VALUES (4,'Ancestor',2); " +
                "SET IDENTITY_INSERT Catalogs OFF;");
            Sql("SET IDENTITY_INSERT Catalogs ON; " +
                "INSERT INTO Catalogs(Id,Name,SiteId) VALUES (5,'Ancestor',3); " +
                "SET IDENTITY_INSERT Catalogs OFF;");
            Sql("SET IDENTITY_INSERT Catalogs ON; " +
                "INSERT INTO Catalogs(Id,Name,SiteId) VALUES (6,'Cremation',3); " +
                "SET IDENTITY_INSERT Catalogs OFF;");
            Sql("SET IDENTITY_INSERT Catalogs ON; " +
                "INSERT INTO Catalogs(Id,Name,SiteId) VALUES (7,'Urn',3); " +
                "SET IDENTITY_INSERT Catalogs OFF;");
            Sql("SET IDENTITY_INSERT Catalogs ON; " +
                "INSERT INTO Catalogs(Id,Name,SiteId) VALUES (8,'Quadrangle',2); " +
                "SET IDENTITY_INSERT Catalogs OFF;");
            Sql("SET IDENTITY_INSERT Catalogs ON; " +
                "INSERT INTO Catalogs(Id,Name,SiteId) VALUES (9,'Quadrangle',3); " +
                "SET IDENTITY_INSERT Catalogs OFF;");
            Sql("SET IDENTITY_INSERT Catalogs ON; " +
                "INSERT INTO Catalogs(Id,Name,SiteId) VALUES (10,'Space',2); " +
                "SET IDENTITY_INSERT Catalogs OFF;");
            Sql("SET IDENTITY_INSERT Catalogs ON; " +
                "INSERT INTO Catalogs(Id,Name,SiteId) VALUES (11,'Miscellaneous',2); " +
                "SET IDENTITY_INSERT Catalogs OFF;");
        }

        public override void Down()
        {
            Sql("DELETE FROM Catalogs; DBCC CHECKIDENT('Catalogs', RESEED, 0); ");
        }
    }
}
