namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveApplicantIdFromAncestorsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ancestors", "ApplicantId", "dbo.Applicants");
            DropIndex("dbo.Ancestors", new[] { "ApplicantId" });
            DropColumn("dbo.Ancestors", "ApplicantId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ancestors", "ApplicantId", c => c.Int());
            CreateIndex("dbo.Ancestors", "ApplicantId");
            AddForeignKey("dbo.Ancestors", "ApplicantId", "dbo.Applicants", "Id");
        }
    }
}
