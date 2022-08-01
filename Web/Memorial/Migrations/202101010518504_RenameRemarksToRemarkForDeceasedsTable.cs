namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameRemarksToRemarkForDeceasedsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Deceaseds", "Remark", c => c.String(maxLength: 255));
            DropColumn("dbo.Deceaseds", "Remarks");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Deceaseds", "Remarks", c => c.String());
            DropColumn("dbo.Deceaseds", "Remark");
        }
    }
}
