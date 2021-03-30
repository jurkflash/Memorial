namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCodeFromPlotTypesTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PlotTypes", "Code");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PlotTypes", "Code", c => c.String(nullable: false, maxLength: 10));
        }
    }
}
