namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCatalogsTableData4 : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE Catalogs SET Name=N'墓地 Cemetery', Area='Cemetery', Controller='Cemeteries' WHERE Name='Plot'");
        }
        
        public override void Down()
        {
        }
    }
}
