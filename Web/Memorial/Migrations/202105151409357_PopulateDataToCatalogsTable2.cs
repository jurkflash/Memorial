namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDataToCatalogsTable2 : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT Catalogs ON; " +
                "INSERT INTO Catalogs(Id,Name,SiteId,Code) VALUES (12,'Miscellaneous',1,'Miscellaneous'); " +
                "SET IDENTITY_INSERT Catalogs OFF;");
        }
        
        public override void Down()
        {
        }
    }
}
