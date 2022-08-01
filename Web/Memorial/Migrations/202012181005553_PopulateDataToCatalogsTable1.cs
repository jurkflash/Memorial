namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDataToCatalogsTable1 : DbMigration
    {
        public override void Up()
        {
            Sql("DELETE FROM Catalogs; DBCC CHECKIDENT('Catalogs', RESEED, 0); ");

            Sql("SET IDENTITY_INSERT Catalogs ON; " +
                "INSERT INTO Catalogs(Id,Name,Code,SiteId) VALUES (1,'Plot','Plot',1); " +
                "SET IDENTITY_INSERT Catalogs OFF;");
            Sql("SET IDENTITY_INSERT Catalogs ON; " +
                "INSERT INTO Catalogs(Id,Name,Code,SiteId) VALUES (2,'Plot','Plot',2); " +
                "SET IDENTITY_INSERT Catalogs OFF;");
            Sql("SET IDENTITY_INSERT Catalogs ON; " +
                "INSERT INTO Catalogs(Id,Name,Code,SiteId) VALUES (3,'Plot','Plot',3); " +
                "SET IDENTITY_INSERT Catalogs OFF;");
            Sql("SET IDENTITY_INSERT Catalogs ON; " +
                "INSERT INTO Catalogs(Id,Name,Code,SiteId) VALUES (4,'Ancestor','Ancestor',2); " +
                "SET IDENTITY_INSERT Catalogs OFF;");
            Sql("SET IDENTITY_INSERT Catalogs ON; " +
                "INSERT INTO Catalogs(Id,Name,Code,SiteId) VALUES (5,'Ancestor','Ancestor',3); " +
                "SET IDENTITY_INSERT Catalogs OFF;");
            Sql("SET IDENTITY_INSERT Catalogs ON; " +
                "INSERT INTO Catalogs(Id,Name,Code,SiteId) VALUES (6,'Cremation','Cremation',3); " +
                "SET IDENTITY_INSERT Catalogs OFF;");
            Sql("SET IDENTITY_INSERT Catalogs ON; " +
                "INSERT INTO Catalogs(Id,Name,Code,SiteId) VALUES (7,'Urn','Urn',3); " +
                "SET IDENTITY_INSERT Catalogs OFF;");
            Sql("SET IDENTITY_INSERT Catalogs ON; " +
                "INSERT INTO Catalogs(Id,Name,Code,SiteId) VALUES (8,'Quadrangle','Quadrangle',2); " +
                "SET IDENTITY_INSERT Catalogs OFF;");
            Sql("SET IDENTITY_INSERT Catalogs ON; " +
                "INSERT INTO Catalogs(Id,Name,Code,SiteId) VALUES (9,'Quadrangle','Quadrangle',3); " +
                "SET IDENTITY_INSERT Catalogs OFF;");
            Sql("SET IDENTITY_INSERT Catalogs ON; " +
                "INSERT INTO Catalogs(Id,Name,Code,SiteId) VALUES (10,'Space','Space',2); " +
                "SET IDENTITY_INSERT Catalogs OFF;");
            Sql("SET IDENTITY_INSERT Catalogs ON; " +
                "INSERT INTO Catalogs(Id,Name,Code,SiteId) VALUES (11,'Miscellaneous','Miscellaneous',2); " +
                "SET IDENTITY_INSERT Catalogs OFF;");
        }

        public override void Down()
        {
            Sql("DELETE FROM Catalogs; DBCC CHECKIDENT('Catalogs', RESEED, 0); ");
        }
    }
}
