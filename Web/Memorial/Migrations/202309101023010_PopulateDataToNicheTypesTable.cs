namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDataToNicheTypesTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO NicheTypes(Id,Name,NumberOfPlacement,ActiveStatus,CreatedUtcTime) VALUES (1,'单 Single',1,1,GETUTCDATE()); ");
            Sql("INSERT INTO NicheTypes(Id,Name,NumberOfPlacement,ActiveStatus,CreatedUtcTime) VALUES (2,'双 Double',2,1,GETUTCDATE()); ");
        }

        public override void Down()
        {
        }
    }
}
