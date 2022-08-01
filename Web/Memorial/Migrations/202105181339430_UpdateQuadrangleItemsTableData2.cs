namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateQuadrangleItemsTableData2 : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE QuadrangleItems SET SystemCode='Orders' WHERE SystemCode='Order'");
            Sql("UPDATE QuadrangleItems SET SystemCode='Photos' WHERE SystemCode='Photo'");
            Sql("UPDATE QuadrangleItems SET SystemCode='Shifts' WHERE SystemCode='Shift'");
            Sql("UPDATE QuadrangleItems SET SystemCode='Transfers' WHERE SystemCode='Transfer'");
        }
        
        public override void Down()
        {
        }
    }
}
