namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSiteFieldToApplicantsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applicants", "SiteId", c => c.Int(nullable: false));
            CreateIndex("dbo.Applicants", "SiteId");
            AddForeignKey("dbo.Applicants", "SiteId", "dbo.Sites", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Applicants", "SiteId", "dbo.Sites");
            DropIndex("dbo.Applicants", new[] { "SiteId" });
            DropColumn("dbo.Applicants", "SiteId");
        }
    }
}
