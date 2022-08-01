namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDataToProductsTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Products(Name, Area, Controller) VALUES(N'墓地 Cemetery','Cemetery','Cemeteries')");
            Sql("INSERT INTO Products(Name, Area, Controller) VALUES(N'祖先牌位 AncestralTablet','AncestralTablet','AncestralTablets')");
            Sql("INSERT INTO Products(Name, Area, Controller) VALUES(N'火葬 Cremation','Cremation','Cremations')");
            Sql("INSERT INTO Products(Name, Area, Controller) VALUES(N'骨灰甕 Urn','Urn','Urns')");
            Sql("INSERT INTO Products(Name, Area, Controller) VALUES(N'骨灰殿 Columbarium','Columbarium','Columbariums')");
            Sql("INSERT INTO Products(Name, Area, Controller) VALUES(N'空間 Space','Space','Spaces')");
            Sql("INSERT INTO Products(Name, Area, Controller) VALUES(N'雜費 Miscellaneous','Miscellaneous','Miscellaneous')");
        }
        
        public override void Down()
        {
        }
    }
}
