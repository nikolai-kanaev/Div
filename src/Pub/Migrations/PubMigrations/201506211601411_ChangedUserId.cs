namespace Pub.Migrations.PubMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedUserId : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PublicProfiles", "UserId", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PublicProfiles", "UserId", c => c.Int(nullable: false));
        }
    }
}
