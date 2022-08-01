namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDataToCremationItemsTable : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT CremationItems ON; " +
                "INSERT INTO CremationItems(Id,Name,Price,Code,CremationId) VALUES (1,N'火化 Cremation',600,'CMHHD',1); "+
                "SET IDENTITY_INSERT CremationItems OFF;");
            Sql("SET IDENTITY_INSERT CremationItems ON; " +
                "INSERT INTO CremationItems(Id,Name,Price,Code,CremationId) VALUES (2,N'免费 Free',0,'CMFRE',1); " +
                "SET IDENTITY_INSERT CremationItems OFF;");
        }

        public override void Down()
        {
            Sql("TRUNCATE TABLE CremationItems");
        }
    }
}
