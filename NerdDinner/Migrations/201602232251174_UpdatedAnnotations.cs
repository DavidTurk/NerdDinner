namespace NerdDinner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedAnnotations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Dinners", "Title", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Dinners", "Description", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.Dinners", "HostedBy", c => c.String(maxLength: 20));
            AlterColumn("dbo.Dinners", "ContactPhone", c => c.String(nullable: false));
            AlterColumn("dbo.Dinners", "Address", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Dinners", "Address", c => c.String());
            AlterColumn("dbo.Dinners", "ContactPhone", c => c.String());
            AlterColumn("dbo.Dinners", "HostedBy", c => c.String());
            AlterColumn("dbo.Dinners", "Description", c => c.String());
            AlterColumn("dbo.Dinners", "Title", c => c.String());
        }
    }
}
