namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCatalogsTableData5 : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE Catalogs SET Name=N'祖先牌位 AncestralTablet', Area='AncestralTablet', Controller='AncestralTablets' WHERE Name='Ancestor'");
        }
        
        public override void Down()
        {
        }
    }
}
