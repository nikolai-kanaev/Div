namespace Pub.Migrations.PubMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testMig1 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.PublicProfiles");
            AlterColumn("dbo.PublicProfiles", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.PublicProfiles", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.PublicProfiles");
            AlterColumn("dbo.PublicProfiles", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.PublicProfiles", "Id");
        }
    }
}
