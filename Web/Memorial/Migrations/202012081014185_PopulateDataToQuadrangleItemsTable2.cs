namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDataToQuadrangleItemsTable2 : DbMigration
    {
        public override void Up()
        {
            Sql("DELETE FROM QuadrangleItems; DBCC CHECKIDENT('QuadrangleItems', RESEED, 0); ");

            Sql("SET IDENTITY_INSERT QuadrangleItems ON; " +
               "INSERT INTO QuadrangleItems(Id,Name,Price,QuadrangleCentreId,Code) VALUES (1,N'Quadrangle',0,2,'QRJAF'); " +
               "SET IDENTITY_INSERT QuadrangleItems OFF;");
            Sql("SET IDENTITY_INSERT QuadrangleItems ON; " +
               "INSERT INTO QuadrangleItems(Id,Name,Price,QuadrangleCentreId,Code) VALUES (2,N'Maintenance',0,2,'QRJGL'); " +
               "SET IDENTITY_INSERT QuadrangleItems OFF;");
            Sql("SET IDENTITY_INSERT QuadrangleItems ON; " +
               "INSERT INTO QuadrangleItems(Id,Name,Price,QuadrangleCentreId,Code) VALUES (3,N'Photo',150,2,'QRJZP'); " +
               "SET IDENTITY_INSERT QuadrangleItems OFF;");
            Sql("SET IDENTITY_INSERT QuadrangleItems ON; " +
               "INSERT INTO QuadrangleItems(Id,Name,Price,QuadrangleCentreId,Code) VALUES (4,N'Shift',0,2,'QRJYW'); " +
               "SET IDENTITY_INSERT QuadrangleItems OFF;");
            Sql("SET IDENTITY_INSERT QuadrangleItems ON; " +
               "INSERT INTO QuadrangleItems(Id,Name,Price,QuadrangleCentreId,Code) VALUES (5,N'Transfer',200,2,'QRJZR'); " +
               "SET IDENTITY_INSERT QuadrangleItems OFF;");
            Sql("SET IDENTITY_INSERT QuadrangleItems ON; " +
               "INSERT INTO QuadrangleItems(Id,Name,Price,QuadrangleCentreId,Code) VALUES (6,N'Free',0,2,'QRJFE'); " +
               "SET IDENTITY_INSERT QuadrangleItems OFF;");
            Sql("SET IDENTITY_INSERT QuadrangleItems ON; " +
               "INSERT INTO QuadrangleItems(Id,Name,Price,QuadrangleCentreId,Code) VALUES (7,N'Quadrangle',0,3,'QRDAF'); " +
               "SET IDENTITY_INSERT QuadrangleItems OFF;");
            Sql("SET IDENTITY_INSERT QuadrangleItems ON; " +
               "INSERT INTO QuadrangleItems(Id,Name,Price,QuadrangleCentreId,Code) VALUES (8,N'Maintenance',0,3,'QRDGL'); " +
               "SET IDENTITY_INSERT QuadrangleItems OFF;");
            Sql("SET IDENTITY_INSERT QuadrangleItems ON; " +
               "INSERT INTO QuadrangleItems(Id,Name,Price,QuadrangleCentreId,Code) VALUES (9,N'Photo',150,3,'QRDZP'); " +
               "SET IDENTITY_INSERT QuadrangleItems OFF;");
            Sql("SET IDENTITY_INSERT QuadrangleItems ON; " +
               "INSERT INTO QuadrangleItems(Id,Name,Price,QuadrangleCentreId,Code) VALUES (10,N'Shift',0,3,'QRDYW'); " +
               "SET IDENTITY_INSERT QuadrangleItems OFF;");
            Sql("SET IDENTITY_INSERT QuadrangleItems ON; " +
               "INSERT INTO QuadrangleItems(Id,Name,Price,QuadrangleCentreId,Code) VALUES (11,N'Transfer',200,3,'QRDZR'); " +
               "SET IDENTITY_INSERT QuadrangleItems OFF;");
            Sql("SET IDENTITY_INSERT QuadrangleItems ON; " +
               "INSERT INTO QuadrangleItems(Id,Name,Price,QuadrangleCentreId,Code) VALUES (12,N'Free',0,3,'QRDFE'); " +
               "SET IDENTITY_INSERT QuadrangleItems OFF;");

        }

        public override void Down()
        {
            Sql("DELETE FROM QuadrangleItems; DBCC CHECKIDENT('QuadrangleItems', RESEED, 0); ");
        }

    }
}
