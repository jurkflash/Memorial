namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSubProductServicesTableSBOOKData : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE SubProductServices SET Name=N'租 Rent' WHERE Code='SBOOK'");
        }
        
        public override void Down()
        {
        }
    }
}
