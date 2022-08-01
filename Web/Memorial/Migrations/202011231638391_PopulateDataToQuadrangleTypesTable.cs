namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDataToQuadrangleTypesTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO QuadrangleTypes(Id,Name,NumberOfPlacement) VALUES (1,N'单 Single',1)");
            Sql("INSERT INTO QuadrangleTypes(Id,Name,NumberOfPlacement) VALUES (2,N'双 Double',2)");
        }
        
        public override void Down()
        {
            Sql("TRUNCATE TABLE QuadrangleTypes");
        }
    }
}
