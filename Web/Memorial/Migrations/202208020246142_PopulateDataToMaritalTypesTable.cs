namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDataToMaritalTypesTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO MaritalTypes(Id,Name,ActiveStatus,CreatedDate) VALUES (1,N'未婚 Single',1,GETDATE())");
            Sql("INSERT INTO MaritalTypes(Id,Name,ActiveStatus,CreatedDate) VALUES (2,N'已婚 Married',1,GETDATE())");
        }
        
        public override void Down()
        {
        }
    }
}
