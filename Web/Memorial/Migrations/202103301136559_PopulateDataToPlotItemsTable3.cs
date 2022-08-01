namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDataToPlotItemsTable3 : DbMigration
    {
        public override void Up()
        {
            Sql("DELETE FROM PlotItems; DBCC CHECKIDENT('PlotItems', RESEED, 0); ");

            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,SystemCode,PlotAreaId,isOrder) VALUES (1,N'墓地-單穴',0,'PSCS1','SingleOrder',1,'True'); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,SystemCode,PlotAreaId,isOrder) VALUES (2,N'墓地-單穴',0,'PDNS1','SingleOrder',2,'True'); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,SystemCode,PlotAreaId,isOrder) VALUES (3,N'墓地-單穴',0,'PDOS1','SingleOrder',3,'True'); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,SystemCode,PlotAreaId,isOrder) VALUES (4,N'墓地-單穴',0,'PDFS1','SingleOrder',4,'True'); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,SystemCode,PlotAreaId,isOrder) VALUES (5,N'墓地-單穴',0,'PSFS1','SingleOrder',5,'True'); " +
                "SET IDENTITY_INSERT PlotItems OFF;");


            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,SystemCode,PlotAreaId,isOrder) VALUES (6,N'墓地-雙穴',0,'PSCD2','DoubleOrder',1,'True'); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,SystemCode,PlotAreaId,isOrder) VALUES (7,N'墓地-雙穴',0,'PDND2','DoubleOrder',2,'True'); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,SystemCode,PlotAreaId,isOrder) VALUES (8,N'墓地-雙穴',0,'PDOD2','DoubleOrder',3,'True'); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,SystemCode,PlotAreaId,isOrder) VALUES (9,N'墓地-雙穴',0,'PDFD2','DoubleOrder',4,'True'); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,SystemCode,PlotAreaId,isOrder) VALUES (10,N'墓地-雙穴',0,'PSFD2','DoubleOrder',5,'True'); " +
                "SET IDENTITY_INSERT PlotItems OFF;");


            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,SystemCode,PlotAreaId,isOrder) VALUES (11,N'墓地-新雙穴',0,'PSCD3','NewDoubleOrder',1,'True'); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,SystemCode,PlotAreaId,isOrder) VALUES (12,N'墓地-新雙穴',0,'PDND3','NewDoubleOrder',2,'True'); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,SystemCode,PlotAreaId,isOrder) VALUES (13,N'墓地-新雙穴',0,'PDOD3','NewDoubleOrder',3,'True'); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,SystemCode,PlotAreaId,isOrder) VALUES (14,N'墓地-新雙穴',0,'PDFD3','NewDoubleOrder',4,'True'); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,SystemCode,PlotAreaId,isOrder) VALUES (15,N'墓地-新雙穴',0,'PSFD3','NewDoubleOrder',5,'True'); " +
                "SET IDENTITY_INSERT PlotItems OFF;");


            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,SystemCode,PlotAreaId,isOrder) VALUES (16,N'墓地-風水地',0,'PSCFD','FengShuiOrder',1,'True'); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,SystemCode,PlotAreaId,isOrder) VALUES (17,N'墓地-風水地',0,'PDNFD','FengShuiOrder',2,'True'); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,SystemCode,PlotAreaId,isOrder) VALUES (18,N'墓地-風水地',0,'PDOFD','FengShuiOrder',3,'True'); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,SystemCode,PlotAreaId,isOrder) VALUES (19,N'墓地-風水地',0,'PDFFD','FengShuiOrder',4,'True'); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,SystemCode,PlotAreaId,isOrder) VALUES (20,N'墓地-風水地',0,'PSFFD','FengShuiOrder',5,'True'); " +
                "SET IDENTITY_INSERT PlotItems OFF;");


            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,SystemCode,PlotAreaId,isOrder) VALUES (21,N'風水地-轉讓費',0,'PSCFZ','FengShuiTransfer',1,'True'); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,SystemCode,PlotAreaId,isOrder) VALUES (22,N'風水地-轉讓費',0,'PDNFZ','FengShuiTransfer',2,'True'); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,SystemCode,PlotAreaId,isOrder) VALUES (23,N'風水地-轉讓費',0,'PDOFZ','FengShuiTransfer',3,'True'); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,SystemCode,PlotAreaId,isOrder) VALUES (24,N'風水地-轉讓費',0,'PDFFZ','FengShuiTransfer',4,'True'); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,SystemCode,PlotAreaId,isOrder) VALUES (25,N'風水地-轉讓費',0,'PSFFZ','FengShuiTransfer',5,'True'); " +
                "SET IDENTITY_INSERT PlotItems OFF;");


            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,SystemCode,PlotAreaId,isOrder) VALUES (26,N'風水師-回歸',0,'PSCFH','FengShuiMasterContribution',1,'False'); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,SystemCode,PlotAreaId,isOrder) VALUES (27,N'風水師-回歸',0,'PDNFH','FengShuiMasterContribution',2,'False'); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,SystemCode,PlotAreaId,isOrder) VALUES (28,N'風水師-回歸',0,'PDOFH','FengShuiMasterContribution',3,'False'); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,SystemCode,PlotAreaId,isOrder) VALUES (29,N'風水師-回歸',0,'PDFFH','FengShuiMasterContribution',4,'False'); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,SystemCode,PlotAreaId,isOrder) VALUES (30,N'風水師-回歸',0,'PSFFH','FengShuiMasterContribution',5,'False'); " +
                "SET IDENTITY_INSERT PlotItems OFF;");


            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,SystemCode,PlotAreaId,isOrder) VALUES (31,N'附葬',0,'PSCSB','SecondBurial',1,'True'); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,SystemCode,PlotAreaId,isOrder) VALUES (32,N'附葬',0,'PDNSB','SecondBurial',2,'True'); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,SystemCode,PlotAreaId,isOrder) VALUES (33,N'附葬',0,'PDOSB','SecondBurial',3,'True'); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,SystemCode,PlotAreaId,isOrder) VALUES (34,N'附葬',0,'PDFSB','SecondBurial',4,'True'); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,SystemCode,PlotAreaId,isOrder) VALUES (35,N'附葬',0,'PSFSB','SecondBurial',5,'True'); " +
                "SET IDENTITY_INSERT PlotItems OFF;");


            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,SystemCode,PlotAreaId,isOrder) VALUES (36,N'拾金',0,'PSCSJ','Clearance',1,'True'); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,SystemCode,PlotAreaId,isOrder) VALUES (37,N'拾金',0,'PDNSJ','Clearance',2,'True'); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,SystemCode,PlotAreaId,isOrder) VALUES (38,N'拾金',0,'PDOSJ','Clearance',3,'True'); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,SystemCode,PlotAreaId,isOrder) VALUES (39,N'拾金',0,'PDFSJ','Clearance',4,'True'); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,SystemCode,PlotAreaId,isOrder) VALUES (40,N'拾金',0,'PSFSJ','Clearance',5,'True'); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM PlotItems; DBCC CHECKIDENT('PlotItems', RESEED, 0); ");
        }
    }
}
