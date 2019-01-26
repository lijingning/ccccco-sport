namespace co_sport.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyusers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "Password", c => c.String(nullable: false));
            AddColumn("dbo.User", "WeChatID", c => c.String());
            AlterColumn("dbo.User", "Name", c => c.String(nullable: false));
            DropColumn("dbo.User", "Gender");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "Gender", c => c.Boolean(nullable: false));
            AlterColumn("dbo.User", "Name", c => c.String());
            DropColumn("dbo.User", "WeChatID");
            DropColumn("dbo.User", "Password");
        }
    }
}
