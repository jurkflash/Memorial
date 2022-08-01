namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateSpaceItemsTableData : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO SpaceItems(SpaceId, SubProductServiceId, CreateDate, Price, Code, AllowDeposit, AllowDoubleBook, ToleranceHour, FormView) VALUES(1,18,GETDATE(),750,'PYBYG',0,0,2,'InfoWithDeceased')");
            Sql("INSERT INTO SpaceItems(SpaceId, SubProductServiceId, CreateDate, Price, Code, AllowDeposit, AllowDoubleBook, ToleranceHour, FormView) VALUES(2,18,GETDATE(),750,'PYBYG',0,0,2,'InfoWithDeceased')");
            Sql("INSERT INTO SpaceItems(SpaceId, SubProductServiceId, CreateDate, Price, Code, AllowDeposit, AllowDoubleBook, ToleranceHour, FormView) VALUES(3,18,GETDATE(),750,'PYBYG',0,0,2,'InfoWithDeceased')");
            Sql("INSERT INTO SpaceItems(SpaceId, SubProductServiceId, CreateDate, Price, Code, AllowDeposit, AllowDoubleBook, ToleranceHour, FormView) VALUES(4,18,GETDATE(),750,'PYBYG',0,0,2,'InfoWithDeceased')");
            Sql("INSERT INTO SpaceItems(SpaceId, SubProductServiceId, CreateDate, Price, Code, AllowDeposit, AllowDoubleBook, ToleranceHour, FormView) VALUES(5,18,GETDATE(),0,'PYBYG',0,0,2,'InfoWithDeceased')");
            Sql("INSERT INTO SpaceItems(SpaceId, SubProductServiceId, CreateDate, Price, Code, AllowDeposit, AllowDoubleBook, ToleranceHour, FormView) VALUES(6,18,GETDATE(),650,'PYBYG',0,0,2,'InfoWithDeceased')");
            Sql("INSERT INTO SpaceItems(SpaceId, SubProductServiceId, CreateDate, Price, Code, AllowDeposit, AllowDoubleBook, ToleranceHour, FormView) VALUES(7,18,GETDATE(),750,'PYBYG',0,0,2,'InfoWithDeceased')");
            Sql("INSERT INTO SpaceItems(SpaceId, SubProductServiceId, CreateDate, Price, Code, AllowDeposit, AllowDoubleBook, ToleranceHour, FormView) VALUES(8,18,GETDATE(),750,'PYBYG',0,0,2,'InfoWithDeceased')");
            Sql("INSERT INTO SpaceItems(SpaceId, SubProductServiceId, CreateDate, Price, Code, AllowDeposit, AllowDoubleBook, ToleranceHour, FormView) VALUES(9,18,GETDATE(),750,'PYBYG',0,0,2,'InfoWithDeceased')");
            Sql("INSERT INTO SpaceItems(SpaceId, SubProductServiceId, CreateDate, Price, Code, AllowDeposit, AllowDoubleBook, ToleranceHour, FormView) VALUES(10,18,GETDATE(),750,'PYBYG',0,0,2,'InfoWithDeceased')");
            Sql("INSERT INTO SpaceItems(SpaceId, SubProductServiceId, CreateDate, Price, Code, AllowDeposit, AllowDoubleBook, ToleranceHour, FormView) VALUES(11,18,GETDATE(),750,'PYBYG',0,0,2,'InfoWithDeceased')");
            Sql("INSERT INTO SpaceItems(SpaceId, SubProductServiceId, CreateDate, Price, Code, AllowDeposit, AllowDoubleBook, ToleranceHour, FormView) VALUES(12,18,GETDATE(),1500,'GDTAN',1,0,2,'InfoWithoutDeceased')");
            Sql("INSERT INTO SpaceItems(SpaceId, SubProductServiceId, CreateDate, Price, Code, AllowDeposit, AllowDoubleBook, ToleranceHour, FormView) VALUES(13,18,GETDATE(),1500,'GDTAN',1,0,2,'InfoWithoutDeceased')");
            Sql("INSERT INTO SpaceItems(SpaceId, SubProductServiceId, CreateDate, Price, Code, AllowDeposit, AllowDoubleBook, ToleranceHour, FormView) VALUES(14,18,GETDATE(),1000,'GDTAN',1,0,2,'InfoWithoutDeceased')");
            Sql("INSERT INTO SpaceItems(SpaceId, SubProductServiceId, CreateDate, Price, Code, AllowDeposit, AllowDoubleBook, ToleranceHour, FormView) VALUES(15,18,GETDATE(),3000,'SBTAN',1,0,2,'InfoWithoutDeceased')");
            Sql("INSERT INTO SpaceItems(SpaceId, SubProductServiceId, CreateDate, Price, Code, AllowDeposit, AllowDoubleBook, ToleranceHour, FormView) VALUES(16,18,GETDATE(),3000,'SHALL',1,0,2,'InfoHallForPray')");
            Sql("INSERT INTO SpaceItems(SpaceId, SubProductServiceId, CreateDate, Price, Code, AllowDeposit, AllowDoubleBook, ToleranceHour, FormView) VALUES(17,18,GETDATE(),1500,'SJSTN',0,0,2,'InfoWithoutDeceasedAndFuneralCo')");
            Sql("INSERT INTO SpaceItems(SpaceId, SubProductServiceId, CreateDate, Price, Code, AllowDeposit, AllowDoubleBook, ToleranceHour, FormView) VALUES(18,18,GETDATE(),1500,'SJDTN',0,0,2,'InfoWithoutDeceasedAndFuneralCo')");
            Sql("INSERT INTO SpaceItems(SpaceId, SubProductServiceId, CreateDate, Price, Code, AllowDeposit, AllowDoubleBook, ToleranceHour, FormView) VALUES(19,18,GETDATE(),300,'SJKDS',0,0,2,'Info')");
            Sql("INSERT INTO SpaceItems(SpaceId, SubProductServiceId, CreateDate, Price, Code, AllowDeposit, AllowDoubleBook, ToleranceHour, FormView) VALUES(20,18,GETDATE(),300,'SDKDS',0,0,2,'Info')");

            Sql("INSERT INTO SpaceItems(SpaceId, SubProductServiceId, CreateDate, Price, Code, AllowDeposit, AllowDoubleBook, ToleranceHour) VALUES(5,19,GETDATE(),0,'SDKDS',0,0,2)");

            Sql("INSERT INTO SpaceItems(SpaceId, SubProductServiceId, CreateDate, Price, Code, AllowDeposit, AllowDoubleBook, ToleranceHour) VALUES(15,20,GETDATE(),0,'SBTYZ',0,1,0)");
            Sql("INSERT INTO SpaceItems(SpaceId, SubProductServiceId, CreateDate, Price, Code, AllowDeposit, AllowDoubleBook, ToleranceHour) VALUES(15,21,GETDATE(),0,'SBTDF',0,1,0)");
            Sql("INSERT INTO SpaceItems(SpaceId, SubProductServiceId, CreateDate, Price, Code, AllowDeposit, AllowDoubleBook, ToleranceHour) VALUES(15,22,GETDATE(),0,'SBTBT',0,1,0)");

            Sql("INSERT INTO SpaceItems(SpaceId, SubProductServiceId, CreateDate, Price, Code, AllowDeposit, AllowDoubleBook, ToleranceHour) VALUES(16,20,GETDATE(),0,'HLZYZ',0,1,0)");
            Sql("INSERT INTO SpaceItems(SpaceId, SubProductServiceId, CreateDate, Price, Code, AllowDeposit, AllowDoubleBook, ToleranceHour) VALUES(16,21,GETDATE(),0,'HLELT',0,1,0)");
            Sql("INSERT INTO SpaceItems(SpaceId, SubProductServiceId, CreateDate, Price, Code, AllowDeposit, AllowDoubleBook, ToleranceHour) VALUES(16,22,GETDATE(),0,'HLBTF',0,1,0)");
        }
        
        public override void Down()
        {
        }
    }
}
