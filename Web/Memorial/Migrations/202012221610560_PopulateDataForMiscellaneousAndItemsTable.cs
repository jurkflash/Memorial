namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDataForMiscellaneousAndItemsTable : DbMigration
    {
        public override void Up()
        {
            Sql("DELETE FROM MiscellaneousItems; DBCC CHECKIDENT('MiscellaneousItems', RESEED, 0); ");
            Sql("DELETE FROM Miscellaneous; DBCC CHECKIDENT('Miscellaneous', RESEED, 0); ");

            Sql("SET IDENTITY_INSERT Miscellaneous ON; " +
               "INSERT INTO Miscellaneous(Id,Name,SiteId) VALUES (1,N'樂捐 Donation',2); " +
               "SET IDENTITY_INSERT Miscellaneous OFF;");
            Sql("SET IDENTITY_INSERT Miscellaneous ON; " +
                "INSERT INTO Miscellaneous(Id,Name,SiteId) VALUES (2,N'賠償費 Compensate',2); " +
                "SET IDENTITY_INSERT Miscellaneous OFF;");
            Sql("SET IDENTITY_INSERT Miscellaneous ON; " +
                "INSERT INTO Miscellaneous(Id,Name,SiteId) VALUES (3,N'租借冷風機 Rent Air Cooler',2); " +
                "SET IDENTITY_INSERT Miscellaneous OFF;");

            Sql("SET IDENTITY_INSERT MiscellaneousItems ON; " +
                "INSERT INTO MiscellaneousItems(Id,Name,Price,Code,SystemCode,MiscellaneousId) VALUES (1,N'單 Order',0,'MSLFJ','Donation',1); " +
                "SET IDENTITY_INSERT MiscellaneousItems OFF;");
            Sql("SET IDENTITY_INSERT MiscellaneousItems ON; " +
                "INSERT INTO MiscellaneousItems(Id,Name,Price,Code,SystemCode,MiscellaneousId) VALUES (2,N'單 Order',0,'MSPCF','Compensate',2); " +
                "SET IDENTITY_INSERT MiscellaneousItems OFF;");
            Sql("SET IDENTITY_INSERT MiscellaneousItems ON; " +
                "INSERT INTO MiscellaneousItems(Id,Name,Price,Code,SystemCode,MiscellaneousId) VALUES (3,N'單 Order',0,'MSLFJ','RentAirCooler',3); " +
                "SET IDENTITY_INSERT MiscellaneousItems OFF;");
        }

        public override void Down()
        {
        }
    }
}
