namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSpaceItemsTableData1 : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE SpaceItems SET FormView='InfoWithDeceased' WHERE Id>=1 and Id <=11");
            Sql("UPDATE SpaceItems SET FormView='InfoWithoutDeceased' WHERE Id>=12 and Id <=15");
            Sql("UPDATE SpaceItems SET FormView='InfoHallForPray' WHERE Id=16");
            Sql("UPDATE SpaceItems SET FormView='InfoWithoutDeceasedAndFuneralCo' WHERE Id>=17 and Id<=18");
            Sql("UPDATE SpaceItems SET FormView='Info' WHERE Id>=19 and Id<=20");
        }
        
        public override void Down()
        {
        }
    }
}
