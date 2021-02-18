namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDataToMiscellaneousItemsTable : DbMigration
    {
        public override void Up()
        {
            Sql("DELETE FROM Miscellaneous; DBCC CHECKIDENT('Miscellaneous', RESEED, 0); ");
            Sql("DELETE FROM MiscellaneousItems; DBCC CHECKIDENT('MiscellaneousItems', RESEED, 0); ");
            Sql("SET IDENTITY_INSERT Miscellaneous ON; " +
               "INSERT INTO Miscellaneous(Id,Name,SiteId) VALUES (1,N'樂捐',2); " +
               "SET IDENTITY_INSERT Miscellaneous OFF;");
            Sql("SET IDENTITY_INSERT Miscellaneous ON; " +
                "INSERT INTO Miscellaneous(Id,Name,SiteId) VALUES (2,N'賠償費',2); " +
                "SET IDENTITY_INSERT Miscellaneous OFF;");
            Sql("SET IDENTITY_INSERT Miscellaneous ON; " +
                "INSERT INTO Miscellaneous(Id,Name,SiteId) VALUES (3,N'租借冷風機',2); " +
                "SET IDENTITY_INSERT Miscellaneous OFF;");
            Sql("SET IDENTITY_INSERT MiscellaneousItems ON; " +
                "INSERT INTO MiscellaneousItems(Id,Name,Price,Code,MiscellaneousId) VALUES (1,N'樂捐',0,'MSLFJ',1); " +
                "SET IDENTITY_INSERT MiscellaneousItems OFF;");
            Sql("SET IDENTITY_INSERT MiscellaneousItems ON; " +
                "INSERT INTO MiscellaneousItems(Id,Name,Price,Code,MiscellaneousId) VALUES (2,N'賠償費',0,'MSPCF',2); " +
                "SET IDENTITY_INSERT MiscellaneousItems OFF;");
            Sql("SET IDENTITY_INSERT MiscellaneousItems ON; " +
                "INSERT INTO MiscellaneousItems(Id,Name,Price,Code,MiscellaneousId) VALUES (3,N'租借冷風機',0,'MSLFJ',3); " +
                "SET IDENTITY_INSERT MiscellaneousItems OFF;");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM MiscellaneousItems; DBCC CHECKIDENT('MiscellaneousItems', RESEED, 0); ");
            Sql("SET IDENTITY_INSERT Miscellaneous ON; " +
               "INSERT INTO Miscellaneous(Id,Name,SiteId) VALUES (1,N'樂捐',2); " +
               "SET IDENTITY_INSERT Miscellaneous OFF;");
            Sql("SET IDENTITY_INSERT Miscellaneous ON; " +
                "INSERT INTO Miscellaneous(Id,Name,SiteId) VALUES (2,N'賠償費',2); " +
                "SET IDENTITY_INSERT Miscellaneous OFF;");
            Sql("SET IDENTITY_INSERT Miscellaneous ON; " +
                "INSERT INTO Miscellaneous(Id,Name,SiteId) VALUES (3,N'租借冷風機',2); " +
                "SET IDENTITY_INSERT Miscellaneous OFF;");
        }
    }
}
