namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDataToGenderTypesTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO GenderTypes(Id,Gender) VALUES (1,N'男 Male')");
            Sql("INSERT INTO GenderTypes(Id,Gender) VALUES (2,N'女 Female')");
        }
        
        public override void Down()
        {
        }
    }
}
