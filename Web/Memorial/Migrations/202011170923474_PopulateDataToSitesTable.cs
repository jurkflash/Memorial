namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDataToSitesTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Sites(Id,Name,Code) VALUES (1,'Sg Buluh','SBH')");
            Sql("INSERT INTO Sites(Id,Name,Code) VALUES (2,'Jit San Tang','JST')");
            Sql("INSERT INTO Sites(Id,Name,Code) VALUES (3,'Desa','DSJ')");
        }
        
        public override void Down()
        {
        }
    }
}
