namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDataForMiscellaneousItemTable : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT MiscellaneousItems ON; " +
               "INSERT INTO MiscellaneousItems(Id,Name,Price,Code,MiscellaneousId,SystemCode,isOrder) VALUES (4,N'收據 Receipt',0,'CLSBH',4,'Reciprocate','False'); " +
               "SET IDENTITY_INSERT MiscellaneousItems OFF;");
        }
        
        public override void Down()
        {
        }
    }
}
