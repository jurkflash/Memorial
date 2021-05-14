namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDataForMiscellaneousTable : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT Miscellaneous ON; " +
               "INSERT INTO Miscellaneous(Id,Name,SiteId) VALUES (4,N'墓地美化 Landscape',1); " +
               "SET IDENTITY_INSERT Miscellaneous OFF;");
        }
        
        public override void Down()
        {
        }
    }
}
