namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDataToGenderTypesTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO GenderTypes(Id,Name,ActiveStatus,CreatedDate) VALUES (1,N'男 Male',1,GETDATE())");
            Sql("INSERT INTO GenderTypes(Id,Name,ActiveStatus,CreatedDate) VALUES (2,N'女 Female',1,GETDATE())");
        }
        
        public override void Down()
        {
        }
    }
}
