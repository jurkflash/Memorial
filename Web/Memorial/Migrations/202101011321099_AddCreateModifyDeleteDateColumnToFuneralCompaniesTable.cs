namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreateModifyDeleteDateColumnToFuneralCompaniesTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FuneralCompanies", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.FuneralCompanies", "ModifyDate", c => c.DateTime());
            AddColumn("dbo.FuneralCompanies", "DeleteDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FuneralCompanies", "DeleteDate");
            DropColumn("dbo.FuneralCompanies", "ModifyDate");
            DropColumn("dbo.FuneralCompanies", "CreateDate");
        }
    }
}
