namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeIsPurchasedToApplicantAndCodeToNameForAncestorsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ancestors", "Name", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Ancestors", "ApplicantId", c => c.Int());
            AddColumn("dbo.Ancestors", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Ancestors", "ModifyDate", c => c.DateTime());
            AddColumn("dbo.Ancestors", "DeleteDate", c => c.DateTime());
            CreateIndex("dbo.Ancestors", "ApplicantId");
            AddForeignKey("dbo.Ancestors", "ApplicantId", "dbo.Applicants", "Id");
            DropColumn("dbo.Ancestors", "Code");
            DropColumn("dbo.Ancestors", "isPurchased");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ancestors", "isPurchased", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ancestors", "Code", c => c.String(nullable: false, maxLength: 50));
            DropForeignKey("dbo.Ancestors", "ApplicantId", "dbo.Applicants");
            DropIndex("dbo.Ancestors", new[] { "ApplicantId" });
            DropColumn("dbo.Ancestors", "DeleteDate");
            DropColumn("dbo.Ancestors", "ModifyDate");
            DropColumn("dbo.Ancestors", "CreateDate");
            DropColumn("dbo.Ancestors", "ApplicantId");
            DropColumn("dbo.Ancestors", "Name");
        }
    }
}
