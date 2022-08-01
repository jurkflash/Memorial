namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCremationItemsData : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE CremationItems SET isOrder='True' WHERE Id=1");
        }
        
        public override void Down()
        {
        }
    }
}
