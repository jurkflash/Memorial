namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCatalogsTableData2 : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE Catalogs SET Area='Miscellaneous', Controller='Miscellaneous' WHERE Code='Miscellaneous'");
        }
        
        public override void Down()
        {
        }
    }
}
