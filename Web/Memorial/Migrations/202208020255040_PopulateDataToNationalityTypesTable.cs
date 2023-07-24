namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDataToNationalityTypesTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO NationalityTypes(Id,Name,ActiveStatus,CreatedDate) VALUES (1,'Malaysian',1,GETDATE())");
            Sql("INSERT INTO NationalityTypes(Id,Name,ActiveStatus,CreatedDate) VALUES (2,'Non-Malaysian',1,GETDATE())");
        }
        
        public override void Down()
        {
        }
    }
}
