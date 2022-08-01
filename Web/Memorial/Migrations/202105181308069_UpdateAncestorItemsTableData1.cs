namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAncestorItemsTableData1 : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE AncestorItems SET SystemCode='Orders' WHERE SystemCode='Order'");
            Sql("UPDATE AncestorItems SET SystemCode='Maintenances' WHERE SystemCode='Maintenance'");
            Sql("UPDATE AncestorItems SET SystemCode='Shifts' WHERE SystemCode='Shift'");
        }
        
        public override void Down()
        {
        }
    }
}
