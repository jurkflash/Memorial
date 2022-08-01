namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateSubProductServicesTableData : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId)VALUES(N'單 Order','',0,'ACSZP','Orders',1,2)");
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId)VALUES(N'管理費 Maintenance','',20,'ACGLF','Maintenances',1,2)");
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId)VALUES(N'移位 Shift','',0,'ACYWF','Shifts',1,2)");
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId)VALUES(N'免費 Free','',0,'ACFRE','Free',0,2)");

            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId)VALUES(N'單 Order','',0,'QRAF1','Orders',1,5)");
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId)VALUES(N'管理 Manage','',150,'QRGLF','Manage',1,5)");
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId)VALUES(N'照片 Photo','',150,'QRZPF','Photos',1,5)");
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId)VALUES(N'移位 Shift','',150,'QRYWF','Shifts',1,5)");
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId)VALUES(N'轉讓 Transfer','',200,'QRZRF','Transfers',1,5)");
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId)VALUES(N'免費 Free','',0,'QRFRE','Free',0,5)");

            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId)VALUES(N'單 Order','',600,'CMHHD','Orders',1,3)");
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId)VALUES(N'免費 Free','',0,'CMFRE','Free',0,3)");

            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId)VALUES(N'收據 Receipt','',0,'MSLJF','Donations',0,7)");
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId)VALUES(N'單 Order','',0,'MSLJF','Compensates',1,7)");
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId)VALUES(N'單 Order','',0,'MSLJF','RentAirCoolers',1,7)");
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId)VALUES(N'回饋 Reciprocate','',0,'MSLJF','Reciprocates',0,7)");

            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId)VALUES(N'購買 Purchase','',0,'URNLJ','Purchases',1,4)");

        }
        
        public override void Down()
        {
        }
    }
}
