namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTinyintToIntForCremationIdDataType : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CremationItems", "CremationId", "dbo.Cremations");
            DropIndex("dbo.CremationItems", new[] { "CremationId" });
            DropPrimaryKey("dbo.Cremations");
            AlterColumn("dbo.CremationItems", "CremationId", c => c.Int(nullable: false));
            AlterColumn("dbo.Cremations", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Cremations", "Id");
            CreateIndex("dbo.CremationItems", "CremationId");
            AddForeignKey("dbo.CremationItems", "CremationId", "dbo.Cremations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CremationItems", "CremationId", "dbo.Cremations");
            DropIndex("dbo.CremationItems", new[] { "CremationId" });
            DropPrimaryKey("dbo.Cremations");
            AlterColumn("dbo.Cremations", "Id", c => c.Byte(nullable: false));
            AlterColumn("dbo.CremationItems", "CremationId", c => c.Byte(nullable: false));
            AddPrimaryKey("dbo.Cremations", "Id");
            CreateIndex("dbo.CremationItems", "CremationId");
            AddForeignKey("dbo.CremationItems", "CremationId", "dbo.Cremations", "Id");
        }
    }
}
