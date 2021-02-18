namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDataToCremationItemsTable1 : DbMigration
    {
        public override void Up()
        {
            Sql("DELETE FROM CremationItems; DBCC CHECKIDENT('CremationItems', RESEED, 0); ");
            Sql("DELETE FROM Cremations;");

            Sql("INSERT INTO Cremations(Id,Name,SiteId) VALUES (1,N'火化 Cremation',3)");

            Sql("SET IDENTITY_INSERT CremationItems ON; " +
                "INSERT INTO CremationItems(Id,Name,Price,Code,SystemCode,CremationId) VALUES (1,N'单 Order',600,'CMHHD','Order',1); " +
                "SET IDENTITY_INSERT CremationItems OFF;");
            Sql("SET IDENTITY_INSERT CremationItems ON; " +
                "INSERT INTO CremationItems(Id,Name,Price,Code,SystemCode,CremationId) VALUES (2,N'免费 Free',0,'CMFRE','Free',1); " +
                "SET IDENTITY_INSERT CremationItems OFF;");
        }

        public override void Down()
        {
            Sql("DELETE FROM CremationItems; DBCC CHECKIDENT('CremationItems', RESEED, 0); ");
        }
    }
}
