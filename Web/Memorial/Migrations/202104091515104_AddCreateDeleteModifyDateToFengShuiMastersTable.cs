namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreateDeleteModifyDateToFengShuiMastersTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FengShuiMasters", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.FengShuiMasters", "ModifyDate", c => c.DateTime());
            AddColumn("dbo.FengShuiMasters", "DeleteDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FengShuiMasters", "DeleteDate");
            DropColumn("dbo.FengShuiMasters", "ModifyDate");
            DropColumn("dbo.FengShuiMasters", "CreateDate");
        }
    }
}
