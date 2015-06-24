namespace Pub.Migrations.PubMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testMig3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PublicProfiles", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PublicProfiles", "UserId", c => c.Guid(nullable: false));
        }
    }
}
