namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreateModifyDeleteDateColumnToApplicantsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applicants", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Applicants", "ModifyDate", c => c.DateTime());
            AddColumn("dbo.Applicants", "DeleteDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Applicants", "DeleteDate");
            DropColumn("dbo.Applicants", "ModifyDate");
            DropColumn("dbo.Applicants", "CreateDate");
        }
    }
}
