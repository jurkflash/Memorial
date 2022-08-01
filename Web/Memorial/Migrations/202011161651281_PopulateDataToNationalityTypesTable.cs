namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDataToNationalityTypesTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO NationalityTypes(Id,Nationality) VALUES (1,'Malaysian')");
            Sql("INSERT INTO NationalityTypes(Id,Nationality) VALUES (2,'Non-Malaysian')");
        }
        
        public override void Down()
        {
        }
    }
}
