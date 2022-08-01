namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDataToRelationshipTypesTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO RelationshipTypes(Id,Name) VALUES (1,N'夫妻 Husband and Wife')");
            Sql("INSERT INTO RelationshipTypes(Id,Name) VALUES (2,N'父母 Parents')");
            Sql("INSERT INTO RelationshipTypes(Id,Name) VALUES (3,N'子女 Children')");
            Sql("INSERT INTO RelationshipTypes(Id,Name) VALUES (4,N'父子 Father and Son')");
            Sql("INSERT INTO RelationshipTypes(Id,Name) VALUES (5,N'父女 Father and Daughter')");
            Sql("INSERT INTO RelationshipTypes(Id,Name) VALUES (6,N'母子 Mother and Son')");
            Sql("INSERT INTO RelationshipTypes(Id,Name) VALUES (7,N'母女 Mother and Daughter')");
            Sql("INSERT INTO RelationshipTypes(Id,Name) VALUES (8,N'兄弟姐妹 Sibling')");
            Sql("INSERT INTO RelationshipTypes(Id,Name) VALUES (9,N'亲戚 Relatives')");
            Sql("INSERT INTO RelationshipTypes(Id,Name) VALUES (10,N'朋友 Friends')");
            Sql("INSERT INTO RelationshipTypes(Id,Name) VALUES (11,N'其他 Other')");
        }
        
        public override void Down()
        {
        }
    }
}
