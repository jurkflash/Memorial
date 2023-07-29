namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicantsTableSiteFieldOffCascadeDelete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Applicants", "SiteId", "dbo.Sites");
            AddForeignKey("dbo.Applicants", "SiteId", "dbo.Sites", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Applicants", "SiteId", "dbo.Sites");
            AddForeignKey("dbo.Applicants", "SiteId", "dbo.Sites", "Id", cascadeDelete: true);
        }
    }
}
