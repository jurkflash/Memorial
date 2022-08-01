namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDataToPlotItemsTable : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (1,N'墓地-單穴',0,'CYSP1',1); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (2,N'墓地-單穴',0,'CYSP1',2); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (3,N'墓地-單穴',0,'CYSP1',3); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (4,N'墓地-單穴',0,'CYSP1',4); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (5,N'墓地-單穴',0,'CYSP1',5); " +
                "SET IDENTITY_INSERT PlotItems OFF;");


            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (6,N'墓地-雙穴',0,'CYDP2',1); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (7,N'墓地-雙穴',0,'CYDP2',2); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (8,N'墓地-雙穴',0,'CYDP2',3); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (9,N'墓地-雙穴',0,'CYDP2',4); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (10,N'墓地-雙穴',0,'CYDP2',5); " +
                "SET IDENTITY_INSERT PlotItems OFF;");


            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (11,N'墓地-新雙穴',0,'CYDP3',1); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (12,N'墓地-新雙穴',0,'CYDP3',2); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (13,N'墓地-新雙穴',0,'CYDP3',3); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (14,N'墓地-新雙穴',0,'CYDP3',4); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (15,N'墓地-新雙穴',0,'CYDP3',5); " +
                "SET IDENTITY_INSERT PlotItems OFF;");


            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (16,N'墓地-風水地',0,'CYFSD',1); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (17,N'墓地-風水地',0,'CYFSD',2); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (18,N'墓地-風水地',0,'CYFSD',3); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (19,N'墓地-風水地',0,'CYFSD',4); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (20,N'墓地-風水地',0,'CYFSD',5); " +
                "SET IDENTITY_INSERT PlotItems OFF;");


            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (21,N'風水地-轉讓費',0,'CYFSF',1); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (22,N'風水地-轉讓費',0,'CYFSF',2); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (23,N'風水地-轉讓費',0,'CYFSF',3); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (24,N'風水地-轉讓費',0,'CYFSF',4); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (25,N'風水地-轉讓費',0,'CYFSF',5); " +
                "SET IDENTITY_INSERT PlotItems OFF;");


            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (26,N'風水地-回歸',0,'CYFSG',1); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (27,N'風水地-回歸',0,'CYFSG',2); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (28,N'風水地-回歸',0,'CYFSG',3); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (29,N'風水地-回歸',0,'CYFSG',4); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (30,N'風水地-回歸',0,'CYFSG',5); " +
                "SET IDENTITY_INSERT PlotItems OFF;");





            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (31,N'附葬',0,'FBFZF',1); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (32,N'附葬',0,'FBFZF',2); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (33,N'附葬',0,'FBFZF',3); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (34,N'附葬',0,'FBFZF',4); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (35,N'附葬',0,'FBFZF',5); " +
                "SET IDENTITY_INSERT PlotItems OFF;");


            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (36,N'拾金',0,'SJCBF',1); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (37,N'拾金',0,'SJCBF',2); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (38,N'拾金',0,'SJCBF',3); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (39,N'拾金',0,'SJCBF',4); " +
                "SET IDENTITY_INSERT PlotItems OFF;");
            Sql("SET IDENTITY_INSERT PlotItems ON; " +
                "INSERT INTO PlotItems(Id,Name,Price,Code,PlotAreaId) VALUES (40,N'拾金',0,'SJCBF',5); " +
                "SET IDENTITY_INSERT PlotItems OFF;");

        }

        public override void Down()
        {
            Sql("DELETE FROM PlotItems; DBCC CHECKIDENT('PlotItems', RESEED, 0); ");
        }
    }
}
