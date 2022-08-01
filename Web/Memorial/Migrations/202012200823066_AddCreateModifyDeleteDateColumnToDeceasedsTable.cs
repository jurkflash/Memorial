namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreateModifyDeleteDateColumnToDeceasedsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Deceaseds", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Deceaseds", "ModifyDate", c => c.DateTime());
            AddColumn("dbo.Deceaseds", "DeleteDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Deceaseds", "DeleteDate");
            DropColumn("dbo.Deceaseds", "ModifyDate");
            DropColumn("dbo.Deceaseds", "CreateDate");
        }
    }
}
