namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSpaceItemsData1 : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE SpaceItems SET AllowDoubleBook='False'");
            Sql("UPDATE SpaceItems SET AllowDoubleBook='True' WHERE SystemCode IN ('Chair','Electric','House')");
        }
        
        public override void Down()
        {
        }
    }
}
