namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDataToAncestorAreasTable : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT AncestorAreas ON; " +
                "INSERT INTO AncestorAreas(Id,Name,SiteId) VALUES (1,'Ancestor',2); " +
                "SET IDENTITY_INSERT AncestorAreas OFF;");
            Sql("SET IDENTITY_INSERT AncestorAreas ON; " +
                "INSERT INTO AncestorAreas(Id,Name,SiteId) VALUES (2,'Ancestor',3); " +
                "SET IDENTITY_INSERT AncestorAreas OFF;");
        }

        public override void Down()
        {
            Sql("DELETE FROM AncestorAreas; DBCC CHECKIDENT('AncestorAreas', RESEED, 0); ");
        }
    }
}
