namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAccessControlTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccessControls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AncestralTablet = c.Byte(nullable: false),
                        Cemetery = c.Byte(nullable: false),
                        Columbarium = c.Byte(nullable: false),
                        Cremation = c.Byte(nullable: false),
                        Miscellaneous = c.Byte(nullable: false),
                        Urn = c.Byte(nullable: false),
                        Space = c.Byte(nullable: false),
                        ActiveStatus = c.Boolean(nullable: false),
                        CreatedById = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedById = c.Int(nullable: false),
                        ModifiedDate = c.DateTime(),
                        DeletedById = c.Int(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AccessControls");
        }
    }
}
