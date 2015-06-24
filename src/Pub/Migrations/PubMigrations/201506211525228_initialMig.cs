namespace Pub.Migrations.PubMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialMig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PublicProfiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        DisplayName = c.String(),
                        Country = c.String(),
                        City = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PublicProfiles");
        }
    }
}
