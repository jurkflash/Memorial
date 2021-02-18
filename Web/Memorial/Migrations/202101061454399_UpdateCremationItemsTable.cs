namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCremationItemsTable : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE CremationItems SET SystemCode='Orders' WHERE Id=1");
        }
        
        public override void Down()
        {
        }
    }
}
