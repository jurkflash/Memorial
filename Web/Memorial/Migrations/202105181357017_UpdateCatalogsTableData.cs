namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCatalogsTableData : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE Catalogs SET Code='Plots' WHERE Code='Plot'");
            Sql("UPDATE Catalogs SET Code='Ancestors' WHERE Code='Ancestor'");
            Sql("UPDATE Catalogs SET Code='Cremations' WHERE Code='Cremation'");
            Sql("UPDATE Catalogs SET Code='Urns' WHERE Code='Urn'");
            Sql("UPDATE Catalogs SET Code='Quadrangles' WHERE Code='Quadrangle'");
            Sql("UPDATE Catalogs SET Code='Spaces' WHERE Code='Space'");
        }
        
        public override void Down()
        {
        }
    }
}
