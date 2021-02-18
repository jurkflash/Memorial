namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDataToPlotItemsTable2 : DbMigration
    {
        public override void Up()
        {
            Sql("DELETE FROM PlotItems; DBCC CHECKIDENT('PlotItems', RESEED, 0); ");

            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (1,N'墓地-單穴',0,'PSCS1',1); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (2,N'墓地-單穴',0,'PDNS1',2); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (3,N'墓地-單穴',0,'PDOS1',3); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (4,N'墓地-單穴',0,'PDFS1',4); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (5,N'墓地-單穴',0,'PSFS1',5); " +
                "SET IDENTITY_INSERT PlotItems OFF;");


            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (6,N'墓地-雙穴',0,'PSCD2',1); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (7,N'墓地-雙穴',0,'PDND2',2); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (8,N'墓地-雙穴',0,'PDOD2',3); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (9,N'墓地-雙穴',0,'PDFD2',4); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (10,N'墓地-雙穴',0,'PSFD2',5); " +
                "SET IDENTITY_INSERT PlotItems OFF;");


            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (11,N'墓地-新雙穴',0,'PSCD3',1); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (12,N'墓地-新雙穴',0,'PDND3',2); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (13,N'墓地-新雙穴',0,'PDOD3',3); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (14,N'墓地-新雙穴',0,'PDFD3',4); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (15,N'墓地-新雙穴',0,'PSFD3',5); " +
                "SET IDENTITY_INSERT PlotItems OFF;");


            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (16,N'墓地-風水地',0,'PSCFD',1); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (17,N'墓地-風水地',0,'PDNFD',2); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (18,N'墓地-風水地',0,'PDOFD',3); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (19,N'墓地-風水地',0,'PDFFD',4); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (20,N'墓地-風水地',0,'PSFFD',5); " +
                "SET IDENTITY_INSERT PlotItems OFF;");


            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (21,N'風水地-轉讓費',0,'PSCFZ',1); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (22,N'風水地-轉讓費',0,'PDNFZ',2); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (23,N'風水地-轉讓費',0,'PDOFZ',3); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (24,N'風水地-轉讓費',0,'PDFFZ',4); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (25,N'風水地-轉讓費',0,'PSFFZ',5); " +
                "SET IDENTITY_INSERT PlotItems OFF;");


            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (26,N'風水地-回歸',0,'PSCFH',1); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (27,N'風水地-回歸',0,'PDNFH',2); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (28,N'風水地-回歸',0,'PDOFH',3); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (29,N'風水地-回歸',0,'PDFFH',4); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (30,N'風水地-回歸',0,'PSFFH',5); " +
                "SET IDENTITY_INSERT PlotItems OFF;");





            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (31,N'附葬',0,'PSCSB',1); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (32,N'附葬',0,'PDNSB',2); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (33,N'附葬',0,'PDOSB',3); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (34,N'附葬',0,'PDFSB',4); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (35,N'附葬',0,'PSFSB',5); " +
                "SET IDENTITY_INSERT PlotItems OFF;");


            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (36,N'拾金',0,'PSCSJ',1); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (37,N'拾金',0,'PDNSJ',2); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (38,N'拾金',0,'PDOSJ',3); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (39,N'拾金',0,'PDFSJ',4); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (40,N'拾金',0,'PSFSJ',5); " +
                "SET IDENTITY_INSERT PlotItems OFF;");

        }

        public override void Down()
        {
            Sql("DELETE FROM PlotItems; DBCC CHECKIDENT('PlotItems', RESEED, 0); ");
        }
    }
}
