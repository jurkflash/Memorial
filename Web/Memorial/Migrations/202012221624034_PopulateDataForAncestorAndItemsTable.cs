namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDataForAncestorAndItemsTable : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE AncestorAreas SET Name=N'祖先牌 Ancestor' ");
            Sql("DELETE FROM AncestorItems; DBCC CHECKIDENT('AncestorItems', RESEED, 0); ");

            Sql("SET IDENTITY_INSERT AncestorItems ON; " +
                "INSERT INTO AncestorItems(Id,Name,Price,Code,SystemCode,AncestorAreaId) VALUES (1,N'單 Order',0,'ACJSZ','Order',1); " +
                "SET IDENTITY_INSERT AncestorItems OFF;");
            Sql("SET IDENTITY_INSERT AncestorItems ON; " +
                "INSERT INTO AncestorItems(Id,Name,Price,Code,SystemCode,AncestorAreaId) VALUES (2,N'管理費 Maintenance',0,'ACJGL','Maintenance',1); " +
                "SET IDENTITY_INSERT AncestorItems OFF;");
            Sql("SET IDENTITY_INSERT AncestorItems ON; " +
                "INSERT INTO AncestorItems(Id,Name,Price,Code,SystemCode,AncestorAreaId) VALUES (3,N'移位 Shift',0,'ACJYW','Shift',1); " +
                "SET IDENTITY_INSERT AncestorItems OFF;");
            Sql("SET IDENTITY_INSERT AncestorItems ON; " +
                "INSERT INTO AncestorItems(Id,Name,Price,Code,SystemCode,AncestorAreaId) VALUES (4,N'免費 Free',0,'ACJFE','Free',1); " +
                "SET IDENTITY_INSERT AncestorItems OFF;");
            Sql("SET IDENTITY_INSERT AncestorItems ON; " +
                "INSERT INTO AncestorItems(Id,Name,Price,Code,SystemCode,AncestorAreaId) VALUES (5,N'單 Order',0,'ACDSZ','Order',2); " +
                "SET IDENTITY_INSERT AncestorItems OFF;");
            Sql("SET IDENTITY_INSERT AncestorItems ON; " +
                "INSERT INTO AncestorItems(Id,Name,Price,Code,SystemCode,AncestorAreaId) VALUES (6,N'管理費 Maintenance',0,'ACDGL','Maintenance',2); " +
                "SET IDENTITY_INSERT AncestorItems OFF;");
            Sql("SET IDENTITY_INSERT AncestorItems ON; " +
                "INSERT INTO AncestorItems(Id,Name,Price,Code,SystemCode,AncestorAreaId) VALUES (7,N'移位 Shift',0,'ACDYW','Shift',2); " +
                "SET IDENTITY_INSERT AncestorItems OFF;");
            Sql("SET IDENTITY_INSERT AncestorItems ON; " +
                "INSERT INTO AncestorItems(Id,Name,Price,Code,SystemCode,AncestorAreaId) VALUES (8,N'免費 Free',0,'ACDFE','Free',2); " +
                "SET IDENTITY_INSERT AncestorItems OFF;");
        }

        public override void Down()
        {
        }
    }
}
