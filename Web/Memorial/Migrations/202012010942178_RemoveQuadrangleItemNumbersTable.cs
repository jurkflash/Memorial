﻿namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveQuadrangleItemNumbersTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.QuadrangleItemNumbers", "QuadrangleItemId", "dbo.QuadrangleItems");
            DropIndex("dbo.QuadrangleItemNumbers", new[] { "QuadrangleItemId" });
            DropTable("dbo.QuadrangleItemNumbers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.QuadrangleItemNumbers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuadrangleItemId = c.Int(nullable: false),
                        Year = c.Byte(nullable: false),
                        AF = c.Byte(nullable: false),
                        PO = c.Byte(nullable: false),
                        IV = c.Byte(nullable: false),
                        RE = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.QuadrangleItemNumbers", "QuadrangleItemId");
            AddForeignKey("dbo.QuadrangleItemNumbers", "QuadrangleItemId", "dbo.QuadrangleItems", "Id");
        }
    }
}
