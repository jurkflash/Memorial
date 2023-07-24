namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDataToRelationshipTypesTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO RelationshipTypes(Id,Name,ActiveStatus,CreatedDate) VALUES (1,N'夫妻 Husband and Wife',1,GETDATE())");
            Sql("INSERT INTO RelationshipTypes(Id,Name,ActiveStatus,CreatedDate) VALUES (2,N'父母 Parents',1,GETDATE())");
            Sql("INSERT INTO RelationshipTypes(Id,Name,ActiveStatus,CreatedDate) VALUES (3,N'子女 Children',1,GETDATE())");
            Sql("INSERT INTO RelationshipTypes(Id,Name,ActiveStatus,CreatedDate) VALUES (4,N'父子 Father and Son',1,GETDATE())");
            Sql("INSERT INTO RelationshipTypes(Id,Name,ActiveStatus,CreatedDate) VALUES (5,N'父女 Father and Daughter',1,GETDATE())");
            Sql("INSERT INTO RelationshipTypes(Id,Name,ActiveStatus,CreatedDate) VALUES (6,N'母子 Mother and Son',1,GETDATE())");
            Sql("INSERT INTO RelationshipTypes(Id,Name,ActiveStatus,CreatedDate) VALUES (7,N'母女 Mother and Daughter',1,GETDATE())");
            Sql("INSERT INTO RelationshipTypes(Id,Name,ActiveStatus,CreatedDate) VALUES (8,N'兄弟姐妹 Sibling',1,GETDATE())");
            Sql("INSERT INTO RelationshipTypes(Id,Name,ActiveStatus,CreatedDate) VALUES (9,N'亲戚 Relatives',1,GETDATE())");
            Sql("INSERT INTO RelationshipTypes(Id,Name,ActiveStatus,CreatedDate) VALUES (10,N'朋友 Friends',1,GETDATE())");
            Sql("INSERT INTO RelationshipTypes(Id,Name,ActiveStatus,CreatedDate) VALUES (11,N'其他 Other',1,GETDATE())");
        }
        
        public override void Down()
        {
        }
    }
}
