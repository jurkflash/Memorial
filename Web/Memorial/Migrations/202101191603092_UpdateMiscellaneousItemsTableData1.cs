namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMiscellaneousItemsTableData1 : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE MiscellaneousItems SET Code='MSLJF' WHERE Id=1");
        }
        
        public override void Down()
        {
        }
    }
}
