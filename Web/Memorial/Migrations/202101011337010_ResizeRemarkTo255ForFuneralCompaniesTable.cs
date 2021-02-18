namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResizeRemarkTo255ForFuneralCompaniesTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FuneralCompanies", "Remark", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FuneralCompanies", "Remark", c => c.String());
        }
    }
}
