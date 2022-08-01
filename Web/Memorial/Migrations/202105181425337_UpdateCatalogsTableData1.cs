namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCatalogsTableData1 : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE Catalogs SET Area='Plot', Controller='Plots' WHERE Code='Plots'");
            Sql("UPDATE Catalogs SET Area='Ancestor', Controller='Ancestors' WHERE Code='Ancestors'");
            Sql("UPDATE Catalogs SET Area='Cremation', Controller='Cremations' WHERE Code='Cremations'");
            Sql("UPDATE Catalogs SET Area='Urn', Controller='Urns' WHERE Code='Urns'");
            Sql("UPDATE Catalogs SET Area='Quadrangle', Controller='Quadrangles' WHERE Code='Quadrangles'");
            Sql("UPDATE Catalogs SET Area='Space', Controller='Spaces' WHERE Code='Spaces'");
        }
        
        public override void Down()
        {
        }
    }
}
