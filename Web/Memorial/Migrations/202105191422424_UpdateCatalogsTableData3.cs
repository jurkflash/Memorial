namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCatalogsTableData3 : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE Catalogs SET Name=N'骨灰殿 Columbarium', Area='Columbarium', Controller='Columbariums' WHERE Name='Quadrangle'");
        }
        
        public override void Down()
        {
        }
    }
}
