namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreateModifyDeleteDateColumnToThoseNecessaryTablesAndSomeChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sites", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Sites", "ModifyDate", c => c.DateTime());
            AddColumn("dbo.Sites", "DeleteDate", c => c.DateTime());
            AddColumn("dbo.GenderTypes", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.GenderTypes", "ModifyDate", c => c.DateTime());
            AddColumn("dbo.GenderTypes", "DeleteDate", c => c.DateTime());
            AddColumn("dbo.MaritalTypes", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.MaritalTypes", "ModifyDate", c => c.DateTime());
            AddColumn("dbo.MaritalTypes", "DeleteDate", c => c.DateTime());
            AddColumn("dbo.NationalityTypes", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.NationalityTypes", "ModifyDate", c => c.DateTime());
            AddColumn("dbo.NationalityTypes", "DeleteDate", c => c.DateTime());
            AddColumn("dbo.PlotTypes", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.PlotTypes", "ModifyDate", c => c.DateTime());
            AddColumn("dbo.PlotTypes", "DeleteDate", c => c.DateTime());
            AddColumn("dbo.QuadrangleTypes", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.QuadrangleTypes", "ModifyDate", c => c.DateTime());
            AddColumn("dbo.QuadrangleTypes", "DeleteDate", c => c.DateTime());
            AddColumn("dbo.RelationshipTypes", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.RelationshipTypes", "ModifyDate", c => c.DateTime());
            AddColumn("dbo.RelationshipTypes", "DeleteDate", c => c.DateTime());
            AddColumn("dbo.ReligionTypes", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ReligionTypes", "ModifyDate", c => c.DateTime());
            AddColumn("dbo.ReligionTypes", "DeleteDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReligionTypes", "DeleteDate");
            DropColumn("dbo.ReligionTypes", "ModifyDate");
            DropColumn("dbo.ReligionTypes", "CreateDate");
            DropColumn("dbo.RelationshipTypes", "DeleteDate");
            DropColumn("dbo.RelationshipTypes", "ModifyDate");
            DropColumn("dbo.RelationshipTypes", "CreateDate");
            DropColumn("dbo.QuadrangleTypes", "DeleteDate");
            DropColumn("dbo.QuadrangleTypes", "ModifyDate");
            DropColumn("dbo.QuadrangleTypes", "CreateDate");
            DropColumn("dbo.PlotTypes", "DeleteDate");
            DropColumn("dbo.PlotTypes", "ModifyDate");
            DropColumn("dbo.PlotTypes", "CreateDate");
            DropColumn("dbo.NationalityTypes", "DeleteDate");
            DropColumn("dbo.NationalityTypes", "ModifyDate");
            DropColumn("dbo.NationalityTypes", "CreateDate");
            DropColumn("dbo.MaritalTypes", "DeleteDate");
            DropColumn("dbo.MaritalTypes", "ModifyDate");
            DropColumn("dbo.MaritalTypes", "CreateDate");
            DropColumn("dbo.GenderTypes", "DeleteDate");
            DropColumn("dbo.GenderTypes", "ModifyDate");
            DropColumn("dbo.GenderTypes", "CreateDate");
            DropColumn("dbo.Sites", "DeleteDate");
            DropColumn("dbo.Sites", "ModifyDate");
            DropColumn("dbo.Sites", "CreateDate");
        }
    }
}
