namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDataToCremationsTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Cremations(Id,Name,Price,SiteId,Code) VALUES (1,N'火化',600,3,'CMHHD')");
        }
        
        public override void Down()
        {
            Sql("Truncate Table Cremations");
        }
    }
}
