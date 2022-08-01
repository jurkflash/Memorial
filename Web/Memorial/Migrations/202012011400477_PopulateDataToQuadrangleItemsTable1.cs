namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDataToQuadrangleItemsTable1 : DbMigration
    {
        public override void Up()
        {
            Sql("DELETE FROM QuadrangleItems; DBCC CHECKIDENT('QuadrangleItems', RESEED, 0); ");

            Sql("SET IDENTITY_INSERT QuadrangleItems ON; " +
               "INSERT INTO QuadrangleItems(Id,Name,Price,QuadrangleCentreId,Code) VALUES (1,N'Quadrangle',0,2,'QRAF1'); " +
               "SET IDENTITY_INSERT QuadrangleItems OFF;");
            Sql("SET IDENTITY_INSERT QuadrangleItems ON; " +
               "INSERT INTO QuadrangleItems(Id,Name,Price,QuadrangleCentreId,Code) VALUES (2,N'Maintenance',0,2,'QRGLF'); " +
               "SET IDENTITY_INSERT QuadrangleItems OFF;");
            Sql("SET IDENTITY_INSERT QuadrangleItems ON; " +
               "INSERT INTO QuadrangleItems(Id,Name,Price,QuadrangleCentreId,Code) VALUES (3,N'Photo',150,2,'QRZPF'); " +
               "SET IDENTITY_INSERT QuadrangleItems OFF;");
            Sql("SET IDENTITY_INSERT QuadrangleItems ON; " +
               "INSERT INTO QuadrangleItems(Id,Name,Price,QuadrangleCentreId,Code) VALUES (4,N'Shift',0,2,'QRYWF'); " +
               "SET IDENTITY_INSERT QuadrangleItems OFF;");
            Sql("SET IDENTITY_INSERT QuadrangleItems ON; " +
               "INSERT INTO QuadrangleItems(Id,Name,Price,QuadrangleCentreId,Code) VALUES (5,N'Transfer',200,2,'QRZRF'); " +
               "SET IDENTITY_INSERT QuadrangleItems OFF;");
            Sql("SET IDENTITY_INSERT QuadrangleItems ON; " +
               "INSERT INTO QuadrangleItems(Id,Name,Price,QuadrangleCentreId,Code) VALUES (6,N'Free',0,2,'QRFRE'); " +
               "SET IDENTITY_INSERT QuadrangleItems OFF;");
            Sql("SET IDENTITY_INSERT QuadrangleItems ON; " +
               "INSERT INTO QuadrangleItems(Id,Name,Price,QuadrangleCentreId,Code) VALUES (7,N'Quadrangle',0,3,'QRAF1'); " +
               "SET IDENTITY_INSERT QuadrangleItems OFF;");
            Sql("SET IDENTITY_INSERT QuadrangleItems ON; " +
               "INSERT INTO QuadrangleItems(Id,Name,Price,QuadrangleCentreId,Code) VALUES (8,N'Maintenance',0,3,'QRGLF'); " +
               "SET IDENTITY_INSERT QuadrangleItems OFF;");
            Sql("SET IDENTITY_INSERT QuadrangleItems ON; " +
               "INSERT INTO QuadrangleItems(Id,Name,Price,QuadrangleCentreId,Code) VALUES (9,N'Photo',150,3,'QRZPF'); " +
               "SET IDENTITY_INSERT QuadrangleItems OFF;");
            Sql("SET IDENTITY_INSERT QuadrangleItems ON; " +
               "INSERT INTO QuadrangleItems(Id,Name,Price,QuadrangleCentreId,Code) VALUES (10,N'Shift',0,3,'QRYWF'); " +
               "SET IDENTITY_INSERT QuadrangleItems OFF;");
            Sql("SET IDENTITY_INSERT QuadrangleItems ON; " +
               "INSERT INTO QuadrangleItems(Id,Name,Price,QuadrangleCentreId,Code) VALUES (11,N'Transfer',200,3,'QRZRF'); " +
               "SET IDENTITY_INSERT QuadrangleItems OFF;");
            Sql("SET IDENTITY_INSERT QuadrangleItems ON; " +
               "INSERT INTO QuadrangleItems(Id,Name,Price,QuadrangleCentreId,Code) VALUES (12,N'Free',0,2,'QRFRE'); " +
               "SET IDENTITY_INSERT QuadrangleItems OFF;");

        }

        public override void Down()
        {
            Sql("DELETE FROM QuadrangleItems; DBCC CHECKIDENT('QuadrangleItems', RESEED, 0); ");
        }
    }
}
