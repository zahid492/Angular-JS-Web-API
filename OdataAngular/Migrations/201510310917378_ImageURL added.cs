namespace OdataAngular.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageURLadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Basic_Information",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Phone = c.String(nullable: false, maxLength: 50),
                        ImageURL = c.String(),
                        Class_id = c.Int(nullable: false),
                        Department_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Class", t => t.Class_id)
                .ForeignKey("dbo.Department", t => t.Department_id)
                .Index(t => t.Class_id)
                .Index(t => t.Department_id);
            
            CreateTable(
                "dbo.Class",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Class1 = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Department_Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.sysdiagrams",
                c => new
                    {
                        diagram_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 128),
                        principal_id = c.Int(nullable: false),
                        version = c.Int(),
                        definition = c.Binary(),
                    })
                .PrimaryKey(t => t.diagram_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Basic_Information", "Department_id", "dbo.Department");
            DropForeignKey("dbo.Basic_Information", "Class_id", "dbo.Class");
            DropIndex("dbo.Basic_Information", new[] { "Department_id" });
            DropIndex("dbo.Basic_Information", new[] { "Class_id" });
            DropTable("dbo.sysdiagrams");
            DropTable("dbo.Department");
            DropTable("dbo.Class");
            DropTable("dbo.Basic_Information");
        }
    }
}
