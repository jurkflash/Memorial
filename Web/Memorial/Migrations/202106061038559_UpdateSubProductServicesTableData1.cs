namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSubProductServicesTableData1 : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE SubProductServices SET Description='', Price=400 WHERE Name=N'附葬 Second Burial'");
        }
        
        public override void Down()
        {
        }
    }
}
