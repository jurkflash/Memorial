namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppendDataToSubProductServicesTable1 : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId,OtherId)VALUES(N'單 Order','',0,'CYSP1','Orders',1,1,1)");
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId,OtherId)VALUES(N'單 Order','',0,'CYDP2','Orders',1,1,2)");
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId,OtherId)VALUES(N'單 Order','',0,'CYDP3','Orders',1,1,3)");
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId,OtherId)VALUES(N'單 Order','',0,'CYFSD','Orders',1,1,4)");

            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId,OtherId)VALUES(N'轉讓 Transfer','',0,'CYFSF','FengShuiTransfers',1,1,4)");

            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId,OtherId)VALUES(N'附葬 Second Burial','400',0,'SBFZF','SecondBurials',1,1,2)");
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId,OtherId)VALUES(N'附葬 Second Burial','400',0,'SBFZF','SecondBurials',1,1,3)");
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId,OtherId)VALUES(N'附葬 Second Burial','400',0,'SBFZF','SecondBurials',1,1,4)");

            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId,OtherId)VALUES(N'拾金 Clearance','',0,'SJINF','Clearances',1,1,1)");
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId,OtherId)VALUES(N'拾金 Clearance','',0,'SJINF','Clearances',1,1,2)");
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId,OtherId)VALUES(N'拾金 Clearance','',0,'SJINF','Clearances',1,1,3)");
            Sql("INSERT INTO SubProductServices(Name,Description,Price,Code,SystemCode,isOrder,ProductId,OtherId)VALUES(N'拾金 Clearance','',0,'SJINF','Clearances',1,1,4)");
        }
        
        public override void Down()
        {
        }
    }
}
