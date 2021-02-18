namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReduceRemarkColumnSizeTo255ForApplicantsTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Applicants", "Remark", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Applicants", "Remark", c => c.String());
        }
    }
}
