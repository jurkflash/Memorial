namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReceiptsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Receipts",
                c => new
                    {
                        RE = c.String(nullable: false, maxLength: 50),
                        AF = c.String(maxLength: 50),
                        IV = c.String(maxLength: 50),
                        Amount = c.Single(nullable: false),
                        Remark = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.RE)
                .ForeignKey("dbo.Invoices", t => t.IV)
                .Index(t => t.IV);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Receipts", "IV", "dbo.Invoices");
            DropIndex("dbo.Receipts", new[] { "IV" });
            DropTable("dbo.Receipts");
        }
    }
}
