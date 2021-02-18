namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDataToAncestorItemsTable1 : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT AncestorItems ON; " +
                "INSERT INTO AncestorItems(Id,Name,Price,Code,AncestorAreaId) VALUES (1,'Ancestor',0,'ACJSZ',1); " +
                "SET IDENTITY_INSERT AncestorItems OFF;");
            Sql("SET IDENTITY_INSERT AncestorItems ON; " +
                "INSERT INTO AncestorItems(Id,Name,Price,Code,AncestorAreaId) VALUES (2,'Maintenance',0,'ACJGL',1); " +
                "SET IDENTITY_INSERT AncestorItems OFF;");
            Sql("SET IDENTITY_INSERT AncestorItems ON; " +
                "INSERT INTO AncestorItems(Id,Name,Price,Code,AncestorAreaId) VALUES (3,'Shift',0,'ACJYW',1); " +
                "SET IDENTITY_INSERT AncestorItems OFF;");
            Sql("SET IDENTITY_INSERT AncestorItems ON; " +
                "INSERT INTO AncestorItems(Id,Name,Price,Code,AncestorAreaId) VALUES (4,'Free',0,'ACJFE',1); " +
                "SET IDENTITY_INSERT AncestorItems OFF;");
            Sql("SET IDENTITY_INSERT AncestorItems ON; " +
                "INSERT INTO AncestorItems(Id,Name,Price,Code,AncestorAreaId) VALUES (5,'Ancestor',0,'ACDSZ',2); " +
                "SET IDENTITY_INSERT AncestorItems OFF;");
            Sql("SET IDENTITY_INSERT AncestorItems ON; " +
                "INSERT INTO AncestorItems(Id,Name,Price,Code,AncestorAreaId) VALUES (6,'Maintenance',0,'ACDGL',2); " +
                "SET IDENTITY_INSERT AncestorItems OFF;");
            Sql("SET IDENTITY_INSERT AncestorItems ON; " +
                "INSERT INTO AncestorItems(Id,Name,Price,Code,AncestorAreaId) VALUES (7,'Shift',0,'ACDYW',2); " +
                "SET IDENTITY_INSERT AncestorItems OFF;");
            Sql("SET IDENTITY_INSERT AncestorItems ON; " +
                "INSERT INTO AncestorItems(Id,Name,Price,Code,AncestorAreaId) VALUES (8,'Free',0,'ACDFE',2); " +
                "SET IDENTITY_INSERT AncestorItems OFF;");
        }

        public override void Down()
        {
            Sql("DELETE FROM AncestorItems; DBCC CHECKIDENT('AncestorItems', RESEED, 0); ");
        }
    }
}
