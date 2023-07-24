namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDataToSitesTable : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT Sites ON; " +
                "INSERT INTO Sites(Id,Name,Code,ActiveStatus,CreatedDate,Header) VALUES (1,'Sg Buluh','SBH',1,GETDATE(),"+
                "'<table style=\"width:100%\"> <tr> <td style=\"text-align:right\"> <img src=\"/Images/cemeterylogo.jpg\" style=\"height:200px\"/> </td> <td style=\"float:left\"> <div style=\"text-align:center\"> <span style=\"font-size:30pt; font-family: KaiTi; font-weight:bold\"> 甲洞華人義山機構<br /> </span> <span style=\"font-size: 15pt; font-family: ''Times New Roman''; font-weight: bold\"> CHINESE CEMETERY ASSOCIATION OF<br /> </span> <span style=\"font-size: 15pt; font-family: ''Times New Roman''; font-weight: bold\"> KEPONG, JINJANG & BATU VILLAGE.<br /> </span> <span style=\"font-size: 10pt; font-family: ''Times New Roman''\"> LOT 182, JALAN JAMBU ARANG DUA,<br /> </span> <span style=\"font-size: 10pt; font-family: ''Times New Roman''\"> JINJANG SELATAN, 5200 KUALA LUMPUR.<br /> </span> <span style=\"font-size: 10pt; font-family: ''Times New Roman''\">Tel: 03-6251 9771&nbsp;&nbsp;&nbsp;Fax: 03-6257 5293&nbsp;&nbsp;&nbsp;E-MAIL: cca8402@gmail.com</span> </div> </td> </tr> </table>');"+
                "SET IDENTITY_INSERT Sites OFF;");
            Sql("SET IDENTITY_INSERT Sites ON; " +
                "INSERT INTO Sites(Id,Name,Code,ActiveStatus,CreatedDate,Header) VALUES (2,'Jit San Tang','JST',1,GETDATE()," +
                "'<table style=\"width:100%\"> <tr> <td style=\"text-align:right\"> <img src=\"/Images/cemeterylogo.jpg\" style=\"height:200px\"/> </td> <td style=\"float:left\"> <div style=\"text-align:center\"> <span style=\"font-size:30pt; font-family: KaiTi; font-weight:bold\"> 甲洞華人義山機構<br /> </span> <span style=\"font-size: 15pt; font-family: ''Times New Roman''; font-weight: bold\"> CHINESE CEMETERY ASSOCIATION OF<br /> </span> <span style=\"font-size: 15pt; font-family: ''Times New Roman''; font-weight: bold\"> KEPONG, JINJANG & BATU VILLAGE.<br /> </span> <span style=\"font-size: 10pt; font-family: ''Times New Roman''\"> LOT 182, JALAN JAMBU ARANG DUA,<br /> </span> <span style=\"font-size: 10pt; font-family: ''Times New Roman''\"> JINJANG SELATAN, 5200 KUALA LUMPUR.<br /> </span> <span style=\"font-size: 10pt; font-family: ''Times New Roman''\">Tel: 03-6251 9771&nbsp;&nbsp;&nbsp;Fax: 03-6257 5293&nbsp;&nbsp;&nbsp;E-MAIL: cca8402@gmail.com</span> </div> </td> </tr> </table>');" +
                "SET IDENTITY_INSERT Sites OFF;");
            Sql("SET IDENTITY_INSERT Sites ON; " +
                "INSERT INTO Sites(Id,Name,Code,ActiveStatus,CreatedDate,Header) VALUES (3,'Desa','DSJ',1,GETDATE()," +
                "'<table style=\"width:100%\"> <tr> <td style=\"text-align:right\"> <img src=\"/Images/cemeterylogo.jpg\" style=\"height:200px\"/> </td> <td style=\"float:left\"> <div style=\"text-align:center\"> <span style=\"font-size:30pt; font-family: KaiTi; font-weight:bold\"> 甲洞華人義山機構<br /> </span> <span style=\"font-size: 15pt; font-family: ''Times New Roman''; font-weight: bold\"> CHINESE CEMETERY ASSOCIATION OF<br /> </span> <span style=\"font-size: 15pt; font-family: ''Times New Roman''; font-weight: bold\"> KEPONG, JINJANG & BATU VILLAGE.<br /> </span> <span style=\"font-size: 10pt; font-family: ''Times New Roman''\"> LOT 182, JALAN JAMBU ARANG DUA,<br /> </span> <span style=\"font-size: 10pt; font-family: ''Times New Roman''\"> JINJANG SELATAN, 5200 KUALA LUMPUR.<br /> </span> <span style=\"font-size: 10pt; font-family: ''Times New Roman''\">Tel: 03-6251 9771&nbsp;&nbsp;&nbsp;Fax: 03-6257 5293&nbsp;&nbsp;&nbsp;E-MAIL: cca8402@gmail.com</span> </div> </td> </tr> </table>');" +
                "SET IDENTITY_INSERT Sites OFF;");
        }
        
        public override void Down()
        {
        }
    }
}
