namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDataToMaritalTypesTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO MaritalTypes(Id,Marital) VALUES (1,N'未婚 Single')");
            Sql("INSERT INTO MaritalTypes(Id,Marital) VALUES (2,N'已婚 Married')");
        }
        
        public override void Down()
        {
        }
    }
}
