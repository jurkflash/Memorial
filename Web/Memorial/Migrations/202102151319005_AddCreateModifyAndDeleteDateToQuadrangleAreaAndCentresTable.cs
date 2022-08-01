namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreateModifyAndDeleteDateToQuadrangleAreaAndCentresTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuadrangleAreas", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.QuadrangleAreas", "ModifyDate", c => c.DateTime());
            AddColumn("dbo.QuadrangleAreas", "DeleteDate", c => c.DateTime());
            AddColumn("dbo.QuadrangleCentres", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.QuadrangleCentres", "ModifyDate", c => c.DateTime());
            AddColumn("dbo.QuadrangleCentres", "DeleteDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.QuadrangleCentres", "DeleteDate");
            DropColumn("dbo.QuadrangleCentres", "ModifyDate");
            DropColumn("dbo.QuadrangleCentres", "CreateDate");
            DropColumn("dbo.QuadrangleAreas", "DeleteDate");
            DropColumn("dbo.QuadrangleAreas", "ModifyDate");
            DropColumn("dbo.QuadrangleAreas", "CreateDate");
        }
    }
}
