namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateNameForUrnItemsTable : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE UrnItems SET Name=N'購買 Purchase'");
        }
        
        public override void Down()
        {
        }
    }
}
